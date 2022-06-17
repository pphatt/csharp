using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Library_Management_System
{
    public class BookList
    {
        List<Book> _bookList = new List<Book>();

        public static string Repeat(string value, int count)
        {
            return new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
        }

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

            Console.WriteLine($"{Repeat("-", 238)}");
            Console.WriteLine(
                $"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Amount",-10}|{"Status",-15}|{"Date",-20}|{"Note",-60}|");
            Console.WriteLine($"{Repeat("-", 238)}");

            for (int i = 0; i < data.Length; i++)
            {
                string[] output = data[i].Split(',');
                
                string[] getAmount = output[4].Split(' ');
                string[] getAmount1 = output[5].Split(' ');

                string statusOutput = $"AVAILABLE ({getAmount[1]})";
                string noteData = "";
                    
                if (int.Parse(getAmount[1]) == 0)
                {
                    statusOutput = $"BORROWED  ({getAmount1[1]})";
                } else if (int.Parse(getAmount1[1]) == 0)
                {
                    statusOutput = $"AVAILABLE ({getAmount[1]})";
                }

                if (int.Parse(getAmount1[1]) > 0)
                {
                    string[] getNote = output[7].Split(' ');
                    noteData = $"Borrowed by customer's IDs {getNote[0]} at {getNote[1]} {getNote[2]}";
                }

                if (output[index].Contains(option))
                {
                    Console.WriteLine(
                        $"|{j + 1,-4}|{output[0],-60}|{output[1],-40}|{output[2],-20}|{output[3],-10}|{statusOutput,-15}|{output[6],-20}|{noteData,-60}|");

                    if (int.Parse(getAmount1[1]) > 0)
                    {
                        statusOutput = "";
                        if (int.Parse(getAmount[1]) > 0)
                        {
                            statusOutput = $"BORROWED  ({int.Parse(output[3]) - int.Parse(getAmount[1])})";
                            if (output.Length == 8)
                            {
                                Console.WriteLine($"{Repeat(" ", 139)}|{statusOutput,-15}|{"",-20}|{"",-60}|");
                            }
                        }

                        for (int k = 8; k <= output.Length - 1; k++)
                        {
                            string[] getNote = output[k].Split(' ');
                            noteData = $"Borrowed by customer's IDs {getNote[0]} at {getNote[1]} {getNote[2]}";
                            Console.WriteLine($"{Repeat(" ", 139)}|{statusOutput,-15}|{"",-20}|{noteData,-60}|");
                        }
                    }
                    Console.WriteLine($"{Repeat("-", 238)}");
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
            Console.Write("Enter amount: ");
            int amount = int.Parse(Console.ReadLine());
            bool status = false;
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            _bookList.Add(new Book(bookName, author, subject, amount, date));

            string output =
                $"{_bookList[0].getName()},{_bookList[0].getAuthor()},{_bookList[0].getCategory()},{_bookList[0].getAmount()},{status} {_bookList[0].getAmount()},{!status} 0,{_bookList[0].getDate()}";

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

                Console.WriteLine($"{Repeat("-", 238)}");
                Console.WriteLine(
                    $"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Amount",-10}|{"Status",-15}|{"Date",-20}|{"Note",-60}|");
                Console.WriteLine($"{Repeat("-", 238)}");

                for (int i = 0; i < data.Length; i++)
                {
                    string[] output = data[i].Split(',');
                    string[] getAmount = output[4].Split(' ');
                    string[] getAmount1 = output[5].Split(' ');

                    string statusOutput = $"AVAILABLE ({getAmount[1]})";
                    string noteData = "";
                    
                    if (int.Parse(getAmount[1]) == 0)
                    {
                        statusOutput = $"BORROWED  ({getAmount1[1]})";
                    } else if (int.Parse(getAmount1[1]) == 0)
                    {
                        statusOutput = $"AVAILABLE ({getAmount[1]})";
                    }

                    if (int.Parse(getAmount1[1]) > 0)
                    {
                        string[] getNote = output[7].Split(' ');
                        noteData = $"Borrowed by customer's IDs {getNote[0]} at {getNote[1]} {getNote[2]}";
                    }

                    Console.WriteLine(
                        $"|{i + 1,-4}|{output[0],-60}|{output[1],-40}|{output[2],-20}|{output[3],-10}|{statusOutput,-15}|{output[6],-20}|{noteData,-60}|");

                    if (int.Parse(getAmount1[1]) > 0)
                    {
                        statusOutput = "";
                        if (int.Parse(getAmount[1]) > 0)
                        {
                            statusOutput = $"BORROWED  ({getAmount1[1]})";
                            if (output.Length == 8)
                            {
                                Console.WriteLine($"{Repeat(" ", 139)}|{statusOutput,-15}|{"",-20}|{"",-60}|");
                            }
                        }

                        for (int j = 8; j <= output.Length - 1; j++)
                        {
                            string[] getNote = output[j].Split(' ');
                            noteData = $"Borrowed by customer's IDs {getNote[0]} at {getNote[1]} {getNote[2]}";
                            Console.WriteLine($"{Repeat(" ", 139)}|{statusOutput,-15}|{"",-20}|{noteData,-60}|");
                            statusOutput = "";
                        }
                    }
                    Console.WriteLine($"{Repeat("-", 238)}");
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

                Console.WriteLine($"{Repeat("\t", 6)}{Repeat("*", 18)} MENU {Repeat("*", 18)}{Repeat("\t", 5)}");
                Console.WriteLine($"{Repeat("\t", 6)}** 1. SEARCH BY ID                      **{Repeat("\t", 6)}");
                Console.WriteLine($"{Repeat("\t", 6)}** 2. SEARCH BY TITLE                   **{Repeat("\t", 6)}");
                Console.WriteLine($"{Repeat("\t", 6)}** 3. SEARCH BY AUTHOR                  **{Repeat("\t", 6)}");
                Console.WriteLine($"{Repeat("\t", 6)}** 4. SEARCH BY CATEGORY                **{Repeat("\t", 6)}");
                Console.WriteLine($"{Repeat("\t", 6)}** 5. SEARCH BY STATUS                  **{Repeat("\t", 6)}");
                Console.WriteLine($"{Repeat("\t", 6)}** 6. SEARCH BY DATE                    **{Repeat("\t", 6)}");
                Console.WriteLine($"{Repeat("\t", 6)}** 7. EXIT                              **{Repeat("\t", 6)}");
                Console.WriteLine($"{Repeat("\t", 6)}{Repeat("*", 42)}{Repeat("\t", 6)}");
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
                        string[] getAmount = output[4].Split(' ');
                        string[] getAmount1 = output[5].Split(' ');

                        string statusOutput = $"AVAILABLE ({getAmount[1]})";
                        string noteData = "";
                    
                        if (int.Parse(getAmount[1]) == 0)
                        {
                            statusOutput = $"BORROWED  ({getAmount1[1]})";
                        } else if (int.Parse(getAmount1[1]) == 0)
                        {
                            statusOutput = $"AVAILABLE ({getAmount[1]})";
                        }

                        if (int.Parse(getAmount1[1]) > 0)
                        {
                            string[] getNote = output[7].Split(' ');
                            noteData = $"Borrowed by customer's IDs {getNote[0]} at {getNote[1]} {getNote[2]}";
                        }

                        Console.WriteLine($"{Repeat("-", 238)}");
                        Console.WriteLine(
                            $"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Amount",-10}|{"Status",-15}|{"Date",-20}|{"Note",-60}|");
                        Console.WriteLine($"{Repeat("-", 238)}");
                        
                        Console.WriteLine(
                            $"|{number2,-4}|{output[0],-60}|{output[1],-40}|{output[2],-20}|{output[3],-10}|{statusOutput,-15}|{output[6],-20}|{noteData,-60}|");

                        if (int.Parse(getAmount1[1]) > 0)
                        {
                            statusOutput = "";
                            if (int.Parse(getAmount[1]) > 0)
                            {
                                statusOutput = $"BORROWED  ({getAmount1[1]})";
                                if (output.Length == 8)
                                {
                                    Console.WriteLine($"{Repeat(" ", 139)}|{statusOutput,-15}|{"",-20}|{"",-60}|");
                                }
                            }

                            for (int j = 8; j <= output.Length - 1; j++)
                            {
                                string[] getNote = output[j].Split(' ');
                                noteData = $"Borrowed by customer's IDs {getNote[0]} at {getNote[1]} {getNote[2]}";
                                Console.WriteLine($"{Repeat(" ", 139)}|{statusOutput,-15}|{"",-20}|{noteData,-60}|");
                            }
                        }
                        Console.WriteLine($"{Repeat("-", 238)}");
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
                        bool check = false;
                        
                        if (number3 == 1)
                        {
                            if (data.Length == 0)
                            {
                                Console.WriteLine("There are no data currently");
                                return;
                            }
                            
                            for (int i = 0; i < data.Length; i++)
                            {
                                string[] output1 = data[i].Split(',');
                                string[] getAmount2 = output1[4].Split(' ');

                                if (getAmount2[0] == "False" && int.Parse(getAmount2[1]) > 0)
                                {
                                    check = true;
                                    break;
                                }
                            }

                            if (!check)
                            {
                                Console.WriteLine("There are no AVAILABLE book currently");
                                return;
                            }

                            Console.WriteLine($"{Repeat("-", 238)}");
                            Console.WriteLine(
                                $"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Amount",-10}|{"Status",-15}|{"Date",-20}|{"Note",-60}|");
                            Console.WriteLine($"{Repeat("-", 238)}");
                            
                            for (int i = 0; i < data.Length; i++)
                            {
                                string[] output1 = data[i].Split(',');
                                string[] getAmount2 = output1[4].Split(' ');

                                if (getAmount2[0] == "False" && int.Parse(getAmount2[1]) > 0)
                                {
                                    Console.WriteLine(
                                        $"|{i + 1,-4}|{output1[0],-60}|{output1[1],-40}|{output1[2],-20}|{output1[3],-10}|{$"AVAILABLE ({getAmount2[1]})",-15}|{output1[6],-20}|{"",-60}|");
                                    Console.WriteLine($"{Repeat("-", 238)}");
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
                            
                            for (int i = 0; i < data.Length; i++)
                            {
                                string[] output1 = data[i].Split(',');
                                string[] getAmount2 = output1[5].Split(' ');

                                if (getAmount2[0] == "True" && int.Parse(getAmount2[1]) > 0)
                                {
                                    check = true;
                                    break;
                                }
                            }

                            if (!check)
                            {
                                Console.WriteLine("There are no BORROWED book currently");
                                return;
                            }

                            Console.WriteLine($"{Repeat("-", 238)}");
                            Console.WriteLine(
                                $"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Amount",-10}|{"Status",-15}|{"Date",-20}|{"Note",-60}|");
                            Console.WriteLine($"{Repeat("-", 238)}");
                            
                            for (int i = 0; i < data.Length; i++)
                            {
                                string[] output1 = data[i].Split(',');
                                string[] getAmount2 = output1[5].Split(' ');
                                string noteData1 = "";
                                
                                if (int.Parse(getAmount2[1]) > 0)
                                {
                                    string[] getNote = output1[7].Split(' ');
                                    noteData1 = $"Borrowed by customer's IDs {getNote[0]} at {getNote[1]} {getNote[2]}";
                                }

                                if (getAmount2[0] == "True" && int.Parse(getAmount2[1]) > 0)
                                {
                                    Console.WriteLine(
                                        $"|{i + 1,-4}|{output1[0],-60}|{output1[1],-40}|{output1[2],-20}|{output1[3],-10}|{$"BORROWED  ({getAmount2[1]})",-15}|{output1[6],-20}|{noteData1,-60}|");
                                    if (int.Parse(getAmount2[1]) > 0)
                                    {
                                        statusOutput = "";

                                        for (int j = 8; j <= output1.Length - 1; j++)
                                        {
                                            string[] getNote = output1[j].Split(' ');
                                            noteData = $"Borrowed by customer's IDs {getNote[0]} at {getNote[1]} {getNote[2]}";
                                            Console.WriteLine($"{Repeat(" ", 139)}|{statusOutput,-15}|{"",-20}|{noteData,-60}|");
                                        }
                                    }
                                    Console.WriteLine($"{Repeat("-", 238)}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid number");
                        }

                        break;
                    case 6:
                        CustomOutput("date", data, "date", 5);
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

        public void UpdateStatus()
        {
            try
            {
                string[] bookData = File.ReadAllLines(@"D:\Dev\School\Library Management System\MyTest.txt");
                string[] customerData = File.ReadAllLines(@"D:\Dev\School\Library Management System\CustomerData.txt");
                
                if (bookData.Length == 0 || customerData.Length == 0)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

                Console.Write("Input IDs customer to borrow: ");
                string IDs = Console.ReadLine();
                int check = -1;

                for (int i = 0; i < customerData.Length; i++)
                {
                    string[] output = customerData[i].Split(',');
                    if (output[0] == IDs)
                    {
                        check = i;
                        break;
                    }
                }

                if (check == -1)
                {
                    Console.WriteLine($"There is no {IDs} in the database");
                    return;
                }
                
                string[] output1 = customerData[check].Split(',');
                
                Console.WriteLine(Repeat("-", 151));
                Console.WriteLine(
                    $"|{"ID",-10}|{"Name",-60}|{"Age",-5}|{"Sex",-6}|{"Phone Number",-15}|{"Status",-48}|");
                Console.WriteLine(Repeat("-", 151));

                Console.WriteLine(
                    $"|{output1[0],-10}|{output1[1],-60}|{output1[2],-5}|{output1[3],-6}|{output1[4],-15}|{output1[5],-48}|");
                Console.WriteLine(Repeat("-", 151));
                
                while (true)
                {
                    Show();

                    Console.Write("Input ID book to borrow: ");
                    int number = int.Parse(Console.ReadLine());

                    if (number < 0 || number > bookData.Length)
                    {
                        Console.WriteLine("Invalid ID");
                        return;
                    }

                    string[] data = bookData[number - 1].Split(',');
                    string[] availableData = data[4].Split(' ');
                    string[] borrowedData = data[5].Split(' ');

                    if (int.Parse(availableData[1]) == 0)
                    {
                        Console.WriteLine("There are no available book to borrow");
                        return;
                    }

                    for (int i = 6; i <= output1.Length - 1; i++)
                    {
                        string[] bookBorrowed = output1[i].Split('"');
                        if (int.Parse(bookBorrowed[1]) == number)
                        {
                            Console.WriteLine($"The {IDs} has already borrowed the book");
                            return;
                        }
                    }

                    availableData[1] = $"{int.Parse(availableData[1]) - 1}";
                    borrowedData[1] = $"{int.Parse(borrowedData[1]) + 1}";
                    
                    data[4] = string.Join(" ", availableData);
                    data[5] = string.Join(" ", borrowedData);
                    
                    List<string> output2 = new List<string>(output1);
                    output2.Add($"Borrowed book's ID: \"{number}\" at {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                    output1 = output2.ToArray();
                    
                    List<string> data1 = new List<string>(data);
                    data1.Add($"{IDs} {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                    data = data1.ToArray();
                    
                    customerData[check] = string.Join(",", output1);
                    bookData[number - 1] = string.Join(",", data);
                    File.WriteAllLines(@"D:\Dev\School\Library Management System\CustomerData.txt", customerData);
                    File.WriteAllLines(@"D:\Dev\School\Library Management System\MyTest.txt", bookData);
                    Console.WriteLine("\t\t\t\t\t\t********* BORROWED SUCCESSFULLY *********\t\t\t\t\t");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("There are no data currently");
            }
        }
    }
}