using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampReader
{
    class CollectionDetail:StampInfo
    {
        public string Type { get; set; }
        public string Condition { get; set; }
        public string CountryName { get; set; }
        public string AlbumDescription { get; set; }
        public int AlbumPage { get; set; }
        public string Notes { get; set; }
        public int Cost { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchasedFrom { get; set; }
    }
}
