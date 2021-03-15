using HqPlusWebService.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HqPlusWebService.Repositories
{
    public class JsonHotelRepository : IHotelRepository
    {
        private readonly JsonHotelDataProvider _dataProvider;
        private readonly string _jsonFilePath;

        public JsonHotelRepository(JsonHotelDataProvider dataProvider, IOptions<JsonHotelRepositoryOptions> options)
        {
            _dataProvider = dataProvider;
            _jsonFilePath = options.Value.FilePath;
            if (_jsonFilePath.StartsWith("/"))
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                _jsonFilePath = Path.Combine(currentDirectory, "input-data", _jsonFilePath[1..]);
            }
        }

        public async Task<IEnumerable<HotelRatesInfo>> GetHotelRateInfos()
        {
            return await _dataProvider.GetFromFile(_jsonFilePath);
        }

        public async Task<IEnumerable<HotelRate>> FilterHotelRates(int? hotelId, DateTimeOffset? arrivalDate)
        {
            var data = await GetHotelRateInfos();
            var targetDay = arrivalDate?.Date;
            return data
                .Where(e => e.Hotel.HotelId == hotelId || hotelId == null)
                .SelectMany(e => e.HotelRates)
                .Where(e => e.TargetDay.Date == targetDay || targetDay == null)
                .ToList();
        }
    }
}
