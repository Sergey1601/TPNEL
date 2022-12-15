using WebApp.Models;
using WebApp.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace WebApp 
{
    public class Program 
    {
        public static void Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DishContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DishsContext")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}