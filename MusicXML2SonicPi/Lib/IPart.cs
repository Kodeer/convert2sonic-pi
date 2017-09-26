using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicXml.SonicPi.MusicScore.ScorePart
{
    public interface IPart
    {
        /// <summary>
        /// Part identifier
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Score part caption and notes
        /// </summary>
        string Caption { get; set; }

        /// <summary>
        /// Is score part wrapped in a thread 
        /// </summary>
        bool Threaded { get; set; }

        /// <summary>
        /// Score part threaded
        /// </summary>
        Block BlockPart { get; set; }
    }
}
