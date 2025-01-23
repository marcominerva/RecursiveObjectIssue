var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.MapOpenApi();
    _ = app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", app.Environment.ApplicationName);
    });
}

app.UseHttpsRedirection();

app.MapGet("/api/categories", () =>
{
    return TypedResults.Ok(new Category { Name = "My Category" });
});

app.Run();

public class Category
{
    public required string Name { get; set; }

    public Category? Parent { get; set; }

    // The issue happens even with I use a primitive type for the collection.
    public IEnumerable<Tag> Tags { get; set; } = [];
}

public class Tag
{
    public required string Name { get; set; }
}