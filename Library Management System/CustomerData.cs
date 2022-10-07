using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Text;

namespace Library_Management_System
{
    public class CustomerData
    {
        public void AddCustomer()
        {
            Console.Write("Enter customer's name: ");
            string name = Console.ReadLine();
            Console.Write("Enter customer's age: ");
            string age = Console.ReadLine();
            Console.Write("Enter customer's sex: ");
            string sex = Console.ReadLine();
            Console.Write("Enter customer's phone number: ");
            string phoneNumber = Console.ReadLine();
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            string[] na = name.Split(' ');
            StringBuilder n = new StringBuilder();

            for (int i = 0; i < na.Length; i++)
            {
                string o = "";
                string nn = na[i][0].ToString().ToUpper() + na[i].Substring(1);

                if (i != na.Length - 1)
                {
                    o = " ";
                }

                n.Append(nn + o);
            }

            string addDataQuery =
                "INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Date, State) VALUES (@CustomerName, @CustomerAge, @CustomerSex, @CustomerPhoneNumber, @Date, @State)";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand insertCommand = new SqlCommand(addDataQuery, connection);

                    insertCommand.Parameters.AddWithValue("@CustomerName", n.ToString());
                    insertCommand.Parameters.AddWithValue("@CustomerAge", age);
                    insertCommand.Parameters.AddWithValue("@CustomerSex",
                        sex[0].ToString().ToUpper() + sex.Substring(1));
                    insertCommand.Parameters.AddWithValue("@CustomerPhoneNumber", phoneNumber);
                    insertCommand.Parameters.AddWithValue("@Date", date);
                    insertCommand.Parameters.AddWithValue("@State", 0);
                    insertCommand.ExecuteNonQuery();
                    Console.WriteLine("\t\t\t\t\t\t═══════════ ADDED SUCCESSFULLY ═══════════\t\t\t\t\t");
                }
                catch (SqlException ex)
                {
                    string[] t = ex.Errors[0].Message.Split(' ');
                    string t1 = t[t.Length - 1];
                    string error = t1.Substring(1, t1.Length - 3);

                    switch (error)
                    {
                        case "CustomerAge":
                            Console.WriteLine("Errors at Customer's Age. Invalid Age");
                            break;
                        case "CustomerSex":
                            Console.WriteLine("Errors at Customer's Sex. Invalid Sex (just Male and Female).");
                            break;
                        case "CustomerPhoneNumber":
                            Console.WriteLine("Errors at Customer's PhoneNumber. Invalid PhoneNumber");
                            break;
                    }
                }
            }
        }

        public void ShowCustomer()
        {
            string queryString =
                "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                "where Customer.State = 0";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                using (SqlDataReader readCustomerInfo = command.ExecuteReader())
                {
                    bool check = false;
                    string prevIDs = "";

                    while (readCustomerInfo.Read())
                    {
                        if (!check)
                        {
                            for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                            {
                                Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                            }

                            Console.WriteLine(
                                $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                            check = true;
                        }

                        if (prevIDs == $"{readCustomerInfo[0]}")
                        {
                            Console.WriteLine(
                                $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║");

                            prevIDs = $"{readCustomerInfo[0]}";

                            continue;
                        }

                        prevIDs = $"{readCustomerInfo[0]}";

                        for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                        {
                            Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                        }

                        string status = $"{readCustomerInfo[6]}" != ""
                            ? $"║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║\n"
                            : $"║{"",-1}{"Empty",-25}║\n";

                        Console.Write(
                            $"\n║{"",-1}{readCustomerInfo[0],-10}║║{"",-1}{readCustomerInfo[1],-60}║║{"",-1}{readCustomerInfo[2],-5}║║{"",-1}{readCustomerInfo[3],-6}║║{"",-1}{readCustomerInfo[4],-15}║║{"",-1}{readCustomerInfo[5],-23}║{status}");
                    }

                    if (check)
                    {
                        for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                        {
                            Console.Write($"╚{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╝");
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

        public void SearchCustomer()
        {
            Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 1. SEARCH BY IDs                       ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 2. SEARCH BY NAME                      ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 3. SEARCH BY AGE                       ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 4. SEARCH BY SEX                       ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 5. SEARCH BY PHONE NUMBER              ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 6. EXIT                                ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
            Console.Write("Input to use: ");
            int number1 = int.Parse(Console.ReadLine());

            switch (number1)
            {
                case 1:
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

                        using (SqlDataReader readCustomerInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readCustomerInfo.Read())
                            {
                                if (!check)
                                {
                                    for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                    {
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readCustomerInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║");

                                    prevIDs = $"{readCustomerInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readCustomerInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readCustomerInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readCustomerInfo[0],-10}║║{"",-1}{readCustomerInfo[1],-60}║║{"",-1}{readCustomerInfo[2],-5}║║{"",-1}{readCustomerInfo[3],-6}║║{"",-1}{readCustomerInfo[4],-15}║║{"",-1}{readCustomerInfo[5],-23}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($"╚{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╝");
                                }

                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Customer's IDs or the IDs does not exist");
                            }
                        }
                    }

                    break;
                case 2:
                    Console.Write("Input Customer's Name to search: ");
                    string name = Console.ReadLine();

                    string getCustomerInfoByName =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                        "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                        $"where Customer.State = 0 and CustomerName like '%{name}%'";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getCustomerInfoByName, connection);

                        connection.Open();

                        using (SqlDataReader readCustomerInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readCustomerInfo.Read())
                            {
                                if (!check)
                                {
                                    for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                    {
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readCustomerInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║");

                                    prevIDs = $"{readCustomerInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readCustomerInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readCustomerInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readCustomerInfo[0],-10}║║{"",-1}{readCustomerInfo[1],-60}║║{"",-1}{readCustomerInfo[2],-5}║║{"",-1}{readCustomerInfo[3],-6}║║{"",-1}{readCustomerInfo[4],-15}║║{"",-1}{readCustomerInfo[5],-23}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($"╚{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╝");
                                }

                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Customer's Name");
                            }
                        }
                    }

                    break;
                case 3:
                    Console.Write("Input Customer's Age to search: ");
                    string age = Console.ReadLine();

                    string getCustomerInfoByAge =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                        "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                        $"where Customer.State = 0 and Customer.CustomerAge = '{age}'";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getCustomerInfoByAge, connection);

                        connection.Open();

                        using (SqlDataReader readCustomerInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readCustomerInfo.Read())
                            {
                                if (!check)
                                {
                                    for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                    {
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readCustomerInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║");

                                    prevIDs = $"{readCustomerInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readCustomerInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readCustomerInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readCustomerInfo[0],-10}║║{"",-1}{readCustomerInfo[1],-60}║║{"",-1}{readCustomerInfo[2],-5}║║{"",-1}{readCustomerInfo[3],-6}║║{"",-1}{readCustomerInfo[4],-15}║║{"",-1}{readCustomerInfo[5],-23}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($"╚{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╝");
                                }

                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Customer's Age");
                            }
                        }
                    }

                    break;
                case 4:
                    Console.Write("Input Customer's Sex to search: ");
                    string sex = Console.ReadLine();

                    string getCustomerInfoBySex =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                        "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                        $"where Customer.State = 0 and Customer.CustomerSex = '{sex}'";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getCustomerInfoBySex, connection);

                        connection.Open();

                        using (SqlDataReader readCustomerInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readCustomerInfo.Read())
                            {
                                if (!check)
                                {
                                    for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                    {
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readCustomerInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║");

                                    prevIDs = $"{readCustomerInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readCustomerInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readCustomerInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readCustomerInfo[0],-10}║║{"",-1}{readCustomerInfo[1],-60}║║{"",-1}{readCustomerInfo[2],-5}║║{"",-1}{readCustomerInfo[3],-6}║║{"",-1}{readCustomerInfo[4],-15}║║{"",-1}{readCustomerInfo[5],-23}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($"╚{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╝");
                                }

                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Customer's Sex");
                            }
                        }
                    }

                    break;
                case 5:
                    Console.Write("Input Customer's PhoneNumber to search: ");
                    string pn = Console.ReadLine();

                    string getCustomerInfoByPhoneNumber =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                        "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                        $"where Customer.State = 0 and Customer.CustomerSex = '{pn}'";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getCustomerInfoByPhoneNumber, connection);

                        connection.Open();

                        using (SqlDataReader readCustomerInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readCustomerInfo.Read())
                            {
                                if (!check)
                                {
                                    for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                    {
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readCustomerInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║");

                                    prevIDs = $"{readCustomerInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readCustomerInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readCustomerInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readCustomerInfo[0],-10}║║{"",-1}{readCustomerInfo[1],-60}║║{"",-1}{readCustomerInfo[2],-5}║║{"",-1}{readCustomerInfo[3],-6}║║{"",-1}{readCustomerInfo[4],-15}║║{"",-1}{readCustomerInfo[5],-23}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($"╚{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╝");
                                }

                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Customer's Phone Number");
                            }
                        }
                    }

                    break;
                case 6:
                    break;
                default:
                    Console.WriteLine("Invalid number");
                    break;
            }
        }

        public void ReturnBook()
        {
            Console.Write("Input Customer's IDs to return: ");
            int ids = int.Parse(Console.ReadLine());

            string getCustomerInfoByIDs =
                "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                $"where Customer.State = 0 and Customer.CustomerIDs = {ids}";

            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                SqlCommand command = new SqlCommand(getCustomerInfoByIDs, connection);

                connection.Open();

                using (SqlDataReader readCustomerInfo = command.ExecuteReader())
                {
                    bool check = false;
                    string prevIDs = "";

                    while (readCustomerInfo.Read())
                    {
                        if ($"{readCustomerInfo[6]}" == "")
                        {
                            Console.WriteLine("This Customer does not borrow any books.");
                            return;
                        }

                        if (!check)
                        {
                            for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                            {
                                Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                            }

                            Console.WriteLine(
                                $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                            check = true;
                        }

                        if (prevIDs == $"{readCustomerInfo[0]}")
                        {
                            Console.WriteLine(
                                $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║");

                            prevIDs = $"{readCustomerInfo[0]}";

                            continue;
                        }

                        prevIDs = $"{readCustomerInfo[0]}";

                        for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                        {
                            Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                        }

                        string status = $"{readCustomerInfo[6]}" != ""
                            ? $"║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║\n"
                            : $"║{"",-1}{"Empty",-25}║\n";

                        Console.Write(
                            $"\n║{"",-1}{readCustomerInfo[0],-10}║║{"",-1}{readCustomerInfo[1],-60}║║{"",-1}{readCustomerInfo[2],-5}║║{"",-1}{readCustomerInfo[3],-6}║║{"",-1}{readCustomerInfo[4],-15}║║{"",-1}{readCustomerInfo[5],-23}║{status}");
                    }

                    if (check)
                    {
                        for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                        {
                            Console.Write($"╚{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╝");
                        }

                        Console.WriteLine("");

                        Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t║ 1. Confirm                             ║\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t║ 2. Reject                              ║\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t║ 3. Exit                                ║\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
                        Console.Write("Confirm this is you: ");
                        int isConfirm = int.Parse(Console.ReadLine());

                        switch (isConfirm)
                        {
                            case 1:
                                while (true)
                                {
                                    Console.Write("Input IDs book to return: ");
                                    string idb = Console.ReadLine();

                                    string s =
                                        "Select BookLog.BookIDs, CustomerIDs, BookAmountAvailable, BookAmountBorrowed " +
                                        "from (BookLog left join Book on Book.BookIDs = BookLog.BookIDs) " +
                                        $"where BookLog.BookIDs = '{idb}' and CustomerIDs = '{ids}'";

                                    SqlCommand c = new SqlCommand(s, connection);

                                    using (SqlDataReader t = c.ExecuteReader())
                                    {
                                        bool ch = false;

                                        while (t.Read())
                                        {
                                            ch = true;

                                            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                                            string ud =
                                                $"update BookLog set State = 1, DateReturn = '{date}' where CustomerIDs = '{ids}' and BookIDs = '{idb}'";

                                            int ab = (int)t[2];
                                            int bb = (int)t[3];

                                            string a =
                                                $"update Book set BookAmountAvailable = {ab + 1} where BookIDs = '{idb}'";
                                            string b =
                                                $"update Book set BookAmountBorrowed = {bb + 1} where BookIDs = '{idb}'";

                                            SqlCommand u = new SqlCommand(ud, connection);
                                            u.ExecuteNonQuery();

                                            SqlCommand abq = new SqlCommand(a, connection);
                                            abq.ExecuteNonQuery();

                                            SqlCommand bbq = new SqlCommand(b, connection);
                                            bbq.ExecuteNonQuery();

                                            Console.WriteLine(
                                                "\t\t\t\t\t\t═══════════ RETURN SUCCESSFULLY ═══════════\t\t\t\t\t");

                                            Console.WriteLine("");
                                        }

                                        if (!ch)
                                        {
                                            Console.WriteLine("Invalid Books IDs.");
                                        }

                                        Console.WriteLine(
                                            "\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
                                        Console.WriteLine(
                                            "\t\t\t\t\t\t║ 1. Confirm                             ║\t\t\t\t\t");
                                        Console.WriteLine(
                                            "\t\t\t\t\t\t║ 2. Reject                              ║\t\t\t\t\t");
                                        Console.WriteLine(
                                            "\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");

                                        Console.Write("Do you want to continue returning: ");
                                        int n = int.Parse(Console.ReadLine());

                                        switch (n)
                                        {
                                            case 1:
                                                break;
                                            default:
                                                return;
                                        }
                                    }
                                }
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
                        Console.WriteLine("Invalid Customer's IDs or the IDs does not exist");
                    }
                }
            }
        }

        public void DeleteCustomer()
        {
            string getTableSize = "Select Count(CustomerIDs) from Customer where Customer.State = 0";

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

            int n = int.Parse(Console.ReadLine());

            switch (n)
            {
                case 1:
                    Console.Write("Input Customer's IDs to delete: ");
                    string ids = Console.ReadLine();

                    List<string> bi = new List<string>();

                    string getCustomerInfoByIDs =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                        "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                        $"where Customer.State = 0 and Customer.CustomerIDs = '{ids}'";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getCustomerInfoByIDs, connection);

                        connection.Open();

                        using (SqlDataReader readCustomerInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readCustomerInfo.Read())
                            {
                                if (!check)
                                {
                                    for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                    {
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                bi.Add($"{readCustomerInfo[6]}");

                                if (prevIDs == $"{readCustomerInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║");

                                    prevIDs = $"{readCustomerInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readCustomerInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readCustomerInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readCustomerInfo[0],-10}║║{"",-1}{readCustomerInfo[1],-60}║║{"",-1}{readCustomerInfo[2],-5}║║{"",-1}{readCustomerInfo[3],-6}║║{"",-1}{readCustomerInfo[4],-15}║║{"",-1}{readCustomerInfo[5],-23}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($"╚{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╝");
                                }

                                int length = bi.Count;

                                if (bi[0] != "")
                                {
                                    StringBuilder m = new StringBuilder();

                                    for (int k = 0; k < length; k++)
                                    {
                                        m.Append($"{bi[k]}");

                                        if (k != length - 1)
                                        {
                                            m.Append(", ");
                                        }
                                    }

                                    Console.WriteLine(
                                        "\nThis Customer has not returned all the books yet. Please make sure return all the books to get the deletion." +
                                        $"\nBooks IDs: {m}.");
                                    return;
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
                                        string delCustomer =
                                            $"update Customer set State = 1 where CustomerIDs = '{ids}'";

                                        string ud =
                                            $"update BookLog set State = 1 where CustomerIDs = '{ids}'";

                                        SqlCommand d = new SqlCommand(delCustomer, connection);
                                        d.ExecuteNonQuery();

                                        SqlCommand u = new SqlCommand(ud, connection);
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
                    List<string> b = new List<string>();

                    string getCustomerInfoByName =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                        "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                        $"where Customer.State = 0 and Customer.CustomerName = '{name}'";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getCustomerInfoByName, connection);

                        connection.Open();

                        using (SqlDataReader readCustomerInfo = command.ExecuteReader())
                        {
                            bool check = false;
                            string prevIDs = "";

                            while (readCustomerInfo.Read())
                            {
                                if (!check)
                                {
                                    i = (int)readCustomerInfo[0];

                                    for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                    {
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                b.Add($"{readCustomerInfo[6]}");

                                if (prevIDs == $"{readCustomerInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║");

                                    prevIDs = $"{readCustomerInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readCustomerInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readCustomerInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readCustomerInfo[0],-10}║║{"",-1}{readCustomerInfo[1],-60}║║{"",-1}{readCustomerInfo[2],-5}║║{"",-1}{readCustomerInfo[3],-6}║║{"",-1}{readCustomerInfo[4],-15}║║{"",-1}{readCustomerInfo[5],-23}║{status}");
                            }

                            if (check)
                            {
                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($"╚{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╝");
                                }

                                int length = b.Count;

                                if (length > 0)
                                {
                                    StringBuilder m = new StringBuilder();

                                    for (int k = 0; k < length; k++)
                                    {
                                        m.Append($"{b[k]}");

                                        if (k != length - 1)
                                        {
                                            m.Append(", ");
                                        }
                                    }

                                    Console.WriteLine(
                                        "\nThis Customer has not returned all the books yet. Please make sure return all the books to get the deletion." +
                                        $"\nBooks IDs: {m}.");
                                    return;
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
                                        string delCustomer =
                                            $"update Customer set State = 1 where CustomerName = '{name}'";

                                        string ud =
                                            $"update BookLog set State = 1 where CustomerIDs = '{i}'";

                                        SqlCommand d = new SqlCommand(delCustomer, connection);
                                        d.ExecuteNonQuery();

                                        SqlCommand u = new SqlCommand(ud, connection);
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

        public void EditCustomer()
        {
            ShowCustomer();
            Console.Write("\nInput IDs to edit: ");
            string ids = Console.ReadLine();

            string name = "";
            string age = "";
            string sex = "";
            string pn = "";

            string checkIDsQuery =
                $"select CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber from Customer where CustomerIDs = '{ids}'";

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

                        name = $"{readerBookInfo[0]}";
                        age = $"{readerBookInfo[1]}";
                        sex = $"{readerBookInfo[2]}";
                        pn = $"{readerBookInfo[3]}";
                    }

                    if (!check)
                    {
                        Console.WriteLine("Invalid ID");
                        return;
                    }
                }
            }

            Console.WriteLine($"\t\t\t\t\t\t          EDITING THE BOOK NO.{ids} \t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 1. EDIT NAME                           ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 2. EDIT AGE                            ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 3. EDIT SEX                            ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 4. EDIT PHONE NUMBER                   ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t║ 5. EXIT                                ║\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
            Console.Write("Input to edit: ");
            int n = int.Parse(Console.ReadLine());

            switch (n)
            {
                case 1:
                    Console.Write($"Changing Name from {name} to: ");
                    string newName = Console.ReadLine();

                    string[] na = newName.Split(' ');
                    StringBuilder nn = new StringBuilder();

                    for (int i = 0; i < na.Length; i++)
                    {
                        string o = "";
                        string nc = na[i][0].ToString().ToUpper() + na[i].Substring(1);

                        if (i != na.Length - 1)
                        {
                            o = " ";
                        }

                        nn.Append(nc + o);
                    }

                    string updateName = $"Update Customer set CustomerName = '{newName}' where CustomerIDs = {ids}";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(updateName, connection);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATE SUCCESSFULLY ═══════════\t\t\t\t\t");

                    break;
                case 2:
                    Console.Write($"Changing Age from {age} to: ");
                    int newAge = int.Parse(Console.ReadLine());

                    if (newAge <= 18 || newAge >= 80)
                    {
                        Console.WriteLine("Invalid Age");
                        return;
                    }

                    string updateAge = $"Update Customer set CustomerAge = '{newAge}' where CustomerIDs = {ids}";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(updateAge, connection);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATE SUCCESSFULLY ═══════════\t\t\t\t\t");

                    break;
                case 3:
                    Console.Write($"Changing Sex from {sex} to: ");
                    string newSex = Console.ReadLine();

                    if (newSex != "Male" || newSex != "Female")
                    {
                        Console.WriteLine("Invalid Sex");
                        return;
                    }

                    string updateSex =
                        $"Update Customer set CustomerAge = '{newSex[0].ToString().ToUpper() + sex.Substring(1)}' where CustomerIDs = {ids}";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(updateSex, connection);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATE SUCCESSFULLY ═══════════\t\t\t\t\t");

                    break;
                case 4:
                    Console.Write($"Changing PhoneNumber from {pn} to: ");
                    string newPn = Console.ReadLine();

                    if (newPn.Length < 10 || newPn.Length > 11)
                    {
                        Console.WriteLine("Invalid Phone Number");
                        return;
                    }

                    string updatePn = $"Update Customer set CustomerPhoneNumber = '{newPn}' where CustomerIDs = {ids}";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(updatePn, connection);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATE SUCCESSFULLY ═══════════\t\t\t\t\t");

                    break;
                default:
                    return;
            }
        }
    }
}