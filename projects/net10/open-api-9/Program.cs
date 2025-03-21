using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder();

builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();

var app = builder.Build();

app.MapOpenApi("/openapi/{documentName}.json");
app.MapOpenApi("/openapi/{documentName}.yaml");

app.UseSwaggerUi(options =>
{
    options.DocumentPath = "/openapi/{documentName}.yaml";
});

app.MapGet("/", () =>
{
    var html = """
    <html>
    <body>
        <h1>OpenAPI JSON/YAML documents</h1>
        <ul>
            <li>
                You can check the generated OpenAPI document in JSON <a href="/openapi/v1.json">here</a>.
            </li>
            <li>
                The OpenAPI document is also available in YAML format <a href="/openapi/v1.yaml">here</a>.
            </li>
            <li>
                You can check the Swagger UI <a href="/swagger/index.html">here</a>.
            </li>
        </ul>
    </body>
    </html>
    """;

    return TypedResults.Content(html, "text/html");
}).ExcludeFromDescription(); //This is not an API endpoint

app.MapGet("/hello/{name}",  Hello);

app.Run();


static partial class Program
{
    ///<summary>
    /// Returns a greeting message
    /// </summary>
    /// <remarks>
    /// This is a sample endpoint that returns a greeting message.
    /// </remarks>
    /// <param name="name">The name of the person to greet</param>
    public static string Hello(string name)
    {
        return $"Hello, {name}";
    }
}
