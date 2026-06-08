using Microsoft.EntityFrameworkCore;
using TechMoves_WebAPI.Data;
using TechMoves_WebAPI.Services;

namespace TechMoves_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddCors(options => {
                options.AddPolicy("AllowFrontend", policy => {
                    policy.WithOrigins(
                              "http://localhost:5186",
                              "https://localhost:7169",
                              "http://techmove-logistics:5186",
                              "https://techmove-logistics:7169"
                          )
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TechMoves_WebAPIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHttpClient();
            builder.Services.AddHttpClient<CurrencyService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowFrontend");

            app.MapControllers();

            app.Run();
        }
    }
}
