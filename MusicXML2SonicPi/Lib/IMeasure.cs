using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MusicXml.SonicPi.MusicScore.ScorePart.MeasureNote;

namespace MusicXml.SonicPi.MusicScore.ScorePart
{
    public interface IMeasure
    {
        /// <summary>
        /// Measure identifier
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Measure caption and notes
        /// </summary>
        string Caption { get; set; }

        /// <summary>
        /// Total beat length for this measure
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Notes and rests information
        /// </summary>
        List<Note> Notes { get; set; }

    }
}
