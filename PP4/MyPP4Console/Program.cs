using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPP4Console;
using CsvHelper;
using System.Text;
using System.Globalization;

using var db = new BooksContext();

// Reviso si la DB esta vacía.
bool dbVacia = !db.Titles.Any();
const string DataFolder = "data";
const string CsvFileName = "books.csv";

if (dbVacia)
{
    Console.WriteLine("La base de datos está vacía, por lo que será llenada a partir de los datos del archivo CSV.");
    ImportarDesdeCsv(db, Path.Combine(DataFolder, CsvFileName));

}
else
{
    Console.WriteLine("La base de datos se está leyendo para crear los archivos TSV.");
    //Exportar a TSV
    ExportarTsv(db, DataFolder);
    Console.WriteLine("Procesando... Listo.");

}

static void ImportarDesdeCsv(BooksContext db, string csvPath)
{
    if (!File.Exists(csvPath))
    {
        Console.WriteLine($"No se encontró el archivo CSV: {csvPath}");
        return;
    }

    // Caches to avoid repetir consultas a la BD
    var autoresCache = db.Authors
        .ToDictionary(a => a.AuthorName, StringComparer.OrdinalIgnoreCase);

    var tagsCache = db.Tags
        .ToDictionary(t => t.TagName, StringComparer.OrdinalIgnoreCase);

    using var reader = new StreamReader(csvPath);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

    csv.Read();
    csv.ReadHeader(); // Lee la fila de encabezados: Author,Title,Tags

    while (csv.Read())
    {
        var authorName = csv.GetField("Author")?.Trim();
        var titleName = csv.GetField("Title")?.Trim();
        var tagsField = csv.GetField("Tags") ?? string.Empty;

        if (string.IsNullOrWhiteSpace(authorName) ||
            string.IsNullOrWhiteSpace(titleName))
        {
            // Fila inválida, la saltamos
            continue;
        }

        // 1) Autor: buscar o crear
        if (!autoresCache.TryGetValue(authorName, out var author))
        {
            author = new Author { AuthorName = authorName };
            db.Authors.Add(author);
            autoresCache[authorName] = author;
        }

        // 2) Título
        var title = new Title
        {
            TitleName = titleName,
            Author = author
        };
        db.Titles.Add(title);

        // 3) Tags (separados por |)
        var tagNames = tagsField
            .Split('|', StringSplitOptions.RemoveEmptyEntries)
            .Select(t => t.Trim())
            .Where(t => t.Length > 0)
            .Distinct(StringComparer.OrdinalIgnoreCase);

        foreach (var tagName in tagNames)
        {
            // Buscar o crear Tag
            if (!tagsCache.TryGetValue(tagName, out var tag))
            {
                tag = new Tag { TagName = tagName };
                db.Tags.Add(tag);
                tagsCache[tagName] = tag;
            }

            // Relación TitleTag
            var titleTag = new TitleTag
            {
                Title = title,
                Tag = tag
            };
            db.TitlesTags.Add(titleTag);
        }
    }

    db.SaveChanges();
}

static void ExportarTsv(BooksContext db, string folder)
{
    Directory.CreateDirectory(folder);

    // Obtenemos todas las combinaciones Autor–Título–Etiqueta
    var rows = db.TitlesTags
        .Include(tt => tt.Title)
            .ThenInclude(t => t.Author)
        .Include(tt => tt.Tag)
        .AsNoTracking()
        .Select(tt => new
        {
            AuthorName = tt.Title.Author.AuthorName.Trim(),
            TitleName  = tt.Title.TitleName.Trim(),
            TagName    = tt.Tag.TagName.Trim()
        })
        .ToList();

    // Agrupar por primera letra del AuthorName (en mayúsculas)
    var gruposPorLetra = rows
        .Where(r => !string.IsNullOrWhiteSpace(r.AuthorName))
        .GroupBy(r => char.ToUpperInvariant(r.AuthorName[0]));

    foreach (var grupo in gruposPorLetra)
    {
        char letra = grupo.Key;
        string fileName = $"{letra}.tsv";
        string filePath = Path.Combine(folder, fileName);

        // Orden descendente por AuthorName, luego TitleName, luego TagName
        var filasOrdenadas = grupo
            .OrderByDescending(r => r.AuthorName)
            .ThenByDescending(r => r.TitleName)
            .ThenByDescending(r => r.TagName);

        var lineas = new List<string>
        {
            "AuthorName\tTitleName\tTagName" // encabezado
        };

        foreach (var r in filasOrdenadas)
        {
            lineas.Add($"{r.AuthorName}\t{r.TitleName}\t{r.TagName}");
        }

        File.WriteAllLines(filePath, lineas, Encoding.UTF8);
    }
}