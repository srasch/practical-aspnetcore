using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.MapGet("/sse", (HttpContext context, CancellationToken cancellationToken) =>
{
    async IAsyncEnumerable<SseItem<string>> GetEventsAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        int count = 0;
        while (true && !cancellationToken.IsCancellationRequested)
        {
            yield return new SseItem<string>($"hello world {++count}", "greeting");
            yield return new SseItem<string>($"{DateTime.UtcNow}");
            await Task.Delay(3000, cancellationToken);
        }
    }

    if (context.Request.Headers.Accept == "text/event-stream")
    {
        return Results.ServerSentEvents(GetEventsAsync(cancellationToken));
    }
    else
    {
        return Results.BadRequest("Unsupported Accept header. Use 'text/event-stream'.");
    }
});

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("""
    <html>
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Bootstrap demo</title>
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.5/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-SgOJa3DmI69IUzQ2PVdRZhwQ+dy64/BUtbMJw1MZ8t5HZApcHrRKUc4W0kG879m7" crossorigin="anonymous">
    </head>
        <body>
            <h1>SSE</h1>
            <div class="row">
                <div class="col-md-6">            
                    <ul id="list"></ul>
                </div>
                <div class="col-md-6">
                    <ul id="greetings"></ul>
                </div>
            </div>
            <script>
                console.log('Connecting to SSE...');
                var source = new EventSource('/sse');
                source.onopen = function(event) {
                    console.log('Connection opened:', event);
                };

                source.onmessage = function(e){
                    var item = document.createElement('li');
                    item.textContent = e.data;
                    const list = document.getElementById("list");
                    list.appendChild(item);
                };

                source.addEventListener('greeting', function(e){
                    var item = document.createElement('li');
                    item.textContent = e.data;
                    const greetings = document.getElementById("greetings");
                    greetings.appendChild(item);
                });

                source.onerror = function(event){
                    console.log(event);
                };
            </script>
        </body>
    </html>
    """);
});
 
app.Run();
