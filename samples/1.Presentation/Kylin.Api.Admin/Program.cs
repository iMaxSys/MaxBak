using iMaxSys.Max;
using iMaxSys.Data;
using iMaxSys.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureConfiguration(builder.Configuration);
ConfigureServices(builder.Services, builder.Configuration);


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//================================start================================

static void ConfigureConfiguration(ConfigurationManager configuration)
{
    //builder.Services.AddMax(builder.Configuration);
}
static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    services.AddMax(configuration);
    services.AddMaxIdentity(configuration);
    services.AddSwaggerGen();
}
/*
static void ConfigureMiddleware(IApplicationBuilder app, IServiceProvider services)
{

}
static void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services)
{

}
*/