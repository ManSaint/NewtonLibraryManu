using Helpers;
using NewtonLibraryManu.Data;
using NewtonLibraryManu.Models;
using Microsoft.EntityFrameworkCore;

namespace NewtonLibraryManu;

internal class Program
{
    static void Main(string[] args)
    {
        DataAccess dataAccess = new DataAccess();
        //dataAccess.Seed();

        do
        {
            Console.Clear();

            MainMenu();

            bool continuue = csConsoleInput.TryReadInt32("Make a choice", 1, 6, out int menuSel);

            if (continuue == false)
                break;

            MenuSelect(menuSel);

        } while (true);
    }

    public static void MainMenu()
    {
        Console.WriteLine("1 - Add an author");
        Console.WriteLine("2 - Add a book");
        Console.WriteLine("3 - Add a loan card");
        Console.WriteLine("4 - Loan a book");
        Console.WriteLine("5 - Return a book");
        Console.WriteLine("6 - Remove");
        Console.WriteLine("Q - Quit program\n");
    }

    private static void MenuSelect(int menuSel)
    {
        switch (menuSel)
        {
            case 1:
                AddAuthor();
                break;

            case 2:
                AddBookDetails();
                break;

            case 3:
                AddLoanCard();
                break;

            case 4:
                LoanBook();
                break;

            case 5:
                ReturnBook();
                break;

            case 6:
                RemoveMenu();
                break;
        }
    }

    public static void AddAuthor()
    {
        using (Context context = new Context())
        {
            Console.Clear();

            Author author = new Author();

            Console.WriteLine("-* Add an author *-\n");
            Console.Write("First Name: ");
            author.FirstName = Console.ReadLine();
            Console.Write("Last Name: ");
            author.LastName = Console.ReadLine();

            context.Add(author);
            var hej = context.SaveChanges(); //

            Console.WriteLine();
            if (author.Id > 0)
            {
                Console.WriteLine($"You added \'{author.FirstName} {author.LastName}\' to authors");
            }
            else
            {
                Console.WriteLine("Naaah son");
            }
            Console.Write("\nPress any key to continue");
            Console.ReadKey();
        }
    }

    public static void AddBookDetails()
    {
        using (Context context = new Context())
        {
            Console.Clear();

            BookDetail bookDetails = new BookDetail();

            Console.WriteLine("-* Add a book *-\n");
            Console.Write("Title: ");
            bookDetails.Title = Console.ReadLine();

            Console.Write("Published: ");
            bookDetails.Published = Console.ReadLine().GetInt("Published");

            Console.Write("ISBN: ");
            bookDetails.ISBN = Console.ReadLine();

            Console.Write("Rating: ");
            bookDetails.Rating = Console.ReadLine().GetInt("Rating");

            Console.Write("How many copies: ");
            int copies = Console.ReadLine().GetInt("Copies");

            for (int i = 0; i < copies; i++)
            {
                Book bookCopy = new Book();
                bookCopy.IsAvailable = true;
                bookDetails.Books.Add(bookCopy);
            }

            var authors = context.Author.ToList();
            List<int> intId = new List<int>();

            Console.WriteLine("\n\n-* Authors *-\n");
            foreach (var item in authors)
            {
                Console.WriteLine($"{item.Id} - {item.FirstName} {item.LastName}");
                intId.Add(item.Id);
            }

            while (true)
            {
                Console.Write("\nChoose a number to connect the book to an auhtor (0 to exit): ");
                int authorId = Console.ReadLine().GetInt("Choose a number").GetRequiredNumbers("Choose a number (0 to exit)", intId);

                if (authorId == 0)
                    break;

                var authauth = authors.FirstOrDefault(a => a.Id == authorId);

                bookDetails.Authors.Add(authauth);

                intId.Remove(authorId);
            }

            context.BookDetails.Add(bookDetails);
            context.SaveChanges();
        }
    }

    public static void AddLoanCard()
    {
        using (Context context = new Context())
        {
            Console.Clear();

            LoanCard customer = new LoanCard();

            Console.WriteLine("-* Add an loan card *-\n");
            Console.Write("First Name: ");
            customer.FirstName = Console.ReadLine();
            Console.Write("Last Name: ");
            customer.LastName = Console.ReadLine();
            Console.Write("Pin code: ");
            customer.LoanCardPin = Console.ReadLine().GetInt("Pin code");

            context.Add(customer);
            var hej = context.SaveChanges(); //

            Console.WriteLine();
            if (customer.Id > 0)
            {
                Console.WriteLine($"You added \'{customer.FirstName} {customer.LastName}\' to loan card");
            }
            else
            {
                Console.WriteLine("Naaah son");
            }
            Console.Write("\nPress any key to continue");
            Console.ReadKey();
        }
    }

