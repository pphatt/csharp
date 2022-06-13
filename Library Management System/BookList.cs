using System;
using System.Collections.Generic;
using System.IO;

namespace Library_Management_System
{
    public class BookList
    {
        List<Book> bookList = new List<Book>();

        public void Add()
        {
            string path = @"D:\Dev\School\Library Management System\MyTest.txt";
            
            Console.Write("Enter book: ");
            string bookName = Console.ReadLine();
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            Console.Write("Enter subject: ");
            string subject = Console.ReadLine();
            Console.Write("Enter Date: ");
            string date = Console.ReadLine();
            bookList.Add(new Book(bookName, author, subject, date));

            string test = $"{bookList[0].getName()} {bookList[0].getAuthor()} {bookList[0].getCategory()} {bookList[0].getDate()}";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(test);
            }
            bookList = new List<Book>();
        }

        public void Show()
        {
            string[] lines = File.ReadAllLines(@"D:\Dev\School\Library Management System\MyTest.txt");
            Console.WriteLine(
                "--------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"|{"ID", -4}|{"Name", -60}|{"Author", -40}|{"Category", -20}|{"Date", -10}|");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");

            for (int i = 0; i < lines.Length; i++)
            {
                string[] output = lines[i].Split();
                Console.WriteLine(
                    $"|{i + 1, -4}|{output[0], -60}|{output[1], -40}|{output[2], -20}|{output[3], -10}|");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");
            }
        }
    }
}