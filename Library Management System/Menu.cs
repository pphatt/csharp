using System;

namespace Library_Management_System
{
    public class Menu
    {
        BookList book = new BookList();

        public Menu()
        {
            while (true)
            {
                Console.WriteLine("\n\t\t\t\t\t\t         LIBRARY MANAGEMENT SYSTEM    \t\t\t\t\t\n");
                Console.WriteLine("\t\t\t\t\t\t****************** MENU ******************\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 1. ADD NEW BOOK                      **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 2. SEARCH BOOK                       **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 3. DELETE BOOK                       **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 4. EDIT BOOK INFORMATION             **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 5. SHOW ALL BOOKS                    **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 6. ADD NEW CUSTOMER                  **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 7. SEARCH CUSTOMER                   **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 8. DELETE CUSTOMER                   **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 9. EXIT                              **\t\t\t\t\t");
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
                    // book.Show();
                    // Console.ReadKey();
                } else if (number == 5)
                {
                    book.Show();
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