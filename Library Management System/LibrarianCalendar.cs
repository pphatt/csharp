using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Library_Management_System
{
    public class LibrarianCalendar
    {
        public void AddLibrarian()
        {
            /*
             * make a book column named it librarian for each schedule
             * make a customer column named it librarian for each schedule
             * 
             * make a customer column named it librarian for each schedule
             */

            Console.Write("Enter Librarian's name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Librarian's age: ");
            string age = Console.ReadLine();
            Console.Write("Enter Librarian's sex: ");
            string sex = Console.ReadLine().ToLower();
            Console.Write("Enter Librarian's phone number: ");
            string pn = Console.ReadLine();
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            string[] lna = name.Split(' ');
            StringBuilder ln = new StringBuilder();

            for (int i = 0; i < lna.Length; i++)
            {
                string o = "";
                string nn = lna[i][0].ToString().ToUpper() + lna[i].Substring(1);

                if (i != lna.Length - 1)
                {
                    o = " ";
                }

                ln.Append(nn + o);
            }

            string addDataQuery =
                "INSERT INTO Librarian (LibrarianName, LibrarianAge, LibrarianSex, LibrarianPhoneNumber, DateEnrol, DateRetire, State) VALUES (@LibrarianName, @LibrarianAge, @LibrarianSex, @LibrarianPhoneNumber, @DateEnrol, @DateRetire, @State)";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(addDataQuery, connection);

                insertCommand.Parameters.AddWithValue("@LibrarianName", ln.ToString());
                insertCommand.Parameters.AddWithValue("@LibrarianAge", age);
                insertCommand.Parameters.AddWithValue("@LibrarianSex", sex);
                insertCommand.Parameters.AddWithValue("@LibrarianPhoneNumber", pn);
                insertCommand.Parameters.AddWithValue("@DateEnrol", date);
                insertCommand.Parameters.AddWithValue("@DateRetire", null);
                insertCommand.Parameters.AddWithValue("@State", 0);
                insertCommand.ExecuteNonQuery();
            }
        }

        public void ShowLibrarian()
        {
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\LibrarianData.txt");
                int[] test = { 11, 61, 6, 11, 16, 49, 49 };

                if (data.Length == 0)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

                for (int i = 0; i < test.Length; i++)
                {
                    Console.Write($"╔{BookList.Repeat("═", test[i])}╗");
                }

                Console.WriteLine(
                    $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-10}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║║{"",-1}{"Shift",-48}║");

                for (int i = 0; i < data.Length; i++)
                {
                    string[] output = data[i].Split(',');

                    for (int j = 0; j < test.Length; j++)
                    {
                        Console.Write($" {BookList.Repeat("═", test[j])} ");
                    }

                    Console.Write("\n");

                    Console.WriteLine(
                        $"║{"",-1}{output[0],-10}║║{"",-1}{output[1],-60}║║{"",-1}{output[2],-5}║║{"",-1}{output[3],-10}║║{"",-1}{output[4],-15}║║{"",-1}{output[5],-48}║║{"",-1}{output[6],-48}║");
                }

                for (int i = 0; i < test.Length; i++)
                {
                    Console.Write($"╚{BookList.Repeat("═", test[i])}╝");
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
                for (int i = 0; i < data.Length; i++)
                {
                    string[] checkD = data[i].Split(',');
                    if (checkD[6] == "")
                    {
                        Console.WriteLine("There is no data currently or the TIME TABLE IS NOT FULL");
                        return;
                    }
                }

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

                for (int i = 0; i < data.Length; i++)
                {
                    string checkDay = data[i].Split(',')[6];
                    int index1 = Array.IndexOf(routineDay, checkDay);
                    employeeDay[index1] = data[i].Split(',')[0];
                }

                for (int i = 0; i < test.Length; i++)
                {
                    Console.Write($"╔{BookList.Repeat("═", test[i])}╗");
                }

                Console.WriteLine(
                    $"\n║{"",-12}║║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-10}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║");

                int checking = 0;
                for (int i = 0; i < routineDay.Length; i++)
                {
                    string[] output = (employeeDay[checking] == null)
                        ? new string[7]
                        : data[Int32.Parse(employeeDay[checking]) - 1].Split(',');

                    for (int j = 0; j < test.Length; j++)
                    {
                        Console.Write($" {BookList.Repeat("═", test[j])} ");
                    }

                    Console.Write("\n");

                    Console.WriteLine(
                        $"║{"",-2}{routineDay[i].ToUpper(),-10}║║{"",-1}{output[0],-10}║║{"",-1}{output[1],-60}║║{"",-1}{output[2],-5}║║{"",-1}{output[3],-10}║║{"",-1}{output[4],-15}║║{"",-1}{output[5],-48}║");
                    checking += 1;
                }

                for (int i = 0; i < test.Length; i++)
                {
                    Console.Write($"╚{BookList.Repeat("═", test[i])}╝");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("There are no data currently");
            }
        }

        public void AddCalendar()
        {
            string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\LibrarianData.txt");
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            for (int i = 0; i < data.Length; i++)
            {
                int check = Array.IndexOf(days, data[i].Split(',')[6]);
                if (check >= 0)
                {
                    days = days.Where((val, indx) => indx != check).ToArray();
                }
            }

            ShowLibrarian();
            Console.Write("\nInput to use: ");
            int number = int.Parse(Console.ReadLine());

            if (number > 0 && number <= data.Length && days.Length > 0)
            {
                string[] librarianCheck = data[number - 1].Split(',');
                if (librarianCheck[6] == "")
                {
                    Console.WriteLine("Here are the available day");
                    foreach (string day in days)
                    {
                        Console.Write($"|{day}|");
                    }

                    Console.Write("\nInput to use: ");
                    int number1 = int.Parse(Console.ReadLine());
                    librarianCheck[6] = $"{days[number1 - 1]}";
                    data[number - 1] = string.Join(",", librarianCheck);
                    File.WriteAllLines(@"D:\Dev\School\Library Management System\LibrarianData.txt", data);
                    Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATED SUCCESSFULLY ═══════════\t\t\t\t\t");
                }
                else
                {
                    Console.WriteLine("The Librarian already has schedule");
                    return;
                }
            }
        }
    }
}