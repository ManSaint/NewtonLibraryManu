namespace NewtonLibraryManu.Models;

internal class Book
{
    public int Id { get; set; }
    public string DetailsISBN { get; set; }
    public bool IsAvailable { get; set; }
    public BookDetail Details { get; set; }
    public ICollection<LoanDetail> LoanDetails { get; set; } = new List<LoanDetail>();
}