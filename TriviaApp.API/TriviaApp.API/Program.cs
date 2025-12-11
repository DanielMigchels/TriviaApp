using Microsoft.OpenApi;
using Serilog;
using Serilog.Sinks.Network;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.TCPSink(builder.Configuration["Elastic:TcpSink"])
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot/triviaapp.ui/browser/";
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TriviaApp.API", Version = "v1" });
});

var app = builder.Build();

app.UseSpaStaticFiles();

app.UseSpa(spa =>
{
    if (app.Environment.IsDevelopment())
    {
        // Angular dev server instead of wwwroot
        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.Run();

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}