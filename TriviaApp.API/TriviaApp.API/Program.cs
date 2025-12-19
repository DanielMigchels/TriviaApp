using Microsoft.OpenApi;
using Serilog;
using TriviaApp.API.Services.Question;
using TriviaApp.API.Services.Question.Api;
using TriviaApp.API.Services.Question.Api.OpenTriviaApi;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot/";
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TriviaApp.API", Version = "v1" });
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("Redis").GetValue<string>("Configuration");
    options.InstanceName = "TriviaApp:";
});

builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<IQuestionApiService, OpenTriviaApiService>();
builder.Services.AddHttpClient<IOpenTriviaApiGateway, OpenTriviaApiGateway>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseSpaStaticFiles();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.UseSerilogRequestLogging();

app.UseSpa(spa =>
{
    if (app.Environment.IsDevelopment())
    {
        // Angular dev server instead of wwwroot
        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    }
});

app.Run();