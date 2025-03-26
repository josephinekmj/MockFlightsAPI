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
            /// G�r det muligt at bruge [ApiController]-baserede endpoints som FlightsController.cs
            /// </summary>
            builder.Services.AddControllers();

            /// <summary>
            /// Konfigurer CORS (Cross-Origin Resource Sharing) s� Blazor fronten kan hente data fra API'et fra en anden port.
            /// Blazor frontend'en k�rer p� https://localhost:7115
            /// API'et k�rer p� https://localhost:7115 .
            /// </summary>
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorFrontend", policy =>
                {
                    policy.WithOrigins("https://localhost:7177") // Tillad foresp�rgsler fra blazor frontend
                          .AllowAnyMethod() // Tillad alle metoder (GET, POST, PUT, DELETE)
                          .AllowAnyHeader(); // Tillad alle headers

                });
                   
            });

            /// <summary>
            /// Tilf�jer Swagger (OpenAPI) dokumentation og test-v�rkt�j.
            /// Swagger dokumenterer alle dine endpoints automatisk.
            /// </summary>
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mock Flights API",
                    Version = "v1",
                    Description = "En mock API til flydata med tilf�ldige flyvninger op til juni 2025."
                });
            });

            /// <summary>   
            /// Bygger appen med de services og konfigurationer der er blevet tilf�jet.
            /// </summary>
            var app = builder.Build();

            /// <summary>
            /// Aktiver Swagger og Swagger UI i udviklingsmilj�
            /// Det betyder, at n�r du k�rer lokalt (Development), kan du se Swagger-dokumentation.
            /// </summary>
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mock Flights API v1");
                    c.RoutePrefix = string.Empty; // G�r Swagger tilg�ngelig p� roden (https://localhost:7115)
                });
            }

            /// <summary>
            /// Aktiver den definerede CORS-policy
            /// Det skal ske f�r routing og controller-h�ndtering.
            /// </summary>
            app.UseCors("AllowBlazorFrontend");

            /// <summary>
            /// Autorisation middleware
            /// Ikke i brug endnu, men kr�ves for at aktivere evt. sikkerhed senere.
            /// </summary>
            app.UseAuthorization();

            /// <summary>
            /// Aktiver controller-routing
            /// Det g�r det muligt at kalde fx /api/flights fra frontend eller Postman.
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
