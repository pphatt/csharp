using System;
using System.Collections.Generic;
using System.IO;

namespace Library_Management_System
{
    public class BookList
    {
        List<Book> _bookList = new List<Book>();

        private void CustomOutput(string option, string[] data, string name, int index)
        {
            int j = 0;
            bool check = false;
            Console.Write($"Input {name} to search: ");
            option = Console.ReadLine();

            for (int i = 0; i < data.Length; i++)
            {
                string[] output1 = data[i].Split(',');
                if (output1[index].Contains(option))
                {
                    check = true;
                    break;
                }
            }

            if (check == false)
            {
                Console.WriteLine($"\t\t\t\t\t     There is no {option} in {name} section in the database\t\t\t\t\t\t");
                return;
            }

            Console.WriteLine(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Status",-10}|{"Date",-20}|");
            Console.WriteLine(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");

            for (int i = 0; i < data.Length; i++)
            {
                string[] output1 = data[i].Split(',');

                string statusOutput = "AVAILABLE";
                if (output1[3] != "False")
                {
                    statusOutput = "BORROWED";
                }

                if (output1[index].Contains(option))
                {
                    Console.WriteLine(
                        $"|{j + 1,-4}|{output1[0],-60}|{output1[1],-40}|{output1[2],-20}|{statusOutput,-10}|{output1[4],-20}|");
                    Console.WriteLine(
                        "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    j++;
                }
            }
        }

        public void Add()
        {
            string path = @"D:\Dev\School\Library Management System\MyTest.txt";

            Console.Write("Enter book's title: ");
            string bookName = Console.ReadLine();
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            Console.Write("Enter subject: ");
            string subject = Console.ReadLine();
            bool status = false;
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            _bookList.Add(new Book(bookName, author, subject, date));

            string output =
                $"{_bookList[0].getName()},{_bookList[0].getAuthor()},{_bookList[0].getCategory()},{status},{_bookList[0].getDate()}";

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(output);
            }

            _bookList = new List<Book>();
        }

        public void Show()
        {
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\MyTest.txt");
                if (data.Length == 0)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

                Console.WriteLine(
                    "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(
                    $"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Status",-10}|{"Date",-20}|");
                Console.WriteLine(
                    "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");

                for (int i = 0; i < data.Length; i++)
                {
                    string[] output = data[i].Split(',');
                    string statusOutput = "AVAILABLE";
                    if (output[3] != "False")
                    {
                        statusOutput = "BORROWED";
                    }

                    Console.WriteLine(
                        $"|{i + 1,-4}|{output[0],-60}|{output[1],-40}|{output[2],-20}|{statusOutput,-10}|{output[4],-20}|");
                    Console.WriteLine(
                        "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("There are no data currently");
            }
        }

        public void Search()
        {
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\MyTest.txt");
                if (data.Length == 0)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

                Console.WriteLine("\t\t\t\t\t\t****************** MENU ******************\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 1. SEARCH BY ID                      **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 2. SEARCH BY TITLE                   **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 3. SEARCH BY AUTHOR                  **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 4. SEARCH BY CATEGORY                **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 5. SEARCH BY STATUS                  **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 6. SEARCH BY DATE                    **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 7. EXIT                              **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t******************************************\t\t\t\t\t");
                Console.Write("Input to use: ");
                int number1 = int.Parse(Console.ReadLine());

                switch (number1)
                {
                    case 1:
                        Console.Write("Input ID to search: ");
                        int number2 = int.Parse(Console.ReadLine());

                        if (number2 < 0 || number2 > data.Length)
                        {
                            Console.WriteLine("Invalid ID");
                            return;
                        }

                        string[] output = data[number2 - 1].Split(',');

                        string statusOutput = "AVAILABLE";
                        if (output[3] != "False")
                        {
                            statusOutput = "BORROWED";
                        }

                        Console.WriteLine(
                            "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine(
                            $"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Status",-10}|{"Date",-20}|");
                        Console.WriteLine(
                            "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine(
                            $"|{number2,-4}|{output[0],-60}|{output[1],-40}|{output[2],-20}|{statusOutput,-10}|{output[4],-20}|");
                        Console.WriteLine(
                            "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        break;
                    case 2:
                        CustomOutput("title", data, "title", 0);
                        break;
                    case 3:
                        CustomOutput("author", data, "author", 1);
                        break;
                    case 4:
                        CustomOutput("category", data, "category", 2);
                        break;
                    case 5:
                        Console.WriteLine("\t\t\t\t\t\t****************** MENU ******************\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 1. AVAILABLE                         **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 2. BORROWED                          **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t******************************************\t\t\t\t\t");
                        Console.Write("Input to use: ");
                        int number3 = int.Parse(Console.ReadLine());
                        if (number3 == 1)
                        {
                            if (data.Length == 0)
                            {
                                Console.WriteLine("There are no data currently");
                                return;
                            }

                            Console.WriteLine(
                                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine(
                                $"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Status",-10}|{"Date",-20}|");
                            Console.WriteLine(
                                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            for (int i = 0; i < data.Length; i++)
                            {
                                string[] output1 = data[i].Split(',');
                                if (output1[3] == "False")
                                {
                                    Console.WriteLine(
                                        $"|{i + 1,-4}|{output1[0],-60}|{output1[1],-40}|{output1[2],-20}|{"AVAILABLE",-10}|{output1[4],-20}|");
                                    Console.WriteLine(
                                        "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                }
                            }
                        }
                        else if (number3 == 2)
                        {
                            if (data.Length == 0)
                            {
                                Console.WriteLine("There are no data currently");
                                return;
                            }

                            Console.WriteLine(
                                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine(
                                $"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Status",-10}|{"Date",-20}|");
                            Console.WriteLine(
                                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            for (int i = 0; i < data.Length; i++)
                            {
                                string[] output1 = data[i].Split(',');
                                if (output1[3] == "True")
                                {
                                    Console.WriteLine(
                                        $"|{i + 1,-4}|{output1[0],-60}|{output1[1],-40}|{output1[2],-20}|{"BORROWED",-10}|{output1[4],-20}|");
                                    Console.WriteLine(
                                        "-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid number");
                        }

                        break;
                    case 6:
                        CustomOutput("date", data, "date", 4);
                        break;
                    case 7:
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
        }

        public void Delete()
        {
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\MyTest.txt");
                if (data.Length == 0)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

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

                        if (number2 < 0 || number2 > data.Length)
                        {
                            Console.WriteLine("Invalid ID");
                            return;
                        }

                        List<string> output = new List<string>(data);
                        output.RemoveAt(number2 - 1);
                        File.WriteAllLines(@"D:\Dev\School\Library Management System\MyTest.txt", output.ToArray());
                        Console.WriteLine("\t\t\t\t\t\t*********** DELETE SUCCESSFULLY **********\t\t\t\t\t");

                        break;
                    case 2:
                        bool check = false;
                        Console.Write("Input Name to delete: ");
                        string name = Console.ReadLine();
                        List<string> output1 = new List<string>(data);

                        for (int i = 0; i < output1.Count; i++)
                        {
                            string[] output2 = output1[i].Split(',');
                            if (output2[0] == name)
                            {
                                output1.RemoveAt(i);
                                i--;
                                check = true;
                            }
                        }

                        if (check == false)
                        {
                            Console.WriteLine($"There is no {name} in Name section in the database");
                            return;
                        }

                        File.WriteAllLines(@"D:\Dev\School\Library Management System\MyTest.txt", output1.ToArray());
                        Console.WriteLine("\t\t\t\t\t\t*********** DELETE SUCCESSFULLY **********\t\t\t\t\t");

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
        }

        public void Edit()
        {
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\MyTest.txt");
                if (data.Length == 0)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

                Show();
                Console.Write("Input to use: ");
                int number1 = int.Parse(Console.ReadLine());
                if (number1 > 0 && number1 <= data.Length)
                {
                    Console.WriteLine($"\t\t\t\t\t\t          EDITING THE BOOK NO.{number1:D2} \t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t****************** MENU ******************\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t** 1. EDIT TITLE                        **\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t** 2. EDIT AUTHOR                       **\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t** 3. EDIT CATEGORY                     **\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t** 4. EDIT STATUS                       **\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t******************************************\t\t\t\t\t");
                    Console.Write("Input to edit: ");
                    int number2 = int.Parse(Console.ReadLine());

                    if (number2 > 0 && number2 <= 3)
                    {
                        string[] output = data[number1 - 1].Split(',');
                        Console.Write($"Changing {output[number2 - 1]} to: ");
                        string newText = Console.ReadLine();
                        output[number2 - 1] = newText;
                        data[number1 - 1] = string.Join(",", output);
                        File.WriteAllLines(@"D:\Dev\School\Library Management System\MyTest.txt", data);
                        Console.WriteLine("\t\t\t\t\t\t*********** UPDATE SUCCESSFULLY **********\t\t\t\t\t");
                    }
                    else if (number2 == 4)
                    {
                        string[] output = data[number1 - 1].Split(',');
                        string status = "AVAILABLE";
                        if (output[number2 - 1] != "False")
                        {
                            status = "BORROWED";
                        }

                        Console.Write($"Changing {status} to: ");
                        string newText = Console.ReadLine().ToLower();

                        if (newText == "borrowed" || newText == "taken")
                        {
                            output[number2 - 1] = "True";
                        }
                        else if (newText == "available")
                        {
                            output[number2 - 1] = "False";
                        }

                        data[number1 - 1] = string.Join(",", output);
                        File.WriteAllLines(@"D:\Dev\School\Library Management System\MyTest.txt", data);
                        Console.WriteLine("\t\t\t\t\t\t*********** UPDATE SUCCESSFULLY **********\t\t\t\t\t");
                    }
                    else
                    {
                        Console.WriteLine("Invalid number");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid number");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("There are no data currently");
            }
        }
    }
}