using System;
using System.Collections.Generic;
using System.IO;

namespace Library_Management_System
{
    public class CustomerData
    {
        List<Customer> _customers = new List<Customer>();

        public void OutputForUniqueData(string name, string[] data, int index)
        {
            string section = name;
            Console.Write($"Input {name} to search: ");
            name = Console.ReadLine();
            int[] storeLength = { 11, 61, 6, 7, 16, 49 };

            for (int i = 0; i < data.Length; i++)
            {
                string[] output = data[i].Split(',');

                if (output[index] == $"{name}")
                {
                    // Console.WriteLine(BookList.Repeat("-", 151));
                    
                    for (int j = 0; j < storeLength.Length; j++)
                    {
                        Console.Write($"╔{BookList.Repeat("═",storeLength[j])}╗");
                    }
                    
                    Console.WriteLine(
                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║");
                    
                    for (int j = 0; j < storeLength.Length; j++)
                    {
                        Console.Write($" {BookList.Repeat("═",storeLength[j])} ");
                    }
                    Console.Write("\n");
                    
                    string x = (output.Length > 6) 
                        ? $"║{"",-1}{output[0],-10}║║{"",-1}{output[1],-60}║║{"",-1}{output[2],-5}║║{"",-1}{output[3],-6}║║{"",-1}{output[4],-15}║║{"",-1}{output[6],-48}║" 
                        : $"║{"",-1}{output[0],-10}║║{"",-1}{output[1],-60}║║{"",-1}{output[2],-5}║║{"",-1}{output[3],-6}║║{"",-1}{output[4],-15}║║{"",-1}{output[5],-48}║";
                    Console.WriteLine(x);
                    
                    if (output.Length >= 8)
                    {
                        for (int j = 7; j <= output.Length - 1; j++)
                        {
                            Console.WriteLine(
                                $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-1}{output[j],-48}║");
                        }
                    }
                    
                    for (int j = 0; j < storeLength.Length; j++)
                    {
                        Console.Write($"╚{BookList.Repeat("═",storeLength[j])}╝");
                    }
                    
                    // Console.WriteLine(BookList.Repeat("-", 151));
                    return;
                }
            }

            Console.WriteLine($"There is no {name} in {section} section in the database");
        }

        public void AddCustomer()
        {
            string path = @"D:\Dev\School\Library Management System\CustomerData.txt";

            Console.Write("Enter customer's IDs: ");
            string ids = Console.ReadLine();
            Console.Write("Enter customer's name: ");
            string name = Console.ReadLine();
            Console.Write("Enter customer's age: ");
            string age = Console.ReadLine();
            Console.Write("Enter customer's sex: ");
            string sex = Console.ReadLine().ToLower();
            Console.Write("Enter customer's phone number: ");
            string phoneNumber = Console.ReadLine();
            string status = "";

            _customers.Add(new Customer(ids, name, age, sex, phoneNumber, status));
            string output =
                $"{_customers[0].getID()},{_customers[0].getName()},{_customers[0].getAge()},{_customers[0].getSex()},{_customers[0].getPhoneNumber()},{_customers[0].getStatus()}";

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(output);
            }

            _customers = new List<Customer>();
        }

