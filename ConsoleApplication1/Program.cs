using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string id = "",
                name = "",
                age = "",
                classroom = "",
                Math_grade = "",
                Physics_grade = "",
                Chemistry_grade = "",
                Literature_grade = "",
                English_grade = "",
                History_grade = "",
                totalGrade = "";
            int tmpTotalGrade = 0;

            List<List<List<string>>> data = new List<List<List<string>>>();
            int checkEd = 0;
            while (true)
            {
                Console.WriteLine("*****************************");
                Console.WriteLine("*--Students Management App--*");
                Console.WriteLine("*1. Input Student           *");
                Console.WriteLine("*2. Show Students           *");
                Console.WriteLine("*3. Exit                    *");
                Console.WriteLine("*****************************");
                while (true)
                {
                    List<string> currentStudentData = new List<string>();
                    List<List<string>> currentClass = new List<List<string>>();
                    Console.Write("Input to use: ");
                    int number = int.Parse(Console.ReadLine());
                    if (number == 1)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.Write("Input student ID: ");
                        id = Console.ReadLine();
                        if (id.Length == 0)
                        {
                            Console.WriteLine("Invalid ID");
                            Console.WriteLine("-----------------------------");
                            break;
                        }

                        currentStudentData.Add(id);

                        Console.Write("Input student name: ");
                        name = Console.ReadLine();
                        if (name.Length == 0)
                        {
                            Console.WriteLine("Invalid Name");
                            Console.WriteLine("-----------------------------");
                            break;
                        }

                        string[] name1 = name.Split(' ');
                        name1 = name1.Where(x => x != "").ToArray();
                        for (int i = 0; i < name1.Length; i++)
                        {
                            name1[i] = name1[i].First().ToString().ToUpper() + name1[i].Substring(1);
                        }

                        name = string.Join(" ", name1);
                        currentStudentData.Add(name);

                        Console.Write("Input student age: ");
                        age = Console.ReadLine();
                        if (age.Length == 0)
                        {
                            Console.WriteLine("Invalid Age");
                            Console.WriteLine("-----------------------------");
                            break;
                        }

                        currentStudentData.Add(age);

                        Console.Write("Input student class: ");
                        classroom = Console.ReadLine();
                        if (classroom.Length == 0)
                        {
                            Console.WriteLine("Invalid Classroom");
                            Console.WriteLine("-----------------------------");
                            break;
                        }

                        classroom = classroom.First().ToString().ToUpper() + classroom.Substring(1);

                        bool check = false;

                        for (int i = 0; i < data.Count; i++)
                        {
                            if (classroom == data[i][0][3])
                            {
                                currentStudentData.Add(classroom);
                                data[i].Add(currentStudentData);
                                // Console.WriteLine("-----------------------------");
                                // Console.WriteLine("---- Update Successfully ----");
                                // Console.WriteLine("-----------------------------");
                                check = true;
                                break;
                            }
                        }

                        if (check == false)
                        {
                            currentStudentData.Add(classroom);
                            currentClass.Add(currentStudentData);
                            data.Add(currentClass);
                        }

                        Console.Write("Input Math grade: ");
                        Math_grade = Console.ReadLine();
                        currentStudentData.Add(Math_grade);
                        Console.Write("Input Physics grade: ");
                        Physics_grade = Console.ReadLine();
                        currentStudentData.Add(Physics_grade);
                        Console.Write("Input Chemistry grade: ");
                        Chemistry_grade = Console.ReadLine();
                        currentStudentData.Add(Chemistry_grade);
                        Console.Write("Input Literature grade: ");
                        Literature_grade = Console.ReadLine();
                        currentStudentData.Add(Literature_grade);
                        Console.Write("Input English grade: ");
                        English_grade = Console.ReadLine();
                        currentStudentData.Add(English_grade);
                        Console.Write("Input History grade: ");
                        History_grade = Console.ReadLine();
                        currentStudentData.Add(History_grade);

                        tmpTotalGrade = Int32.Parse(Math_grade) + Int32.Parse(Physics_grade) +
                                        Int32.Parse(Chemistry_grade) + Int32.Parse(Literature_grade) + Int32.Parse(History_grade) + Int32.Parse(English_grade);

                        tmpTotalGrade = tmpTotalGrade / 6;
                        
                        totalGrade = $"{tmpTotalGrade}";
                        currentStudentData.Add(totalGrade);

                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("---- Update Successfully ----");
                        Console.WriteLine("-----------------------------");

                        break;
                    }
                    else if (number == 2)
                    {
                        Console.WriteLine("-----------------------------");
                        if (data.Count == 0)
                        {
                            Console.WriteLine("There are no data currently");
                            Console.WriteLine("-----------------------------");
                            break;
                        }

                        for (int i = 0; i < data.Count; i++)
                        {
                            Console.WriteLine($"-> {i + 1}. {data[i][0][3]}");
                        }

                        Console.Write("Input to use: ");
                        int number1 = int.Parse(Console.ReadLine());

                        if (number1 >= 1 && number1 <= data.Count)
                        {
                            Console.WriteLine("-----------------------------");
                            if (data[number1 - 1].Count == 1)
                            {
                                Console.WriteLine("There is only one student in the class");
                            }
                            else
                            {
                                Console.WriteLine($"There are {data[number1 - 1].Count} students in the class");
                            }

                            for (int i = 0; i < data[number1 - 1].Count; i++)
                            {
                                Console.WriteLine("-----------------------------");
                                Console.WriteLine($"**Student No.{(i + 1):D2}");
                                Console.WriteLine($"-> IDs: {data[number1 - 1][i][0]}");
                                Console.WriteLine($"-> Name: {data[number1 - 1][i][1]}");
                                Console.WriteLine($"-> Age: {data[number1 - 1][i][2]}");
                                Console.WriteLine($"-> Class: {data[number1 - 1][i][3]}");
                                Console.WriteLine($"-> Math grade: {data[number1 - 1][i][4]}");
                                Console.WriteLine($"-> Physics grade: {data[number1 - 1][i][5]}");
                                Console.WriteLine($"-> Chemistry grade: {data[number1 - 1][i][6]}");
                                Console.WriteLine($"-> Literature grade: {data[number1 - 1][i][7]}");
                                Console.WriteLine($"-> English grade: {data[number1 - 1][i][8]}");
                                Console.WriteLine($"-> History grade: {data[number1 - 1][i][9]}");
                                Console.WriteLine($"-> Total grade: {data[number1 - 1][i][10]}");
                                // Console.WriteLine("-----------------------------");
                                
                                if (Int32.Parse(data[number1 - 1][i][10]) >= 5 && Int32.Parse(data[number1 - 1][i][10]) <= 7)
                                {
                                    Console.WriteLine($"Pass with the score: {data[number1 - 1][i][10]} -> (P)");
                                }
                                else if (Int32.Parse(data[number1 - 1][i][10]) > 7 && Int32.Parse(data[number1 - 1][i][10]) <= 8.5)
                                {
                                    Console.WriteLine($"Pass with the score: {data[number1 - 1][i][10]} -> (M)");
                                }
                                else if (Int32.Parse(data[number1 - 1][i][10]) > 8.5)
                                {
                                    Console.WriteLine($"Pass with the score: {data[number1 - 1][i][10]} -> (D)");
                                }
                                else
                                {
                                    Console.WriteLine($"Failed with the score: {data[number1 - 1][i][10]} -> (F)");
                                }

                                Console.WriteLine("-----------------------------");
                            }

                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("Invalid Number");
                        break;
                    }
                    else if (number == 3)
                    {
                        checkEd = 3;
                        break;
                    }

                    Console.WriteLine("Invalid number");
                }

                if (checkEd == 3)
                {
                    break;
                }
            }
        }
    }
}