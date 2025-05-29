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
        private static readonly List<string> Airports = new List<string>
        {
           "AEP",   // Buenos Aires Aeroparque Jorge Newbery, Argentina
            "EZE",   // Buenos Aires Ministro Pistarini, Argentina
            "GIG",   // Rio de Janeiro (Galeão), Brasilien
            "GRU",   // São Paulo Guarulhos, Brasilien
            "YYZ",   // Toronto Pearson, Canada
            "MEX",   // Mexico City International, Mexico
            "CAI",   // Cairo, Egypten
            "HRG",   // Hurghada, Egypten
            "SSH",   // Sharm El Sheikh, Egypten
            "CPH",   // København, Danmark
            "AAR",   // Aarhus, Danmark
            "BLL",   // Billund, Danmark 
            "HEL",   // Helsinki, Finland
            "CDG",   // Paris Charles de Gaulle, Frankrig
            "ORY",   // Paris Orly, Frankrig
            "HER",   // Heraklion (Kreta),  Grækenland
            "AMS",   // Amsterdam Schiphol, Holland
            "DUB",   // Dublin, Irland
            "FCO",   // Rome Fiumicino, Italien
            "MXP",   // Milan Malpensa, Italien
            "VCE",   // Venice Marco Polo, Italien
            "MLE",   // Malé International, Maldiverne
            "OSL",   // Oslo Gardermoen, Norge
            "BCN",   // Barcelona El Prat, Spanien
            "LPA",   // Las Palmas (Gran Canaria), Spanien
            "MAD",   // Madrid Barajas, Spanien
            "PMI",   // Palma de Mallorca, Spanien
            "ARN",   // Stockholm Arlanda, Sverige
            "ZRH",   // Zurich, Schweiz
            "IST",   // Istanbul Airport, Tyrkiet
            "LGW",   // London Gatwick, Storbritannien
            "LHR",   // London Heathrow, Storbritannien
            "BER",   // Berlin Brandenburg, Tyskland
            "FRA",   // Frankfurt, Tyskland
            "DEL",   // Indira Gandhi International, Delhi, Indien
            "HKG",   // Hong Kong International, Hongkong
            "HND",   // Tokyo Haneda, Japan
            "NRT",   // Tokyo Narita, Japan
            "KUL",   // Kuala Lumpur,  Malaysia
            "ICN",   // Incheon International, Sydkorea
            "SIN",   // Singapore Changi, Singapore
            "BKK",   // Bangkok Suvarnabhumi, Thailand
            "DOH",   // Hamad International, Doha, Qatar
            "DXB",   // Dubai International, Forenede Arabiske Emirater
            "SYD",   // Sydney Kingsford Smith, Australien
            "CPT",   // Cape Town, Sydafrika
            "ATL",   // Atlanta Hartsfield–Jackson, USA
            "JFK",   // New York (John F. Kennedy), USA
            "LGA",   // New York LaGuardia, USA
            "LAX",   // Los Angeles, USA
            "MIA",   // Miami International, USA
            "MDW",   // Chicago Midway International, USA
            "ORD",   // Chicago O'Hare, USA
            "SEA",   // Seattle-Tacoma, USA
            "SFO",   // San Francisco, USA
        };

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