        public void ShowCustomer()
        {
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\CustomerData.txt");
                int[] storeLength = { 11, 61, 6, 7, 16, 49 };
                
                if (data.Length == 0)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

                for (int j = 0; j < storeLength.Length; j++)
                {
                    Console.Write($"╔{BookList.Repeat("═",storeLength[j])}╗");
                }
                
                Console.WriteLine(
                    $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║");

                for (int i = 0; i < data.Length; i++)
                {
                    for (int j = 0; j < storeLength.Length; j++)
                    {
                        Console.Write($" {BookList.Repeat("═",storeLength[j])} ");
                    }
                    
                    Console.Write("\n");
                    
                    string[] output = data[i].Split(',');

                    string x = (output.Length > 6) 
                        ? $"║{"",-1}{output[0],-10}║║{"",-1}{output[1],-60}║║{"",-1}{output[2],-5}║║{"",-1}{output[3],-6}║║{"",-1}{output[4],-15}║║{"",-1}{output[6],-48}║" 
                        : $"║{"",-1}{output[0],-10}║║{"",-1}{output[1],-60}║║{"",-1}{output[2],-5}║║{"",-1}{output[3],-6}║║{"",-1}{output[4],-15}║║{"",-1}{output[5],-48}║";
                    Console.WriteLine(x);
                    
                    if (output.Length >= 8)
                    {
                        for (int j = 7; j <= output.Length - 1; j++)
                        {
                            Console.WriteLine(
                                $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-1}{output[j],-48}║");
                        }
                    }
                }
                
                for (int j = 0; j < storeLength.Length; j++)
                {
                    Console.Write($"╚{BookList.Repeat("═",storeLength[j])}╝");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("There are no data currently");
            }
        }

        public void SearchCustomer()
        {
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\CustomerData.txt");
                int[] storeLength = { 11, 61, 6, 7, 16, 49 };
                
                if (data.Length == 0)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

                Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 1. SEARCH BY IDs                       ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 2. SEARCH BY NAME                      ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 3. SEARCH BY AGE                       ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 4. SEARCH BY SEX                       ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 5. SEARCH BY PHONE NUMBER              ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 6. SEARCH BY STATUS                    ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t║ 7. EXIT                                ║\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
                Console.Write("Input to use: ");
                int number1 = int.Parse(Console.ReadLine());

                switch (number1)
                {
                    case 1:
                        OutputForUniqueData("IDs", data, 0);
                        break;
                    case 2:
                        OutputForUniqueData("Name", data, 1);
                        break;
                    case 3:
                        bool check = false;

                        Console.Write("Input age to search: ");
                        int age = int.Parse(Console.ReadLine());

                        for (int i = 0; i < data.Length; i++)
                        {
                            string[] output1 = data[i].Split(',');
                            if (output1[2] == $"{age}")
                            {
                                check = true;
                                break;
                            }
                        }

                        if (check == false)
                        {
                            Console.WriteLine(
                                $"\t\t\t\t\t     There is no {age} in Age section in the database\t\t\t\t\t\t");
                            return;
                        }

                        for (int j = 0; j < storeLength.Length; j++)
                        {
                            Console.Write($"╔{BookList.Repeat("═",storeLength[j])}╗");
                        }
                
                        Console.WriteLine(
                            $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║");

                        for (int i = 0; i < data.Length; i++)
                        {
                            string[] output1 = data[i].Split(',');

                            if (output1[2] == $"{age}")
                            {
                                for (int j = 0; j < storeLength.Length; j++)
                                {
                                    Console.Write($" {BookList.Repeat("═",storeLength[j])} ");
                                }
                    
                                Console.Write("\n");
                                
                                string x = (output1.Length > 6) 
                                    ? $"║{"",-1}{output1[0],-10}║║{"",-1}{output1[1],-60}║║{"",-1}{output1[2],-5}║║{"",-1}{output1[3],-6}║║{"",-1}{output1[4],-15}║║{"",-1}{output1[6],-48}║" 
                                    : $"║{"",-1}{output1[0],-10}║║{"",-1}{output1[1],-60}║║{"",-1}{output1[2],-5}║║{"",-1}{output1[3],-6}║║{"",-1}{output1[4],-15}║║{"",-1}{output1[5],-48}║";
                                
                                Console.WriteLine(x);
                                
                                if (output1.Length >= 8)
                                {
                                    for (int j = 7; j <= output1.Length - 1; j++)
                                    {
                                        Console.WriteLine(
                                            $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-1}{output1[j],-48}║");
                                    }
                                }
                            }
                        }
                        
                        for (int j = 0; j < storeLength.Length; j++)
                        {
                            Console.Write($"╚{BookList.Repeat("═",storeLength[j])}╝");
                        }

                        break;
                    case 4:
                        Console.Write("Input sex to search: ");
                        string sex = Console.ReadLine().ToLower();

                        if (sex == "male" || sex == "female")
                        {
                            for (int j = 0; j < storeLength.Length; j++)
                            {
                                Console.Write($"╔{BookList.Repeat("═",storeLength[j])}╗");
                            }
                
                            Console.WriteLine(
                                $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║");

                            for (int i = 0; i < data.Length; i++)
                            {
                                string[] output1 = data[i].Split(',');

                                if (output1[3] == sex)
                                {
                                    for (int j = 0; j < storeLength.Length; j++)
                                    {
                                        Console.Write($" {BookList.Repeat("═",storeLength[j])} ");
                                    }
                    
                                    Console.Write("\n");
                                    
                                    string x = (output1.Length > 6) 
                                        ? $"║{"",-1}{output1[0],-10}║║{"",-1}{output1[1],-60}║║{"",-1}{output1[2],-5}║║{"",-1}{output1[3],-6}║║{"",-1}{output1[4],-15}║║{"",-1}{output1[6],-48}║" 
                                        : $"║{"",-1}{output1[0],-10}║║{"",-1}{output1[1],-60}║║{"",-1}{output1[2],-5}║║{"",-1}{output1[3],-6}║║{"",-1}{output1[4],-15}║║{"",-1}{output1[5],-48}║";
                                
                                    Console.WriteLine(x);
                                    
                                    if (output1.Length >= 8)
                                    {
                                        for (int j = 7; j <= output1.Length - 1; j++)
                                        {
                                            Console.WriteLine(
                                                $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-1}{output1[j],-48}║");
                                        }
                                    }
                                }
                            }
                            
                            for (int j = 0; j < storeLength.Length; j++)
                            {
                                Console.Write($"╚{BookList.Repeat("═",storeLength[j])}╝");
                            }
                        }

                        break;
                    case 5:
                        bool check1 = false;

                        Console.Write("Input phone number to search: ");
                        string phone = Console.ReadLine();

                        for (int i = 0; i < data.Length; i++)
                        {
                            string[] output1 = data[i].Split(',');
                            if (output1[4] == phone)
                            {
                                check1 = true;
                                break;
                            }
                        }

                        if (check1 == false)
                        {
                            Console.WriteLine(
                                $"\t\t\t\t\t     There is no {phone} in Number Phone section in the database\t\t\t\t\t\t");
                            return;
                        }

                        for (int j = 0; j < storeLength.Length; j++)
                        {
                            Console.Write($"╔{BookList.Repeat("═",storeLength[j])}╗");
                        }
                
                        Console.WriteLine(
                            $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║");

                        for (int i = 0; i < data.Length; i++)
                        {
                            string[] output1 = data[i].Split(',');

                            if (output1[4] == phone)
                            {
                                for (int j = 0; j < storeLength.Length; j++)
                                {
                                    Console.Write($" {BookList.Repeat("═",storeLength[j])} ");
                                }
                    
                                Console.Write("\n");
                                
                                string x = (output1.Length > 6) 
                                    ? $"║{"",-1}{output1[0],-10}║║{"",-1}{output1[1],-60}║║{"",-1}{output1[2],-5}║║{"",-1}{output1[3],-6}║║{"",-1}{output1[4],-15}║║{"",-1}{output1[6],-48}║" 
                                    : $"║{"",-1}{output1[0],-10}║║{"",-1}{output1[1],-60}║║{"",-1}{output1[2],-5}║║{"",-1}{output1[3],-6}║║{"",-1}{output1[4],-15}║║{"",-1}{output1[5],-48}║";
                                
                                Console.WriteLine(x);
                                
                                if (output1.Length >= 8)
                                {
                                    for (int j = 7; j <= output1.Length - 1; j++)
                                    {
                                        Console.WriteLine(
                                            $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-1}{output1[j],-48}║");
                                    }
                                }
                            }
                        }
                        
                        for (int j = 0; j < storeLength.Length; j++)
                        {
                            Console.Write($"╚{BookList.Repeat("═",storeLength[j])}╝");
                        }

                        break;
                    case 6:
                        // CustomOutput("date", data, "date", 4);
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

        public void DeleteCustomer()
        {
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\CustomerData.txt");
                string[] data1 = File.ReadAllLines(@"D:\Dev\School\Library Management System\MyTest.txt");
                
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
                        bool check = false;
                        Console.Write("Input IDs to delete: ");
                        string ids = Console.ReadLine();
                        List<string> output1 = new List<string>(data);
                        List<string> bookData = new List<string>(data1);

                        for (int i = 0; i < output1.Count; i++)
                        {
                            string[] output2 = output1[i].Split(',');
                            if (output2[0] == ids)
                            {
                                output1.RemoveAt(i);
                                check = true;
                                break;
                            }
                        }

                        if (check == false)
                        {
                            Console.WriteLine($"There is no {ids} in IDs section in the database");
                            return;
                        }

                        for (int i = 0; i < bookData.Count; i++)
                        {
                            string[] aOutput = bookData[i].Split(',');
                            List<string> aOutputList = new List<string>(aOutput);
                            bool checkIng = false;
                            
                            for (int j = 7; j <= aOutputList.Count - 1; j++)
                            {
                                string[] checkIDs = aOutputList[j].Split(' ');
                                string[] checkAvailable = aOutputList[4].Split(' ');
                                string[] checkBorrowed = aOutputList[5].Split(' ');
                                
                                if (checkIDs[0] == ids)
                                {
                                    aOutputList.RemoveAt(j);
                                    checkAvailable[1] = $"{int.Parse(checkAvailable[1]) + 1}";
                                    aOutputList[4] = string.Join(" ", checkAvailable);
                                    checkBorrowed[1] = $"{int.Parse(checkBorrowed[1]) - 1}";
                                    aOutputList[5] = string.Join(" ", checkBorrowed);
                                    checkIng = true;
                                    break;
                                }
                            }
                            
                            if (checkIng)
                            {
                                bookData[i] = string.Join(",", aOutputList.ToArray());
                            }
                        }

                        File.WriteAllLines(@"D:\Dev\School\Library Management System\CustomerData.txt", output1.ToArray());
                        File.WriteAllLines(@"D:\Dev\School\Library Management System\MyTest.txt", bookData.ToArray());
                        Console.WriteLine("\t\t\t\t\t\t*********** DELETE SUCCESSFULLY **********\t\t\t\t\t");

                        break;
                    case 2:
                        bool check1 = false;
                        Console.Write("Input Name to delete: ");
                        string name = Console.ReadLine();
                        List<string> output3 = new List<string>(data);
                        List<string> bookData1 = new List<string>(data1);
                        string checkID = "";
                        
                        for (int i = 0; i < output3.Count; i++)
                        {
                            string[] output2 = output3[i].Split(',');
                            if (output2[1] == name)
                            {
                                output3.RemoveAt(i);
                                checkID = output2[0];
                                check1 = true;
                                break;
                            }
                        }

                        if (check1 == false)
                        {
                            Console.WriteLine($"There is no {name} in Name section in the database");
                            return;
                        }
                        
                        for (int i = 0; i < bookData1.Count; i++)
                        {
                            string[] aOutput = bookData1[i].Split(',');
                            List<string> aOutputList = new List<string>(aOutput);
                            bool checkIng = false;
                            
                            for (int j = 7; j <= aOutputList.Count - 1; j++)
                            {
                                string[] checkIDs = aOutputList[j].Split(' ');
                                string[] checkAvailable = aOutputList[4].Split(' ');
                                string[] checkBorrowed = aOutputList[5].Split(' ');
                                
                                if (checkIDs[0] == checkID)
                                {
                                    aOutputList.RemoveAt(j);
                                    checkAvailable[1] = $"{int.Parse(checkAvailable[1]) + 1}";
                                    aOutputList[4] = string.Join(" ", checkAvailable);
                                    checkBorrowed[1] = $"{int.Parse(checkBorrowed[1]) - 1}";
                                    aOutputList[5] = string.Join(" ", checkBorrowed);
                                    checkIng = true;
                                    break;
                                }
                            }
                            
                            if (checkIng)
                            {
                                bookData1[i] = string.Join(",", aOutputList.ToArray());
                            }
                        }

                        File.WriteAllLines(@"D:\Dev\School\Library Management System\CustomerData.txt", output3.ToArray());
                        File.WriteAllLines(@"D:\Dev\School\Library Management System\MyTest.txt", bookData1.ToArray());
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

        public void EditCustomer()
        {
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\CustomerData.txt");
                if (data.Length == 0)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

                ShowCustomer();
                Console.Write("Input IDs to edit: ");
                string ids = Console.ReadLine();

                for (int i = 0; i < data.Length; i++)
                {
                    string[] output = data[i].Split(',');
                    if (output[0] == ids)
                    {
                        Console.WriteLine($"\t\t\t\t\t\t          EDITING THE BOOK NO.{ids} \t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t****************** MENU ******************\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 1. EDIT IDs                          **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 2. EDIT NAME                         **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 3. EDIT AGE                          **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 4. EDIT SEX                          **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 5. EDIT PHONE NUMBER                 **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t** 6. EXIT                              **\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t******************************************\t\t\t\t\t");
                        Console.Write("Input to edit: ");
                        int number2 = int.Parse(Console.ReadLine());

                        if (number2 > 0 && number2 <= 5)
                        {
                            Console.Write($"Changing {output[number2 - 1]} to: ");
                            string newText = Console.ReadLine();
                            output[number2 - 1] = newText;
                            data[i] = string.Join(",", output);
                            File.WriteAllLines(@"D:\Dev\School\Library Management System\CustomerData.txt", data);
                            Console.WriteLine("\t\t\t\t\t\t*********** UPDATE SUCCESSFULLY **********\t\t\t\t\t");
                        }
                        else if (number2 == 6)
                        {

                        }
                        else
                        {
                            Console.WriteLine("Invalid number");
                        }

                        return;
                    }
                }

                Console.WriteLine($"There is no {ids} in IDs section in database!");
            }
            catch (IOException)
            {
                Console.WriteLine("There are no data currently");
            }
        }
    }
}