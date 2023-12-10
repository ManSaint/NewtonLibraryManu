namespace NewtonLibraryManu.Models;

internal class LoanDetail
{
    public int Id { get; set; }
    public int LoanCardId { get; set; }
    public LoanCard LoanCard { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public DateTime? Loaned { get; set; } = DateTime.Now;
    public DateTime? Returned { get; set; }
}