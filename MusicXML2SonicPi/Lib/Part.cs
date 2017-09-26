using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicXml.SonicPi.MusicScore.ScorePart
{
    public class Part : IPart
    {
        public string Id { get; set; }
        public string Caption { get; set; }
        public bool Threaded { get; set; }
        public Block BlockPart { get; set; }

        //public Part()
        //{
        //    Id = string.Empty;
        //    Caption = string.Empty;
        //    Threaded = false;
        //    BlockPart = new Block();
        //}
        public Part(string id, string caption, bool threaded)
        {
            Id = id;
            Caption = caption;
            Threaded = threaded;
            BlockPart = new Block(); 
        }
    }
}
