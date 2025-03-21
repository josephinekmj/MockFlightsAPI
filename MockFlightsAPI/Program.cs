using Microsoft.OpenApi.Models;

namespace MockFlightsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Tilføj tjenester til API, Swagger og CORS
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Konfigurer Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mock Flights API",
                    Version = "v1",
                    Description = "En mock API til flydata med tilfældige flyvninger op til juni 2025."
                });
            });

            // Konfigurer CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            var app = builder.Build();

            // Aktiver Swagger i udviklingsmiljø
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mock Flights API v1");
                    c.RoutePrefix = string.Empty; // Åbner Swagger på roden (http://localhost:5000/)
                });
            }

            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
