using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library_Management_System
{
    public class LibrarianCalendar
    {
        List<Librarian> _librarians = new List<Librarian>();

        public void AddLibrarian()
        {
            try
            {
                string path = @"D:\Dev\School\Library Management System\LibrarianData.txt";
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\LibrarianData.txt");

                if (data.Length > 6)
                {
                    Console.WriteLine("No more librarian need to be recruited");
                    return;
                }
                
                Console.Write("Enter Librarian's IDs: ");
                string ids = Console.ReadLine();
                Console.Write("Enter Librarian's name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Librarian's age: ");
                string age = Console.ReadLine();
                Console.Write("Enter Librarian's sex: ");
                string sex = Console.ReadLine().ToLower();
                Console.Write("Enter Librarian's phone number: ");
                string phoneNumber = Console.ReadLine();
                string status = "";
                string calendar = "";

                _librarians.Add(new Librarian(ids, name, age, sex, phoneNumber, status, calendar));
                string output =
                    $"{_librarians[0].getID()},{_librarians[0].getName()},{_librarians[0].getAge()},{_librarians[0].getSex()},{_librarians[0].getPhoneNumber()},{_librarians[0].getStatus()},{_librarians[0].getCalendar()}";

                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(output);
                }

                _librarians = new List<Librarian>();
            }
            catch (IOException)
            {
                Console.WriteLine("There is no data currently");
                using (StreamWriter writer = File.CreateText(@"D:\Dev\School\Library Management System\LibrarianData.txt")) 
                {
                    writer.WriteLine("");
                }
            }
        }

        public void ShowLibrarian()
        {
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\LibrarianData.txt");
                int[] test = { 11, 61, 6, 11, 16, 49, 49 };
                
                if (data.Length == 0 || data.Length == 1)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

                for (int i = 0; i < test.Length; i++)
                {
                    Console.Write($"╔{BookList.Repeat("═",test[i])}╗");
                }
                
                Console.WriteLine(
                    $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-10}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║║{"",-1}{"Shift",-48}║");
                
                for (int i = 0; i < test.Length; i++)
                {
                    Console.Write($" {BookList.Repeat("═",test[i])} ");
                }
                
                Console.Write("\n");

                for (int i = 1; i < data.Length; i++)
                {
                    string[] output = data[i].Split(',');
                    Console.WriteLine(
                        $"║{"",-1}{output[0],-10}║║{"",-1}{output[1],-60}║║{"",-1}{output[2],-5}║║{"",-1}{output[3],-10}║║{"",-1}{output[4],-15}║║{"",-1}{output[5],-48}║║{"",-1}{output[6],-48}║");
                }
                for (int i = 0; i < test.Length; i++)
                {
                    Console.Write($"╚{BookList.Repeat("═",test[i])}╝");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("There are no data currently");
            }
        }

        public void ShowCalendar()
        {
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\LibrarianData.txt");
                string[] day = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                string[] routineDay = new string[7];
                string[] employeeDay = new string[7];
                int[] test = { 12, 11, 61, 6, 11, 16, 49 };
                int check = 0;

                int index = Array.IndexOf(day, DateTime.Today.ToString("F").Split(',')[0]);

                for (int i = index; i < day.Length; i++)
                {
                    routineDay[check] = day[i];
                    check += 1;
                }

                for (int i = 0; i < day.Length - check; i++)
                {
                    check += i;
                    routineDay[check] = day[i];
                    check -= i;
                }
                
                for (int i = 1; i < data.Length; i++)
                {
                    string checkDay = data[i].Split(',')[6];
                    int index1 = Array.IndexOf(routineDay, checkDay);
                    employeeDay[index1] = data[i].Split(',')[0];
                }
                
                for (int i = 0; i < test.Length; i++)
                {
                    Console.Write($"╔{BookList.Repeat("═",test[i])}╗");
                }
                
                Console.WriteLine(
                    $"\n║{"",-12}║║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-10}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║");
                
                for (int i = 0; i < test.Length; i++)
                {
                    Console.Write($" {BookList.Repeat("═",test[i])} ");
                }
                
                Console.Write("\n");
                
                int checking = 0;
                for (int i = 0; i < routineDay.Length; i++)
                {
                    string[] output;
                    if (employeeDay[checking] == null)
                    {
                        output = new string[7];
                    }
                    else
                    {
                        output = data[Int32.Parse(employeeDay[checking])].Split(',');
                    }
                    
                    Console.WriteLine(
                        $"║{"",-2}{routineDay[i].ToUpper(),-10}║║{"",-1}{output[0],-10}║║{"",-1}{output[1],-60}║║{"",-1}{output[2],-5}║║{"",-1}{output[3],-10}║║{"",-1}{output[4],-15}║║{"",-1}{output[5],-48}║");
                    checking += 1;
                }
                for (int i = 0; i < test.Length; i++)
                {
                    Console.Write($"╚{BookList.Repeat("═",test[i])}╝");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("There are no data currently");
            }
        }

        public void AddCalendar()
        {
            
        }
    }
}