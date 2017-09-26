using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicXml.SonicPi.MusicScore.ScorePart.MeasureNote
{
    public enum NoteAccidental
    {
        nil,
        sharp,
        flat,
        natural,
    }

    public interface INote
    {
        /// <summary>
        /// Note index / position with in the measure
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// Pitch information for this note or rest
        /// </summary>
        Pitch NotePitch { get; set; }

        /// <summary>
        /// The note or rest duration
        /// </summary>
        int Length { get; set; }

        /// <summary>
        /// Voice identifier
        /// </summary>
        int VoiceId { get; set; }

        /// <summary>
        /// Indicate if this note is part of a chord
        /// </summary>
        bool IsChord { get; set; }

        /// <summary>
        /// Note accidental, default is natural. Rest default to natural.
        /// </summary>
        NoteAccidental Accidental { get; set; }
    }
}
