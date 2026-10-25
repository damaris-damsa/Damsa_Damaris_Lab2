using Microsoft.EntityFrameworkCore;

public class Damsa_Damaris_Lab2Context(DbContextOptions<Damsa_Damaris_Lab2Context> options) : DbContext(options)
{
    public DbSet<Damsa_Damaris_Lab2.Models.Book> Book { get; set; } = default!;

    public DbSet<Damsa_Damaris_Lab2.Models.Category> Category { get; set; } = default!;

    public DbSet<Damsa_Damaris_Lab2.Models.BookCategory> BookCategory { get; set; } = default!;
}
