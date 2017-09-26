using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MusicXml.SonicPi.MusicScore.ScorePart;

namespace MusicXml.SonicPi.MusicScore
{
    public interface IScore
    {
        /// <summary>
        /// Score caption and notes
        /// </summary>
        string Caption { get; set; }

        /// <summary>
        /// Beats per minute (BPM)
        /// </summary>
        int Tempo { get; set; }

        /// <summary>
        /// Instrument name
        /// </summary>
        string Instrument { get; set; }

        /// <summary>
        /// Score part list
        /// </summary>
        List<Part> Parts { get; set; }

        string ToString();
    }
}
