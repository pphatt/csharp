using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Bogus;

namespace Library_Management_System
{
    public class CustomerData
    {
        private enum Gender
        {
            Male,
            Female
        }

        private class User
        {
            public Gender Gender { get; set; }
            public string FullName { get; set; }
        }

        public static bool DisplayTable(string queryString, string yieldErrorMessage)
        {
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
                                $"\n║{"",-1}{"ID",-4}║║{"",-1}{"Name",-60}║║{"",-1}{"Age",-5}║║{"",-1}{"Sex",-7}║║{"",-1}{"Phone Number",-15}║║{"",-1}{"Date",-23}║║{"",-1}{"Status",-25}║");

                            check = true;
                        }

                        if (prevIDs == $"{readCustomerInfo[0]}")
                        {
                            Console.WriteLine(
                                $"║{"",-5}║║{"",-61}║║{"",-6}║║{"",-8}║║{"",-16}║║{"",-24}║║{"",-1}{$"Borrowed Book's IDs: {readCustomerInfo[6]}",-25}║");

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
                            $"\n║{"",-1}{readCustomerInfo[0],-4}║║{"",-1}{readCustomerInfo[1],-60}║║{"",-1}{readCustomerInfo[2],-5}║║{"",-1}{readCustomerInfo[3],-7}║║{"",-1}{readCustomerInfo[4],-15}║║{"",-1}{readCustomerInfo[5],-23}║{status}");
                    }

                    if (check)
                    {
                        for (int k = 0; k < Program.StoreLengthCustomer.Length; k++)
                        {
                            Console.Write($"╚{BookList.Repeat("═", Program.StoreLengthCustomer[k])}╝");
                        }

                        Console.WriteLine("");
                        return true;
                    }

