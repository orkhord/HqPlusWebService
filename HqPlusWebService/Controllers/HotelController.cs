using HqPlusWebService.Models;
using HqPlusWebService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HqPlusWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> _logger;
        private readonly IHotelRepository _repo;

        public HotelController(ILogger<HotelController> logger, IHotelRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<HotelRate>> Get(int? hotelId = null, DateTimeOffset? arrivalDate = null)
        {
            return await _repo.FilterHotelRates(hotelId, arrivalDate);
        }
    }
}
