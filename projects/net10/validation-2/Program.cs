using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Validation;

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
            <li><a href="/validate/Jonathan-Brand/51">Invalid Route</a></li>
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

[ValidatableType]
public class RouteInput : IValidatableObject 
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int Age { get; set; } = 1;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Name.Length > 10)
        {
            if (Age > 50)
                yield return new ValidationResult("Age must be less than 50 when name is longer than 10 characters", [nameof(Name), nameof(Age)]);
        }
    }
}