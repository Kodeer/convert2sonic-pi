using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MusicXml.SonicPi.MusicScore.ScorePart.MeasureNote;

namespace MusicXml.SonicPi.MusicScore.ScorePart
{
    public class Measure : IMeasure
    {
        public string Id { get; set; }
        public string Caption { get; set; }
        public int Length
        {
            get
            {
                if (Notes != null)
                {
                    return Notes.Sum(p => p.Length);
                }
                else
                {
                    return 0;
                }
            }
        }

        public List<Note> Notes { get; set; }

        public Measure(string id, string caption)
        {
            Id = id;
            Caption = caption;
            Notes = new List<Note>();
        }
    }
}
