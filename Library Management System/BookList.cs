using System;
using System.Collections.Generic;
using System.IO;

namespace Library_Management_System
{
    public class BookList
    {
        List<Book> _bookList = new List<Book>();

        public void Add()
        {
            string path = @"D:\Dev\School\Library Management System\MyTest.txt";

            Console.Write("Enter book's title: ");
            string bookName = Console.ReadLine();
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            Console.Write("Enter subject: ");
            string subject = Console.ReadLine();
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            _bookList.Add(new Book(bookName, author, subject, date));

            string test =
                $"{_bookList[0].getName()},{_bookList[0].getAuthor()},{_bookList[0].getCategory()},{_bookList[0].getDate()}";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(test);
            }

            _bookList = new List<Book>();
        }

        public void Show()
        {
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\MyTest.txt");
                Console.WriteLine(
                    "------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Date",-20}|");
                Console.WriteLine(
                    "------------------------------------------------------------------------------------------------------------------------------------------------------");

                for (int i = 0; i < data.Length; i++)
                {
                    string[] output = data[i].Split(',');
                    Console.WriteLine(
                        $"|{i + 1,-4}|{output[0],-60}|{output[1],-40}|{output[2],-20}|{output[3],-20}|");
                    Console.WriteLine(
                        "------------------------------------------------------------------------------------------------------------------------------------------------------");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("There is no data");
            }
        }
    }
}