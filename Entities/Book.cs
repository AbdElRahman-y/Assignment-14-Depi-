namespace LibrarySystem.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;

    // Many-to-One: Book -> Author
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;

    // Many-to-Many: Book <-> Borrower (through Loan)
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    public ICollection<Borrower> Borrowers { get; set; } = new List<Borrower>();
}
