using System.Collections.Generic;

namespace HqPlusWebService.Models
{
    public class HotelRatesInfo
    {
        public Hotel Hotel { get; set; }
        public ICollection<HotelRate> HotelRates { get; set; }
    }
}
