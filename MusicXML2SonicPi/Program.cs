using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

using MusicXml.SonicPi.MusicScore;
using MusicXml.SonicPi.MusicScore.ScorePart;
using MusicXml.SonicPi.MusicScore.ScorePart.MeasureNote;

namespace MusicXML2SonicPi
{
/// <summary>
/// MusicXML file convert to Sonic-Pi file
/// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string, string[]> Arguments = ParseCommands(args);
            string inputFile = string.Empty;
            string outputFile = string.Empty;

            if (Arguments.ContainsKey("input"))
            {
                inputFile = Arguments["input"][0];
            }

            if (Arguments.ContainsKey("output"))
            {
                outputFile = Arguments["output"][0];
            }

            if(string.IsNullOrEmpty(inputFile))
            {
                Console.WriteLine("Invalid arguments.");
                Console.WriteLine();
                Console.WriteLine("Usage:");
                Console.WriteLine("\tMusicXML2SonicPi.exe -input <filename> [-output <filename>]");

                return;
            }

            if (string.IsNullOrEmpty(outputFile))
            {
                outputFile = string.Concat(inputFile, ".rb");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(scorepartwise));
            TextReader reader = new StreamReader(inputFile);
            scorepartwise score = (scorepartwise)serializer.Deserialize(reader);
            TextWriter writer = new StreamWriter(outputFile);

            reader.Close();
            ProcessScore(score, ref writer);
            writer.Close();
        }

        private static void ProcessScore(scorepartwise score, ref TextWriter writer)
        {
            var tempo = (score.part.Select(p => p.measure).Select(p => p.FirstOrDefault().Items.Where(i => i.GetType() == typeof(direction))).First().DefaultIfEmpty(new direction().sound = new sound() { tempo = 60 }).First() as direction).sound.tempo;

            Score score_ = new Score("SonicPi Music Score", (int)tempo, "piano");

            score.part.ToList().ForEach(delegate (scorepartwisePart part)
            {
                Part part_ = new Part(part.id, "Score part", true);
                score_.Parts.Add(part_);

                part.measure.ToList().ForEach(delegate (scorepartwisePartMeasure measure)
                {
                    Measure measure_ = new Measure(measure.number, "Score measure");
                    part_.BlockPart.Measures.Add(measure_);

                    measure.Items.ToList().ForEach(delegate (object measureItem)
                    {
                        switch (measureItem.GetType().Name)
                        {
                            case "note":
                                int voice = 1;

                                int.TryParse((measureItem as note).voice, out voice);

                                Note note_ = new Note(0, voice, false);

                                measure_.Notes.Add(note_);

                                if ((measureItem as note).accidental != null)
                                {
                                    note_.Accidental = (NoteAccidental)Enum.Parse(typeof(NoteAccidental), ((measureItem as note).accidental).Value.ToString(), true);
                                }

                                ((measureItem as note).ItemsElementName as ItemsChoiceType1[]).ToList().ForEach(delegate (ItemsChoiceType1 item)
                                {
                                    int i = ((measureItem as note).ItemsElementName as ItemsChoiceType1[]).ToList().IndexOf(item);

                                    switch (item)
                                    {
                                        case ItemsChoiceType1.chord:
                                            note_.IsChord = true;
                                            break;
                                        case ItemsChoiceType1.cue:
                                            break;
                                        case ItemsChoiceType1.duration:
                                            note_.Length = int.Parse((measureItem as note).Items[i].ToString());
                                            break;
                                        case ItemsChoiceType1.grace:
                                            break;
                                        case ItemsChoiceType1.pitch:
                                            note_.NotePitch = new Pitch(
                                                (NoteStep)Enum.Parse(typeof(NoteStep), ((measureItem as note).Items[i] as pitch).step.ToString(), true),
                                                ((measureItem as note).Items[i] as pitch).octave);
                                            break;
                                        case ItemsChoiceType1.rest:
                                            note_.NotePitch = new Pitch(NoteStep.Rest);
                                            break;
                                        case ItemsChoiceType1.tie:
                                            if (((measureItem as note).Items[i] as tie).type == startstop.stop)
                                            {
                                                note_.Length = note_.Length * -1;
                                            }
                                            break;
                                        case ItemsChoiceType1.unpitched:
                                            break;
                                    }
                                });
                                break;
                            case "direction":
                                break;
                            case "attributes":
                                break;
                        }

                    });
                });

            });

            string code = score_.ToString();

            writer.Write(code);
        }

        private static IDictionary<string, string[]> ParseCommands(string[] args)
        {
            IDictionary<string, string[]> Arguments = new Dictionary<string, string[]>();
            string currentName = "";
            List<string> values = new List<string>();

            args.ToList().ForEach(delegate (string arg)
            {
                if (arg.StartsWith("-"))
                {
                    if (currentName != "")
                    {
                        Arguments[currentName] = values.ToArray();
                    }

                    values.Clear();
                    currentName = arg.Substring(1);
                }
                else if (currentName == "")
                {
                    Arguments[arg] = new string[0];
                }
                else
                {
                    values.Add(arg);
                }
            });

            if (currentName != "")
            {
                Arguments[currentName] = values.ToArray();
            }

            return Arguments;
        }
    }
}