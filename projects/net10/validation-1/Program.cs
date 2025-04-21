using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder();

builder.Services.AddValidation();

var app = builder.Build();

app.MapGet("/", () =>
{
    var html = """
    <html>
    <body>
        <h1>Validation on Route Parameters</h1>
        <ul>
            <li><a href="/validate/John/25">Valid Route</a></li>
            <li><a href="/validate/J/25">Invalid Route (Name too short)</a></li>
            <li><a href="/validate/Jonathan-Brand/25">Invalid Route (Name too long)</a></li>
            <li><a href="/validate/John/150">Invalid Route (Age out of range)</a></li>
        </ul>
    </body>
    </html>
    """;

    return TypedResults.Content(html, "text/html");
}); //This is not an API endpoint

app.MapGet("/validate/{name}/{age}", ([AsParameters]RouteInput input) =>
{
    return TypedResults.Ok(input);
});

app.Run();

public class RouteInput 
{
    [Required, MinLength(3), MaxLength(10)]
    public string Name { get; set; } = string.Empty;

    [Required, Range(1, 100)]
    public int Age { get; set; } = 1;
}