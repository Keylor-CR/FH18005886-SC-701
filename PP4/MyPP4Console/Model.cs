using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace MyPP4Console;
public class BooksContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TitleTag> TitlesTags  { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var folder = Environment.CurrentDirectory;
        var path = Path.Join(folder, "data", "books.db");
        optionsBuilder.UseSqlite($"Data Source={path}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitleTag>().ToTable("TitlesTags");

        modelBuilder.Entity<Title>((entityTitle) =>
        {
            entityTitle.Property(prop => prop.TitleId).HasColumnOrder(0);
            entityTitle.Property(prop => prop.AuthorId).HasColumnOrder(1);
            entityTitle.Property(prop => prop.TitleName).HasColumnOrder(2);
        });
    }
}


public class Author
{
    public int AuthorId { get; set; }
    [NotNull]
    public string AuthorName { get; set; }
    [NotNull]
    public List<Title> Titles { get; } = [];
}

public class Title
{
    [Column(Order = 1)]
    public int TitleId { get; set; }
    [Column(Order = 3)]
    [NotNull]
    public string TitleName { get; set; }
    [Column(Order = 2)]
    public int AuthorId { get; set; }
    [NotNull]
    public Author Author { get; set; }
    [NotNull]
    public List<TitleTag> TitleTags { get; } = [];

}

public class Tag
{
    public int TagId { get; set; }
    [NotNull]
    public string TagName { get; set; }
    [NotNull]
    public List<TitleTag> TitleTags { get; } = [];
}

public class TitleTag
{
    public int TitleTagId { get; set; }
    public int TitleId { get; set; }
    [NotNull]
    public Title Title { get; set; }
    public int TagId { get; set; }
    [NotNull]
    public Tag Tag { get; set; }
}