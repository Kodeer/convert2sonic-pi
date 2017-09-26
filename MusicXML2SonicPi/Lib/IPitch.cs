using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicXml.SonicPi.MusicScore.ScorePart.MeasureNote
{
    public enum NoteStep
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        Rest,
    }

    public interface IPitch
    {
        /// <summary>
        /// Note or rest step 
        /// </summary>
        NoteStep Step { get; set; }

        /// <summary>
        /// Note octave, rest default to blank
        /// </summary>
        string Octove { get; set; }
    }
}
