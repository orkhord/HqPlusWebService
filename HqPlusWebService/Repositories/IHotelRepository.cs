using HqPlusWebService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HqPlusWebService.Repositories
{
    public interface IHotelRepository
    {
        Task<IEnumerable<HotelRatesInfo>> GetHotelRateInfos();
        Task<IEnumerable<HotelRate>> FilterHotelRates(int? hotelId, DateTimeOffset? arrivalDate);
    }
}
