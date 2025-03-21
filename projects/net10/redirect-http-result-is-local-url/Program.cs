using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.MapGet("/", () =>
{
    var html = $$"""
    <html>
    <body>
        <h1>RedirectHttpResult.IsLocalUrl</h1>
        <ul>
            <li>https://www.cnn.com - {{ RedirectHttpResult.IsLocalUrl("https://www.cnn.com")}}</li>
            <li>/about - {{ RedirectHttpResult.IsLocalUrl("/about")}}</li>
            <li>~/ - {{ RedirectHttpResult.IsLocalUrl("~/")}}</li>
    </body>
    </html>
    """;

    return TypedResults.Content(html, "text/html");
}).ExcludeFromDescription(); //This is not an API endpoint

app.Run();