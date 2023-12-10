using System.ComponentModel.DataAnnotations;

namespace NewtonLibraryManu.Models;

internal class BookDetail
{
    [Key]
    public string ISBN { get; set; }
    public string Title { get; set; }
    public int Published { get; set; }
    public int Rating { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public ICollection<Author> Authors { get; set; } = new List<Author>();
}