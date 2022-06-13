using System;
using System.IO;

namespace Library_Management_System
{
    public class Menu
    {
        BookList book = new BookList();

        private void CustomOutput(string option, string[] data, int index)
        {
            int j = 0;
            Console.Write($"Input {option} to search: ");
            option = Console.ReadLine();
            Console.WriteLine(
                "------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Date",-20}|");
            Console.WriteLine(
                "------------------------------------------------------------------------------------------------------------------------------------------------------");

            for (int i = 0; i < data.Length; i++)
            {
                string[] output1 = data[i].Split(',');
                if (output1[index] == option)
                {
                    Console.WriteLine(
                        $"|{j + 1,-4}|{output1[0],-60}|{output1[1],-40}|{output1[2],-20}|{output1[3],-20}|");
                    Console.WriteLine(
                        "------------------------------------------------------------------------------------------------------------------------------------------------------");
                    j++;
                }
            }
        }

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
                    try
                    {
                        string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\MyTest.txt");
                        Console.WriteLine("\t\t\t\t\t\t****************** MENU ******************\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 1. SEARCH BY ID                      **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 2. SEARCH BY TITLE                   **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 3. SEARCH BY AUTHOR                  **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 4. SEARCH BY CATEGORY                **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 5. SEARCH BY DATE                    **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t******************************************\t\t\t\t\t");
                        Console.Write("Input to use: ");
                        int number1 = int.Parse(Console.ReadLine());

                        switch (number1)
                        {
                            case 1:
                                Console.Write("Input ID to search: ");
                                int number2 = int.Parse(Console.ReadLine());
                                
                                string[] output = data[number2 - 1].Split(',');
                                Console.WriteLine(
                                    "------------------------------------------------------------------------------------------------------------------------------------------------------");
                                Console.WriteLine($"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Date",-20}|");
                                Console.WriteLine(
                                    "------------------------------------------------------------------------------------------------------------------------------------------------------");
                                Console.WriteLine(
                                    $"|{number2,-4}|{output[0],-60}|{output[1],-40}|{output[2],-20}|{output[3],-20}|");
                                Console.WriteLine(
                                    "------------------------------------------------------------------------------------------------------------------------------------------------------");
                                break;
                            case 2:
                                CustomOutput("title", data, 0);
                                break;
                            case 3:
                                CustomOutput("author", data, 1);
                                break;
                            case 4:
                                CustomOutput("category", data, 2);
                                break;
                            case 5:
                                CustomOutput("date", data, 3);
                                break;
                            default:
                                Console.WriteLine("Invalid number");
                                break;
                        }
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("There are no data currently");
                    }
                } else if (number == 5)
                {
                    book.Show();
                    Console.ReadKey();
                } else
                {
                    break;
                }
            }
        }
    }
}