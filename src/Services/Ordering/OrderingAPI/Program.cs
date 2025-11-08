using OrderingInfrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplicationService()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();
// add http request pipeline
app.UseApiServices();

// if (app.Environment.IsDevelopment())
// {
//     await app.InitialiseDatabaseAsync();
// }

app.MapGet("/", () => "Hello World!");

app.Run();
