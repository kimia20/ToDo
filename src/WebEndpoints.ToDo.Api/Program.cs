using Application;
using Infrastructure;
using Microsoft.IdentityModel.Logging;
using Serilog;
using System.Diagnostics;
using WebEndpoints.ToDo.Api;
using WebEndpoints.ToDo.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ApplicationName", "")
        .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);

#if DEBUG
    // Used to filter out potentially bad data due debugging.
    // Very useful when doing Seq dashboards and want to remove logs under debugging session.
    loggerConfiguration.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
#endif
});


// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebEndpoinServices(builder.Configuration);


//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
    app.UseHttpsRedirection();
}
app.UseCustomizedCors();
app.UseCustomizedSwagger();
app.UseCustomizedDataStore();
app.UseHealthChecks("/health");
app.CustomExceptionMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
