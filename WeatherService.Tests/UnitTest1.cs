﻿using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeatherService.Tests
{
    public abstract class WeatherServiceTests
    {
        public abstract IWeatherService Subject { get; }

        [TestMethod]
        public async Task TemperatureIsInExpectedRange()
        {
            var result = await Subject.GetWeatherAsync("Seattle, WA");
            // Roughly the hottest and coldest ever recorded, plus a margin
            Assert.IsTrue(result.Temperature > 150 && result.Temperature < 350);
        }
    }

    [TestClass]
    public class OpenweathermapWeatherServiceTests : WeatherServiceTests
    {
        private readonly IWeatherService _subject = new OpenweathermapWeatherService();

        public override IWeatherService Subject
        {
            get { return _subject; }
        }
    }
}
