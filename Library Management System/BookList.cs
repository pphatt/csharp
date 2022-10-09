using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Library_Management_System
{
    public class BookList
    {
        public static string Repeat(string value, int count)
        {
            return new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
        }

        // private void CustomOutput(string option, string[] data, string name, int index)
        // {
        //     int j = 0;
        //     bool check = false;
        //     int[] storeLength = { 5, 61, 41, 21, 11, 16, 21, 61 };
        //
        //     Console.Write($"Input {name} to search: ");
        //     option = Console.ReadLine();
        //
        //     for (int i = 0; i < data.Length; i++)
        //     {
        //         string[] output1 = data[i].Split(',');
        //         if (output1[index].Contains(option))
        //         {
        //             check = true;
        //             break;
        //         }
        //     }
        //
        //     if (check == false)
        //     {
        //         Console.WriteLine($"\t\t\t\t\t     There is no {option} in {name} section in the database\t\t\t\t\t\t");
        //         return;
        //     }
        //
        //     for (int k = 0; k < storeLength.Length; k++)
        //     {
        //         Console.Write($"╔{Repeat("═", storeLength[k])}╗");
        //     }
        //
        //     Console.WriteLine(
        //         $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Amount",-10}║║{"",-1}{"Status",-15}║║{"",-1}{"Date",-20}║║{"",-1}{"Note",-60}║");
        //
        //     for (int i = 0; i < data.Length; i++)
        //     {
        //         string[] output = data[i].Split(',');
        //
        //         string[] getAmount = output[4].Split(' ');
        //         string[] getAmount1 = output[5].Split(' ');
        //
        //         string statusOutput = $"AVAILABLE ({getAmount[1]})";
        //         string noteData = "";
        //
        //         if (int.Parse(getAmount[1]) == 0)
        //         {
        //             statusOutput = $"BORROWED  ({getAmount1[1]})";
        //         }
        //         else if (int.Parse(getAmount1[1]) == 0)
        //         {
        //             statusOutput = $"AVAILABLE ({getAmount[1]})";
        //         }
        //
        //         if (int.Parse(getAmount1[1]) > 0)
        //         {
        //             string[] getNote = output[7].Split(' ');
        //             noteData = $"Borrowed by customer's IDs {getNote[0]} at {getNote[1]} {getNote[2]}";
        //         }
        //
        //         if (output[index].Contains(option))
        //         {
        //             for (int k = 0; k < storeLength.Length; k++)
        //             {
        //                 Console.Write($" {Repeat("═", storeLength[k])} ");
        //             }
        //
        //             Console.WriteLine(
        //                 $"\n║{"",-1}{j + 1,-4}║║{"",-1}{output[0],-60}║║{"",-1}{output[1],-40}║║{"",-1}{output[2],-20}║║{"",-1}{output[3],-10}║║{"",-1}{statusOutput,-15}║║{"",-1}{output[6],-20}║║{"",-1}{noteData,-60}║");
        //
        //             if (int.Parse(getAmount1[1]) > 0)
        //             {
        //                 statusOutput = "";
        //                 if (int.Parse(getAmount[1]) > 0)
        //                 {
        //                     statusOutput = $"BORROWED  ({int.Parse(output[3]) - int.Parse(getAmount[1])})";
        //                     if (output.Length == 8)
        //                     {
        //                         Console.WriteLine(
        //                             $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-1}{statusOutput,-15}║║{"",-1}{"",-20}║║{"",-1}{"",-60}║");
        //                     }
        //                 }
        //
        //                 for (int k = 8; k <= output.Length - 1; k++)
        //                 {
        //                     string[] getNote = output[k].Split(' ');
        //                     noteData = $"Borrowed by customer's IDs {getNote[0]} at {getNote[1]} {getNote[2]}";
        //                     Console.WriteLine(
        //                         $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-1}{statusOutput,-15}║║{"",-1}{"",-20}║║{"",-1}{noteData,-60}║");
        //                 }
        //             }
        //
        //             j++;
        //         }
        //     }
        //
        //     for (int k = 0; k < storeLength.Length; k++)
        //     {
        //         Console.Write($"╚{Repeat("═", storeLength[k])}╝");
        //     }
        // }

        public void Add()
        {
            DateTime date = DateTime.Now;
            int ln = 0;

            string lq = "Select Librarian.LibrarianIDs " +
                        "from (Scheduled left join Librarian on Scheduled.LibrarianIDs = Librarian.LibrarianIDs) " +
                        $"where Scheduled.DateOfWeek = '{date.DayOfWeek}' and '{date.Hour}:{date.Minute}' between TimeStart and TimeEnd ";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                connection.Open();

                SqlCommand q = new SqlCommand(lq, connection);

                using (SqlDataReader r = q.ExecuteReader())
                {
                    bool c = false;
                    while (r.Read())
                    {
                        ln = (int)r[0];
                        c = true;
                    }

                    if (!c)
                    {
                        Console.WriteLine("There is no currently active librarian.");
                        return;
                    }
                }
            }

            Console.Write("Enter book's title: ");
            string bookName = Console.ReadLine();
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            Console.Write("Enter subject: ");
            string subject = Console.ReadLine();
            Console.Write("Enter amount: ");
            int amount = int.Parse(Console.ReadLine());
            Console.Write("Enter Publishing Year: ");
            string py = Console.ReadLine();

            string[] bna = bookName.Split(' ');
            StringBuilder bn = new StringBuilder();

            for (int i = 0; i < bna.Length; i++)
            {
                string o = "";
                string nn = bna[i][0].ToString().ToUpper() + bna[i].Substring(1);

                if (i != bna.Length - 1)
                {
                    o = " ";
                }

                bn.Append(nn + o);
            }

            string[] aut = author.Split(' ');
            StringBuilder au = new StringBuilder();

            for (int i = 0; i < aut.Length; i++)
            {
                string o = "";
                string nn = aut[i][0].ToString().ToUpper() + aut[i].Substring(1);

                if (i != aut.Length - 1)
                {
                    o = " ";
                }

                au.Append(nn + o);
            }

            string addDataQuery =
                "INSERT INTO Book (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, PublishDate, Date, State, LIDs) VALUES (@BookName, @BookAuthor, @BookCategory, @BookAmountAvailable, @BookAmountBorrowed, @PublishDate, @Date, @State, @LIDs)";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(addDataQuery, connection);

                insertCommand.Parameters.AddWithValue("@BookName", bn.ToString());
                insertCommand.Parameters.AddWithValue("@BookAuthor", au.ToString());
                insertCommand.Parameters.AddWithValue("@BookCategory", subject);
                insertCommand.Parameters.AddWithValue("@BookAmountAvailable", amount);
                insertCommand.Parameters.AddWithValue("@BookAmountBorrowed", 0);
                insertCommand.Parameters.AddWithValue("@PublishDate", py);
                insertCommand.Parameters.AddWithValue("@Date", date.ToString("dd/MM/yyyy HH:mm:ss"));
                insertCommand.Parameters.AddWithValue("@State", 0);
                insertCommand.Parameters.AddWithValue("@LIDs", ln);
                insertCommand.ExecuteNonQuery();
                Console.WriteLine("\t\t\t\t\t\t═══════════ ADDED SUCCESSFULLY ═══════════\t\t\t\t\t");
            }
        }

        public void Show()
        {
            string queryString =
                "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0)" +
                "where Book.State = 0";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                {
                    bool check = false;
                    string prevIDs = "";

                    while (readerBookInfo.Read())
                    {
                        if (!check)
                        {
                            for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                            {
                                Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                            }

                            Console.WriteLine(
                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                            check = true;
                        }

                        if (prevIDs == $"{readerBookInfo[0]}")
                        {
                            Console.WriteLine(
                                $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                            prevIDs = $"{readerBookInfo[0]}";

                            continue;
                        }

                        prevIDs = $"{readerBookInfo[0]}";

                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                        {
                            Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                        }

                        string status = $"{readerBookInfo[7]}" != ""
                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                            : $"║{"",-1}{"Empty",-22}║\n";

                        Console.Write(
                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{readerBookInfo[4],-10}║║{"",-1}{readerBookInfo[5],-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                    }

                    if (check)
                    {
                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                        {
                            Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
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

        public void Search()
        {
            /*
             * getTableSize is that we check if there is a book or not. If there is which means that the Count(size of table) > 1, we can query the db.
             * Otherwise we can't query the db because there is no books in the database currently.
             */

            string getTableSize = "Select Count(BookIDs) from Book where Book.State = 0";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                SqlCommand getTableSizeCommand = new SqlCommand(getTableSize, connection);

                connection.Open();

                using (SqlDataReader reader = getTableSizeCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if ((int)reader[0] > 0)
                        {
                            Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t\t");
                            Console.WriteLine("\t\t\t\t\t\t║ 1. SEARCH BY ID                        ║\t\t\t\t\t\t");
                            Console.WriteLine("\t\t\t\t\t\t║ 2. SEARCH BY TITLE                     ║\t\t\t\t\t\t");
                            Console.WriteLine("\t\t\t\t\t\t║ 3. SEARCH BY AUTHOR                    ║\t\t\t\t\t\t");
                            Console.WriteLine("\t\t\t\t\t\t║ 4. SEARCH BY CATEGORY                  ║\t\t\t\t\t\t");
                            Console.WriteLine("\t\t\t\t\t\t║ 5. SEARCH BY STATUS                    ║\t\t\t\t\t\t");
                            Console.WriteLine("\t\t\t\t\t\t║ 6. SEARCH BY DATE                      ║\t\t\t\t\t\t");
                            Console.WriteLine("\t\t\t\t\t\t║ 7. EXIT                                ║\t\t\t\t\t\t");
                            Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t\t");
                            Console.Write("Input to use: ");
                        }
                        else
                        {
                            Console.WriteLine("There are no data currently");
                            return;
                        }
                    }
                }
            }

            int number1 = int.Parse(Console.ReadLine());

            switch (number1)
            {
                case 1:
                    Console.Write("Input Book's ID to search: ");
                    int ids = int.Parse(Console.ReadLine());

                    /*
                     * Using many other query commands can solve the problem. No need MultipleActiveResultSets.
                     * 
                     * That's it.
                     */

                    string getIDsQuery =
                        "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                        "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                        $"where Book.BookIDs = {ids} and Book.State = 0";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getIDsQuery, connection);

                        connection.Open();

                        using (SqlDataReader readerBookInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readerBookInfo.Read())
                            {
                                if (!check)
                                {
                                    for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                    {
                                        Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                {
                                    Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                }

                                string status = $"{readerBookInfo[7]}" != ""
                                    ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                    : $"║{"",-1}{"Empty",-22}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{readerBookInfo[4],-10}║║{"",-1}{readerBookInfo[5],-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                {
                                    Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                }

                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID");
                            }
                        }
                    }

                    break;
                case 2:
                    Console.Write("Input Book's Title to search: ");

                    string title = Console.ReadLine();

                    string getTitlesQuery =
                        "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                        "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                        $"where BookName like '%{title}%' and Book.State = 0";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getTitlesQuery, connection);

                        connection.Open();

                        using (SqlDataReader readerBookInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readerBookInfo.Read())
                            {
                                if (!check)
                                {
                                    for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                    {
                                        Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                {
                                    Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                }

                                string status = $"{readerBookInfo[7]}" != ""
                                    ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                    : $"║{"",-1}{"Empty",-22}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{readerBookInfo[4],-10}║║{"",-1}{readerBookInfo[5],-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                {
                                    Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                }

                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine($"There is no Book's Title ({title}) in the database");
                            }
                        }
                    }

                    break;
                case 3:
                    Console.Write("Input Author's Name to search: ");

                    string name = Console.ReadLine();

                    string getAuthorNamesQuery =
                        "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                        "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                        $"where BookAuthor like '%{name}%' and Book.State = 0";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getAuthorNamesQuery, connection);

                        connection.Open();

                        using (SqlDataReader readerBookInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readerBookInfo.Read())
                            {
                                if (!check)
                                {
                                    for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                    {
                                        Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                {
                                    Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                }

                                string status = $"{readerBookInfo[7]}" != ""
                                    ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                    : $"║{"",-1}{"Empty",-22}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{readerBookInfo[4],-10}║║{"",-1}{readerBookInfo[5],-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                {
                                    Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                }

                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine($"There is no Book's Author ({name}) in the database");
                            }
                        }
                    }

                    break;
                case 4:
                    Console.Write("Input Category to search: ");

                    string category = Console.ReadLine();

                    string getCategoryQuery =
                        "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                        "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                        $"where BookCategory like '%{category}%' and Book.State = 0";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getCategoryQuery, connection);

                        connection.Open();

                        using (SqlDataReader readerBookInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readerBookInfo.Read())
                            {
                                if (!check)
                                {
                                    for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                    {
                                        Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                {
                                    Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                }

                                string status = $"{readerBookInfo[7]}" != ""
                                    ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                    : $"║{"",-1}{"Empty",-22}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{readerBookInfo[4],-10}║║{"",-1}{readerBookInfo[5],-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                {
                                    Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                }

                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Category");
                            }
                        }
                    }

                    break;
                case 5:
                    Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 1. AVAILABLE                           ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 2. BORROWED                            ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
                    Console.Write("Input to use: ");
                    int s = int.Parse(Console.ReadLine());

                    int[] currentStoreLength = { 5, 61, 41, 21, 11, 23, 23 };

                    switch (s)
                    {
                        case 1:
                            string getStatusQueryAvailable =
                                "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                                "where BookAmountAvailable > 0 and Book.State = 0";

                            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                            {
                                SqlCommand command = new SqlCommand(getStatusQueryAvailable, connection);

                                connection.Open();

                                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                                {
                                    bool c1 = false;
                                    string prevIDs = "";

                                    while (readerBookInfo.Read())
                                    {
                                        if (!c1)
                                        {
                                            for (int k = 0; k < currentStoreLength.Length; k++)
                                            {
                                                Console.Write($"╔{Repeat("═", currentStoreLength[k])}╗");
                                            }

                                            Console.WriteLine(
                                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                            c1 = true;
                                        }

                                        if (prevIDs == $"{readerBookInfo[0]}")
                                        {
                                            Console.WriteLine(
                                                $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                            prevIDs = $"{readerBookInfo[0]}";

                                            continue;
                                        }

                                        prevIDs = $"{readerBookInfo[0]}";

                                        for (int k = 0; k < currentStoreLength.Length; k++)
                                        {
                                            Console.Write($" {Repeat("═", currentStoreLength[k])} ");
                                        }

                                        string status = $"{readerBookInfo[7]}" != ""
                                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                            : $"║{"",-1}{"Empty",-22}║\n";

                                        Console.Write(
                                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{$"{readerBookInfo[4]} ({(int)readerBookInfo[4] + (int)readerBookInfo[5]})",-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                                    }

                                    if (c1)
                                    {
                                        for (int k = 0; k < currentStoreLength.Length; k++)
                                        {
                                            Console.Write($"╚{Repeat("═", currentStoreLength[k])}╝");
                                        }

                                        Console.WriteLine("");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There is no AVAILABLE BOOK right now. Come back later!");
                                    }
                                }
                            }

                            break;
                        case 2:
                            string getStatusQueryBorrowed =
                                "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                                "where BookAmountBorrowed > 0 and Book.State = 0";

                            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                            {
                                SqlCommand command = new SqlCommand(getStatusQueryBorrowed, connection);

                                connection.Open();

                                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                                {
                                    bool c1 = false;
                                    string prevIDs = "";

                                    while (readerBookInfo.Read())
                                    {
                                        if (!c1)
                                        {
                                            for (int k = 0; k < currentStoreLength.Length; k++)
                                            {
                                                Console.Write($"╔{Repeat("═", currentStoreLength[k])}╗");
                                            }

                                            Console.WriteLine(
                                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                            c1 = true;
                                        }

                                        if (prevIDs == $"{readerBookInfo[0]}")
                                        {
                                            Console.WriteLine(
                                                $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                            prevIDs = $"{readerBookInfo[0]}";

                                            continue;
                                        }

                                        prevIDs = $"{readerBookInfo[0]}";

                                        for (int k = 0; k < currentStoreLength.Length; k++)
                                        {
                                            Console.Write($" {Repeat("═", currentStoreLength[k])} ");
                                        }

                                        string status = $"{readerBookInfo[7]}" != ""
                                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                            : $"║{"",-1}{"Empty",-22}║\n";

                                        Console.Write(
                                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{$"{readerBookInfo[5]} ({(int)readerBookInfo[4] + (int)readerBookInfo[5]})",-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                                    }

                                    if (c1)
                                    {
                                        for (int k = 0; k < currentStoreLength.Length; k++)
                                        {
                                            Console.Write($"╚{Repeat("═", currentStoreLength[k])}╝");
                                        }

                                        Console.WriteLine("");
                                    }
                                    else
                                    {
                                        Console.WriteLine("All the BOOKs are AVAILABLE.");
                                    }
                                }
                            }

                            break;
                        default:
                            Console.WriteLine("Invalid Input.");
                            return;
                    }

                    break;
                case 6:
                    Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 1. Today                               ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 2. Yesterday                           ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 3. Five days                           ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 4. A Week                              ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 5. A Month                             ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 6. Six Months                          ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 7. A Year                              ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 8. Two Years                           ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 9. Five Years                          ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
                    Console.Write("Input to use: ");
                    int d = int.Parse(Console.ReadLine());

                    DateTime today = DateTime.Today;
                    string to = $"{today.Year}/{today.Month}/{today.Day}";

                    switch (d)
                    {
                        case 1:
                            string gettodayBooks =
                                "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                                $"where datediff(day, Book.Date, '{to}') = 0 and Book.State = 0";

                            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                            {
                                SqlCommand command = new SqlCommand(gettodayBooks, connection);

                                connection.Open();

                                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                                {
                                    bool c1 = false;
                                    string prevIDs = "";

                                    while (readerBookInfo.Read())
                                    {
                                        if (!c1)
                                        {
                                            for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                            {
                                                Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                            }

                                            Console.WriteLine(
                                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                            c1 = true;
                                        }

                                        if (prevIDs == $"{readerBookInfo[0]}")
                                        {
                                            Console.WriteLine(
                                                $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                            prevIDs = $"{readerBookInfo[0]}";

                                            continue;
                                        }

                                        prevIDs = $"{readerBookInfo[0]}";

                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                        }

                                        string status = $"{readerBookInfo[7]}" != ""
                                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                            : $"║{"",-1}{"Empty",-22}║\n";

                                        Console.Write(
                                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{$"{readerBookInfo[4]}",-10}║║{"",-1}{$"{readerBookInfo[5]}",-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                                    }

                                    if (c1)
                                    {
                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                        }

                                        Console.WriteLine("");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was no BOOKs was added in the library.");
                                    }
                                }
                            }

                            break;
                        case 2:
                            string getyesterdayBooks =
                                "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                                $"where datediff(day, Book.Date, '{to}') = 1 and Book.State = 0";

                            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                            {
                                SqlCommand command = new SqlCommand(getyesterdayBooks, connection);

                                connection.Open();

                                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                                {
                                    bool c1 = false;
                                    string prevIDs = "";

                                    while (readerBookInfo.Read())
                                    {
                                        if (!c1)
                                        {
                                            for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                            {
                                                Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                            }

                                            Console.WriteLine(
                                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                            c1 = true;
                                        }

                                        if (prevIDs == $"{readerBookInfo[0]}")
                                        {
                                            Console.WriteLine(
                                                $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                            prevIDs = $"{readerBookInfo[0]}";

                                            continue;
                                        }

                                        prevIDs = $"{readerBookInfo[0]}";

                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                        }

                                        string status = $"{readerBookInfo[7]}" != ""
                                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                            : $"║{"",-1}{"Empty",-22}║\n";

                                        Console.Write(
                                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{$"{readerBookInfo[4]}",-10}║║{"",-1}{$"{readerBookInfo[5]}",-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                                    }

                                    if (c1)
                                    {
                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                        }

                                        Console.WriteLine("");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was no BOOKs was added in the library.");
                                    }
                                }
                            }

                            break;
                        case 3:
                            string getFDaysBooks =
                                "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                                $"where (datediff(day, Book.Date, '{to}') between 2 and 5) and Book.State = 0";

                            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                            {
                                SqlCommand command = new SqlCommand(getFDaysBooks, connection);

                                connection.Open();

                                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                                {
                                    bool c1 = false;
                                    string prevIDs = "";

                                    while (readerBookInfo.Read())
                                    {
                                        if (!c1)
                                        {
                                            for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                            {
                                                Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                            }

                                            Console.WriteLine(
                                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                            c1 = true;
                                        }

                                        if (prevIDs == $"{readerBookInfo[0]}")
                                        {
                                            Console.WriteLine(
                                                $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                            prevIDs = $"{readerBookInfo[0]}";

                                            continue;
                                        }

                                        prevIDs = $"{readerBookInfo[0]}";

                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                        }

                                        string status = $"{readerBookInfo[7]}" != ""
                                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                            : $"║{"",-1}{"Empty",-22}║\n";

                                        Console.Write(
                                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{$"{readerBookInfo[4]}",-10}║║{"",-1}{$"{readerBookInfo[5]}",-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                                    }

                                    if (c1)
                                    {
                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                        }

                                        Console.WriteLine("");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was no BOOKs was added in the library.");
                                    }
                                }
                            }

                            break;
                        case 4:
                            string getAWeekBooks =
                                "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                                $"where datediff(week, Book.Date, '{to}') = 1 and Book.State = 0";

                            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                            {
                                SqlCommand command = new SqlCommand(getAWeekBooks, connection);

                                connection.Open();

                                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                                {
                                    bool c1 = false;
                                    string prevIDs = "";

                                    while (readerBookInfo.Read())
                                    {
                                        if (!c1)
                                        {
                                            for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                            {
                                                Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                            }

                                            Console.WriteLine(
                                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                            c1 = true;
                                        }

                                        if (prevIDs == $"{readerBookInfo[0]}")
                                        {
                                            Console.WriteLine(
                                                $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                            prevIDs = $"{readerBookInfo[0]}";

                                            continue;
                                        }

                                        prevIDs = $"{readerBookInfo[0]}";

                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                        }

                                        string status = $"{readerBookInfo[7]}" != ""
                                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                            : $"║{"",-1}{"Empty",-22}║\n";

                                        Console.Write(
                                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{$"{readerBookInfo[4]}",-10}║║{"",-1}{$"{readerBookInfo[5]}",-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                                    }

                                    if (c1)
                                    {
                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                        }

                                        Console.WriteLine("");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was no BOOKs was added in the library.");
                                    }
                                }
                            }

                            break;
                        case 5:
                            string getMonthBooks =
                                "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                                $"where (datediff(day, Book.Date, '{to}') between 8 and 31) and Book.State = 0";

                            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                            {
                                SqlCommand command = new SqlCommand(getMonthBooks, connection);

                                connection.Open();

                                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                                {
                                    bool c1 = false;
                                    string prevIDs = "";

                                    while (readerBookInfo.Read())
                                    {
                                        if (!c1)
                                        {
                                            for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                            {
                                                Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                            }

                                            Console.WriteLine(
                                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                            c1 = true;
                                        }

                                        if (prevIDs == $"{readerBookInfo[0]}")
                                        {
                                            Console.WriteLine(
                                                $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                            prevIDs = $"{readerBookInfo[0]}";

                                            continue;
                                        }

                                        prevIDs = $"{readerBookInfo[0]}";

                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                        }

                                        string status = $"{readerBookInfo[7]}" != ""
                                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                            : $"║{"",-1}{"Empty",-22}║\n";

                                        Console.Write(
                                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{$"{readerBookInfo[4]}",-10}║║{"",-1}{$"{readerBookInfo[5]}",-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                                    }

                                    if (c1)
                                    {
                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                        }

                                        Console.WriteLine("");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was no BOOKs was added in the library.");
                                    }
                                }
                            }

                            break;
                        case 6:
                            string getSMonthBooks =
                                "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                                $"where (datediff(Month, Book.Date, '{to}') between 2 and 6) and Book.State = 0";

                            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                            {
                                SqlCommand command = new SqlCommand(getSMonthBooks, connection);

                                connection.Open();

                                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                                {
                                    bool c1 = false;
                                    string prevIDs = "";

                                    while (readerBookInfo.Read())
                                    {
                                        if (!c1)
                                        {
                                            for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                            {
                                                Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                            }

                                            Console.WriteLine(
                                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                            c1 = true;
                                        }

                                        if (prevIDs == $"{readerBookInfo[0]}")
                                        {
                                            Console.WriteLine(
                                                $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                            prevIDs = $"{readerBookInfo[0]}";

                                            continue;
                                        }

                                        prevIDs = $"{readerBookInfo[0]}";

                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                        }

                                        string status = $"{readerBookInfo[7]}" != ""
                                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                            : $"║{"",-1}{"Empty",-22}║\n";

                                        Console.Write(
                                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{$"{readerBookInfo[4]}",-10}║║{"",-1}{$"{readerBookInfo[5]}",-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                                    }

                                    if (c1)
                                    {
                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                        }

                                        Console.WriteLine("");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was no BOOKs was added in the library.");
                                    }
                                }
                            }

                            break;
                        case 7:
                            string getAyBooks =
                                "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                                $"where (datediff(Month, Book.Date, '{to}') between 7 and 12) and Book.State = 0";

                            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                            {
                                SqlCommand command = new SqlCommand(getAyBooks, connection);

                                connection.Open();

                                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                                {
                                    bool c1 = false;
                                    string prevIDs = "";

                                    while (readerBookInfo.Read())
                                    {
                                        if (!c1)
                                        {
                                            for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                            {
                                                Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                            }

                                            Console.WriteLine(
                                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                            c1 = true;
                                        }

                                        if (prevIDs == $"{readerBookInfo[0]}")
                                        {
                                            Console.WriteLine(
                                                $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                            prevIDs = $"{readerBookInfo[0]}";

                                            continue;
                                        }

                                        prevIDs = $"{readerBookInfo[0]}";

                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                        }

                                        string status = $"{readerBookInfo[7]}" != ""
                                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                            : $"║{"",-1}{"Empty",-22}║\n";

                                        Console.Write(
                                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{$"{readerBookInfo[4]}",-10}║║{"",-1}{$"{readerBookInfo[5]}",-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                                    }

                                    if (c1)
                                    {
                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                        }

                                        Console.WriteLine("");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was no BOOKs was added in the library.");
                                    }
                                }
                            }

                            break;
                        case 8:
                            string getTyBooks =
                                "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                                $"where (datediff(year, Book.Date, '{to}') between 1 and 2) and Book.State = 0";

                            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                            {
                                SqlCommand command = new SqlCommand(getTyBooks, connection);

                                connection.Open();

                                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                                {
                                    bool c1 = false;
                                    string prevIDs = "";

                                    while (readerBookInfo.Read())
                                    {
                                        if (!c1)
                                        {
                                            for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                            {
                                                Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                            }

                                            Console.WriteLine(
                                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                            c1 = true;
                                        }

                                        if (prevIDs == $"{readerBookInfo[0]}")
                                        {
                                            Console.WriteLine(
                                                $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                            prevIDs = $"{readerBookInfo[0]}";

                                            continue;
                                        }

                                        prevIDs = $"{readerBookInfo[0]}";

                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                        }

                                        string status = $"{readerBookInfo[7]}" != ""
                                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                            : $"║{"",-1}{"Empty",-22}║\n";

                                        Console.Write(
                                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{$"{readerBookInfo[4]}",-10}║║{"",-1}{$"{readerBookInfo[5]}",-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                                    }

                                    if (c1)
                                    {
                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                        }

                                        Console.WriteLine("");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was no BOOKs was added in the library.");
                                    }
                                }
                            }

                            break;
                        case 9:
                            DateTime tYrD = DateTime.Today.AddYears(-2);
                            tYrD = tYrD.AddDays(-1);

                            DateTime fYr = DateTime.Today.AddYears(-2);

                            string tyrd = tYrD.ToString("MM/dd/yyyy 23:59:59");
                            string fyr = fYr.ToString("MM/dd/yyyy HH:mm:ss");

                            string getFyBooks =
                                "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                                $"where (datediff(year, Book.Date, '{to}') between 3 and 5) and Book.State = 0";

                            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                            {
                                SqlCommand command = new SqlCommand(getFyBooks, connection);

                                connection.Open();

                                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                                {
                                    bool c1 = false;
                                    string prevIDs = "";

                                    while (readerBookInfo.Read())
                                    {
                                        if (!c1)
                                        {
                                            for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                            {
                                                Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                            }

                                            Console.WriteLine(
                                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                            c1 = true;
                                        }

                                        if (prevIDs == $"{readerBookInfo[0]}")
                                        {
                                            Console.WriteLine(
                                                $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                            prevIDs = $"{readerBookInfo[0]}";

                                            continue;
                                        }

                                        prevIDs = $"{readerBookInfo[0]}";

                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                        }

                                        string status = $"{readerBookInfo[7]}" != ""
                                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                            : $"║{"",-1}{"Empty",-22}║\n";

                                        Console.Write(
                                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{$"{readerBookInfo[4]}",-10}║║{"",-1}{$"{readerBookInfo[5]}",-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                                    }

                                    if (c1)
                                    {
                                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                        {
                                            Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                        }

                                        Console.WriteLine("");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was no BOOKs was added in the library.");
                                    }
                                }
                            }

                            break;
                        default:
                            Console.WriteLine("Invalid Input.");
                            return;
                    }

                    break;
                case 7:
                    break;
                default:
                    Console.WriteLine("Invalid number");
                    break;
            }
        }

        /*         *
         * Borrowed Multiple Books.
         * 
         */

        public void Delete()
        {
            string getTableSize = "Select Count(BookIDs) from Book where Book.State = 0";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                SqlCommand getTableSizeCommand = new SqlCommand(getTableSize, connection);

                connection.Open();

                using (SqlDataReader reader = getTableSizeCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if ((int)reader[0] > 0)
                        {
                            Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
                            Console.WriteLine("\t\t\t\t\t\t║ 1. DELETE BY ID                        ║\t\t\t\t\t");
                            Console.WriteLine("\t\t\t\t\t\t║ 2. DELETE BY NAME                      ║\t\t\t\t\t");
                            Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
                            Console.Write("Input to use: ");
                        }
                        else
                        {
                            Console.WriteLine("There are no data currently");
                            return;
                        }
                    }
                }
            }

            int number = int.Parse(Console.ReadLine());

            switch (number)
            {
                case 1:
                    Console.Write("Input ID to delete: ");
                    int ids = int.Parse(Console.ReadLine());

                    string getIDsQuery =
                        "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                        "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                        $"where Book.BookIDs = {ids} and Book.State = 0";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getIDsQuery, connection);

                        connection.Open();

                        using (SqlDataReader readerBookInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readerBookInfo.Read())
                            {
                                if (!check)
                                {
                                    for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                    {
                                        Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                {
                                    Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                }

                                string status = $"{readerBookInfo[7]}" != ""
                                    ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                    : $"║{"",-1}{"Empty",-22}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{readerBookInfo[4],-10}║║{"",-1}{readerBookInfo[5],-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                {
                                    Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                }

                                Console.WriteLine("");

                                Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
                                Console.WriteLine("\t\t\t\t\t\t║ 1. Confirm                             ║\t\t\t\t\t");
                                Console.WriteLine("\t\t\t\t\t\t║ 2. Reject                              ║\t\t\t\t\t");
                                Console.WriteLine("\t\t\t\t\t\t║ 3. Exit                                ║\t\t\t\t\t");
                                Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
                                Console.Write("Confirm that this is the book you want to delete: ");
                                int isConfirm = int.Parse(Console.ReadLine());

                                switch (isConfirm)
                                {
                                    case 1:
                                        string delBookIDs = $"update Book set Book.State = 1 where BookIDs = '{ids}'";
                                        string updateBookFromBookAmount =
                                            $"update BookLog set State = 1 where BookIDs = '{ids}' ";

                                        SqlCommand d = new SqlCommand(delBookIDs, connection);
                                        d.ExecuteNonQuery();

                                        SqlCommand u = new SqlCommand(updateBookFromBookAmount, connection);
                                        u.ExecuteNonQuery();

                                        Console.WriteLine(
                                            "\t\t\t\t\t\t═══════════ DELETE SUCCESSFULLY ═══════════\t\t\t\t\t");
                                        break;
                                    case 2:
                                        Console.WriteLine("Cancelled Successfully.");
                                        break;
                                    case 3:
                                        return;
                                    default:
                                        Console.WriteLine("Invalid confirmation.");
                                        return;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID");
                            }
                        }
                    }

                    break;
                case 2:
                    Console.Write("Input Name to delete: ");
                    string name = Console.ReadLine();
                    int i = 0;

                    string getNameQuery =
                        "select Book.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Book.Date, CustomerIDs " +
                        "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0) " +
                        $"where BookName = '{name}' and Book.State = 0";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getNameQuery, connection);

                        connection.Open();

                        using (SqlDataReader readerBookInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readerBookInfo.Read())
                            {
                                if (!check)
                                {
                                    i = (int)readerBookInfo[0];

                                    for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                    {
                                        Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-22}║║{"",-1}{"Status",-22}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-5}║║{"",-61}║║{"",-41}║║{"",-21}║║{"",-11}║║{"",-11}║║{"",-23}║║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                {
                                    Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                                }

                                string status = $"{readerBookInfo[7]}" != ""
                                    ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                    : $"║{"",-1}{"Empty",-22}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{readerBookInfo[4],-10}║║{"",-1}{readerBookInfo[5],-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                                {
                                    Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                                }

                                Console.WriteLine("");

                                Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
                                Console.WriteLine("\t\t\t\t\t\t║ 1. Confirm                             ║\t\t\t\t\t");
                                Console.WriteLine("\t\t\t\t\t\t║ 2. Reject                              ║\t\t\t\t\t");
                                Console.WriteLine("\t\t\t\t\t\t║ 3. Exit                                ║\t\t\t\t\t");
                                Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
                                Console.Write("Confirm that this is the book you want to delete: ");
                                int isConfirm = int.Parse(Console.ReadLine());

                                switch (isConfirm)
                                {
                                    case 1:
                                        string delBookIDs =
                                            $"update Book set Book.State = 1 where BookName = '{name}'";

                                        string updateBookFromBookAmount =
                                            $"update BookLog set State = 1 where BookIDs = '{i}' ";

                                        SqlCommand d = new SqlCommand(delBookIDs, connection);
                                        d.ExecuteNonQuery();

                                        SqlCommand u = new SqlCommand(updateBookFromBookAmount, connection);
                                        u.ExecuteNonQuery();

                                        Console.WriteLine(
                                            "\t\t\t\t\t\t═══════════ DELETE SUCCESSFULLY ═══════════\t\t\t\t\t");
                                        break;
                                    case 2:
                                        Console.WriteLine("Cancelled Successfully.");
                                        break;
                                    case 3:
                                        return;
                                    default:
                                        Console.WriteLine("Invalid confirmation.");
                                        return;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Name");
                            }
                        }
                    }

                    break;
                default:
                    Console.WriteLine("Invalid number");
                    break;
            }
        }

        public void Edit()
        {
            Show();

            Console.Write("\nInput IDs to use: ");

            int ids = int.Parse(Console.ReadLine());
            string title = "";
            string author = "";
            string category = "";

            string checkIDsQuery =
                $"select BookName, BookAuthor, BookCategory from Book where BookIDs = {ids} and Book.State = 0";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                bool check = false;
                SqlCommand command = new SqlCommand(checkIDsQuery, connection);

                connection.Open();

                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                {
                    while (readerBookInfo.Read())
                    {
                        check = true;

                        title = $"{readerBookInfo[0]}";
                        author = $"{readerBookInfo[1]}";
                        category = $"{readerBookInfo[2]}";
                    }

                    if (!check)
                    {
                        Console.WriteLine("Invalid ID");
                        return;
                    }
                }
            }

            Console.WriteLine($"\t\t\t\t\t\t          EDITING THE BOOK NO.{ids:D2} \t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 1. EDIT TITLE                          ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 2. EDIT AUTHOR                         ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 3. EDIT CATEGORY                       ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
            Console.Write("Input to edit: ");

            int number = int.Parse(Console.ReadLine());

            switch (number)
            {
                case 1:
                    Console.Write($"Changing from {title} to: ");
                    string newTitle = Console.ReadLine();

                    string[] bna = newTitle.Split(' ');
                    StringBuilder bn = new StringBuilder();

                    for (int i = 0; i < bna.Length; i++)
                    {
                        string o = "";
                        string nn = bna[i][0].ToString().ToUpper() + bna[i].Substring(1);

                        if (i != bna.Length - 1)
                        {
                            o = " ";
                        }

                        bn.Append(nn + o);
                    }

                    string updateTitle = $"Update Book set BookName = '{bn}' where BookIDs = {ids}";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(updateTitle, connection);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATE SUCCESSFULLY ═══════════\t\t\t\t\t");

                    break;
                case 2:
                    Console.Write($"Changing from {author} to: ");
                    string newAuthor = Console.ReadLine();

                    string[] aut = newAuthor.Split(' ');
                    StringBuilder au = new StringBuilder();

                    for (int i = 0; i < aut.Length; i++)
                    {
                        string o = "";
                        string nn = aut[i][0].ToString().ToUpper() + aut[i].Substring(1);

                        if (i != aut.Length - 1)
                        {
                            o = " ";
                        }

                        au.Append(nn + o);
                    }

                    string updateAuthor = $"Update Book set BookAuthor = '{au}' where BookIDs = {ids}";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(updateAuthor, connection);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATE SUCCESSFULLY ═══════════\t\t\t\t\t");

                    break;
                case 3:
                    Console.Write($"Changing from {category} to: ");
                    string newCategory = Console.ReadLine();

                    string updateCategory = $"Update Book set BookCategory = '{newCategory}' where BookIDs = {ids}";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(updateCategory, connection);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATE SUCCESSFULLY ═══════════\t\t\t\t\t");

                    break;
                default:
                    return;
            }
        }

        public void Borrowed()
        {
            DateTime date = DateTime.Now;
            int ln = 0;

            string lq = "Select Librarian.LibrarianIDs " +
                        "from (Scheduled left join Librarian on Scheduled.LibrarianIDs = Librarian.LibrarianIDs) " +
                        $"where Scheduled.DateOfWeek = '{date.DayOfWeek}' and '{date.Hour}:{date.Minute}' between TimeStart and TimeEnd ";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                connection.Open();

                SqlCommand q = new SqlCommand(lq, connection);

                using (SqlDataReader r = q.ExecuteReader())
                {
                    bool c = false;
                    while (r.Read())
                    {
                        ln = (int)r[0];
                        c = true;
                    }

                    if (!c)
                    {
                        Console.WriteLine("There is no currently active librarian.");
                        return;
                    }
                }
            }

            Console.Write("Input Customer's IDs to search: ");
            string ids = Console.ReadLine();

            string getCustomerInfoByIDs =
                "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                $"where Customer.State = 0 and Customer.CustomerIDs = {ids}";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                SqlCommand command = new SqlCommand(getCustomerInfoByIDs, connection);

                connection.Open();

                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                {
                    bool check = false;
                    string prevIDs = "";

                    while (readerBookInfo.Read())
                    {
                        if (!check)
                        {
                            for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                            {
                                Console.Write($"╔{Repeat("═", Program.StoreLengthCustomer[k])}╗");
                            }

                            Console.WriteLine(
                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-7}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                            check = true;
                        }

                        if (prevIDs == $"{readerBookInfo[0]}")
                        {
                            Console.WriteLine(
                                $"║{"",-5}║║{"",-61}║║{"",-6}║║{"",-8}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║");

                            prevIDs = $"{readerBookInfo[0]}";

                            continue;
                        }

                        prevIDs = $"{readerBookInfo[0]}";

                        for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                        {
                            Console.Write($" {Repeat("═", Program.StoreLengthCustomer[k])} ");
                        }

                        string status = $"{readerBookInfo[6]}" != ""
                            ? $"║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║\n"
                            : $"║{"",-1}{"Empty",-25}║\n";

                        Console.Write(
                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-5}║║{"",-1}{readerBookInfo[3],-7}║║{"",-1}{readerBookInfo[4],-15}║║{"",-1}{readerBookInfo[5],-23}║{status}");
                    }

                    if (check)
                    {
                        for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                        {
                            Console.Write($"╚{Repeat("═", Program.StoreLengthCustomer[k])}╝");
                        }

                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Customer's IDs or the IDs does not exist");
                        return;
                    }
                }
            }

            Console.Write("Input ID book to borrow: ");
            int id = int.Parse(Console.ReadLine());

            string checkIDsQuery =
                $"select BookAmountAvailable, BookAmountBorrowed from Book where BookIDs = {id} and Book.State = 0";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                bool check = false;
                SqlCommand command = new SqlCommand(checkIDsQuery, connection);

                connection.Open();

                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                {
                    while (readerBookInfo.Read())
                    {
                        if ((int)readerBookInfo[0] > 1)
                        {
                            int newAmountAvailable = (int)readerBookInfo[0] - 1;
                            int newAmountBorrowed = (int)readerBookInfo[1] + 1;
                            check = true;

                            string borrowedBookQuery =
                                $"Insert into BookLog (BookIDs, CustomerIDs, DateBorrow, LIDsCheckIn, DateReturn, LIDsCheckOut, State) values ({id}, {ids}, '{DateTime.Now}', {ln}, null, null, 0)";

                            SqlCommand b = new SqlCommand(borrowedBookQuery, connection);
                            b.ExecuteNonQuery();

                            string updateBookAmount =
                                "Update Book " +
                                $"set BookAmountAvailable = {newAmountAvailable}, BookAmountBorrowed = {newAmountBorrowed} " +
                                $"where BookIDs = {id}";

                            SqlCommand u = new SqlCommand(updateBookAmount, connection);
                            u.ExecuteNonQuery();
                        }
                    }

                    if (!check)
                    {
                        Console.WriteLine("Invalid ID");
                        return;
                    }
                }
            }

            Console.WriteLine("\t\t\t\t\t\t═════════ BORROWED SUCCESSFULLY ═════════\t\t\t\t\t");
        }
    }
}