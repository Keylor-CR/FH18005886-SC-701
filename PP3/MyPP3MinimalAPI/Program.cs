using System.IO.Pipelines;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;


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

app.MapGet("/", () => Results.Redirect("swagger/index.html"));


app.MapPost("/include/{position:int}", ([FromRoute] int position, [FromQuery] string value, [FromForm] string text, [FromHeader] bool xml = false) =>
{
    if (position < 0)
    {
        return Results.BadRequest(new { error = "Error de validación: 'position' debe ser igual o mayor que cero." });
    }
    else if (value.Length <= 0)
    {
        return Results.BadRequest(new { error = "Error de validación: 'value' no puede estar vacío." });
    }
    else if (text.Length <= 0)
    {
        return Results.BadRequest(new { error = "Error de validación: 'text' no puede estar vacío." });

    }
    
    //Lista de Palabras
    var wordsList = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

    if (position >= wordsList.Count) //Nuevo position es al final de la lista de palabras
    {
        position = wordsList.Count;
    }

    //Inserta en la posición definida
    wordsList.Insert(position, value);
    
    //Reconstruye la oración
    string newSentence = string.Join(" ", wordsList);

    var payload = new Result { Ori = text, New = newSentence };
    
    //Construye la respuesta
    if (xml)
    {
        var xmlResult = Serializer(payload);
        return Results.Content(xmlResult, "application/xml");
    }
    else
    {
        return Results.Ok(payload);
    }
    
}).DisableAntiforgery();

app.MapPut("/replace/{length:int}", ([FromRoute] int length, [FromQuery] string value, [FromForm] string text, [FromHeader] bool xml = false) =>
{
    if (length <= 0)
    {
        return Results.BadRequest(new { error = "Error de validación: 'length' debe ser mayor que cero." });
    }
    else if (value.Length <= 0)
    {
        return Results.BadRequest(new { error = "Error de validación: 'value' no puede estar vacío." });

    }
    else if (text.Length <= 0)
    {
        return Results.BadRequest(new { error = "Error de validación: 'text' no puede estar vacío." });
    }

    //Lista de Palabras
    var wordsList = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

    //Itero la lista de Palabras
    for (int i = 0; i < wordsList.Count; i++)
    {
        if (wordsList[i].Length == length)
            wordsList[i] = value;
    }

    //Reconstruye la oración
    string newSentence = string.Join(" ", wordsList);

    var payload = new Result { Ori = text, New = newSentence };
    
    //Construye la respuesta
    if (xml)
    {
        var xmlResult = Serializer(payload);
        return Results.Content(xmlResult, "application/xml");
    }
    else
    {
        return Results.Ok(payload);
    }

}).DisableAntiforgery();

app.MapDelete("/erase/{length:int}", ([FromRoute] int length, [FromForm] string text, [FromHeader] bool xml = false) =>
{
    if (length <= 0)
    {
        return Results.BadRequest(new { error = "Error de validación: 'length' debe ser mayor que cero." });
    }
    else if (text.Length <= 0)
    {
        return Results.BadRequest(new { error = "Error de validación: 'text' no puede estar vacío." });
    }

    //Lista de Palabras
    var wordsList = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

    // Elimina las palabras cuya longitud sea 'length'
    wordsList.RemoveAll(w => w.Length == length);

    //Reconstruye la oración
    string newSentence = string.Join(" ", wordsList);

    var payload = new Result { Ori = text, New = newSentence };
    
    //Construye la respuesta
    if (xml)
    {
        var xmlResult = Serializer(payload);
        return Results.Content(xmlResult, "application/xml");
    }
    else
    {
        return Results.Ok(payload);
    }

}).DisableAntiforgery();

app.Run();

static string Serializer<T>(T obj)
{
    var xmlSerializer = new XmlSerializer(typeof(T));
    using var stringWriter = new StringWriter();
    xmlSerializer.Serialize(stringWriter, obj);
    return stringWriter.ToString();
}

public record Result
{
    public string Ori { get; set; } = "";
    public string New { get; set; } = "";
}
