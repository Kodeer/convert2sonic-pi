using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicXml.SonicPi.MusicScore.ScorePart
{
    public class Block : IBlock
    {
        public List<Measure> Measures { get; set; }

        public Block()
        {
            Measures = new List<Measure>();
        }
    }
}
