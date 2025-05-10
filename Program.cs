using PaymentService.Interfaces;
using PaymentService.Module;

var builder = WebApplication.CreateBuilder(args);
// Register the service with DI container
builder.Services.AddSingleton<MongoContext>();
builder.Services.AddTransient<IPaymentService, PaymentService.Services.PaymentService>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
