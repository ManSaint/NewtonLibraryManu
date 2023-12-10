namespace NewtonLibraryManu.Models;

internal class LoanCard
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int LoanCardPin { get; set; }
    public ICollection<LoanDetail> LoanDetails { get; set; } = new List<LoanDetail>();
}