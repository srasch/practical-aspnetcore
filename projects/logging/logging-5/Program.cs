using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder();

// Adjust the minimum level here and see the impact 
// on the displayed logs.
// The rule is it will show >= minimum level
// The levels are:
// - Trace = 0
// - Debug = 1
// - Information = 2
// - Warning = 3
// - Error = 4
// - Critical = 5
// - None = 6

builder.Logging.SetMinimumLevel(LogLevel.Warning);
builder.Logging.AddConsole();

var app = builder.Build();
Log.Configure(app.Services.GetRequiredService<ILoggerFactory>());  

app.Run(context =>
{
    var log = Log.CreateLogger("main");
    log.LogTrace("Trace message");
    log.LogDebug("Debug message");
    log.LogInformation("Information message");
    log.LogWarning("Warning message");
    log.LogError("Error message");
    log.LogCritical("Critical message");
    return context.Response.WriteAsync("Hello world. Take a look at your terminal to see the logging messages.");
});
app.Run();

 public static class Log
{
    static ILoggerFactory _loggerFactory;

    static readonly ConcurrentDictionary<string, ILogger> _loggers = new();
    
    public static void Configure(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }
    
    public static ILogger CreateLogger<T>() => _loggers.GetOrAdd(typeof(T).FullName, _ => _loggerFactory.CreateLogger<T>());
    
    public static ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, _ => _loggerFactory.CreateLogger(categoryName));
}