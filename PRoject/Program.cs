using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;
using static Card;


public class Card
{
    

    public Card()
    {
        
    }
    List<Book> cardtoCustomer = new List<Book>();
    // Method to print the invoice to a file
    public void PrintInvoice()
    {
        string fileName = "Invoice.txt"; // File where the invoice will be saved

        try
        {
            using (StreamWriter writer = new StreamWriter(fileName, append: true)) // Overwrite if file exists
            {
                writer.WriteLine("Invoice:");
                writer.WriteLine("---------------------------");

                foreach (var item in cardtoCustomer)
                {
                    writer.WriteLine($"Item Title: {item.title}");
                    writer.WriteLine($"Price: {item.Price:C}");

                    // Check if the item is a Book and print author if it is
                    if (item is Book book)
                    {
                        writer.WriteLine($"Author: {book.author}");
                    }

                    writer.WriteLine("---------------------------");
                }

                writer.WriteLine($"Date: {DateTime.Now}");
            }

            Console.WriteLine($"Invoice successfully saved to {fileName}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving the invoice: {ex.Message}");
        }
    }
        public void AddToCard(Book book)
    {

        cardtoCustomer.Add(book);

    }

}

public class item
{
    public string title { set; get; }
    public decimal Price { get; set; }
    public item(string title, decimal price)
    {
        title = title;
        Price = price;
    }

    // Virtual method to allow overriding in derived classes
    public virtual void PrintDetails(StreamWriter writer)
    {
        writer.WriteLine($"Title: {title}");
        writer.WriteLine($"Price: {Price:C}");
    }
}

public class Book:item
{
   
    public string author { set; get; }
   
    private bool IsBorrowed;
    public Book(string title, string author, bool isborrowed, decimal price) : base(title, price)
    {
        this.author = author;
        this.title = title;
        IsBor = isborrowed;
    }
    public bool IsBor
    {
        set => IsBorrowed = value;
        get => IsBorrowed;
    }

}
public class Library:Card
{

    List<Book> listOfBooks = new List<Book>();
   

    public void CheckOut()
    {
        PrintInvoice();
    }
    public void CheckIfBookExists(String title)
    {
        var book = listOfBooks.FirstOrDefault(b => b.title.Equals(title, StringComparison.OrdinalIgnoreCase));
        AddToCard(book);
    }
    public void addBook(Book book)
    {
        listOfBooks.Add(book);
        Console.WriteLine("Book Added Successfully");

    }

    public void BorrowBook(string title)
    {

        var findBook = listOfBooks.FirstOrDefault(b => b.title.Contains(title, StringComparison.OrdinalIgnoreCase) && !b.IsBor);
        if (findBook == null)
        {
            Console.WriteLine("Error404 , the book title was not found in our system");
        }
        else
        {
            findBook.IsBor = true;
            Console.WriteLine("Error200, the book which have title of "+findBook.IsBor+" for the author "+findBook.author+" will be added to ur card<3");

        }

    }

    public void ReturnBook(string title)
    {
        var findBook = listOfBooks.FirstOrDefault(b => b.title.Contains(title, StringComparison.OrdinalIgnoreCase) && b.IsBor);
        if (findBook == null)
        {
            Console.WriteLine("you are wrong , there is no book with this title found");
        }
        else
        {
            findBook.IsBor = false;
            Console.WriteLine("thanks for shoping at FatayriLib");
        }
    }

    public List<Book> SearchByTitle(string title)
    {
        return listOfBooks.Where(b => b.title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();


    }

    public List<Book> SearchByAuthor(string author)
    {
        return listOfBooks.Where(b => b.author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public void DisplayAvailableBooks()
    {

        var availableBooks = listOfBooks.Where(b => !b.IsBor).ToList();

        if (availableBooks.Count == 0)
        {
            Console.WriteLine("Sorry we have no boooks availbe at the moment ");

        }else
        {
            int a = 1;
            foreach (Book b in availableBooks)
            {
                Console.WriteLine(a + "-" + b.title + ", " + b.author+" price : "+b.Price);
                a++;
            }
        }
    }
}

public class LinkedListProgram
{
    public static void Main(string[] args)
    {

        Book book2 = new Book("The Catcher in the Rye", "J.D. Salinger", false,44.4m);

        // Create a card for the book and print the invoice
        Console.WriteLine("WelCome TO FatayriLib\n" + "PLease fell free to contact us at 79126133");
        Library library = new Library();
        bool running = true;
        while (running)
        {
            Console.WriteLine("1. Add Book\n2. Borrow Book\n3. Return Book\n4. Search Books by Title\n5. Search Books by Author\n6. Display Available Books\n7.buy book\n8.Go to checkout\n9. Exit");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.Write("Enter book title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter book author: ");
                    string author = Console.ReadLine();

                    Console.Write("Enter price: ");
                    decimal price =  Convert.ToDecimal(Console.ReadLine());

                    library.addBook(new Book(title, author,false, price));
                    break;
                case "2":
                    Console.Write("Enter the title of the book to borrow: ");
                    title = Console.ReadLine();
                    library.BorrowBook(title);
                    break;
                case "3":
                    Console.Write("Enter the title of the book to borrow: ");
                    title = Console.ReadLine();
                    library.ReturnBook(title);
                    break;


                case "4":
                    Console.Write("Enter book title to search: ");
                    title = Console.ReadLine();
                    var booksByTitle = library.SearchByTitle(title);
                    if (booksByTitle.Any())
                    {
                        Console.WriteLine("Books found:");
                        foreach (var book in booksByTitle)
                        {
                            Console.WriteLine($"Title: {book.title}, Author: {book.author}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No books found with the given title.");
                    }
                    break;
                   case "5":
                    Console.Write("Enter author to search: ");
                    author = Console.ReadLine();
                    var booksByAuthor = library.SearchByAuthor(author);
                    if (booksByAuthor.Any())
                    {
                        Console.WriteLine("Books found:");
                        foreach (var book in booksByAuthor)
                        {
                            Console.WriteLine($"Title: {book.title}, Author: {book.author}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No books found by the given author.");
                    }
                    break;

                case "6":
                    library.DisplayAvailableBooks();
                    break;

                case "7":
                    Console.Write("Enter the title of the book to buy: ");
                    title = Console.ReadLine();
                    library.CheckIfBookExists(title);
                    break;

                case "8":

                    library.CheckOut();
                    break;

                case "9":
                    running = false;
                    break;
            }


        }



    }
}
