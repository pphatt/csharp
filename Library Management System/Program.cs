using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Library_Management_System
{
    internal class Program
    {
        public static readonly string ConnectionString =
            @"data source=DESKTOP-0FQDJF2\SQLEXPRESS;initial catalog=Management_Library;trusted_connection=true;MultipleActiveResultSets=true";

        public static readonly int[] StoreLengthBooks = { 5, 61, 41, 21, 11, 11, 23, 23 };
        public static readonly int[] StoreLengthCustomer = { 5, 61, 6, 8, 16, 24, 26 };
        public static readonly int[] StoreLengthLibrary = { 5, 61, 6, 8, 16 };
        public static readonly int[] StoreLengthSchedule = { 13, 24, 24, 24, 24, 24, 24, 24 };

        public static void Main(string[] args)
        {
            // string queryString = "INSERT INTO Books (BookIDs, BookName, BookAuthor, BookCategory, BookAmount, Date, CustomerIDs) VALUES (@BookIDs, @BookName, @BookAuthor, @BookCategory, @BookAmount, @Date, @CustomerIDs)";
            // string queryString = "Select BookIDs, BookName, BookAuthor, BookCategory, BookAmount, Date, CustomerIDs from Books where BookName like '1'";

            /*
             * Update Row, Insert, Add data to Table
             */

            // using (SqlConnection connection = new SqlConnection(ConnectionString))
            // {
            //     SqlCommand command = new SqlCommand(queryString, connection);
            //     // command.Parameters.AddWithValue("@BookIDs", 1);
            //     // command.Parameters.AddWithValue("@BookName", "1");
            //     // command.Parameters.AddWithValue("@BookAuthor", "1");
            //     // command.Parameters.AddWithValue("@BookCategory", "bookName");
            //     // command.Parameters.AddWithValue("@BookAmount", 1);
            //     // command.Parameters.AddWithValue("@Date", "2022-1-1");
            //     // command.Parameters.AddWithValue("@CustomerIDs", "1");
            //     connection.Open();
            //     command.ExecuteNonQuery();
            //     Console.WriteLine(command.);
            // }


            /*
             * Read Row or Data from Table
             */

            // using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            // {
            //     SqlCommand command = new SqlCommand(queryString, connection);
            //     
            //     connection.Open();
            //     
            //     using(SqlDataReader reader = command.ExecuteReader())
            //     {
            //         while (reader.Read())
            //         {
            //             Console.WriteLine(String.Format($"{reader[0]} {reader[1]} {reader[2]} {reader[3]} {reader[4]} {reader[5]}"));
            //         }
            //     }
            // }

            Menu menu = new Menu();

            // ╔
            // ═
            // ║
            // ╗
            //
            // ╚
            // ╝

            // 13, 32, 32, 32, 32, 32 ,32, 32

            /* ╔═════════════╗╔════════╗╔════════╗
             * ║ Time ║ Date ║║ Monday ║║ Monday ║
             *  ═════════════  ════════  ════════
             * ║    08:00    ║║ Lopez  ║║ Lopez  ║
             * ║    10:00    ║║ Lopez  ║║ Lopez  ║
             * ║    12:00    ║║ Lopez  ║║ Lopez  ║
             * ║    12:30    ║║ Lopez  ║║ Lopez  ║
             * ║    01:30    ║║ Break  ║║ Lopez  ║
             * ║    03:00    ║║ Math   ║║ Lopez  ║
             * ║    06:00    ║║ Math   ║║ Lopez  ║
             * ╚═════════════╝╚════════╝╚════════╝
             * 
             */


            // DateTime tDays = DateTime.Today;

            // DateTime d1 = DateTime.Now;
            // DateTime d2 = d1.AddDays(1);
            // DateTime d3 = d1.AddDays(2);
            // DateTime d4 = d1.AddDays(3);
            // DateTime d5 = d1.AddDays(4);
            // DateTime d6 = d1.AddDays(5);
            // DateTime d7 = d1.AddDays(6);
            //
            // // DateTime fDays = t.AddMonths(-1);
            // // fDays = fDays.AddDays(-1);
            //
            // Console.WriteLine(d1.DayOfWeek);
            // Console.WriteLine(d2.DayOfWeek);
            // Console.WriteLine(d3.DayOfWeek);
            // Console.WriteLine(d4.DayOfWeek);
            // Console.WriteLine(d5.DayOfWeek);
            // Console.WriteLine(d6.DayOfWeek);
            // Console.WriteLine(d7.DayOfWeek);
            //
            // Console.WriteLine($"║{$"{d1.DayOfWeek.ToString().PadLeft(((23 - 8) / 2) + 8)}",-60}║");
            // Console.WriteLine($"║{d1.DayOfWeek.ToString().PadLeft(((60 - 8) / 2) + 8).PadRight(60)}║");

            // ║      Saturday         ║
            // ║       Saturday        ║
            // Friday
            //     Saturday
            // Sunday
            //     Monday
            // Tuesday
            //     Wednesday
            // Thursday

            // Console.WriteLine(fDays.ToString("MM/dd/yyyy HH:mm:ss"));
            // var yesterday = DateTime.Today.AddDays(-1);
            // DateTime today = DateTime.Parse("2022-10-03");
            // Console.WriteLine(yesterday);

            // DateTime date = DateTime.Now;
            // DateTime d1 = DateTime.Parse("2022-10-03 19:48:52.000");

            // TimeSpan t = date - d1;
            // Console.WriteLine(date);
            // Console.WriteLine(date.Day);
            // Console.WriteLine(date.Month);
            // Console.WriteLine(d1.Day);
            // Console.WriteLine(d1.Month);
            // Console.WriteLine(t);

            // string date1 = ;
            // Console.WriteLine(date.Split('/'));

            // DateTime date1 = new DateTime(1996, 6, 3, 22, 15, 0);
            // DateTime date2 = new DateTime(1996, 12, 6, 13, 2, 0);
            //
            // TimeSpan res = date2.Subtract(date1);
            // // returns equal to 0 since d1 is equal to d2
            // Console.WriteLine(res);
            // string path = @"D:\Dev\School\Library Management System\MyTest.txt";
            //
            // try
            // {
            //     string[] lines = File.ReadAllLines(path);
            //     lines[0] = "1";
            // }
            // catch (System.IO.FileNotFoundException)
            // {
            //     StreamWriter sw = new StreamWriter(path);
            // }

            // List<string> test = new List<string>(test1);
            // test1[1] = "2 2";
            // readText = string.Join("\n", test1);
            //
            // for (int i = 0; i < test1.Length; i++)
            // {
            //     Console.WriteLine(test1[i]);
            // }
            //
            // string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            // Console.WriteLine(date);
            // Console.Write("Input title to search: ");
            // string title = Console.ReadLine();
            // Console.WriteLine(
            //     "------------------------------------------------------------------------------------------------------------------------------------------------------");
            // Console.WriteLine($"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Date",-20}|");
            // Console.WriteLine(
            //     "------------------------------------------------------------------------------------------------------------------------------------------------------");
            //
            // for (int i = 0; i < data.Length; i++)
            // {
            //     string[] output1 = data[i].Split(',');
            //     if (output1[0] == title)
            //     {
            //         Console.WriteLine(
            //             $"|{i + 1,-4}|{output1[0],-60}|{output1[1],-40}|{output1[2],-20}|{output1[3],-20}|");
            //         Console.WriteLine(
            //             "------------------------------------------------------------------------------------------------------------------------------------------------------");
            //     }
            // }
            // List<string> test = new List<string>();
            // test.Add("1");
            // test.Add("2");
            // test.Add("2");
            // test.Add("4");
            // for (int i = 0; i < test.Count; i++)
            // {
            //     if (test[i] == "2")
            //     {
            //         test.RemoveAt(i);
            //         i--;
            //     }
            // }
            //
            // foreach (string test1 in test)
            // {
            //     Console.WriteLine(test1);
            // }
            // string Repeat(string value, int count)
            // {
            //     return new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
            // }
            //
            // Console.WriteLine(
            //     $"{Repeat("-", 172)}");
            // Console.WriteLine(
            //     $"|{"ID",-4}|{"Name",-60}|{"Author",-40}|{"Category",-20}|{"Amount",-10}|{"Status",-10}|{"Date",-20}|");
            // Console.WriteLine(
            //     $"{Repeat("-", 172)}");
            // string test = "1,2,3,";
            // string[] test1 = test.Split(',');
            // Console.WriteLine(test1[3].Length == 0);
            // string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\MyTest.txt");
            // Console.WriteLine(data.Length);
            // string test = "a 1";
            // string[] test1 = test.Split(' ');
            // test1[1] = $"{int.Parse(test1[1]) + 1}";
            // Console.WriteLine(string.Join(" ", test1));
            // DateTime test = DateTime.Today;
            // string[] test1 = test.ToString("F").Split(',');
            // // Console.WriteLine(test1[0] == "Saturday");
            // string[] day = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            // int check = 0;
            //
            // int index = day.ToList().IndexOf(test1[0]);
            //
            // for (int i = index; i < day.Length; i++)
            // {
            //     Console.WriteLine(day[i]);
            //     check += 1;
            // }
            //
            // for (int i = 0; i < day.Length - check; i++)
            // {
            //     Console.WriteLine(day[i]);
            // }
            // string[] day = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            // string[] routineDay = new string[7];
            // int check = 0;
            //
            // int index = day.ToList().IndexOf(DateTime.Today.ToString("F").Split(',')[0]);
            //
            // for (int i = index; i < day.Length; i++)
            // {
            //     routineDay[check] = day[i];
            //     check += 1;
            // }
            //
            // for (int i = 0; i < day.Length - check; i++)
            // {
            //     check += i;
            //     routineDay[check] = day[i];
            //     check -= i;
            // }
            //
            // foreach (string test1 in routineDay)
            // {
            //     Console.WriteLine(test1);
            // }
            // string[] test = new string[1];
            // Console.WriteLine(test[0]);
            // Console.WriteLine(
            //     $"{"",-12}|{"",-2}{"ID",-10}|{"",-2}{"Name",-60}|{"",-2}{"Age",-5}|{"",-2}{"Sex",-6}|{"",-2}{"Phone Number",-15}|{"",-2}{"Status",-48}|{"",-2}{"Shift",-48}|");
            // Console.WriteLine(BookList.Repeat("-", 226));
            // Console.WriteLine(
            //     $"{"",-2}{"Monday".ToUpper(),-10}|{"",-2}{"",-10}|{"",-2}{"",-60}|{"",-2}{"",-5}|{"",-2}{"",-6}|{"",-2}{"",-15}|{"",-2}{"",-48}|{"",-2}{"",-48}|");
            // Console.WriteLine(
            //     $"{"",-2}{"Tuesday".ToUpper(),-10}|{"",-2}{"",-10}|{"",-2}{"",-60}|{"",-2}{"",-5}|{"",-2}{"",-6}|{"",-2}{"",-15}|{"",-2}{"",-48}|{"",-2}{"",-48}|");
            // Console.WriteLine(
            //     $"{"",-2}{"Wednesday".ToUpper(),-10}|{"",-2}{"",-10}|{"",-2}{"",-60}|{"",-2}{"",-5}|{"",-2}{"",-6}|{"",-2}{"",-15}|{"",-2}{"",-48}|{"",-2}{"",-48}|");
            // Console.WriteLine(
            //     $"{"",-2}{"Thursday".ToUpper(),-10}|{"",-2}{"",-10}|{"",-2}{"",-60}|{"",-2}{"",-5}|{"",-2}{"",-6}|{"",-2}{"",-15}|{"",-2}{"",-48}|{"",-2}{"",-48}|");
            // Console.WriteLine(
            //     $"{"",-2}{"Friday".ToUpper(),-10}|{"",-2}{"",-10}|{"",-2}{"",-60}|{"",-2}{"",-5}|{"",-2}{"",-6}|{"",-2}{"",-15}|{"",-2}{"",-48}|{"",-2}{"",-48}|");
            // Console.WriteLine(
            //     $"{"",-2}{"Saturday".ToUpper(),-10}|{"",-2}{"",-10}|{"",-2}{"",-60}|{"",-2}{"",-5}|{"",-2}{"",-6}|{"",-2}{"",-15}|{"",-2}{"",-48}|{"",-2}{"",-48}|");
            // Console.WriteLine(BookList.Repeat("-", 226));
            // Console.WriteLine("══╔═══════╗══");
            // Console.WriteLine("══╔═══════╗══");
            // Console.WriteLine("══║ Hello ║══");
            // Console.WriteLine("══╚═══════╝══");
            // Console.WriteLine("┌─┐");
            // Console.WriteLine("│1│");
            // Console.WriteLine("└─┘");
            // Console.WriteLine(
            //     $"║{"",-12}║{"",-1}{"ID",-10}║{"",-1}{"Name",-60}║{"",-1}{"Age",-5}║{"",-1}{"Sex",-6}║{"",-1}{"Phone Number",-15}║{"",-1}{"Status",-48}║{"",-1}{"Shift",-48}║");

            // int[] test = { 12, 11, 61, 6, 7, 16, 49, 49 };
            // for (int i = 0; i < test.Length; i++)
            // {
            //     Console.Write($"╔{BookList.Repeat("═",test[i])}╗");
            // }
            // // Console.WriteLine("\nA");
            // // Console.WriteLine($"╔{BookList.Repeat("═",12)}╗╔{BookList.Repeat("═",11)}╗╔{BookList.Repeat("═",61)}╗╔{BookList.Repeat("═",6)}╗");
            // Console.WriteLine(
            //     $"\n║{"",-12}║║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║║{"",-1}{"Shift",-48}║");
            // // Console.Write("║");
            // for (int i = 0; i < test.Length; i++)
            // {
            //     Console.Write($" {BookList.Repeat("═",test[i])} ");
            // }
            // Console.Write("║");
            // Console.WriteLine(
            //     $"\n║{"Monday".ToUpper(),-12}║║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║║{"",-1}{"Shift",-48}║");
            // Console.WriteLine(
            //     $"║{"Monday".ToUpper(),-12}║║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║║{"",-1}{"Shift",-48}║");
            // string[] test = { "1", "2" };
            // Console.WriteLine(test[0]);
            // string[] day = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            // int check = Array.IndexOf(day, "Tuesay");
            // Console.WriteLine(check);
        }
    }
}