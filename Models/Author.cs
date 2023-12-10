namespace NewtonLibraryManu.Models;

internal class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<BookDetail> BookDetails { get; set; } = new List<BookDetail>();
}