    public static void LoanBook()
    {
        using (Context context = new Context())
        {
            Console.Clear();

            var loanCard = new LoanCard();

            while (true)
            {
                Console.Write("Enter loan card pin: ");
                var pin = Console.ReadLine().GetInt("Enter pin");

                loanCard = context.LoanCards.FirstOrDefault(l => l.LoanCardPin == pin);

                if (loanCard == null)
                {
                    Console.WriteLine("Try again\n");
                }
                else
                    break;
            }

            Console.Clear();
            Console.WriteLine($"Welcome {loanCard.FirstName} {loanCard.LastName}");
            Console.WriteLine("\n-* Books *-\n");

            var books = context.BookDetails.Include(b => b.Books).ToList();

            foreach (var item in books)
            {
                int amountAvailable = item.Books.Count(c => c.IsAvailable == true);

                Console.WriteLine($"{item.Title,-43} - ISBN: {item.ISBN,-10}      - Copies available: {amountAvailable}");
            }

            var bookDetail = new BookDetail();
            while (true)
            {
                Console.Write("\nEnter ISBN of desired book (0 to exit): ");
                string strIsbn = Console.ReadLine();

                bookDetail = books.FirstOrDefault(b => b.ISBN == strIsbn);

                if (bookDetail != null)
                    break;

                Console.WriteLine("ISBN does not exist, try again");
            }

            var copyOfBook = bookDetail.Books.FirstOrDefault(b => b.IsAvailable == true);

            if (copyOfBook == null)
            {
                Console.WriteLine("0 copies of this book in library");
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
            }

            if (copyOfBook != null)
            {
                copyOfBook.IsAvailable = false;

                LoanDetail bookLoan = new LoanDetail();
                bookLoan.LoanCard = loanCard;
                bookLoan.Book = copyOfBook;

                context.LoanDetails.Add(bookLoan);
                context.SaveChanges();

                Console.WriteLine($"\nYou loaned a copy of {bookDetail.Title}");
                Console.Write("\nPress any key to continue");
                Console.ReadKey();
            }
        }
    }

