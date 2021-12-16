using iMaxSys.Max;
using iMaxSys.Data;
using iMaxSys.Identity.Data.EFCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureConfiguration(builder.Configuration);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ConfigureMiddleware(app, app.Services);
ConfigureEndpoints(app, app.Services);

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
    //services.AddMaxIdentity(configuration);
    services.AddDbContext<MaxIdentityContext>().AddUnitOfWork<MaxIdentityContext>();
    services.AddSwaggerGen();
}
static void ConfigureMiddleware(IApplicationBuilder app, IServiceProvider services)
{

}
static void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services)
{

}