# Library System – EF Core

    dotnet tool install --global dotnet-ef        
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    dotnet run




|---|---|---|
| Author → Book | One-to-Many | `HasOne(Author).WithMany(Books).HasForeignKey(AuthorId)` |
| Book ↔ Borrower | Many-to-Many | من خلال `Loan` (`UsingEntity<Loan>()`) |
| Loan | Join entity | Composite PK (`BookId`, `BorrowerId`) + `LoanDate`, `ReturnDate` |

## ER Diagram

```mermaid
erDiagram
    Author  ||--o{ Book  : writes
    Book    ||--o{ Loan  : "is loaned in"
    Borrower||--o{ Loan  : makes
    Author { int Id PK  string Name  date BirthDate }
    Book { int Id PK  string Title  string ISBN  int AuthorId FK }
    Borrower { int Id PK  string Name  date MembershipDate }
    Loan { int BookId PK_FK  int BorrowerId PK_FK  date LoanDate  date ReturnDate }
```

