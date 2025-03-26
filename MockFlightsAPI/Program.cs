using Microsoft.OpenApi.Models;

namespace MockFlightsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /// <summary>
            /// opretter en builder som bruges til at konfigurere  applikation.
            /// </summary>
            var builder = WebApplication.CreateBuilder(args);

            /// <summary>
            /// Registrerer support til controllers i ASP.NET Core.
            /// Gør det muligt at bruge [ApiController]-baserede endpoints som FlightsController.cs
            /// </summary>
            builder.Services.AddControllers();

            /// <summary>
            /// Konfigurer CORS (Cross-Origin Resource Sharing) så Blazor fronten kan hente data fra API'et fra en anden port.
            /// Blazor frontend'en kører på https://localhost:7115
            /// API'et kører på https://localhost:7115 .
            /// </summary>
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorFrontend", policy =>
                {
                    policy.WithOrigins("https://localhost:7177") // Tillad forespørgsler fra blazor frontend
                          .AllowAnyMethod() // Tillad alle metoder (GET, POST, PUT, DELETE)
                          .AllowAnyHeader(); // Tillad alle headers

                });
                   
            });

            /// <summary>
            /// Tilføjer Swagger (OpenAPI) dokumentation og test-værktøj.
            /// Swagger dokumenterer alle dine endpoints automatisk.
            /// </summary>
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mock Flights API",
                    Version = "v1",
                    Description = "En mock API til flydata med tilfældige flyvninger op til juni 2025."
                });
            });

            /// <summary>   
            /// Bygger appen med de services og konfigurationer der er blevet tilføjet.
            /// </summary>
            var app = builder.Build();

            /// <summary>
            /// Aktiver Swagger og Swagger UI i udviklingsmiljø
            /// Det betyder, at når du kører lokalt (Development), kan du se Swagger-dokumentation.
            /// </summary>
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mock Flights API v1");
                    c.RoutePrefix = string.Empty; // Gør Swagger tilgængelig på roden (https://localhost:7115)
                });
            }

            /// <summary>
            /// Aktiver den definerede CORS-policy
            /// Det skal ske før routing og controller-håndtering.
            /// </summary>
            app.UseCors("AllowBlazorFrontend");

            /// <summary>
            /// Autorisation middleware
            /// Ikke i brug endnu, men kræves for at aktivere evt. sikkerhed senere.
            /// </summary>
            app.UseAuthorization();

            /// <summary>
            /// Aktiver controller-routing
            /// Det gør det muligt at kalde fx /api/flights fra frontend eller Postman.
            /// </summary>
            app.MapControllers();

            /// <summary>
            /// Start applikationen
            /// Denne linje starter selve webserveren.
            /// </summary>
            app.Run();
        }
    }
}