    public static void ReturnBook()
    {
        using (Context context = new Context())
        {
            Console.Clear();

            var loanCard = new LoanCard();

            while (true)
            {
                Console.Write("Enter loan card pin: ");
                var pin = Console.ReadLine().GetInt("Enter pin");

                loanCard = context.LoanCards
                    .Include(lc => lc.LoanDetails)
                    .ThenInclude(ld => ld.Book)
                    .ThenInclude(b => b.Details)
                    .FirstOrDefault(l => l.LoanCardPin == pin);

                if (loanCard == null)
                {
                    Console.WriteLine("\nTry again");
                }
                else
                    break;
            }

            Console.Clear();
            Console.WriteLine($"Welcome {loanCard.FirstName} {loanCard.LastName}");
            Console.WriteLine("\n-* Loaned books *-\n");

            var loanedBooks = loanCard.LoanDetails
                .Where(ld => ld.Returned == null) // Filter out returned books
                .Select(ld => ld.Book)
                .ToList();

            foreach (var item in loanedBooks)
            {
                Console.WriteLine($"{item.Id} - {item.Details.Title}");
            }

            Console.Write("\nChoose a book to return by entering it's ID: ");
            var bookIdToReturn = Console.ReadLine().GetInt("Enter book ID");

            var bookToReturn = context.Books
                .Include(b => b.Details)
                .FirstOrDefault(b => b.Id == bookIdToReturn);

            if (bookToReturn != null)
            {
                var loanDetailToReturn = context.LoanDetails
                    .FirstOrDefault(ld => ld.BookId == bookIdToReturn && ld.LoanCardId == loanCard.Id);

                if (loanDetailToReturn != null)
                {
                    loanDetailToReturn.Returned = DateTime.Now;
                    context.SaveChanges();

                    Console.WriteLine($"\nBook '{bookToReturn.Details.Title}' has been returned.");

                    loanedBooks = loanCard.LoanDetails
                        .Where(ld => ld.Returned == null)
                        .Select(ld => ld.Book)
                        .ToList();
                }
                else
                {
                    Console.WriteLine("Invalid book ID or the book has already been returned.");
                }
            }
            else
            {
                Console.WriteLine("Invalid book ID.");
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }

    public static void RemoveMenu()
    {
        do
        {
            Console.Clear();

            Console.WriteLine("-* What do you want to remove? *-\n");
            Console.WriteLine("1 - Remove author");
            Console.WriteLine("2 - Remove book");
            Console.WriteLine("3 - Remove loan card\n");

            bool continuue = csConsoleInput.TryReadInt32("Make a choice", 1, 3, out int menuSel);

            if (continuue == false)
                break;

            switch (menuSel)
            {
                case 1:
                    RemoveAuthor();
                    break;

                case 2:
                    RemoveBook();
                    break;

                case 3:
                    RemoveLoanCard();
                    break;
            }
        } while (true);
    }

    public static void RemoveAuthor()
    {
        using (Context context = new Context())
        {
            Console.Clear();

            var authors = context.Author.ToList();

            Console.WriteLine("-* Remove author *-\n");

            List<int> intId = new List<int>();

            foreach (var item in authors)
            {
                Console.WriteLine($"{item.Id} - {item.FirstName} {item.LastName}");
                intId.Add(item.Id);
            }

            while (true)
            {
                Console.Write("\nChoose a number to remove an author (0 to exit): ");
                int authorId = Console.ReadLine().GetInt("Choose a number").GetRequiredNumbers("Choose a number (0 to exit)", intId);

                if (authorId == 0)
                    break;

                var authorToRemove = authors.FirstOrDefault(a => a.Id == authorId);

                if (authorToRemove != null)
                {
                    context.Author.Remove(authorToRemove);
                    context.SaveChanges();
                    Console.WriteLine($"\nAuthor {authorToRemove.FirstName} {authorToRemove.LastName} was removed");
                }
                else
                {
                    Console.WriteLine("Author not found. Please choose a valid author number");
                }
            }
        }
    }

    public static void RemoveBook()
    {
        using (Context context = new Context())
        {
            Console.Clear();

            var books = context.Books.Include(b => b.Details).ToList();

            Console.WriteLine("-* Remove book *-\n");

            List<int> intId = new List<int>();

            foreach (var item in books)
            {
                Console.WriteLine($"{item.Id} - {item.Details.Title}");
                intId.Add(item.Id);
            }

            while (true)
            {
                Console.Write("\nChoose a number to remove a book (0 to exit): ");
                int ookId = Console.ReadLine().GetInt("Choose a number").GetRequiredNumbers("Choose a number (0 to exit)", intId);

                if (ookId == 0)
                    break;

                var bookToRemove = books.FirstOrDefault(a => a.Id == ookId);

                if (bookToRemove != null)
                {
                    context.Books.Remove(bookToRemove);
                    context.SaveChanges();
                    Console.WriteLine($"\nBook {bookToRemove.Details.Title} was removed");
                }
                else
                {
                    Console.WriteLine("Book not found. Please choose a valid book number");
                }
            }
        }
    }

    public static void RemoveLoanCard()
    {
        using (Context context = new Context())
        {
            Console.Clear();

            var loanCards = context.LoanCards.ToList();

            Console.WriteLine("-* Remove loan card *-\n");

            List<int> intId = new List<int>();

            foreach (var item in loanCards)
            {
                Console.WriteLine($"{item.Id} - {item.FirstName} {item.LastName}");
                intId.Add(item.Id);
            }

            while (true)
            {
                Console.Write("\nChoose a number to remove an loan card (0 to exit): ");
                int loanCardId = Console.ReadLine().GetInt("Choose a number").GetRequiredNumbers("Choose a number (0 to exit)", intId);

                if (loanCardId == 0)
                    break;

                var loanCardToRemove = loanCards.FirstOrDefault(a => a.Id == loanCardId);

                if (loanCardToRemove != null)
                {
                    context.LoanCards.Remove(loanCardToRemove);
                    context.SaveChanges();
                    Console.WriteLine($"\nLoan card {loanCardToRemove.FirstName} {loanCardToRemove.LastName} was removed");
                }
                else
                {
                    Console.WriteLine("Loan card not found. Please choose a valid author number");
                }
            }
        }
    }
}