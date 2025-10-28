using OrderingApplication;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services
    .AddApplicationService()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

app.MapGet("/", () => "Hello World!");

app.Run();
