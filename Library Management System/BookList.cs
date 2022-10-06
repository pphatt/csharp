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
            Console.Write("Enter book's title: ");
            string bookName = Console.ReadLine();
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            Console.Write("Enter subject: ");
            string subject = Console.ReadLine();
            Console.Write("Enter amount: ");
            int amount = int.Parse(Console.ReadLine());
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

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

            string[] aut = bookName.Split(' ');
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
                "INSERT INTO Books (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date, State) VALUES (@BookName, @BookAuthor, @BookCategory, @BookAmountAvailable, @BookAmountBorrowed, @Date, @State)";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(addDataQuery, connection);

                insertCommand.Parameters.AddWithValue("@BookName", bn.ToString());
                insertCommand.Parameters.AddWithValue("@BookAuthor", au.ToString());
                insertCommand.Parameters.AddWithValue("@BookCategory", subject);
                insertCommand.Parameters.AddWithValue("@BookAmountAvailable", amount);
                insertCommand.Parameters.AddWithValue("@BookAmountBorrowed", 0);
                insertCommand.Parameters.AddWithValue("@Date", date);
                insertCommand.Parameters.AddWithValue("@State", 0);
                insertCommand.ExecuteNonQuery();
            }
        }

        public void Show()
        {
            string queryString =
                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.date, CustomerIDs " +
                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)" +
                "where Books.State = 0";

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
            // string queryString =
            //     "Select BookIDs, BookName, BookAuthor, BookCategory, BookAmount, Date, CustomerIDs from Books where BookName like 'C++%'";

            /*
             * getTableSize is that we check if there is a book or not. If there is which means that the Count(size of table) > 1, we can query the db.
             * Otherwise we can't query the db because there is no books in the database currently.
             */

            string getTableSize = "Select Count(BookIDs) from Books where Books.State = 0";

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
                        "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                        "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                        $"where Books.BookIDs = {ids} and Books.State = 0";

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
                        "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                        "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                        $"where BookName like '%{title}%' and Books.State = 0";

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
                        "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                        "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                        $"where BookAuthor like '%{name}%' and Books.State = 0";

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
                        "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                        "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                        $"where BookCategory like '%{category}%' and Books.State = 0";

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
                                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                                "where BookAmountAvailable > 0 and Books.State = 0";

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
                                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                                "where BookAmountBorrowed > 0 and Books.State = 0";

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

                    switch (d)
                    {
                        case 1:
                            DateTime today = DateTime.Today;
                            string to = $"{today.Year}/{today.Day}/{today.Month}";

                            string gettodayBooks =
                                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                                $"where cast(Books.Date as date) = '{to}' and Books.State = 0";

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
                            DateTime yesterday = DateTime.Today.AddDays(-1);
                            string y = $"{yesterday.Year}/{yesterday.Day}/{yesterday.Month}";

                            string getyesterdayBooks =
                                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                                $"where cast(Books.Date as date) = '{y}' and Books.State = 0";

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
                            DateTime tDays = DateTime.Today.AddDays(-1);
                            DateTime fDays = DateTime.Today.AddDays(-5);

                            string t = tDays.ToString("MM/dd/yyyy 23:59:59");
                            string f = fDays.ToString("MM/dd/yyyy HH:mm:ss");

                            string getFDaysBooks =
                                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                                $"where Books.Date between '{t}' and '{f}' and Books.State = 0";

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
                            DateTime sDays = DateTime.Today.AddDays(-6);
                            DateTime week = DateTime.Today.AddDays(-7);

                            string sd = sDays.ToString("MM/dd/yyyy 23:59:59");
                            string w = week.ToString("MM/dd/yyyy HH:mm:ss");

                            string getAWeekBooks =
                                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                                $"where Books.Date between '{sd}' and '{w}' and Books.State = 0";

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
                            DateTime tWeek = DateTime.Today.AddDays(-8);
                            DateTime month = DateTime.Today.AddMonths(-1);

                            string tw = tWeek.ToString("MM/dd/yyyy 23:59:59");
                            string m = month.ToString("MM/dd/yyyy HH:mm:ss");

                            string getMonthBooks =
                                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                                $"where Books.Date between '{tw}' and '{m}' and Books.State = 0";

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
                            DateTime tMonth = DateTime.Today.AddMonths(-1);
                            tMonth = tMonth.AddDays(-1);

                            DateTime sMonth = DateTime.Today.AddMonths(-6);

                            string tm = tMonth.ToString("MM/dd/yyyy 23:59:59");
                            string sm = sMonth.ToString("MM/dd/yyyy HH:mm:ss");

                            string getSMonthBooks =
                                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                                $"where Books.Date between '{tm}' and '{sm}' and Books.State = 0";

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
                            DateTime sM = DateTime.Today.AddMonths(-6);
                            sM = sM.AddDays(-1);

                            DateTime aY = DateTime.Today.AddYears(-1);

                            string smt = sM.ToString("MM/dd/yyyy 23:59:59");
                            string ay = aY.ToString("MM/dd/yyyy HH:mm:ss");

                            string getAyBooks =
                                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                                $"where Books.Date between '{smt}' and '{ay}' and Books.State = 0";

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
                            DateTime aYr = DateTime.Today.AddYears(-1);
                            aYr = aYr.AddDays(-1);

                            DateTime tYr = DateTime.Today.AddYears(-2);

                            string ayr = aYr.ToString("MM/dd/yyyy 23:59:59");
                            string tyr = tYr.ToString("MM/dd/yyyy HH:mm:ss");

                            string getTyBooks =
                                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                                $"where Books.Date between '{ayr}' and '{tyr}' and Books.State = 0";

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
                                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                                $"where Books.Date between '{tyrd}' and '{fyr}' and Books.State = 0";

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

        public void Delete()
        {
            string getTableSize = "Select Count(BookIDs) from Books where Books.State = 0";

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
                        "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                        "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                        $"where Books.BookIDs = {ids} and Books.State = 0";

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
                                        string delBookIDs = $"update Books set Books.State = 1 where BookIDs = '{ids}'";
                                        string updateBookFromBookAmount =
                                            $"update BookAmount set State = 1 where BookIDs = '{ids}' ";

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
                        "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                        "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                        $"where BookName = '{name}' and Books.State = 0";

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
                                            $"update Books set Books.State = 1 where BookName = '{name}'";

                                        string updateBookFromBookAmount =
                                            $"update BookAmount set State = 1 where BookIDs = '{i}' ";

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
                $"select BookName, BookAuthor, BookCategory from Books where BookIDs = {ids} and Books.State = 0";

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

                    string updateTitle = $"Update Books set BookName = '{newTitle}' where BookIDs = {ids}";

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

                    string updateAuthor = $"Update Books set BookAuthor = '{newAuthor}' where BookIDs = {ids}";

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

                    string updateCategory = $"Update Books set BookCategory = '{newCategory}' where BookIDs = {ids}";

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
            Console.Write("Input IDs customer to borrow: ");
            string cids = Console.ReadLine();

            string getCustomerIDsQuery =
                "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, BookIDs " +
                "from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs) " +
                $"where Customer.CustomerIDs = {cids}";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                SqlCommand command = new SqlCommand(getCustomerIDsQuery, connection);

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
                                $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-28}║");

                            check = true;
                        }

                        if (prevIDs == $"{readerBookInfo[0]}")
                        {
                            Console.WriteLine(
                                $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[5]}",-28}║");

                            prevIDs = $"{readerBookInfo[0]}";

                            continue;
                        }

                        prevIDs = $"{readerBookInfo[0]}";

                        for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                        {
                            Console.Write($" {Repeat("═", Program.StoreLengthCustomer[k])} ");
                        }

                        string status = $"{readerBookInfo[5]}" != ""
                            ? $"║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[5]}",-28}║\n"
                            : $"║{"",-1}{"Empty",-28}║\n";

                        Console.Write(
                            $"\n║{"",-1}{readerBookInfo[0],-10}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-5}║║{"",-1}{readerBookInfo[3],-6}║║{"",-1}{readerBookInfo[4],-15}║{status}");
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
                        Console.WriteLine("Invalid ID");
                        return;
                    }
                }
            }

            Console.Write("Input ID book to borrow: ");
            int ids = int.Parse(Console.ReadLine());

            string checkIDsQuery =
                $"select BookAmountAvailable, BookAmountBorrowed from Books where BookIDs = {ids} and Books.State = 0";

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
                                $"Insert into BookAmount (BookIDs, CustomerIDs, Date) values ({ids}, {cids}, '{DateTime.Now}')";

                            SqlCommand b = new SqlCommand(borrowedBookQuery, connection);
                            b.ExecuteNonQuery();

                            string updateBookAmount =
                                "Update Books " +
                                $"set BookAmountAvailable = {newAmountAvailable}, BookAmountBorrowed = {newAmountBorrowed} " +
                                $"where BookIDs = {ids}";

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