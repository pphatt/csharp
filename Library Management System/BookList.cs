using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Library_Management_System
{
    public class BookList
    {
        public static string Repeat(string value, int count)
        {
            return new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
        }

        private void HandleStoredProcedure(string functionName, string[] listFunctionParameter,
            string[] listValueParameter)
        {
            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                connection.Open();
                SqlCommand a = new SqlCommand(functionName, connection);
                a.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < listValueParameter.Length; i++)
                {
                    a.Parameters.Add(new SqlParameter(listFunctionParameter[i], listValueParameter[i]));
                }

                a.ExecuteNonQuery();
            }
        }

        private string HandleStoredProcedureMessage(string functionName, string[] listFunctionParameter,
            string[] listValueParameter)
        {
            string i = "";
            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                connection.Open();
                SqlCommand a = new SqlCommand(functionName, connection);
                a.CommandType = CommandType.StoredProcedure;

                for (int j = 0; j < listValueParameter.Length; j++)
                {
                    a.Parameters.Add(new SqlParameter(listFunctionParameter[j], listValueParameter[j]));
                }

                connection.InfoMessage += SqlInfoMessageEventHandler;

                void SqlInfoMessageEventHandler(object sender, SqlInfoMessageEventArgs e)
                {
                    i = e.Message;
                }

                a.ExecuteNonQuery();
            }

            return i;
        }

        // for trigger when something went wrong
        private void DisplaySqlErrors(SqlException exception)
        {
            for (int i = 0; i < exception.Errors.Count; i++)
            {
                Console.WriteLine("Index #" + i + "\n" +
                                  "Source: " + exception.Errors[i].Source + "\n" +
                                  "Number: " + exception.Errors[i].Number.ToString() + "\n" +
                                  "State: " + exception.Errors[i].State.ToString() + "\n" +
                                  "Class: " + exception.Errors[i].Class.ToString() + "\n" +
                                  "Server: " + exception.Errors[i].Server + "\n" +
                                  "Message: " + exception.Errors[i].Message + "\n" +
                                  "Procedure: " + exception.Errors[i].Procedure + "\n" +
                                  "LineNumber: " + exception.Errors[i].LineNumber.ToString());
            }

            Console.ReadLine();
        }

        public static bool DisplayTable(string queryString, string yieldErrorMessage)
        {
            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                {
                    bool check = false;
                    string prevIDs = "";

                    string id = "";
                    string n = "";
                    string a = "";
                    string b = "";
                    string c = "";
                    string d = "";
                    List<string> LA = new List<string>();
                    List<int> LC = new List<int>();

                    while (readerBookInfo.Read())
                    {
                        if (!check)
                        {
                            for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                            {
                                Console.Write($"╔{Repeat("═", Program.StoreLengthBooks[k])}╗");
                            }

                            Console.WriteLine(
                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-75}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-22}║");

                            check = true;
                        }

                        if (prevIDs == $"{readerBookInfo[0]}" || prevIDs == "")
                        {
                            string i = $"{readerBookInfo[7]}" == "" ? "0" : $"{readerBookInfo[7]}";

                            if (id == "")
                            {
                                id = $"{readerBookInfo[0]}";
                            }

                            if (n == "")
                            {
                                n = $"{readerBookInfo[1]}";
                            }

                            if (a == "")
                            {
                                a = $"{readerBookInfo[4]}";
                            }

                            if (b == "")
                            {
                                b = $"{readerBookInfo[5]}";
                            }

                            if (c == "")
                            {
                                c = $"{readerBookInfo[3]}";
                            }

                            if (d == "")
                            {
                                d = $"{readerBookInfo[6]}";
                            }

                            if (!LA.Contains(readerBookInfo[2]))
                            {
                                LA.Add($"{readerBookInfo[2]}");
                            }

                            if (!LC.Contains(Int32.Parse(i)))
                            {
                                LC.Add(Int32.Parse(i));
                            }

                            prevIDs = $"{readerBookInfo[0]}";
                            continue;
                        }

                        int length = LA.Count > LC.Count ? LA.Count : LC.Count;

                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                        {
                            Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                        }

                        string status = LC.Count != 0 && LC[0] != 0
                            ? $"║{"",-1}{$"Customer's IDs: {LC[0]}",-22}║"
                            : $"║{"",-1}{"Empty",-22}║";

                        Console.Write(
                            $"\n║{"",-1}{id,-4}║║{"",-1}{n,-75}║║{"",-1}{LA[0],-40}║║{"",-1}{c,-20}║║{"",-1}{a,-10}║║{"",-1}{b,-10}║║{"",-1}{d,-23}║{status}");

                        for (int i = 1; i < length; i++)
                        {
                            string s = LC.Count > i
                                ? $"║{"",-1}{$"Customer's IDs: {LC[i]}",-22}║"
                                : $"║{"",-1}{"",-22}║";

                            string am = LA.Count > i
                                ? $"║{"",-1}{$"{LA[i]}",-40}║"
                                : $"║{"",-1}{"",-40}║";

                            Console.Write(
                                $"\n║{"",-5}║║{"",-76}║{am}║{"",-21}║║{"",-11}║║{"",-11}║║{"",-24}║{s}");
                        }

                        Console.WriteLine("");

                        string ia = $"{readerBookInfo[7]}" == "" ? "0" : $"{readerBookInfo[7]}";

                        id = $"{readerBookInfo[0]}";
                        n = $"{readerBookInfo[1]}";
                        a = $"{readerBookInfo[4]}";
                        b = $"{readerBookInfo[5]}";
                        c = $"{readerBookInfo[3]}";
                        d = $"{readerBookInfo[6]}";
                        LA = new List<string> { $"{readerBookInfo[2]}" };
                        LC = new List<int> { Int32.Parse(ia) };

                        prevIDs = $"{readerBookInfo[0]}";
                    }

                    if (check)
                    {
                        int length = LA.Count > LC.Count ? LA.Count : LC.Count;

                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                        {
                            Console.Write($" {Repeat("═", Program.StoreLengthBooks[k])} ");
                        }

                        string status = LC.Count != 0 && LC[0] != 0
                            ? $"║{"",-1}{$"Customer's IDs: {LC[0]}",-22}║"
                            : $"║{"",-1}{"Empty",-22}║";

                        Console.Write(
                            $"\n║{"",-1}{id,-4}║║{"",-1}{n,-75}║║{"",-1}{LA[0],-40}║║{"",-1}{c,-20}║║{"",-1}{a,-10}║║{"",-1}{b,-10}║║{"",-1}{d,-23}║{status}");

                        for (int i = 1; i < length; i++)
                        {
                            string s = LC.Count > i
                                ? $"║{"",-1}{$"Customer's IDs: {LC[i]}",-22}║"
                                : $"║{"",-1}{"",-22}║";

                            string am = LA.Count > i
                                ? $"║{"",-1}{$"{LA[i]}",-40}║"
                                : $"║{"",-1}{"",-40}║";

                            Console.Write(
                                $"\n║{"",-5}║║{"",-76}║{am}║{"",-21}║║{"",-11}║║{"",-11}║║{"",-24}║{s}");
                        }

                        Console.WriteLine("");

                        for (int k = 0; k < Program.StoreLengthBooks.Length; k++)
                        {
                            Console.Write($"╚{Repeat("═", Program.StoreLengthBooks[k])}╝");
                        }

                        Console.WriteLine("");
                        return true;
                    }

                    Console.WriteLine(yieldErrorMessage);
                    return false;
                }
            }
        }

        private void DisplayTableAddOns(string getStatusQueryAvailable, string yieldErrorMessage, int number)
        {
            int[] currentStoreLength = { 5, 76, 41, 21, 11, 24, 23 };
            bool check = false;

            string id = "";
            string n = "";
            string a = "";
            string b = "";
            string c = "";
            string d = "";
            List<string> LA = new List<string>();
            List<int> LC = new List<int>();

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                SqlCommand command = new SqlCommand(getStatusQueryAvailable, connection);

                connection.Open();

                using (SqlDataReader readerBookInfo = command.ExecuteReader())
                {
                    string prevIDs = "";

                    while (readerBookInfo.Read())
                    {
                        if (!check)
                        {
                            for (int k = 0; k < currentStoreLength.Length; k++)
                            {
                                Console.Write($"╔{Repeat("═", currentStoreLength[k])}╗");
                            }

                            if (number == 1)
                            {
                                Console.WriteLine(
                                    $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-75}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Available",-10}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-22}║");
                            }
                            else
                            {
                                Console.WriteLine(
                                    $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-75}║║{"",-1}{"Author",-40}║║{"",-1}{"Category",-20}║║{"",-1}{"Borrowed",-10}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-22}║");
                            }

                            check = true;
                        }

                        if (prevIDs == $"{readerBookInfo[0]}" || prevIDs == "")
                        {
                            string i = $"{readerBookInfo[7]}" == "" ? "0" : $"{readerBookInfo[7]}";

                            if (id == "")
                            {
                                id = $"{readerBookInfo[0]}";
                            }

                            if (n == "")
                            {
                                n = $"{readerBookInfo[1]}";
                            }

                            if (a == "")
                            {
                                a = $"{readerBookInfo[4]}";
                            }

                            if (b == "")
                            {
                                b = $"{readerBookInfo[5]}";
                            }

                            if (c == "")
                            {
                                c = $"{readerBookInfo[3]}";
                            }

                            if (d == "")
                            {
                                d = $"{readerBookInfo[6]}";
                            }

                            if (!LA.Contains(readerBookInfo[2]))
                            {
                                LA.Add($"{readerBookInfo[2]}");
                            }

                            if (!LC.Contains(Int32.Parse(i)))
                            {
                                LC.Add(Int32.Parse(i));
                            }

                            prevIDs = $"{readerBookInfo[0]}";
                            continue;
                        }

                        int length = LA.Count > LC.Count ? LA.Count : LC.Count;

                        for (int k = 0; k < currentStoreLength.Length; k++)
                        {
                            Console.Write($" {Repeat("═", currentStoreLength[k])} ");
                        }

                        string status = LC.Count != 0 && LC[0] != 0
                            ? $"║{"",-1}{$"Customer's IDs: {LC[0]}",-22}║"
                            : $"║{"",-1}{"Empty",-22}║";

                        if (number == 1)
                        {
                            Console.Write(
                                $"\n║{"",-1}{id,-4}║║{"",-1}{n,-75}║║{"",-1}{LA[0],-40}║║{"",-1}{c,-20}║║{"",-1}{$"{readerBookInfo[4]} ({(int)readerBookInfo[4] + (int)readerBookInfo[5]})",-10}║║{"",-1}{d,-23}║{status}");
                        }
                        else
                        {
                            Console.Write(
                                $"\n║{"",-1}{id,-4}║║{"",-1}{n,-75}║║{"",-1}{LA[0],-40}║║{"",-1}{c,-20}║║{"",-1}{$"{readerBookInfo[4]} ({(int)readerBookInfo[4] + (int)readerBookInfo[5]})",-10}║║{"",-1}{d,-23}║{status}");
                        }

                        for (int i = 1; i < length; i++)
                        {
                            string ss = LC.Count > i
                                ? $"║{"",-1}{$"Customer's IDs: {LC[i]}",-22}║"
                                : $"║{"",-1}{"",-22}║";

                            string am = LA.Count > i
                                ? $"║{"",-1}{$"{LA[i]}",-40}║"
                                : $"║{"",-1}{"",-40}║";

                            Console.Write(
                                $"\n║{"",-5}║║{"",-76}║{am}║{"",-21}║║{"",-11}║║{"",-24}║{ss}");
                        }

                        Console.WriteLine("");

                        string ia = $"{readerBookInfo[7]}" == "" ? "0" : $"{readerBookInfo[7]}";

                        id = $"{readerBookInfo[0]}";
                        n = $"{readerBookInfo[1]}";
                        a = $"{readerBookInfo[4]}";
                        b = $"{readerBookInfo[5]}";
                        c = $"{readerBookInfo[3]}";
                        d = $"{readerBookInfo[6]}";
                        LA = new List<string> { $"{readerBookInfo[2]}" };
                        LC = new List<int> { Int32.Parse(ia) };

                        prevIDs = $"{readerBookInfo[0]}";
                    }

                    if (check)
                    {
                        int length = LA.Count > LC.Count ? LA.Count : LC.Count;

                        for (int k = 0; k < currentStoreLength.Length; k++)
                        {
                            Console.Write($" {Repeat("═", currentStoreLength[k])} ");
                        }

                        string status = LC.Count != 0 && LC[0] != 0
                            ? $"║{"",-1}{$"Customer's IDs: {LC[0]}",-22}║"
                            : $"║{"",-1}{"Empty",-22}║";

                        if (number == 1)
                        {
                            Console.Write(
                                $"\n║{"",-1}{id,-4}║║{"",-1}{n,-75}║║{"",-1}{LA[0],-40}║║{"",-1}{c,-20}║║{"",-1}{$"{a} ({Int32.Parse(a) + Int32.Parse(b)})",-10}║║{"",-1}{d,-23}║{status}");
                        }
                        else
                        {
                            Console.Write(
                                $"\n║{"",-1}{id,-4}║║{"",-1}{n,-75}║║{"",-1}{LA[0],-40}║║{"",-1}{c,-20}║║{"",-1}{$"{b} ({Int32.Parse(a) + Int32.Parse(b)})",-10}║║{"",-1}{d,-23}║{status}");
                        }

                        for (int i = 1; i < length; i++)
                        {
                            string ss = LC.Count > i
                                ? $"║{"",-1}{$"Customer's IDs: {LC[i]}",-22}║"
                                : $"║{"",-1}{"",-22}║";

                            string am = LA.Count > i
                                ? $"║{"",-1}{$"{LA[i]}",-40}║"
                                : $"║{"",-1}{"",-40}║";

                            Console.Write(
                                $"\n║{"",-5}║║{"",-76}║{am}║{"",-21}║║{"",-11}║║{"",-24}║{ss}");
                        }

                        Console.WriteLine("");

                        for (int k = 0; k < currentStoreLength.Length; k++)
                        {
                            Console.Write($"╚{Repeat("═", currentStoreLength[k])}╝");
                        }

                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine(yieldErrorMessage);
                    }
                }
            }
        }

        public void Add()
        {
            DateTime date = DateTime.Now;

            string[] param = { };
            string[] vparam = { };

            string ln = HandleStoredProcedureMessage("GetLibrarian", param, vparam);

            if (ln == " ")
            {
                Console.WriteLine("There is no currently active librarian...");
                return;
            }

            string addDataQuery =
                "INSERT INTO Book (BookName, CategoryIDs, BookAmountAvailable, BookAmountBorrowed, Date, PublishDate, State, LIDs) VALUES (@BookName, @CategoryIDs, @BookAmountAvailable, @BookAmountBorrowed, @Date, @PublishDate, @State, @LIDs)";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(addDataQuery, connection);

                try
                {
                    Console.Write("Enter book's title: ");
                    string bookName = Console.ReadLine();

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

                    insertCommand.Parameters.AddWithValue("@BookName", bn.ToString());

                    Console.Write("Enter author: ");
                    string author = Console.ReadLine();

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

                    Console.Write("Enter Category: ");
                    string ca = Console.ReadLine();
                    string cai = "";

                    string[] cat = ca.Split(' ');
                    StringBuilder cas = new StringBuilder();

                    for (int i = 0; i < cat.Length; i++)
                    {
                        string o = "";
                        string nn = cat[i][0].ToString().ToUpper() + cat[i].Substring(1);

                        if (i != cat.Length - 1)
                        {
                            o = " ";
                        }

                        cas.Append(nn + o);
                    }

                    string[] paramName = { "@category_name" };
                    string[] paramValue = { cas.ToString() };

                    insertCommand.Parameters.AddWithValue("@CategoryIDs",
                        HandleStoredProcedureMessage("CheckCategory", paramName, paramValue));

                    Console.Write("Enter amount: ");
                    int amount = int.Parse(Console.ReadLine());

                    Console.Write("Enter Publishing Year: ");
                    string py = Console.ReadLine();

                    insertCommand.Parameters.AddWithValue("@BookAmountAvailable", amount);
                    insertCommand.Parameters.AddWithValue("@BookAmountBorrowed", 0);
                    insertCommand.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd HH:mm:ss"));
                    insertCommand.Parameters.AddWithValue("@PublishDate", py);
                    insertCommand.Parameters.AddWithValue("@State", 0);
                    insertCommand.Parameters.AddWithValue("@LIDs", ln);
                    insertCommand.ExecuteNonQuery();

                    string aaa =
                        "insert into BookAuthor (BookIDs, AuthorIDs) values ((select Count(BookIDs) from Book), @ai)";
                    SqlCommand ac = new SqlCommand(aaa, connection);

                    string[] paramName1 = { "@author_name" };
                    string[] paramValue1 = { au.ToString() };

                    ac.Parameters.AddWithValue("@ai",
                        HandleStoredProcedureMessage("CheckAuthor", paramName1, paramValue1));
                    ac.ExecuteNonQuery();

                    Console.WriteLine("\t\t\t\t\t\t═══════════ ADDED SUCCESSFULLY ═══════════\t\t\t\t\t");
                }
                catch (SqlException e)
                {
                    DisplaySqlErrors(e);
                }
            }
        }

        public void Show()
        {
            string queryString =
                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                "where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs " +
                "order by BookIDs";

            DisplayTable(queryString, "There are no data currently...");
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
                        "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                        "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                        $"where Book.BookIDs = {ids} and Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs";

                    DisplayTable(getIDsQuery, "Invalid IDs");

                    break;
                case 2:
                    Console.Write("Input Book's Title to search: ");

                    string title = Console.ReadLine();

                    string getTitlesQuery =
                        "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                        "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                        $"where BookName like '%{title}%' and Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs " +
                        "order by BookIDs";

                    DisplayTable(getTitlesQuery, $"There is no Book's Title ({title}) in the database...");

                    break;
                case 3:
                    Console.Write("Input Author's Name to search: ");

                    string name = Console.ReadLine();

                    string getAuthorNamesQuery =
                        "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                        "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                        "where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs " +
                        $"and Book.BookIDs in (select Book.BookIDs from Book, Author, BookAuthor where Author.Name like '%{name}%' and Book.BookIDs = BookAuthor.BookIDs and AuthorIDs = Author.IDs) " +
                        "order by BookIDs";

                    DisplayTable(getAuthorNamesQuery, $"There is no Book's Author ({name}) in the database");

                    break;
                case 4:
                    Console.Write("Input Category to search: ");

                    string category = Console.ReadLine();

                    string getCategoryQuery =
                        "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                        "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                        $"where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs and Category.Name = '{category}' " +
                        "order by BookIDs";

                    DisplayTable(getCategoryQuery, "Invalid Category");

                    break;
                case 5:
                    Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 1. AVAILABLE                           ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t║ 2. BORROWED                            ║\t\t\t\t\t");
                    Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
                    Console.Write("Input to use: ");
                    int sss = int.Parse(Console.ReadLine());

                    switch (sss)
                    {
                        case 1:
                            string getStatusQueryAvailable =
                                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                                "where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs and BookAmountAvailable > 0 " +
                                "order by BookIDs";

                            DisplayTableAddOns(getStatusQueryAvailable,
                                "There is no AVAILABLE BOOK right now. Come back later!", sss);

                            break;
                        case 2:
                            string getStatusQueryBorrowed =
                                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                                "where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs and BookAmountBorrowed > 0 " +
                                "order by BookIDs";

                            DisplayTableAddOns(getStatusQueryBorrowed,
                                "All the BOOKs are AVAILABLE...", sss);

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
                            string getTodayBooks =
                                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                                $"where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs and datediff(day, Book.Date, '{to}') = 0 " +
                                "order by BookIDs";

                            DisplayTable(getTodayBooks, "There was no BOOKs was added in the library...");

                            break;
                        case 2:
                            string getYesterdayBooks =
                                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                                $"where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs and datediff(day, Book.Date, '{to}') = 1";

                            DisplayTable(getYesterdayBooks, "There was no BOOKs was added in the library...");

                            break;
                        case 3:
                            string getFDaysBooks =
                                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                                $"where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs and (datediff(day, Book.Date, '{to}') between 2 and 5)";

                            DisplayTable(getFDaysBooks, "There was no BOOKs was added in the library...");

                            break;
                        case 4:
                            string getAWeekBooks =
                                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                                $"where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs and datediff(week, Book.Date, '{to}') = 1";

                            DisplayTable(getAWeekBooks, "There was no BOOKs was added in the library...");

                            break;
                        case 5:
                            string getMonthBooks =
                                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                                $"where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs and (datediff(day, Book.Date, '{to}') between 8 and 31)";

                            DisplayTable(getMonthBooks, "There was no BOOKs was added in the library...");

                            break;
                        case 6:
                            string getSMonthBooks =
                                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                                $"where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs and (datediff(Month, Book.Date, '{to}') between 2 and 6)";

                            DisplayTable(getSMonthBooks, "There was no BOOKs was added in the library...");

                            break;
                        case 7:
                            string getAyBooks =
                                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                                $"where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs and (datediff(Month, Book.Date, '{to}') between 7 and 12)";

                            DisplayTable(getAyBooks, "There was no BOOKs was added in the library...");

                            break;
                        case 8:
                            string getTyBooks =
                                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                                $"where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs and (datediff(year, Book.Date, '{to}') between 1 and 2)";

                            DisplayTable(getTyBooks, "There was no BOOKs was added in the library...");

                            break;
                        case 9:
                            string getFyBooks =
                                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                                $"where Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs and (datediff(year, Book.Date, '{to}') between 1 and 2)";

                            DisplayTable(getFyBooks, "There was no BOOKs was added in the library...");

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
                        "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                        "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                        $"where Book.BookIDs = {ids} and Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs";

                    if (DisplayTable(getIDsQuery, "Invalid IDs"))
                    {
                        using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                        {
                            connection.Open();

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
                    }

                    break;
                case 2:
                    Console.Write("Input Name to delete: ");
                    string title = Console.ReadLine();
                    int i = 0;

                    string getTitlesQuery =
                        "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                        "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                        $"where BookName = '{title}' and Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs";

                    if (DisplayTable(getTitlesQuery, "Invalid Name"))
                    {
                        using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                        {
                            connection.Open();

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
                                        $"update Book set Book.State = 1 where BookName = '{title}'";

                                    string updateBookFromBookAmount =
                                        $"update BookLog set State = 1 where BookIDs = (select BookIDs from Book where BookName = '{title}') ";

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
                $"select BookName, Author.Name, Category.Name from Book, Author, BookAuthor, Category where Book.BookIDs = BookAuthor.BookIDs and Author.IDs = AuthorIDs and CategoryIDs = Category.IDs and Book.BookIDs = {ids} and Book.State = 0";

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

                    string[] listParameterA = { "@author_name", "@book_id" };
                    string[] listParameterValueA = { newAuthor, $"{ids}" };

                    HandleStoredProcedure("EditAuthor", listParameterA, listParameterValueA);

                    Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATE SUCCESSFULLY ═══════════\t\t\t\t\t");

                    break;
                case 3:
                    Console.Write($"Changing from {category} to: ");
                    string newCategory = Console.ReadLine();

                    string[] listParameterC = { "@category_name", "@book_id" };
                    string[] listParameterValueC = { newCategory, $"{ids}" };

                    HandleStoredProcedure("EditCategory", listParameterC, listParameterValueC);

                    Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATE SUCCESSFULLY ═══════════\t\t\t\t\t");

                    break;
                default:
                    return;
            }
        }

        public void Borrowed()
        {
            string[] param = { };
            string[] vparam = { };

            string ln = HandleStoredProcedureMessage("GetLibrarian", param, vparam);

            if (ln == " ")
            {
                Console.WriteLine("There is no currently active librarian...");
                return;
            }

            Console.Write("Input Customer's IDs to search: ");
            string ids = Console.ReadLine();

            string getCustomerInfoByIDs =
                "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                $"where Customer.State = 0 and Customer.CustomerIDs = {ids}";

            CustomerData.DisplayTable(getCustomerInfoByIDs, "Invalid Customer's IDs");

            Console.Write("Input ID book to borrow: ");
            int id = int.Parse(Console.ReadLine());
            
            string getIDsQuery =
                "select Book.BookIDs, BookName, Author.Name, Category.Name, BookAmountAvailable, BookAmountBorrowed, Book.date, CustomerIDs " +
                "from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0), Category, Author, BookAuthor " +
                $"where Book.BookIDs = {id} and Book.State = 0 and Category.IDs = Book.CategoryIDs and Author.IDs = BookAuthor.AuthorIDs and BookAuthor.BookIDs = Book.BookIDs";

            if (DisplayTable(getIDsQuery, "Invalid IDs"))
            {
                using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                {
                    connection.Open();
                    string borrowedBookQuery =
                        $"Insert into BookLog (BookIDs, CustomerIDs, DateBorrow, LIDsCheckIn, DateReturn, LIDsCheckOut, State) values ({id}, {ids}, '{DateTime.Now}', {ln}, null, null, 0)";

                    SqlCommand b = new SqlCommand(borrowedBookQuery, connection);
                    b.ExecuteNonQuery();
                    
                    Console.WriteLine("\t\t\t\t\t\t═════════ BORROWED SUCCESSFULLY ═════════\t\t\t\t\t");
                }
            }
        }

        public async Task FetchBookData()
        {
            DateTime date = DateTime.Now;
            string[] param = { };
            string[] vparam = { };

            string ln = HandleStoredProcedureMessage("GetLibrarian", param, vparam);

            if (ln == " ")
            {
                Console.WriteLine("There is no currently active librarian...");
                return;
            }

            Console.Write("Insert the amount of books you want to fetch (max is 35): ");
            int n = Int32.Parse(Console.ReadLine());

            if (n > 35 || n < 1)
            {
                Console.WriteLine("Invalid amount...");
                return;
            }

            int tm;
            int m = 0;

            if (n == 25)
            {
                tm = 25;
            }
            else if (n < 25)
            {
                tm = n;
            }
            else
            {
                tm = 25;
                m = n - tm;
            }

            int[] id = { 35243, 44227, 9115, 651, 21525, 104039, 34053, 100448, 74697, 91941 };
            var baseAddress = new Uri("https://api.jikan.moe/v4/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                using (var response = await httpClient.GetAsync($"top/manga?limit={tm}"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var a = JObject.Parse(responseData);

                    foreach (var c in a["data"])
                    {
                        string bn = $"{c["title"]}";
                        string au = $"{c["authors"][0]["name"]}";
                        string subject = "Comic";
                        Random rnd = new Random();
                        int amount = rnd.Next(20, 100);
                        string py =
                            $"{c["published"]["prop"]["from"]["year"]}-{c["published"]["prop"]["from"]["month"]}-{c["published"]["prop"]["from"]["day"]}";

                        string addDataQuery =
                            "INSERT INTO Book (BookName, CategoryIDs, BookAmountAvailable, BookAmountBorrowed, Date, PublishDate, State, LIDs) VALUES (@BookName, @CategoryIDs, @BookAmountAvailable, @BookAmountBorrowed, @Date, @PublishDate, @State, @LIDs)";

                        using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                        {
                            connection.Open();

                            SqlCommand insertCommand = new SqlCommand(addDataQuery, connection);

                            insertCommand.Parameters.AddWithValue("@BookName", bn);

                            string[] paramName = { "@category_name" };
                            string[] paramValue = { subject };

                            insertCommand.Parameters.AddWithValue("@CategoryIDs",
                                HandleStoredProcedureMessage("CheckCategory", paramName, paramValue));
                            insertCommand.Parameters.AddWithValue("@BookAmountAvailable", amount);
                            insertCommand.Parameters.AddWithValue("@BookAmountBorrowed", 0);
                            insertCommand.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd HH:mm:ss"));
                            insertCommand.Parameters.AddWithValue("@PublishDate", py);
                            insertCommand.Parameters.AddWithValue("@State", 0);
                            insertCommand.Parameters.AddWithValue("@LIDs", ln);
                            insertCommand.ExecuteNonQuery();

                            string aaa =
                                "insert into BookAuthor (BookIDs, AuthorIDs) values ((select Count(BookIDs) from Book), @ai)";
                            SqlCommand ac = new SqlCommand(aaa, connection);

                            string[] paramName1 = { "@author_name" };
                            string[] paramValue1 = { au };
                            
                            ac.Parameters.AddWithValue("@ai",
                                HandleStoredProcedureMessage("CheckAuthor", paramName1, paramValue1));
                            ac.ExecuteNonQuery();
                        }
                    }
                }
            }

            for (var i = 0; i < m; i++)
            {
                var baseAddress1 = new Uri("https://api.jikan.moe/v4/");

                using (var httpClient = new HttpClient { BaseAddress = baseAddress1 })
                {
                    using (var response = await httpClient.GetAsync($"manga/{id[i]}/full"))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var a = JObject.Parse(responseData);

                        string bn = a["data"]["title"].ToString();
                        string au = $"{a["data"]["authors"][0]["name"]}";
                        string subject = "Comic";
                        string amount = "20";
                        string p =
                            $"{a["data"]["published"]["prop"]["from"]["year"]}-{a["data"]["published"]["prop"]["from"]["month"]}-{a["data"]["published"]["prop"]["from"]["day"]}";

                        string addDataQuery =
                            "INSERT INTO Book (BookName, CategoryIDs, BookAmountAvailable, BookAmountBorrowed, Date, PublishDate, State, LIDs) VALUES (@BookName, @CategoryIDs, @BookAmountAvailable, @BookAmountBorrowed, @Date, @PublishDate, @State, @LIDs)";

                        using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                        {
                            connection.Open();

                            SqlCommand insertCommand = new SqlCommand(addDataQuery, connection);

                            insertCommand.Parameters.AddWithValue("@BookName", bn);
                            
                            string[] paramName = { "@category_name" };
                            string[] paramValue = { subject };
                            
                            insertCommand.Parameters.AddWithValue("@CategoryIDs",
                                HandleStoredProcedureMessage("CheckCategory", paramName, paramValue));
                            insertCommand.Parameters.AddWithValue("@BookAmountAvailable", amount);
                            insertCommand.Parameters.AddWithValue("@BookAmountBorrowed", 0);
                            insertCommand.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd HH:mm:ss"));
                            insertCommand.Parameters.AddWithValue("@PublishDate", p);
                            insertCommand.Parameters.AddWithValue("@State", 0);
                            insertCommand.Parameters.AddWithValue("@LIDs", ln);
                            insertCommand.ExecuteNonQuery();

                            string aaa =
                                "insert into BookAuthor (BookIDs, AuthorIDs) values ((select Count(BookIDs) from Book), @ai)";
                            SqlCommand ac = new SqlCommand(aaa, connection);

                            string[] paramName1 = { "@author_name" };
                            string[] paramValue1 = { au };
                            
                            ac.Parameters.AddWithValue("@ai",
                                HandleStoredProcedureMessage("CheckAuthor", paramName1, paramValue1));
                            ac.ExecuteNonQuery();
                        }
                    }
                }
            }

            Console.WriteLine("\t\t\t\t\t\tYOU JUST CAN INSERT AUTOMATICALLY ONCE TIMES\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t═══════════ ADDED SUCCESSFULLY ═══════════\t\t\t\t\t");
        }
    }
}