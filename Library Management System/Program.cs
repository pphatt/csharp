using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library_Management_System
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Menu menu = new Menu();
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
        }
    }
}