                    Console.WriteLine(yieldErrorMessage);
                    return false;
                }
            }
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

        public void AddCustomer()
        {
            DateTime date = DateTime.Now;

            string[] param = { };
            string[] vparam = { };

            string ln = HandleStoredProcedureMessage("GetLibrarian", param, vparam);

            if (ln == "")
            {
                Console.WriteLine("There is no currently active librarian...");
                return;
            }

            Console.Write("Enter customer's name: ");
            string name = Console.ReadLine();
            Console.Write("Enter customer's age: ");
            string age = Console.ReadLine();
            Console.Write("Enter customer's sex: ");
            string sex = Console.ReadLine();
            Console.Write("Enter customer's phone number: ");
            string phoneNumber = Console.ReadLine();

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
                "INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Date, State, LIDs) VALUES (@CustomerName, @CustomerAge, @CustomerSex, @CustomerPhoneNumber, @Date, @State, @LIDs)";

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
                    insertCommand.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd HH:mm:ss"));
                    insertCommand.Parameters.AddWithValue("@State", 0);
                    insertCommand.Parameters.AddWithValue("@LIDs", ln);
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

            DisplayTable(queryString, "There are no data currently...");
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

                    DisplayTable(getCustomerInfoByIDs, "Invalid Customer's IDs or the IDs does not exist");

                    break;
                case 2:
                    Console.Write("Input Customer's Name to search: ");
                    string name = Console.ReadLine();

                    string getCustomerInfoByName =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                        "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                        $"where Customer.State = 0 and CustomerName like '%{name}%'";

                    DisplayTable(getCustomerInfoByName, "Invalid Customer's Name");

                    break;
                case 3:
                    Console.Write("Input Customer's Age to search: ");
                    string age = Console.ReadLine();

                    string getCustomerInfoByAge =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                        "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                        $"where Customer.State = 0 and Customer.CustomerAge = '{age}'";

                    DisplayTable(getCustomerInfoByAge, "Invalid Customer's Age");

                    break;
                case 4:
                    Console.Write("Input Customer's Sex to search: ");
                    string sex = Console.ReadLine();

                    string getCustomerInfoBySex =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                        "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                        $"where Customer.State = 0 and Customer.CustomerSex = '{sex}'";

                    DisplayTable(getCustomerInfoBySex, "Invalid Customer's Sex");

                    break;
                case 5:
                    Console.Write("Input Customer's PhoneNumber to search: ");
                    string pn = Console.ReadLine();

                    string getCustomerInfoByPhoneNumber =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                        "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                        $"where Customer.State = 0 and Customer.CustomerPhoneNumber = '{pn}'";

                    DisplayTable(getCustomerInfoByPhoneNumber, "Invalid Customer's Phone Number");

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
            DateTime date = DateTime.Now;

            string[] param = { };
            string[] vparam = { };

            string ln = HandleStoredProcedureMessage("GetLibrarian", param, vparam);

            if (ln == "")
            {
                Console.WriteLine("There is no currently active librarian...");
                return;
            }

            Console.Write("Input Customer's IDs to return: ");
            int ids = int.Parse(Console.ReadLine());

            string getCustomerInfoByIDs =
                "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                $"where Customer.State = 0 and Customer.CustomerIDs = {ids}";

            if (DisplayTable(getCustomerInfoByIDs, "Invalid Customer's IDs"))
            {
                using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                {
                    connection.Open();
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
                                    "Select BookAmountAvailable, BookAmountBorrowed " +
                                    "from (BookLog left join Book on Book.BookIDs = BookLog.BookIDs) " +
                                    $"where BookLog.BookIDs = '{idb}' and CustomerIDs = '{ids}' and BookLog.State = 0";

                                SqlCommand qu = new SqlCommand(s, connection);

                                using (SqlDataReader t = qu.ExecuteReader())
                                {
                                    bool ch = false;

                                    while (t.Read())
                                    {
                                        ch = true;

                                        string ud =
                                            $"update BookLog set State = 1, DateReturn = '{date:yyyy-MM-dd HH:mm:ss}', LIDsCheckOut = '{ln}' where CustomerIDs = '{ids}' and BookIDs = '{idb}'";

                                        int ab = (int)t[0];
                                        int bb = (int)t[1];

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
                                        return;
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

                    string getCustomerInfoByIDs =
                        "select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Customer.Date, BookLog.BookIDs " +
                        "from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0) " +
                        $"where Customer.State = 0 and Customer.CustomerIDs = '{ids}'";

                    if (DisplayTable(getCustomerInfoByIDs, "Invalid IDs"))
                    {
                        using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                        {
                            connection.Open();
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

                    if (DisplayTable(getCustomerInfoByName, "Invalid Name"))
                    {
                        using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                        {
                            connection.Open();
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

        public void GenerateCustomerFakeData()
        {
            DateTime date = DateTime.Now;

            string[] param = { };
            string[] vparam = { };

            string ln = HandleStoredProcedureMessage("GetLibrarian", param, vparam);

            if (ln == "")
            {
                Console.WriteLine("There is no currently active librarian...");
                return;
            }

            Console.Write("Insert the amount of customer information you want to generate: ");
            int n = Int32.Parse(Console.ReadLine());

            if (n < 1)
            {
                Console.WriteLine("Invalid amount...");
                return;
            }

            for (var i = 0; i < n; i++)
            {
                var phone = new Bogus.DataSets.PhoneNumbers("vi");
                var testUsers = new Faker<User>()
                    .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                    .RuleFor(u => u.FullName, (f, u) => f.Name.FullName());

                var user = testUsers.Generate();
                Random rnd = new Random();
                int age = rnd.Next(20, 80);

                string addDataQuery =
                    "INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Date, State, LIDs) VALUES (@CustomerName, @CustomerAge, @CustomerSex, @CustomerPhoneNumber, @Date, @State, @LIDs)";

                using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
                {
                    connection.Open();

                    SqlCommand insertCommand = new SqlCommand(addDataQuery, connection);

                    insertCommand.Parameters.AddWithValue("@CustomerName", user.FullName);
                    insertCommand.Parameters.AddWithValue("@CustomerAge", age);
                    insertCommand.Parameters.AddWithValue("@CustomerSex", user.Gender.ToString());
                    insertCommand.Parameters.AddWithValue("@CustomerPhoneNumber", phone.PhoneNumber());
                    insertCommand.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd HH:mm:ss"));
                    insertCommand.Parameters.AddWithValue("@State", 0);
                    insertCommand.Parameters.AddWithValue("@LIDs", ln);
                    insertCommand.ExecuteNonQuery();
                }
            }

            Console.WriteLine("\t\t\t\t\t\t═══════════ ADDED SUCCESSFULLY ═══════════\t\t\t\t\t");
        }
    }
}