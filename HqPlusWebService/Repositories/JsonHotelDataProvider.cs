using HqPlusWebService.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HqPlusWebService.Repositories
{
    public class JsonHotelDataProvider
    {
        public async virtual Task<ICollection<HotelRatesInfo>> GetFromFile(string path)
        {
            using FileStream fs = File.OpenRead(path);
            return await Get(fs);
        }

        public async Task<ICollection<HotelRatesInfo>> Get(FileStream fs)
        {
            using var sr = new StreamReader(fs, Encoding.UTF8);
            var json = await sr.ReadToEndAsync();
            return Get(json);
        }

        public ICollection<HotelRatesInfo> Get(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<ICollection<HotelRatesInfo>>(json, options);
        }
    }
}
