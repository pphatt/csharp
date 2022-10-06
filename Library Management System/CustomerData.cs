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
                "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookAmount.BookIDs " +
                "from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs) " +
                "where Customer.State = 0";

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
                            for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                            {
                                Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                            }

                            Console.WriteLine(
                                $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                            check = true;
                        }

                        if (prevIDs == $"{readerBookInfo[0]}")
                        {
                            Console.WriteLine(
                                $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║");

                            prevIDs = $"{readerBookInfo[0]}";

                            continue;
                        }

                        prevIDs = $"{readerBookInfo[0]}";

                        for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                        {
                            Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                        }

                        string status = $"{readerBookInfo[6]}" != ""
                            ? $"║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║\n"
                            : $"║{"",-1}{"Empty",-25}║\n";

                        Console.Write(
                            $"\n║{"",-1}{readerBookInfo[0],-10}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-5}║║{"",-1}{readerBookInfo[3],-6}║║{"",-1}{readerBookInfo[4],-15}║║{"",-1}{readerBookInfo[5],-23}║{status}");
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
                        Console.WriteLine("Invalid ID");
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
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookAmount.BookIDs " +
                        "from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs) " +
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
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readerBookInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-10}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-5}║║{"",-1}{readerBookInfo[3],-6}║║{"",-1}{readerBookInfo[4],-15}║║{"",-1}{readerBookInfo[5],-23}║{status}");
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
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookAmount.BookIDs " +
                        "from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs) " +
                        $"where Customer.State = 0 and CustomerName like '%{name}%'";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getCustomerInfoByName, connection);

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
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readerBookInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-10}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-5}║║{"",-1}{readerBookInfo[3],-6}║║{"",-1}{readerBookInfo[4],-15}║║{"",-1}{readerBookInfo[5],-23}║{status}");
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
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookAmount.BookIDs " +
                        "from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs) " +
                        $"where Customer.State = 0 and Customer.CustomerAge = '{age}'";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getCustomerInfoByAge, connection);

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
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readerBookInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-10}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-5}║║{"",-1}{readerBookInfo[3],-6}║║{"",-1}{readerBookInfo[4],-15}║║{"",-1}{readerBookInfo[5],-23}║{status}");
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
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookAmount.BookIDs " +
                        "from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs) " +
                        $"where Customer.State = 0 and Customer.CustomerSex = '{sex}'";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getCustomerInfoBySex, connection);

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
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readerBookInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-10}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-5}║║{"",-1}{readerBookInfo[3],-6}║║{"",-1}{readerBookInfo[4],-15}║║{"",-1}{readerBookInfo[5],-23}║{status}");
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
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookAmount.BookIDs " +
                        "from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs) " +
                        $"where Customer.State = 0 and Customer.CustomerSex = '{pn}'";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getCustomerInfoByPhoneNumber, connection);

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
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readerBookInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-10}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-5}║║{"",-1}{readerBookInfo[3],-6}║║{"",-1}{readerBookInfo[4],-15}║║{"",-1}{readerBookInfo[5],-23}║{status}");
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
                    // bool check = false;
                    // Console.Write("Input IDs to delete: ");
                    // string ids = Console.ReadLine();
                    // List<string> output1 = new List<string>(data);
                    // List<string> bookData = new List<string>(data1);
                    //
                    // for (int i = 0; i < output1.Count; i++)
                    // {
                    //     string[] output2 = output1[i].Split(',');
                    //     if (output2[0] == ids)
                    //     {
                    //         output1.RemoveAt(i);
                    //         check = true;
                    //         break;
                    //     }
                    // }
                    //
                    // if (check == false)
                    // {
                    //     Console.WriteLine($"There is no {ids} in IDs section in the database");
                    //     return;
                    // }
                    //
                    // for (int i = 0; i < bookData.Count; i++)
                    // {
                    //     string[] aOutput = bookData[i].Split(',');
                    //     List<string> aOutputList = new List<string>(aOutput);
                    //     bool checkIng = false;
                    //
                    //     for (int j = 7; j <= aOutputList.Count - 1; j++)
                    //     {
                    //         string[] checkIDs = aOutputList[j].Split(' ');
                    //         string[] checkAvailable = aOutputList[4].Split(' ');
                    //         string[] checkBorrowed = aOutputList[5].Split(' ');
                    //
                    //         if (checkIDs[0] == ids)
                    //         {
                    //             aOutputList.RemoveAt(j);
                    //             checkAvailable[1] = $"{int.Parse(checkAvailable[1]) + 1}";
                    //             aOutputList[4] = string.Join(" ", checkAvailable);
                    //             checkBorrowed[1] = $"{int.Parse(checkBorrowed[1]) - 1}";
                    //             aOutputList[5] = string.Join(" ", checkBorrowed);
                    //             checkIng = true;
                    //             break;
                    //         }
                    //     }
                    //
                    //     if (checkIng)
                    //     {
                    //         bookData[i] = string.Join(",", aOutputList.ToArray());
                    //     }
                    // }
                    //
                    // File.WriteAllLines(@"D:\Dev\School\Library Management System\CustomerData.txt",
                    //     output1.ToArray());
                    // File.WriteAllLines(@"D:\Dev\School\Library Management System\MyTest.txt", bookData.ToArray());
                    // Console.WriteLine("\t\t\t\t\t\t═══════════ DELETE SUCCESSFULLY ═══════════\t\t\t\t\t");
                    //

                    Console.Write("Input IDs to delete: ");
                    string ids = Console.ReadLine();

                    string getCustomerInfoByIDs =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookAmount.BookIDs " +
                        "from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs) " +
                        $"where Customer.State = 0 and Customer.CustomerIDs = '{ids}'";

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
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readerBookInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-10}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-5}║║{"",-1}{readerBookInfo[3],-6}║║{"",-1}{readerBookInfo[4],-15}║║{"",-1}{readerBookInfo[5],-23}║{status}");
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
                                Console.Write("Confirm that this is the book you want to delete: ");
                                int isConfirm = int.Parse(Console.ReadLine());

                                switch (isConfirm)
                                {
                                    case 1:
                                        string delCustomer =
                                            $"update Customer set State = 1 where CustomerIDs = '{ids}'";
                                        SqlCommand d = new SqlCommand(delCustomer, connection);
                                        d.ExecuteNonQuery();
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
                    Console.Write("Input IDs to delete: ");
                    string name = Console.ReadLine();

                    string getCustomerInfoByName =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookAmount.BookIDs " +
                        "from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs) " +
                        $"where Customer.State = 0 and Customer.CustomerName = '{name}'";

                    using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(getCustomerInfoByName, connection);

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
                                        Console.Write($"╔{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╗");
                                    }

                                    Console.WriteLine(
                                        $"\n║{"",-1}{"ID",-10}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-6}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                                    check = true;
                                }

                                if (prevIDs == $"{readerBookInfo[0]}")
                                {
                                    Console.WriteLine(
                                        $"║{"",-11}║║{"",-61}║║{"",-6}║║{"",-7}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║");

                                    prevIDs = $"{readerBookInfo[0]}";

                                    continue;
                                }

                                prevIDs = $"{readerBookInfo[0]}";

                                for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                                {
                                    Console.Write($" {BookList.Repeat("═", Program.StoreLengthCustomer[k])} ");
                                }

                                string status = $"{readerBookInfo[6]}" != ""
                                    ? $"║{"",-1}{$"Borrowed Book's IDs: {readerBookInfo[6]}",-25}║\n"
                                    : $"║{"",-1}{"Empty",-25}║\n";

                                Console.Write(
                                    $"\n║{"",-1}{readerBookInfo[0],-10}║║{"",-1}{readerBookInfo[1],-60}║║{"",-1}{readerBookInfo[2],-5}║║{"",-1}{readerBookInfo[3],-6}║║{"",-1}{readerBookInfo[4],-15}║║{"",-1}{readerBookInfo[5],-23}║{status}");
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
                                Console.Write("Confirm that this is the book you want to delete: ");
                                int isConfirm = int.Parse(Console.ReadLine());

                                switch (isConfirm)
                                {
                                    case 1:
                                        string delCustomer =
                                            $"update Customer set State = 1 where CustomerName = '{name}'";
                                        SqlCommand d = new SqlCommand(delCustomer, connection);
                                        d.ExecuteNonQuery();
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
            try
            {
                string[] data = File.ReadAllLines(@"D:\Dev\School\Library Management System\CustomerData.txt");
                if (data.Length == 0)
                {
                    Console.WriteLine("There are no data currently");
                    return;
                }

                ShowCustomer();
                Console.Write("\nInput IDs to edit: ");
                string ids = Console.ReadLine();

                for (int i = 0; i < data.Length; i++)
                {
                    string[] output = data[i].Split(',');
                    if (output[0] == ids)
                    {
                        Console.WriteLine($"\t\t\t\t\t\t          EDITING THE BOOK NO.{ids} \t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t╔═════════════════ MENU ═════════════════╗\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t║ 1. EDIT IDs                            ║\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t║ 2. EDIT NAME                           ║\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t║ 3. EDIT AGE                            ║\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t║ 4. EDIT SEX                            ║\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t║ 5. EDIT PHONE NUMBER                   ║\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t║ 6. EXIT                                ║\t\t\t\t\t");
                        Console.WriteLine("\t\t\t\t\t\t╚════════════════════════════════════════╝\t\t\t\t\t");
                        Console.Write("Input to edit: ");
                        int number2 = int.Parse(Console.ReadLine());

                        if (number2 > 0 && number2 <= 5)
                        {
                            Console.Write($"Changing {output[number2 - 1]} to: ");
                            string newText = Console.ReadLine();
                            output[number2 - 1] = newText;
                            data[i] = string.Join(",", output);
                            File.WriteAllLines(@"D:\Dev\School\Library Management System\CustomerData.txt", data);
                            Console.WriteLine("\t\t\t\t\t\t═══════════ UPDATE SUCCESSFULLY ═══════════\t\t\t\t\t");
                        }
                        else if (number2 == 6)
                        {
                        }
                        else
                        {
                            Console.WriteLine("Invalid number");
                        }

                        return;
                    }
                }

                Console.WriteLine($"There is no {ids} in IDs section in database!");
            }
            catch (IOException)
            {
                Console.WriteLine("There are no data currently");
            }
        }
    }
}