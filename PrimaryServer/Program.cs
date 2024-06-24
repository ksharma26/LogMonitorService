var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure AllowedIpsConfig
builder.Services.Configure<LogMonitorService.Models.AllowedIpsConfig>(builder.Configuration.GetSection("AllowedIpsConfig"));

// Register LogProvider and Validator as singletons
builder.Services.AddSingleton<LogMonitorService.Providers.LogProvider>();
builder.Services.AddSingleton<LogMonitorService.Services.Validator>();

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
