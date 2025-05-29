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
            "JFK",   // New York (John F. Kennedy),USA
            "LAX",   // Los Angeles,USA
            "ORD",   // Chicago O'Hare,USA
            "LHR",   // London Heathrow, Storbritannien
            "CDG",   // Paris Charles de Gaulle,Frankrig
            "FRA",   // Frankfurt, Tyskland
            "BER",   // Berlin Brandenburg, Tyskland
            "CPH",   // København, Danmark
            "AAR",   // Aarhus,Danmark
            "BLL",   // Billund, Danmark 
            "ARN",   // Stockholm Arlanda, Sverige
            "OSL",   // Oslo Gardermoen, Norge
            "AMS",   // Amsterdam Schiphol,Holland
            "PMI",   // Palma de Mallorca, Spanien
            "LPA",   // Las Palmas (Gran Canaria),Spanien
            "HER",   // Heraklion (Kreta),  Grækenland
            "CAI",   // Cairo, Egypten
            "SSH",   // Sharm El Sheikh, Egypten
            "HRG",   // Hurghada, Egypten
            "BKK",   // Bangkok Suvarnabhumi, Thailand
            "KUL",   // Kuala Lumpur,  Malaysia
            "MLE",   // Malé International, Maldiverne
            "SYD",   // Sydney Kingsford Smith, Australien
            "CPT",   // Cape Town, Sydafrika
            "HND",   // Tokyo Haneda, Japan
            "SIN",   // Singapore Changi, Singapore
            "GIG",   // Rio de Janeiro (Galeão), Brasilien
            "VCE",   // Venice Marco Polo, Italien
            "FCO",   // Rome Fiumicino, Italien
            "MXP",   // Milan Malpensa, Italien
            "BCN",   // Barcelona El Prat, Spanien
            "MAD",   // Madrid Barajas, Spanien
            "DUB",   // Dublin, Irland
            "ZRH",   // Zurich, Schweiz
            "HEL",   // Helsinki, Finland
            "DXB",   // Dubai International, Forenede Arabiske Emirater
            "DEL",   // Indira Gandhi International, Delhi, Indien
            "ICN",   // Incheon International, Sydkorea
            "YYZ",   // Toronto Pearson, Canada
            "GRU",   // São Paulo Guarulhos, Brasilien
            "SFO",   // San Francisco, USA
            "MIA",   // Miami International, USA
            "ATL",   // Atlanta Hartsfield–Jackson, USA
            "SEA",   // Seattle-Tacoma, USA
            "IST",   // Istanbul Airport, Tyrkiet
            "DOH",   // Hamad International, Doha, Qatar
            "LGA",   // New York LaGuardia, USA
            "LGW",   // London Gatwick, Storbritannien
            "ORY",   // Paris Orly, Frankrig
            "NRT",   // Tokyo Narita, Japan
            "EZE",   // Buenos Aires Ministro Pistarini, Argentina
            "AEP",   // Buenos Aires Aeroparque Jorge Newbery, Argentina
            "MEX",   // Mexico City International, Mexico
            "HKG",   // Hong Kong International, Hongkong
            "MDW",   // Chicago Midway International, USA
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
