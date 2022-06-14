using System;
using System.Collections.Generic;
using System.IO;

namespace Library_Management_System
{
    public class Menu
    {
        BookList book = new BookList();
        CustomerData customer = new CustomerData();

        public Menu()
        {
            while (true)
            {
                Console.WriteLine("\n\t\t\t\t\t\t         LIBRARY MANAGEMENT SYSTEM    \t\t\t\t\t\n");
                Console.WriteLine("\t\t\t\t\t\t****************** MENU ******************\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 01. ADD NEW BOOK                     **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 02. SEARCH BOOK                      **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 03. DELETE BOOK                      **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 04. EDIT BOOK INFORMATION            **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 05. SHOW ALL BOOKS                   **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 06. ADD NEW CUSTOMER                 **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 07. SEARCH CUSTOMER                  **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 08. DELETE CUSTOMER                  **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 09. EDIT CUSTOMER INFORMATION        **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 10. SHOW ALL CUSTOMERS               **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 11. EXIT                             **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t******************************************\t\t\t\t\t");
                Console.Write("Input to use: ");
                int number = int.Parse(Console.ReadLine());
                if (number == 1)
                {
                    Console.WriteLine("\t\t\t\t\t\t************ ADDING NEW BOOK *************\t\t\t\t\t");
                    book.Add();
                    Console.WriteLine("\t\t\t\t\t\t*********** ADDED SUCCESSFULLY ***********\t\t\t\t\t");
                    Console.ReadKey();
                } else if (number == 2)
                {
                    book.Search();
                    Console.ReadKey();
                } else if (number == 3)
                {
                    book.Delete();
                    Console.ReadKey();
                } else if (number == 4)
                {
                    book.Edit();
                    Console.ReadKey();
                } else if (number == 5)
                {
                    book.Show();
                    Console.ReadKey();
                } else if (number == 6)
                {
                    Console.WriteLine("\t\t\t\t\t\t********** ADDING NEW CUSTOMER ***********\t\t\t\t\t");
                    customer.AddCustomer();
                    Console.WriteLine("\t\t\t\t\t\t*********** ADDED SUCCESSFULLY ***********\t\t\t\t\t");
                    Console.ReadKey();
                } else if (number == 7)
                {
                    customer.SearchCustomer();
                    Console.ReadKey();
                } else if (number == 8)
                {
                    customer.DeleteCustomer();
                    Console.ReadKey();
                }
                else if (number == 10)
                {
                    customer.ShowCustomer();
                    Console.ReadKey();
                }
                else
                {
                    break;
                }
            }
        }
    }
}