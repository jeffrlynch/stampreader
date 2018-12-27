using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampReader
{
    class CatalogInfo:StampInfo
    {
        public string Color { get; set; }
        public int MintVF { get; set; }
        public int MintF { get; set; }
        public int MintVG { get; set; }
        public int UsedVF { get; set; }
        public int UsedF { get; set; }
        public int UsedVG { get; set; }
        public bool Commemorative { get; set; }
        public bool Definitive { get; set; }
    }
}
