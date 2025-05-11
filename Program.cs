using Microsoft.Extensions.Configuration;
using PaymentService.DTOs;
using PaymentService.Interfaces;
using PaymentService.Middleware;
using PaymentService.Module;
using System.Runtime;

internal class Program
{
    private const string MONGODB_ADMIN_USER_NAME = "$(MONGODB_ADMIN_USER_NAME)";
    private const string MONGODB_ADMIN_USER_PASSWORD = "$(MONGODB_ADMIN_USER_PASSWORD)";
    private const string MONGODB_URL_PATH = "$(MONGODB_URL_PATH)";
    private const string MONGODB_USER_DB = "$(MONGODB_USER_DB)";

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Register the service with DI container
        builder.Services.AddSingleton<MongoContext>();
        builder.Services.AddTransient<IPaymentService, PaymentService.Services.PaymentService>();

        var MONGODB_URL = Environment.GetEnvironmentVariable("MONGODB_URL");
        var MONGODB_DB_NAME = Environment.GetEnvironmentVariable("MONGODB_DB_NAME");
        var AUTH_URL = Environment.GetEnvironmentVariable("AUTH_URL");

        AppSettings? appSettings;
        if (string.IsNullOrEmpty(MONGODB_URL) || string.IsNullOrEmpty(MONGODB_DB_NAME) || string.IsNullOrEmpty(AUTH_URL))
        {
            appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
        }
        else
        {
            Console.WriteLine(AUTH_URL);
            Console.WriteLine(MONGODB_DB_NAME);
            Console.WriteLine(MONGODB_URL);
            appSettings = new AppSettings
            {
                AUTH_URL = AUTH_URL,
                MONGODB_DB_NAME = MONGODB_DB_NAME,
                MONGODB_URL = MONGODB_URL,
            };
        }

        // Register the fully initialized object as a singleton
        builder.Services.AddSingleton(appSettings);


        // Add services to the container.
        builder.Services.AddControllers();
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

        app.UseMiddleware<AuthorizationMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

