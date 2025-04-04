using Clubcore.Api.Mappings;
using Clubcore.Api.Services;
using Clubcore.Domain.Services;
using Clubcore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Clubcore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Configuration.AddEnvironmentVariables();
            builder.Services.AddDbContext<ClubcoreDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("ClubcoreDb"));
            });

            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<IClubService, ClubService>();
            builder.Services.AddScoped<IPersonService, PersonService>();

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Apply migrations on startup
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ClubcoreDbContext>();
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Log the error or handle it as needed
                    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
