using System;
using System.Collections.Generic;
using System.IO;

namespace Library_Management_System
{
    public class Menu
    {
        BookList book = new BookList();

        private void CustomOutput(string option, string[] data, string name, int index)
        {
            int j = 0;
            bool check = false;
            Console.Write($"Input {name} to search: ");
            option = Console.ReadLine();
            
            for (int i = 0; i < data.Length; i++)
            {
                string[] output1 = data[i].Split(',');
                if (output1[index] == option)
                {
                    check = true;
                }
            }
            
            if (check == false)
            {
                Console.Write($"\t\t\t\t\t\tThere is no {option} in {name} section in the database\t\t\t\t\t\t");
                return;
            }

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

            Console.ReadKey();
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
                        Console.WriteLine("\t\t\t\t\t\t** 6. EXIT                              **\t\t\t\t\t");
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
                                CustomOutput("title", data, "title",0);
                                break;
                            case 3:
                                CustomOutput("author", data, "author",1);
                                break;
                            case 4:
                                CustomOutput("category", data, "category",2);
                                break;
                            case 5:
                                CustomOutput("date", data, "date",3);
                                break;
                            case 6:
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
                } else if (number == 3)
                {
                    string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\MyTest.txt");
                    Console.WriteLine("\t\t\t\t\t\t****************** MENU ******************\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t** 1. DELETE BY ID                      **\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t** 2. DELETE BY NAME                    **\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t******************************************\t\t\t\t\t");
                    Console.Write("Input to use: ");
                    int number1 = int.Parse(Console.ReadLine());

                    switch (number1)
                    {
                        case 1:
                            Console.Write("Input ID to delete: ");
                            int number2 = int.Parse(Console.ReadLine());
                            
                            List<string> output = new List<string>(data);
                            output.RemoveAt(number2 - 1);
                            File.WriteAllLines(@"D:\Dev\School\Library Management System\MyTest.txt", output.ToArray());
                            Console.WriteLine("\t\t\t\t\t\t*********** DELETE SUCCESSFULLY **********\t\t\t\t\t");
                            
                            break;
                        case 2:
                            Console.Write("Input Name to delete: ");
                            string name = Console.ReadLine();
                            List<string> output1 = new List<string>(data);
                            
                            for (int i = 0; i < data.Length; i++)
                            {
                                string[] output_ = data[i].Split(',');
                                if (output_[0] == name)
                                {
                                    output1.RemoveAt(i);
                                    i--;
                                }
                            }
                            File.WriteAllLines(@"D:\Dev\School\Library Management System\MyTest.txt", output1.ToArray());
                            Console.WriteLine("\t\t\t\t\t\t*********** DELETE SUCCESSFULLY **********\t\t\t\t\t");
                            
                            break;
                        default:
                            Console.WriteLine("Invalid number");
                            break;
                    }
                }
                else if (number == 5)
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