using System;
using System.Collections.Generic;
using System.IO;

namespace Library_Management_System
{
    public class Menu
    {
        BookList _book = new BookList();
        CustomerData _customer = new CustomerData();
        LibrarianCalendar _LibrarianCalendar = new LibrarianCalendar();

        public Menu()
        {
            while (true)
            {
                Console.WriteLine("\n\t\t\t\t\t\t         LIBRARY MANAGEMENT SYSTEM    \t\t\t\t\t\n");
                Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 01. ADD NEW BOOK                       ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 02. SEARCH BOOK                        ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 03. DELETE BOOK                        ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 04. EDIT BOOK INFORMATION              ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 05. BORROWED BOOK                      ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 06. SHOW ALL BOOKS                     ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 07. ADD NEW CUSTOMER                   ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 08. SEARCH CUSTOMER                    ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 09. DELETE CUSTOMER                    ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 10. EDIT CUSTOMER INFORMATION          ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 11. RETURN BOOK                        ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 12. SHOW ALL CUSTOMERS                 ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 13. ADD LIBRARIAN                      ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 14. SHOW LIBRARIAN                     ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 15. ADD LIBRARIAN's CALENDAR           ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 16. SHOW LIBRARIAN's CALENDAR          ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 17. ADD BOOK (AUTOMATIC)               ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 18. ADD CUSTOMER (AUTOMATIC)           ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 19. ADD LIBRARIAN (AUTOMATIC)          ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 20. EXIT                               ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
                Console.Write("Input to use: ");
                int number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 1:
                        Console.WriteLine("\t\t\t\t\t\t════════════ ADDING NEW BOOK ═════════════\t\t\t\t\t");
                        _book.Add();
                        Console.ReadKey();
                        break;
                    case 2:
                        _book.Search();
                        Console.ReadKey();
                        break;
                    case 3:
                        _book.Delete();
                        Console.ReadKey();
                        break;
                    case 4:
                        _book.Edit();
                        Console.ReadKey();
                        break;
                    case 5:
                        _book.Borrowed();
                        Console.ReadKey();
                        break;
                    case 6:
                        _book.Show();
                        Console.ReadKey();
                        break;
                    case 7:
                        Console.WriteLine("\t\t\t\t\t\t═══════════ ADDING NEW CUSTOMER ══════════\t\t\t\t\t");
                        _customer.AddCustomer();
                        Console.ReadKey();
                        break;
                    case 8:
                        _customer.SearchCustomer();
                        Console.ReadKey();
                        break;
                    case 9:
                        _customer.DeleteCustomer();
                        Console.ReadKey();
                        break;
                    case 10:
                        _customer.EditCustomer();
                        Console.ReadKey();
                        break;
                    case 11:
                        _customer.ReturnBook();
                        Console.ReadKey();
                        break;
                    case 12:
                        _customer.ShowCustomer();
                        Console.ReadKey();
                        break;
                    case 13:
                        Console.WriteLine("\t\t\t\t\t\t═══════════ ADDING NEW LIBRARIAN ══════════\t\t\t\t\t");
                        _LibrarianCalendar.AddLibrarian();
                        Console.ReadKey();
                        break;
                    case 14:
                        _LibrarianCalendar.ShowLibrarian();
                        Console.ReadKey();
                        break;
                    case 15:
                        _LibrarianCalendar.AddCalendar();
                        Console.ReadKey();
                        break;
                    case 16:
                        _LibrarianCalendar.ShowCalendar();
                        Console.ReadKey();
                        break;
                    case 17:
                        var f = _book.FetchBookData();
                        f.Wait();
                        Console.ReadKey();
                        break;
                    case 18:
                        _customer.GenerateCustomerFakeData();
                        Console.ReadKey();
                        break;
                    default:
                        return;
                }
            }
        }
    }
}