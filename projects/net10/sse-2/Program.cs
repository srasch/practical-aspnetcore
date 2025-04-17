using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.MapGet("/sse", (HttpContext context, CancellationToken cancellationToken) =>
{
    async IAsyncEnumerable<string> CounterAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        int count = 0;
        while (true && !cancellationToken.IsCancellationRequested)
        {
            yield return $"hello world {++count}";
            await Task.Delay(3000, cancellationToken);
        }
    }

    if (context.Request.Headers["Accept"] == "text/event-stream")
    {
        return Results.ServerSentEvents(CounterAsync(cancellationToken), "greeting");
    }
    else
    {
        return Results.BadRequest("Unsupported Accept header. Use 'text/event-stream'.");
    }
});

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync(@"
    <html>
        <head>
        </head>
        <body>
            <h1>SSE</h1>
            <ul id=""list""></ul>
            <script>
                console.log('Connecting to SSE...');
                var source = new EventSource('/sse');
                source.onopen = function(event) {
                    console.log('Connection opened:', event);
                };
                source.onmessage = function(e) {
                    var item = document.createElement('li');
                    item.textContent = e.data;
                    list.appendChild(item);
                };
                source.onerror = function(event){
                    console.log(event);
                };
            </script>
        </body>
    </html>
    ");
});
 
app.Run();
