var builder = WebApplication.CreateBuilder(args);

var assembly=typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    //config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// register the validators depency sevrice
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

// add marten service
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

// we basically add our custom exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

app.MapCarter();

// we configure our application according to the global exception handling
app.UseExceptionHandler(options => { });
// {}->menas we rely on custom exception handler to handle all the exceptions

app.Run();
