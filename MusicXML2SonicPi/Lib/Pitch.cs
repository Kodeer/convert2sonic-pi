using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicXml.SonicPi.MusicScore.ScorePart.MeasureNote
{
    public class Pitch : IPitch
    {
        public NoteStep Step { get; set; }
        public string Octove { get; set; }

        public Pitch(NoteStep step)
        {
            Step = step;
            Octove = string.Empty;
        }
        public Pitch(NoteStep step, string octove)
        {
            Step = step;
            Octove = octove;
        }
        public Pitch(NoteStep step, NoteAccidental accidental, string octove)
        {
            Step = step;
            Octove = octove;
        }
    }
}
