using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MusicXml.SonicPi.MusicScore.ScorePart;

namespace MusicXml.SonicPi.MusicScore
{
    public class Score : IScore
    {
        public string Caption { get; set; }
        public int Tempo { get; set; }
        public string Instrument { get; set; }
        public List<Part> Parts { get; set; }

        public Score(string caption, int tempo, string instrument)
        {
            Caption = caption;
            Tempo = tempo;
            Instrument = instrument;
            Parts = new List<Part>();
        }

        override public string ToString()
        {
            if (this != null)
            {
                StringWriter writer = new StringWriter();

                writer.WriteLine("##############################################");
                writer.WriteLine("# {0}", Caption);
                writer.WriteLine("##############################################");
                writer.WriteLine();
                writer.WriteLine("use_bpm {0}", Tempo);
                writer.WriteLine();

                Parts.ForEach(delegate (Part part)
                {
                    writer.WriteLine("##############################################");
                    writer.WriteLine("# {0} : {1}", part.Id, part.Caption);
                    writer.WriteLine("##############################################");
                    writer.WriteLine("# Tempo: {0}", Tempo);
                    writer.WriteLine("# Instrument: {0}", Instrument);
                    writer.WriteLine("##############################################");
                    writer.WriteLine();
                    writer.WriteLine(string.Format("in_thread(name: :part{0}) do", part.Id));

                    if (part.BlockPart != null)
                    {
                        int maxVoice = part.BlockPart.Measures.Max(p => p.Notes.Max(n => n.VoiceId));
                        StringWriter[] voiceBag = new StringWriter[maxVoice];

                        part.BlockPart.Measures.ForEach(delegate (Measure measure)
                        {
                            measure.Notes.ForEach(delegate (ScorePart.MeasureNote.Note note)
                            {
                                if (voiceBag[note.VoiceId - 1] == null)
                                {
                                    voiceBag[note.VoiceId - 1] = new StringWriter();
                                }

                                if (note.NotePitch.Step == ScorePart.MeasureNote.NoteStep.Rest)
                                {
                                    voiceBag[note.VoiceId - 1].WriteLine("play :rest");
                                }
                                else
                                {
                                    if (note.Length > 0)
                                    {
                                        voiceBag[note.VoiceId - 1].WriteLine("play :{0}{1}, sustain:{2}", note.NotePitch.Step, note.NotePitch.Octove, note.Length);
                                    }
                                    else
                                    {
                                    //    voiceBag[note.VoiceId - 1].WriteLine("play :{0}{1}", note.NotePitch.Step, note.NotePitch.Octove);
                                    }
                                }

                                if (note.Length > 0)
                                {
                                    voiceBag[note.VoiceId - 1].WriteLine("sleep {0}", note.Length);
                                }
                            });

                        });

                        int v = 0;

                        voiceBag.ToList().ForEach(delegate (StringWriter voiceWriter) {
                            writer.WriteLine("##############################################");
                            writer.WriteLine("# Voice: {0}",v);
                            writer.WriteLine("##############################################");
                            writer.WriteLine();
                            writer.WriteLine(string.Format("in_thread(name: :voice{0}) do", v));
                            if(v==0)
                            {
                                writer.WriteLine("cue :tick");
                            }
                            else
                            {
                                writer.WriteLine("sync :tick");
                            }
                            writer.Write(voiceWriter.ToString());
                            writer.WriteLine("end");

                            v++;
                        });

                        writer.WriteLine("end");
                    }
                });

                return writer.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
