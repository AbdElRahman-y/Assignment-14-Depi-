using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(l => new { l.BookId, l.BorrowerId });

        builder.HasOne(l => l.Book)
               .WithMany(b => b.Loans)
               .HasForeignKey(l => l.BookId);

        builder.HasOne(l => l.Borrower)
               .WithMany(b => b.Loans)
               .HasForeignKey(l => l.BorrowerId);

        builder.Property(l => l.LoanDate).HasColumnType("date");
        builder.Property(l => l.ReturnDate).HasColumnType("date");
    }
}
