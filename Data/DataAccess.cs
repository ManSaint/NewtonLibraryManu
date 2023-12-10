using NewtonLibraryManu.Models;

namespace NewtonLibraryManu.Data;

internal class DataAccess
{
    public void Seed()
    {
        using (Context context = new Context())
        {
            Author author1 = new Author() { FirstName = "Dale", LastName = "Carnegie" };
            Author author2 = new Author() { FirstName = "Haruki", LastName = "Muramaki" };
            Author author3 = new Author() { FirstName = "Terry", LastName = "Pratchett" };
            Author author4 = new Author() { FirstName = "Neil", LastName = "Gaiman" };
            Author author5 = new Author() { FirstName = "James", LastName = "Frey" };
            Author author6 = new Author() { FirstName = "Douglas", LastName = "Adams" };

            BookDetail bookDetail1 = new BookDetail();
            bookDetail1.Published = 1936;
            bookDetail1.Title = "How to Win Friends and Influence People";
            bookDetail1.ISBN = "1439167346";
            bookDetail1.Rating = 10;
            bookDetail1.Authors = new List<Author>() { author1 };

            BookDetail bookDetail2 = new BookDetail();
            bookDetail2.Published = 1994;
            bookDetail2.Title = "The Wind-Up Bird";
            bookDetail2.ISBN = "0679775439";
            bookDetail2.Rating = 7;
            bookDetail2.Authors = new List<Author>() { author2 };

            BookDetail bookDetail3 = new BookDetail();
            bookDetail3.Published = 1990;
            bookDetail3.Title = "Good Omens";
            bookDetail3.ISBN = "057504800";
            bookDetail3.Rating = 8;
            bookDetail3.Authors = new List<Author>() { author3, author4 };

            BookDetail bookDetail4 = new BookDetail();
            bookDetail4.Published = 2003;
            bookDetail4.Title = "A Million Little Pieces";
            bookDetail4.ISBN = "0385507755";
            bookDetail4.Rating = 9;
            bookDetail4.Authors = new List<Author>() { author5 };

            BookDetail bookDetail5 = new BookDetail();
            bookDetail5.Published = 1979;
            bookDetail5.Title = "The Hitchhiker's Guide to the Galaxy";
            bookDetail5.ISBN = "0330258648";
            bookDetail5.Rating = 9;
            bookDetail5.Authors = new List<Author>() { author6 };

            LoanCard loanCard1 = new LoanCard();
            loanCard1.FirstName = "Huey";
            loanCard1.LastName = "Freeman";
            loanCard1.LoanCardPin = 0000;

            Book book1 = new Book();
            book1.Details = bookDetail1;
            book1.IsAvailable = true;

            Book book2 = new Book();
            book2.Details = bookDetail2;
            book2.IsAvailable = true;

            Book book3 = new Book();
            book3.Details = bookDetail3;
            book3.IsAvailable = true;

            Book book4 = new Book();
            book4.Details = bookDetail4;
            book4.IsAvailable = true;

            Book book5 = new Book();
            book5.Details = bookDetail5;
            book5.IsAvailable = true;

            context.BookDetails.AddRange(bookDetail1, bookDetail2, bookDetail3, bookDetail4, bookDetail5);
            context.LoanCards.Add(loanCard1);
            context.Books.AddRange(book1, book2, book3, book4, book5);
            context.SaveChanges();
        }
    }
}
