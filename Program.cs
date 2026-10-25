using LibrarySystem.Data;
using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem;

internal class Program
{
    static void Main(string[] args)
    {
        using LibraryDbContext context = new LibraryDbContext();

        context.Database.Migrate();

        if (!context.Authors.Any())
            SeedData(context);

        var books = context.Books
            .Include(b => b.Author)
            .Include(b => b.Loans)
                .ThenInclude(l => l.Borrower)
            .ToList();

        foreach (Book book in books)
        {
            Console.WriteLine($"{book.Title} (ISBN {book.ISBN}) by {book.Author.Name}");
            foreach (Loan loan in book.Loans)
            {
                string returned = loan.ReturnDate?.ToString("yyyy-MM-dd") ?? "not returned yet";
                Console.WriteLine($"    borrowed by {loan.Borrower.Name} on {loan.LoanDate:yyyy-MM-dd} (returned: {returned})");
            }
        }

        Console.WriteLine();
        var borrowers = context.Borrowers.Include(b => b.Books).ToList();
        foreach (Borrower borrower in borrowers)
            Console.WriteLine($"{borrower.Name}: {string.Join(", ", borrower.Books.Select(b => b.Title))}");
    }

    static void SeedData(LibraryDbContext context)
    {
        Author a1 = new Author { Name = "Robert Martin", BirthDate = new DateTime(1952, 12, 5) };
        Author a2 = new Author { Name = "Jon Skeet", BirthDate = new DateTime(1976, 4, 1) };

        Book b1 = new Book { Title = "Clean Code", ISBN = "9780132350884", Author = a1 };
        Book b2 = new Book { Title = "Clean Architecture", ISBN = "9780134494166", Author = a1 };
        Book b3 = new Book { Title = "C# in Depth", ISBN = "9781617294532", Author = a2 };

        Borrower r1 = new Borrower { Name = "Ahmed Ali", MembershipDate = new DateTime(2024, 1, 10) };
        Borrower r2 = new Borrower { Name = "Sara Omar", MembershipDate = new DateTime(2025, 3, 5) };

        context.AddRange(a1, a2, b1, b2, b3, r1, r2);

        context.Loans.AddRange(
            new Loan { Book = b1, Borrower = r1, LoanDate = new DateTime(2026, 9, 1), ReturnDate = new DateTime(2026, 9, 15) },
            new Loan { Book = b3, Borrower = r1, LoanDate = new DateTime(2026, 10, 1) },
            new Loan { Book = b1, Borrower = r2, LoanDate = new DateTime(2026, 10, 5) });

        context.SaveChanges();
    }
}
