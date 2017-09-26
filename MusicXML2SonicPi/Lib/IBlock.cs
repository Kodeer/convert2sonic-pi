using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicXml.SonicPi.MusicScore.ScorePart
{
    public interface IBlock
    {
        /// <summary>
        /// Measure list
        /// </summary>
        List<Measure> Measures { get; set; }
    }
}
