using HqPlusWebService.Models;
using HqPlusWebService.Repositories;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HqPlusWebService.Tests
{
    [TestFixture]
    public class JsonHotelRepositoryTest
    {
        private JsonHotelRepository _repository;

        [SetUp]
        public void Setup()
        {
            var dataProvider = new Mock<JsonHotelDataProvider>();
            dataProvider.Setup(dp => dp.GetFromFile("x").Result).Returns(new List<HotelRatesInfo> {
                    new HotelRatesInfo {
                        Hotel = new Hotel { HotelId = 1234 },
                        HotelRates = new List<HotelRate>
                        {
                            new HotelRate {  TargetDay = new DateTime(2012, 04, 12, 7, 1, 30) },
                            new HotelRate {  TargetDay = new DateTime(1997, 03, 03) },
                            new HotelRate {  TargetDay = new DateTime(1997, 03, 03, 6, 12, 14) },
                            new HotelRate {  TargetDay = new DateTime(2003, 09, 29) },
                        }
                    }
                });

            _repository = new JsonHotelRepository(dataProvider.Object, Options.Create(new JsonHotelRepositoryOptions { FilePath = "x" }));
        }

        [Test]
        public void TestBothParams()
        {
            var result = _repository.FilterHotelRates(1234, new DateTime(1997, 03, 03)).Result;
            var count = result.Count();
            var expected = 2;
            Assert.AreEqual(expected, count, $"Collection length should be ${expected}");
        }

        [Test]
        public void TestBookId()
        {
            var result = _repository.FilterHotelRates(12345, null).Result;
            var count = result.Count();
            var expected = 0;
            Assert.AreEqual(expected, count, $"Collection length should be ${expected}");
        }

        [Test]
        public void TestArrivalDate()
        {
            var result = _repository.FilterHotelRates(null, new DateTime(2012, 04, 12)).Result;
            var count = result.Count();
            var expected = 1;
            Assert.AreEqual(expected, count, $"Collection length should be ${expected}");
        }

        [Test]
        public void TestWithoutParams()
        {
            var result = _repository.FilterHotelRates(null, null).Result;
            var count = result.Count();
            var expected = 4;
            Assert.AreEqual(expected, count, $"Collection length should be ${expected}");
        }
    }
}