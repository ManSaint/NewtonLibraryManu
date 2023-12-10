using Microsoft.EntityFrameworkCore;
using NewtonLibraryManu.Models;

namespace NewtonLibraryManu.Data;

internal class Context : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<LoanCard> LoanCards { get; set; }
    public DbSet<LoanDetail> LoanDetails { get; set; }
    public DbSet<BookDetail> BookDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"
                    Server=localhost; 
                    Database=NewtonLibraryManu; 
                    Trusted_Connection=True; 
                    Trust Server Certificate=Yes; 
                    User Id=NewtonLibrary; 
                    password=NewtonLibrary");
    }
}
