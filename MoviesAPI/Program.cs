
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using Microsoft.IdentityModel.Tokens;
using MoviesAPI.Controllers;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using MoviesAPI.Services;

namespace MoviesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
        IConfiguration configuration;

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IRepository,InMemoryRepository>();
            builder.Services.AddAutoMapper(typeof (Program));
            //var genresController = new GenresController(new InMemoryRepository());
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();
            // Configure the HTTP request pipeline.
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
