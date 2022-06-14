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
            
            for (int i = 0; i < data.Length; i++)
            {
                string[] output = data[i].Split(',');
                
                if (output[index] == $"{name}")
                {
                    Console.WriteLine(
                        "--------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine(
                        $"|{"ID",-10}|{"Name",-60}|{"Age",-5}|{"Sex",-6}|{"Phone Number",-15}|{"Status",-25}|");
                    Console.WriteLine(
                        "--------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine(
                        $"|{output[0],-10}|{output[1],-60}|{output[2],-5}|{output[3],-6}|{output[4],-15}|{output[5],-25}|");
                    Console.WriteLine(
                        "--------------------------------------------------------------------------------------------------------------------------------");
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
            Console.Write("Enter customer's status: ");
            string status = Console.ReadLine();

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
                if (data.Length == 0)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

                Console.WriteLine(
                    "--------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(
                    $"|{"ID",-10}|{"Name",-60}|{"Age",-5}|{"Sex",-6}|{"Phone Number",-15}|{"Status",-25}|");
                Console.WriteLine(
                    "--------------------------------------------------------------------------------------------------------------------------------");

                for (int i = 0; i < data.Length; i++)
                {
                    string[] output = data[i].Split(',');

                    Console.WriteLine(
                        $"|{output[0],-10}|{output[1],-60}|{output[2],-5}|{output[3],-6}|{output[4],-15}|{output[5],-25}|");
                    Console.WriteLine(
                        "--------------------------------------------------------------------------------------------------------------------------------");
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
                if (data.Length == 0)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

                Console.WriteLine("\t\t\t\t\t\t****************** MENU ******************\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 1. SEARCH BY IDs                     **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 2. SEARCH BY NAME                    **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 3. SEARCH BY AGE                     **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 4. SEARCH BY SEX                     **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 5. SEARCH BY PHONE NUMBER            **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 6. SEARCH BY STATUS                  **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t** 7. EXIT                              **\t\t\t\t\t");
                Console.WriteLine("\t\t\t\t\t\t******************************************\t\t\t\t\t");
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
                        
                        Console.WriteLine("Input age to search: ");
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

                        Console.WriteLine(
                            "--------------------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"|{"ID",-10}|{"Name",-60}|{"Age",-5}|{"Sex",-6}|{"Phone Number",-15}|{"Status",-25}|");
                        Console.WriteLine(
                            "--------------------------------------------------------------------------------------------------------------------------------");

                        for (int i = 0; i < data.Length; i++)
                        {
                            string[] output1 = data[i].Split(',');

                            if (output1[2] == $"{age}")
                            {
                                Console.WriteLine(
                                    $"|{output1[0],-10}|{output1[1],-60}|{output1[2],-5}|{output1[3],-6}|{output1[4],-15}|{output1[5],-25}|");
                                Console.WriteLine(
                                    "--------------------------------------------------------------------------------------------------------------------------------");
                            }
                        }

                        break;
                    case 4:
                        Console.Write("Input sex to search: ");
                        string sex = Console.ReadLine().ToLower();

                        if (sex == "male" || sex == "female")
                        {
                            Console.WriteLine(
                                "--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"|{"ID",-10}|{"Name",-60}|{"Age",-5}|{"Sex",-6}|{"Phone Number",-15}|{"Status",-25}|");
                            Console.WriteLine(
                                "--------------------------------------------------------------------------------------------------------------------------------");

                            for (int i = 0; i < data.Length; i++)
                            {
                                string[] output1 = data[i].Split(',');

                                if (output1[3] == sex)
                                {
                                    Console.WriteLine(
                                        $"|{output1[0],-10}|{output1[1],-60}|{output1[2],-5}|{output1[3],-6}|{output1[4],-15}|{output1[5],-25}|");
                                    Console.WriteLine(
                                        "--------------------------------------------------------------------------------------------------------------------------------");
                                }
                            }
                        }
                        
                        break;
                    case 5:
                        bool check1 = false;
                        
                        Console.WriteLine("Input age to search: ");
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

                        Console.WriteLine(
                            "--------------------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"|{"ID",-10}|{"Name",-60}|{"Age",-5}|{"Sex",-6}|{"Phone Number",-15}|{"Status",-25}|");
                        Console.WriteLine(
                            "--------------------------------------------------------------------------------------------------------------------------------");

                        for (int i = 0; i < data.Length; i++)
                        {
                            string[] output1 = data[i].Split(',');

                            if (output1[4] == phone)
                            {
                                Console.WriteLine(
                                    $"|{output1[0],-10}|{output1[1],-60}|{output1[2],-5}|{output1[3],-6}|{output1[4],-15}|{output1[5],-25}|");
                                Console.WriteLine(
                                    "--------------------------------------------------------------------------------------------------------------------------------");
                            }
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
    }
}