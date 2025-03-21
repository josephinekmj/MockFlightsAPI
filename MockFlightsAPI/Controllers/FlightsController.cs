using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MockFlightsAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : ControllerBase
    {
        private static readonly Random _random = new Random();
        private static readonly List<string> Airports = new List<string> { "JFK", "LHR", "CDG", "FRA", "DXB", "HND", "ORD", "LAX", "SYD", "SIN" };
        private static readonly List<string> Airlines = new List<string> { "Delta", "British Airways", "Air France", "Lufthansa", "Emirates", "ANA", "United", "Qantas", "Singapore Airlines" };

        [HttpGet]
        public IActionResult GetFlights()
        {
            var flights = GenerateFlights();
            return Ok(new { pagination = new { limit = 10, offset = 0, count = flights.Count, total = flights.Count }, data = flights });
        }

        private List<object> GenerateFlights()
        {
            var flights = new List<object>();
            var today = DateTime.UtcNow.Date;
            var endDate = new DateTime(today.Year, 6, 30); // Slutdato i juni

            for (var date = today; date <= endDate; date = date.AddDays(1))
            {
                flights.Add(new
                {
                    flight_date = date.ToString("yyyy-MM-dd"),
                    flight_status = _random.Next(2) == 0 ? "scheduled" : "delayed",
                    departure = new
                    {
                        airport = Airports[_random.Next(Airports.Count)],
                        timezone = "UTC",
                        iata = Airports[_random.Next(Airports.Count)],
                        scheduled = date.AddHours(_random.Next(6, 20)).ToString("yyyy-MM-ddTHH:mm:ss+00:00")
                    },
                    arrival = new
                    {
                        airport = Airports[_random.Next(Airports.Count)],
                        timezone = "UTC",
                        iata = Airports[_random.Next(Airports.Count)],
                        scheduled = date.AddHours(_random.Next(7, 23)).ToString("yyyy-MM-ddTHH:mm:ss+00:00")
                    },
                    airline = new
                    {
                        name = Airlines[_random.Next(Airlines.Count)],
                        iata = $"A{_random.Next(10, 99)}",
                        icao = $"ICA{_random.Next(100, 999)}"
                    },
                    flight = new
                    {
                        number = $"{_random.Next(100, 999)}",
                        iata = $"FL{_random.Next(100, 999)}",
                        icao = $"FIC{_random.Next(100, 999)}"
                    },
                     price = new
                     {
                         amount = _random.Next(500, 5000), // Tilfældig pris mellem 500 og 5000 DKK
                         currency = "DKK",
                         classType = _random.Next(2) == 0 ? "Economy" : "Business"
                     }
                });
            }

            return flights;
        }
    }
}
