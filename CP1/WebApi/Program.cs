using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var list = new List<object>();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapPost("/", ([FromHeader] bool xml = false) =>
{
    var payload = list;

    // Construye la respuesta
    if (xml)
    {
        var xmlResult = Serializer(payload);
        return Results.Content(xmlResult, "application/xml");
    }
    else
    {
        return Results.Ok(payload);
    }
}
);


app.MapPut("/", ([FromForm] int quantity, [FromForm] string type) =>
{
    if (quantity <= 0)
    {
        return Results.BadRequest(new { error = "El valor de 'quantity' debe ser mayor a 0" });
    }
    var random = new Random();

    if (type == "int")
    {
        for (; quantity > 0; quantity--)
        {
            list.Add(random.Next());
        }
        return Results.Ok("");
    }
    else if (type == "float")
    {
        for (; quantity > 0; quantity--)
        {
            list.Add(random.NextSingle());
        }
        return Results.Ok("");
    }
    else
    {
        return Results.BadRequest(new { error = "El valor de 'type' debe ser 'float' o 'int'." });
    }
}).DisableAntiforgery();

app.MapDelete("/", ([FromForm] int quantity) =>
{
    if (quantity <= 0)
    {
        return Results.BadRequest(new { error = "El valor de 'quantity' debe ser mayor a 0." });
    }
    else if (quantity > list.Count())
    {
        return Results.BadRequest(new { error = "El valor de 'quantity' debe ser mayor a la cantidad de items en la lista." });
    }
    else
    {
        for (; quantity > 0; quantity--)
        {
            list.RemoveAt(0);
        }
        return Results.Ok();
    }
}).DisableAntiforgery();

app.MapPatch("/", () =>
{
    list = [];
    return Results.Ok();
});

app.Run();

static string Serializer<T>(T obj)
{
    var xmlSerializer = new XmlSerializer(typeof(T));
    using var stringWriter = new StringWriter();
    xmlSerializer.Serialize(stringWriter, obj);
    return stringWriter.ToString();
}