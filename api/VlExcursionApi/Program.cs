using BL.Services;
using DAL;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace VlExcursionApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddMvc();
        builder.Services.AddDbContext<Context>(optionsBuilder =>
        {
            optionsBuilder
                .UseNpgsql("Host=localhost; Database=excursionDb; Username=postgres; Password=qwer1234")
                .UseSnakeCaseNamingConvention()
                .LogTo(Console.WriteLine);
        });
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        builder.Services.AddScoped<RepositoryManager>();

        builder.Services.AddScoped<UserService>();



        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
