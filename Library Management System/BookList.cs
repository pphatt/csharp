using System;
using System.Collections.Generic;

namespace Library_Management_System
{
    public class BookList
    {
        List<Book> bookList = new List<Book>();

        public void Add()
        {
            Console.Write("Enter book: ");
            string bookName = Console.ReadLine();
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            Console.Write("Enter subject: ");
            string subject = Console.ReadLine();
            Console.Write("Enter Date");
            string date = Console.ReadLine();
            bookList.Add(new Book(bookName, author, subject, date));
        }

        public void Show()
        {
            Console.WriteLine(
                "--------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"|{"ID", -4}|{"Name", -60}|{"Author", -40}|{"Category", -20}|{"Date", -10}|");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");

            for (int i = 0; i < bookList.Count; i++)
            {
                Console.WriteLine(
                    $"|{i + 1, -4}|{bookList[i].getName(), -60}|{bookList[i].getAuthor(), -40}|{bookList[i].getCategory(), -20}|{bookList[i].getDate(), -10}|");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            }
        }
    }
}