using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaxon.ObtainBaiDuAddress.Model
{
    public class ShopModel
    {
        public int ShopID { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Province { get; set; }

        public string CityID { get; set; }

        public string County { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }

        public double bd_Longitude { get; set; }

        public double bd_Latitude { get; set; }

        public string Uri { get; set; }

    }
}
