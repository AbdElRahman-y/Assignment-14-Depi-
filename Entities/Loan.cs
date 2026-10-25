namespace LibrarySystem.Entities;

public class Loan
{
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int BorrowerId { get; set; }
    public Borrower Borrower { get; set; } = null!;

    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
