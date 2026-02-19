using AutoMapper;
using BL.DTOs;
using BL.Mapping;
using DAL.Contracts;
using DAL.DbContext;
using DAL.Repositories;
using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            RegisterServicesHelper.RegisterServices(builder);


            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            var app = builder.Build();

            app.UseSerilogRequestLogging();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
