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
            string queryString =
                "select LibrarianIDs, LibrarianName, LibrarianAge, LibrarianSex, LibrarianPhoneNumber, DateEnrol from Librarian where Librarian.State = 0";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                using (SqlDataReader readCustomerInfo = command.ExecuteReader())
                {
                    bool check = false;

                    while (readCustomerInfo.Read())
                    {
                        if (!check)
                        {
                            for (int k = 0; k < Program.StoreLengthLibrary.Length; k++)
                            {
                                Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthLibrary[k])}╗");
                            }

                            Console.WriteLine(
                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-7}║║{"",-1}{"Phone Number",-15}║");

                            check = true;
                        }

                        for (int k = 0; k < Program.StoreLengthLibrary.Length; k++)
                        {
                            Console.Write($" {BookList.Repeat("═", Program.StoreLengthLibrary[k])} ");
                        }

                        Console.WriteLine(
                            $"\n║{"",-1}{readCustomerInfo[0],-4}║║{"",-1}{readCustomerInfo[1],-60}║║{"",-1}{readCustomerInfo[2],-5}║║{"",-1}{readCustomerInfo[3],-7}║║{"",-1}{readCustomerInfo[4],-15}║");
                    }

                    if (check)
                    {
                        for (int k = 0; k < Program.StoreLengthLibrary.Length; k++)
                        {
                            Console.Write($"╚{BookList.Repeat("═", Program.StoreLengthLibrary[k])}╝");
                        }

                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("There are no data currently");
                    }
                }
            }
        }

        public void ShowCalendar()
        {
            DateTime d1 = DateTime.Now;
            DateTime d2 = d1.AddDays(1);
            DateTime d3 = d1.AddDays(2);
            DateTime d4 = d1.AddDays(3);
            DateTime d5 = d1.AddDays(4);
            DateTime d6 = d1.AddDays(5);
            DateTime d7 = d1.AddDays(6);

            string[] time = { "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:30", "15:00", "17:00" };

            for (int k = 0; k < Program.StoreLengthSchedule.Length; k++)
            {
                Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthSchedule[k])}╗");
            }

            Console.WriteLine(
                $"\n║{"",-2}{"Time",-5}║{"",-1}{"Date",-6}║" +
                $"║{"",-1}{$"{d1.DayOfWeek} ({d1:M/d/yy})",-23}║" +
                $"║{"",-1}{$"{d2.DayOfWeek} ({d2:M/d/yy})",-23}║" +
                $"║{"",-1}{$"{d3.DayOfWeek} ({d3:M/d/yy})",-23}║" +
                $"║{"",-1}{$"{d4.DayOfWeek} ({d4:M/d/yy})",-23}║" +
                $"║{"",-1}{$"{d5.DayOfWeek} ({d5:M/d/yy})",-23}║" +
                $"║{"",-1}{$"{d6.DayOfWeek} ({d6:M/d/yy})",-23}║" +
                $"║{"",-1}{$"{d7.DayOfWeek} ({d7:M/d/yy})",-23}║");

            for (int k = 0; k < Program.StoreLengthSchedule.Length; k++)
            {
                Console.Write($" {BookList.Repeat("═", Program.StoreLengthSchedule[k])} ");
            }

            for (int i = 0; i < time.Length - 1; i++)
            {
                StringBuilder o = new StringBuilder();
                string q = "select LibrarianName " +
                           "from (Scheduled left join Librarian on Librarian.LibrarianIDs = Scheduled.LibrarianIDs) " +
                           $"where TimeStart = '{time[i]}' and DateOfWeek in ('Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday')";

                o.Append($"\n║{"",-1}{$"{time[i]} - {time[i + 1]}",-14}║");

                using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(q, connection);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        bool check = false;

                        while (reader.Read())
                        {
                            if (!check)
                            {
                                check = true;
                            }

                            string tmp = $"║{"",-1}{reader[0],-23}║";

                            if ($"{reader[0]}" == "")
                            {
                                tmp = $"║{"",-1}{"EMPTY",-23}║";
                            }

                            o.Append(tmp);
                        }

                        if (!check)
                        {
                            o.Append($"║{"",-1}{"BREAK",-23}║" +
                                     $"║{"",-1}{"BREAK",-23}║" +
                                     $"║{"",-1}{"BREAK",-23}║" +
                                     $"║{"",-1}{"BREAK",-23}║" +
                                     $"║{"",-1}{"BREAK",-23}║" +
                                     $"║{"",-1}{"BREAK",-23}║" +
                                     $"║{"",-1}{"BREAK",-23}║");
                        }

                        Console.WriteLine(o.ToString());

                        for (int k = 0; k < Program.StoreLengthSchedule.Length; k++)
                        {
                            Console.Write($" {BookList.Repeat("═", Program.StoreLengthSchedule[k])} ");
                        }
                    }
                }
            }

            Console.WriteLine("");
        }

        public void AddCalendar()
        {
            // string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\LibrarianData.txt");
            // string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            //
            // for (int i = 0; i < data.Length; i++)
            // {
            //     int check = Array.IndexOf(days, data[i].Split(',')[6]);
            //     if (check >= 0)
            //     {
            //         days = days.Where((val, indx) => indx != check).ToArray();
            //     }
            // }
            //
            // ShowLibrarian();
            // Console.Write("\nInput to use: ");
            // int number = int.Parse(Console.ReadLine());
            //
            // if (number > 0 && number <= data.Length && days.Length > 0)
            // {
            //     string[] librarianCheck = data[number - 1].Split(',');
            //     if (librarianCheck[6] == "")
            //     {
            //         Console.WriteLine("Here are the available day");
            //         foreach (string day in days)
            //         {
            //             Console.Write($"|{day}|");
            //         }
            //
            //         Console.Write("\nInput to use: ");
            //         int number1 = int.Parse(Console.ReadLine());
            //         librarianCheck[6] = $"{days[number1 - 1]}";
            //         data[number - 1] = string.Join(",", librarianCheck);
            //         File.WriteAllLines(@"D:\Dev\School\Library Management System\LibrarianData.txt", data);
            //         Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATED SUCCESSFULLY ═══════════\t\t\t\t\t");
            //     }
            //     else
            //     {
            //         Console.WriteLine("The Librarian already has schedule");
            //         return;
            //     }
            // }
        }
    }
}