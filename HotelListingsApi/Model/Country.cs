using System.Collections.Generic;

namespace HotelListingsApi.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string SCO { get; set; }

        public virtual IList<Hotel> Hotels { get; set; }
    }
}