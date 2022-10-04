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

            string addDataQuery =
                "INSERT INTO Books (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date) VALUES (@BookName, @BookAuthor, @BookCategory, @BookAmountAvailable, @BookAmountBorrowed, @Date)";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(addDataQuery, connection);

                insertCommand.Parameters.AddWithValue("@BookName", bookName);
                insertCommand.Parameters.AddWithValue("@BookAuthor", author);
                insertCommand.Parameters.AddWithValue("@BookCategory", subject);
                insertCommand.Parameters.AddWithValue("@BookAmountAvailable", amount);
                insertCommand.Parameters.AddWithValue("@BookAmountBorrowed", 0);
                insertCommand.Parameters.AddWithValue("@Date", date);
                insertCommand.ExecuteNonQuery();
            }
        }

        public void Show()
        {
            string queryString =
                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.date, CustomerIDs " +
                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)";

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
                            for (int k = 0; k < Program.StoreLength.Length; k++)
                            {
                                Console.Write($"╔{Repeat("═", Program.StoreLength[k])}╗");
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

                        for (int k = 0; k < Program.StoreLength.Length; k++)
                        {
                            Console.Write($" {Repeat("═", Program.StoreLength[k])} ");
                        }

                        string status = $"{readerBookInfo[7]}" != ""
                            ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                            : $"║{"",-1}{"Empty",-22}║\n";

                        Console.Write(
                            $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{readerBookInfo[4],-10}║║{"",-1}{readerBookInfo[5],-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                    }

                    if (check)
                    {
                        for (int k = 0; k < Program.StoreLength.Length; k++)
                        {
                            Console.Write($"╚{Repeat("═", Program.StoreLength[k])}╝");
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

            string getTableSize = "Select Count(BookIDs) from Books";

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
                        $"where Books.BookIDs = {ids}";

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
                                    for (int k = 0; k < Program.StoreLength.Length; k++)
                                    {
                                        Console.Write($"╔{Repeat("═", Program.StoreLength[k])}╗");
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

                                for (int k = 0; k < Program.StoreLength.Length; k++)
                                {
                                    Console.Write($" {Repeat("═", Program.StoreLength[k])} ");
                                }

                                string status = $"{readerBookInfo[7]}" != ""
                                    ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                    : $"║{"",-1}{"Empty",-22}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{readerBookInfo[4],-10}║║{"",-1}{readerBookInfo[5],-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLength.Length; k++)
                                {
                                    Console.Write($"╚{Repeat("═", Program.StoreLength[k])}╝");
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
                        $"where BookName like '%{title}%'";

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
                                    for (int k = 0; k < Program.StoreLength.Length; k++)
                                    {
                                        Console.Write($"╔{Repeat("═", Program.StoreLength[k])}╗");
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

                                for (int k = 0; k < Program.StoreLength.Length; k++)
                                {
                                    Console.Write($" {Repeat("═", Program.StoreLength[k])} ");
                                }

                                string status = $"{readerBookInfo[7]}" != ""
                                    ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                    : $"║{"",-1}{"Empty",-22}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{readerBookInfo[4],-10}║║{"",-1}{readerBookInfo[5],-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLength.Length; k++)
                                {
                                    Console.Write($"╚{Repeat("═", Program.StoreLength[k])}╝");
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
                        $"where BookAuthor like '%{name}%'";

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
                                    for (int k = 0; k < Program.StoreLength.Length; k++)
                                    {
                                        Console.Write($"╔{Repeat("═", Program.StoreLength[k])}╗");
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

                                for (int k = 0; k < Program.StoreLength.Length; k++)
                                {
                                    Console.Write($" {Repeat("═", Program.StoreLength[k])} ");
                                }

                                string status = $"{readerBookInfo[7]}" != ""
                                    ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                    : $"║{"",-1}{"Empty",-22}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{readerBookInfo[4],-10}║║{"",-1}{readerBookInfo[5],-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLength.Length; k++)
                                {
                                    Console.Write($"╚{Repeat("═", Program.StoreLength[k])}╝");
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
                        $"where BookCategory like '%{category}%'";

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
                                    for (int k = 0; k < Program.StoreLength.Length; k++)
                                    {
                                        Console.Write($"╔{Repeat("═", Program.StoreLength[k])}╗");
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

                                for (int k = 0; k < Program.StoreLength.Length; k++)
                                {
                                    Console.Write($" {Repeat("═", Program.StoreLength[k])} ");
                                }

                                string status = $"{readerBookInfo[7]}" != ""
                                    ? $"║{"",-1}{$"Customer's IDs: {readerBookInfo[7]}",-22}║\n"
                                    : $"║{"",-1}{"Empty",-22}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-4}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-40}║║{"",-1}{readerBookInfo[3],-20}║║{"",-1}{readerBookInfo[4],-10}║║{"",-1}{readerBookInfo[5],-10}║║{"",-1}{readerBookInfo[6],-22}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLength.Length; k++)
                                {
                                    Console.Write($"╚{Repeat("═", Program.StoreLength[k])}╝");
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
                    int number3 = int.Parse(Console.ReadLine());

                    int[] currentStoreLength = { 5, 61, 41, 21, 11, 23, 23 };

                    switch (number3)
                    {
                        case 1:
                            string getStatusQueryAvailable =
                                "select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs " +
                                "from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs) " +
                                "where BookAmountAvailable > 0";

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
                                "where BookAmountBorrowed > 0";

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
                /*
                 * one day ago
                 * five days ago
                 * week ago
                 * month ago
                 * 6 months ago
                 * year ago
                 * 2 years ago
                 * 5 years ago
                 */

                // case 6:
                //     Console.Write("Input Category to search: ");
                //
                //     string category = Console.ReadLine();
                //
                //     string getCategoryQuery =
                //         $"Select BookIDs, BookName, BookAuthor, BookCategory, BookAmount, Date, CustomerIDs from Books where BookCategory like '{category}%'";
                //
                //     using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                //     {
                //         SqlCommand command = new SqlCommand(getCategoryQuery, connection);
                //
                //         connection.Open();
                //
                //         using (SqlDataReader reader = command.ExecuteReader())
                //         {
                //             bool check = false;
                //
                //             while (reader.Read())
                //             {
                //                 if (!check)
                //                 {
                //                     for (int k = 0; k < storeLength.Length; k++)
                //                     {
                //                         Console.Write($"╔{Repeat("═", storeLength[k])}╗");
                //                     }
                //
                //                     Console.WriteLine(
                //                         $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Amount",-10}║║{"",-1}{"Date",-22}║");
                //
                //                     check = true;
                //                 }
                //
                //                 for (int k = 0; k < storeLength.Length; k++)
                //                 {
                //                     Console.Write($" {Repeat("═", storeLength[k])} ");
                //                 }
                //
                //                 Console.WriteLine(
                //                     $"\n║{"",-1}{reader[0],-4}║║{"",-1}{reader[1],-60}║║{"",-1}{reader[2],-40}║║{"",-1}{reader[3],-20}║║{"",-1}{reader[4],-10}║║{"",-1}{reader[5],-22}║");
                //             }
                //
                //             if (check)
                //             {
                //                 for (int k = 0; k < storeLength.Length; k++)
                //                 {
                //                     Console.Write($"╚{Repeat("═", storeLength[k])}╝");
                //                 }
                //
                //                 Console.WriteLine("");
                //             }
                //             else
                //             {
                //                 Console.WriteLine($"Category: {category} does not exist in the database.");
                //             }
                //         }
                //     }
                //
                //     break;
                case 7:
                    break;
                default:
                    Console.WriteLine("Invalid number");
                    break;
            }
        }

        public void Delete()
        {
            // if (data.Length == 0)
            // {
            //     Console.WriteLine("There are no data currently");
            //     return;
            // }
            //
            // Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
            // Console.WriteLine("\t\t\t\t\t\t║ 1. DELETE BY ID                        ║\t\t\t\t\t");
            // Console.WriteLine("\t\t\t\t\t\t║ 2. DELETE BY NAME                      ║\t\t\t\t\t");
            // Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
            // Console.Write("Input to use: ");
            // int number1 = int.Parse(Console.ReadLine());
            //
            // switch (number1)
            // {
            //     case 1:
            //         Console.Write("Input ID to delete: ");
            //         int number2 = int.Parse(Console.ReadLine());
            //
            //         if (number2 < 0 || number2 > data.Length)
            //         {
            //             Console.WriteLine("Invalid ID");
            //             return;
            //         }
            //
            //         List<string> output = new List<string>(data);
            //         output.RemoveAt(number2 - 1);
            //         File.WriteAllLines(@"D:\Dev\School\Library Management System\MyTest.txt", output.ToArray());
            //         Console.WriteLine("\t\t\t\t\t\t═══════════ DELETE SUCCESSFULLY ═══════════\t\t\t\t\t");
            //
            //         break;
            //     case 2:
            //         bool check = false;
            //         Console.Write("Input Name to delete: ");
            //         string name = Console.ReadLine();
            //         List<string> output1 = new List<string>(data);
            //
            //         for (int i = 0; i < output1.Count; i++)
            //         {
            //             string[] output2 = output1[i].Split(',');
            //             if (output2[0] == name)
            //             {
            //                 output1.RemoveAt(i);
            //                 i--;
            //                 check = true;
            //             }
            //         }
            //
            //         if (check == false)
            //         {
            //             Console.WriteLine($"There is no {name} in Name section in the database");
            //             return;
            //         }
            //
            //         File.WriteAllLines(@"D:\Dev\School\Library Management System\MyTest.txt", output1.ToArray());
            //         Console.WriteLine("\t\t\t\t\t\t═══════════ DELETE SUCCESSFULLY ═══════════\t\t\t\t\t");
            //
            //         break;
            //     default:
            //         Console.WriteLine("Invalid number");
            //         break;
            // }
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
                Console.Write("\nInput to use: ");
                int number1 = int.Parse(Console.ReadLine());
                if (number1 > 0 && number1 <= data.Length)
                {
                    Console.WriteLine($"\t\t\t\t\t\t          EDITING THE BOOK NO.{number1:D2} \t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 1. EDIT TITLE                          ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 2. EDIT AUTHOR                         ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 3. EDIT CATEGORY                       ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
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
                        Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATE SUCCESSFULLY ═══════════\t\t\t\t\t");
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
                int[] storeLength = { 5, 61, 41, 21, 11, 16, 21, 61 };
                int[] storeLength1 = { 11, 61, 6, 7, 16, 49 };

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

                for (int k = 0; k < storeLength1.Length; k++)
                {
                    Console.Write($"╔{Repeat("═", storeLength1[k])}╗");
                }

                Console.WriteLine(
                    $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Status",-48}║");

                for (int k = 0; k < storeLength1.Length; k++)
                {
                    Console.Write($" {Repeat("═", storeLength1[k])} ");
                }

                Console.WriteLine(
                    $"\n║{"",-1}{output1[0],-10}║║{"",-1}{output1[1],-60}║║{"",-1}{output1[2],-5}║║{"",-1}{output1[3],-6}║║{"",-1}{output1[4],-15}║║{"",-1}{output1[5],-48}║");

                for (int k = 0; k < storeLength1.Length; k++)
                {
                    Console.Write($"╚{Repeat("═", storeLength1[k])}╝");
                }

                while (true)
                {
                    Console.Write("\n");
                    Show();

                    Console.Write("\nInput ID book to borrow: ");
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
                    Console.WriteLine("\t\t\t\t\t\t═════════ BORROWED SUCCESSFULLY ═════════\t\t\t\t\t");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("There are no data currently");
            }
        }
    }
}