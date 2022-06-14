using System;
using System.Collections.Generic;
using System.IO;

namespace Library_Management_System
{
    public class CustomerData
    {
        List<Customer> _customers = new List<Customer>();

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
            string sex = Console.ReadLine();
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
                Console.WriteLine($"|{"ID",-10}|{"Name",-60}|{"Age",-5}|{"Sex",-6}|{"Phone Number",-15}|{"Status",-25}|");
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
            
        }
    }
}