using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<List<List<string>>> data = new List<List<List<string>>>();
            int checkEd = 0;
            while (true)
            {
                Console.WriteLine("*****************************");
                Console.WriteLine("*--Students Management App--*");
                Console.WriteLine("*1. Input Student           *");
                Console.WriteLine("*2. Show Students           *");
                Console.WriteLine("*3. Remove Students         *");
                Console.WriteLine("*4. Remove Class            *");
                Console.WriteLine("*5. Edit Student Info       *");
                Console.WriteLine("*6. Exit                    *");
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
                        string id = Console.ReadLine();
                        if (id.Length == 0)
                        {
                            Console.WriteLine("Invalid ID");
                            Console.WriteLine("-----------------------------");
                            break;
                        }

                        currentStudentData.Add(id);

                        Console.Write("Input student name: ");
                        string name = Console.ReadLine();
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
                        string age = Console.ReadLine();
                        if (age.Length == 0)
                        {
                            Console.WriteLine("Invalid Age");
                            Console.WriteLine("-----------------------------");
                            break;
                        }

                        currentStudentData.Add(age);

                        Console.Write("Input student class: ");
                        string classroom = Console.ReadLine();
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
                        string Math_grade = Console.ReadLine();
                        currentStudentData.Add(Math_grade);
                        Console.Write("Input Physics grade: ");
                        string Physics_grade = Console.ReadLine();
                        currentStudentData.Add(Physics_grade);
                        Console.Write("Input Chemistry grade: ");
                        string Chemistry_grade = Console.ReadLine();
                        currentStudentData.Add(Chemistry_grade);
                        Console.Write("Input Literature grade: ");
                        string Literature_grade = Console.ReadLine();
                        currentStudentData.Add(Literature_grade);
                        Console.Write("Input English grade: ");
                        string English_grade = Console.ReadLine();
                        currentStudentData.Add(English_grade);
                        Console.Write("Input History grade: ");
                        string History_grade = Console.ReadLine();
                        currentStudentData.Add(History_grade);

                        Double tmpTotalGrade = Double.Parse(Math_grade) + Double.Parse(Physics_grade) +
                                               Double.Parse(Chemistry_grade) + Double.Parse(Literature_grade) +
                                               Double.Parse(History_grade) + Double.Parse(English_grade);

                        tmpTotalGrade = tmpTotalGrade / 6;

                        string totalGrade = $"{tmpTotalGrade:F1}";
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

                            Console.WriteLine(
                                "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine(
                                "|{0,-4}|{1, -30} |{2,-3}|{3,-6}|{4,-11}|{5,-15}|{6,-16}|{7,-17}|{8,-14}|{9,-14}|{10, -12}|{11, -18}|",
                                "IDs",
                                "Name", "Age",
                                "Class", "Math Grade", "Physics Grade", "Chemistry Grade", "Literature Grade",
                                "English Grade",
                                "History Grade", "Total Grade", "Status");
                            Console.WriteLine(
                                "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                            for (int i = 0; i < data[number1 - 1].Count; i++)
                            {
                                string getStatus = "";
                                if (Double.Parse(data[number1 - 1][i][10]) >= 5 &&
                                    Double.Parse(data[number1 - 1][i][10]) <= 7)
                                {
                                    getStatus = "(P)";
                                }
                                else if (Double.Parse(data[number1 - 1][i][10]) > 7 &&
                                         Double.Parse(data[number1 - 1][i][10]) <= 8.5)
                                {
                                    getStatus = "(P) => (M)";
                                }
                                else if (Double.Parse(data[number1 - 1][i][10]) > 8.5)
                                {
                                    getStatus = "(P) => (M) => (D)";
                                }
                                else
                                {
                                    getStatus = "(F)";
                                }

                                Console.WriteLine(
                                    "|{0,-4}|{1, -30} |{2,-3}|{3,-6}|{4,-11}|{5,-15}|{6,-16}|{7,-17}|{8,-14}|{9,-14}|{10, -12}|{11, -18}|",
                                    data[number1 - 1][i][0], data[number1 - 1][i][1], data[number1 - 1][i][2],
                                    data[number1 - 1][i][3], data[number1 - 1][i][4], data[number1 - 1][i][5],
                                    data[number1 - 1][i][6], data[number1 - 1][i][7], data[number1 - 1][i][8],
                                    data[number1 - 1][i][9], data[number1 - 1][i][10], getStatus);

                                Console.WriteLine(
                                    "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            }

                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("Invalid Number");
                        break;
                    }
                    else if (number == 3)
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

                            Console.WriteLine(
                                "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine(
                                "|{0,-4}|{1, -30} |{2,-3}|{3,-6}|{4,-11}|{5,-15}|{6,-16}|{7,-17}|{8,-14}|{9,-14}|{10, -12}|{11, -18}|",
                                "IDs",
                                "Name", "Age",
                                "Class", "Math Grade", "Physics Grade", "Chemistry Grade", "Literature Grade",
                                "English Grade",
                                "History Grade", "Total Grade", "Status");
                            Console.WriteLine(
                                "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                            for (int i = 0; i < data[number1 - 1].Count; i++)
                            {
                                string getStatus = "";
                                if (Double.Parse(data[number1 - 1][i][10]) >= 5 &&
                                    Double.Parse(data[number1 - 1][i][10]) <= 7)
                                {
                                    getStatus = "(P)";
                                }
                                else if (Double.Parse(data[number1 - 1][i][10]) > 7 &&
                                         Double.Parse(data[number1 - 1][i][10]) <= 8.5)
                                {
                                    getStatus = "(P) => (M)";
                                }
                                else if (Double.Parse(data[number1 - 1][i][10]) > 8.5)
                                {
                                    getStatus = "(P) => (M) => (D)";
                                }
                                else
                                {
                                    getStatus = "(F)";
                                }

                                Console.WriteLine(
                                    "|{0,-4}|{1, -30} |{2,-3}|{3,-6}|{4,-11}|{5,-15}|{6,-16}|{7,-17}|{8,-14}|{9,-14}|{10, -12}|{11, -18}|",
                                    data[number1 - 1][i][0], data[number1 - 1][i][1], data[number1 - 1][i][2],
                                    data[number1 - 1][i][3], data[number1 - 1][i][4], data[number1 - 1][i][5],
                                    data[number1 - 1][i][6], data[number1 - 1][i][7], data[number1 - 1][i][8],
                                    data[number1 - 1][i][9], data[number1 - 1][i][10], getStatus);

                                Console.WriteLine(
                                    "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            }

                            Console.Write("Input to remove: ");
                            int index = int.Parse(Console.ReadLine());

                            if (index > 0 && index <= data[number1 - 1].Count)
                            {
                                if (index == data[number1 - 1].Count && data[number1 - 1].Count == 1)
                                {
                                    data.RemoveAt(index - 1);
                                }
                                else
                                {
                                    data[number1 - 1].RemoveAt(index - 1);
                                }

                                Console.WriteLine("Remove Successfully");
                                Console.WriteLine("-----------------------------");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Number");
                            }

                            break;
                        }

                        Console.WriteLine("Invalid Number");
                        break;
                    }
                    else if (number == 4)
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
                            data.RemoveAt(number1 - 1);
                            Console.WriteLine("Remove Successfully");
                            Console.WriteLine("-----------------------------");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Number");
                        }

                        break;
                    }
                    else if (number == 5)
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

                            Console.WriteLine(
                                "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine(
                                "|{0,-4}|{1, -30} |{2,-3}|{3,-6}|{4,-11}|{5,-15}|{6,-16}|{7,-17}|{8,-14}|{9,-14}|{10, -12}|{11, -18}|",
                                "IDs",
                                "Name", "Age",
                                "Class", "Math Grade", "Physics Grade", "Chemistry Grade", "Literature Grade",
                                "English Grade",
                                "History Grade", "Total Grade", "Status");
                            Console.WriteLine(
                                "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                            for (int i = 0; i < data[number1 - 1].Count; i++)
                            {
                                string getStatus = "";
                                if (Double.Parse(data[number1 - 1][i][10]) >= 5 &&
                                    Double.Parse(data[number1 - 1][i][10]) <= 7)
                                {
                                    getStatus = "(P)";
                                }
                                else if (Double.Parse(data[number1 - 1][i][10]) > 7 &&
                                         Double.Parse(data[number1 - 1][i][10]) <= 8.5)
                                {
                                    getStatus = "(P) => (M)";
                                }
                                else if (Double.Parse(data[number1 - 1][i][10]) > 8.5)
                                {
                                    getStatus = "(P) => (M) => (D)";
                                }
                                else
                                {
                                    getStatus = "(F)";
                                }

                                Console.WriteLine(
                                    "|{0,-4}|{1, -30} |{2,-3}|{3,-6}|{4,-11}|{5,-15}|{6,-16}|{7,-17}|{8,-14}|{9,-14}|{10, -12}|{11, -18}|",
                                    data[number1 - 1][i][0], data[number1 - 1][i][1], data[number1 - 1][i][2],
                                    data[number1 - 1][i][3], data[number1 - 1][i][4], data[number1 - 1][i][5],
                                    data[number1 - 1][i][6], data[number1 - 1][i][7], data[number1 - 1][i][8],
                                    data[number1 - 1][i][9], data[number1 - 1][i][10], getStatus);

                                Console.WriteLine(
                                    "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            }

                            Console.Write("Input to edit: ");
                            int index = int.Parse(Console.ReadLine());

                            string[] titleInfo =
                            {
                                "IDs", "Name", "Age", "Class", "Math Grade", "Physics Grade", "Chemistry Grade",
                                "Literature Grade", "English Grade", "History grade"
                            };

                            if (index > 0 && index <= data[number1 - 1].Count)
                            {
                                while (true)
                                {
                                    Console.WriteLine($"--- Editing Student IDs {data[number1 - 1][index - 1][0]} ---");
                                    Console.WriteLine("->1. IDs");
                                    Console.WriteLine("->2. Name");
                                    Console.WriteLine("->3. Age");
                                    Console.WriteLine("->4. Class");
                                    Console.WriteLine("->5. Math Grade");
                                    Console.WriteLine("->6. Physics Grade");
                                    Console.WriteLine("->7. Chemistry Grade");
                                    Console.WriteLine("->8. Literature Grade");
                                    Console.WriteLine("->9. English Grade");
                                    Console.WriteLine("->10. History Grade");
                                    Console.WriteLine("->11. Exit");
                                    Console.Write("Input to use: ");
                                    int numberEdit = int.Parse(Console.ReadLine());

                                    if (numberEdit == 11)
                                    {
                                        break;
                                    }

                                    Console.Write(
                                        $"The data of {titleInfo[numberEdit - 1]} was {data[number1 - 1][index - 1][numberEdit - 1]} -> ");

                                    string newData = Console.ReadLine();

                                    if (numberEdit == 2)
                                    {
                                        string[] name1 = newData.Split(' ');
                                        name1 = name1.Where(x => x != "").ToArray();
                                        for (int i = 0; i < name1.Length; i++)
                                        {
                                            name1[i] = name1[i].First().ToString().ToUpper() +
                                                       name1[i].Substring(1);
                                        }

                                        newData = string.Join(" ", name1);
                                        data[number1 - 1][index - 1][numberEdit - 1] = newData;
                                        Console.WriteLine("-----------------------------");
                                        Console.WriteLine("---- Update Successfully ----");
                                        Console.WriteLine("-----------------------------");
                                    }
                                    else if (numberEdit == 4)
                                    {
                                        bool check = false;

                                        currentStudentData.Add(data[number1 - 1][index - 1][0]);
                                        currentStudentData.Add(data[number1 - 1][index - 1][1]);
                                        currentStudentData.Add(data[number1 - 1][index - 1][2]);
                                        currentStudentData.Add(newData);
                                        currentStudentData.Add(data[number1 - 1][index - 1][4]);
                                        currentStudentData.Add(data[number1 - 1][index - 1][5]);
                                        currentStudentData.Add(data[number1 - 1][index - 1][6]);
                                        currentStudentData.Add(data[number1 - 1][index - 1][7]);
                                        currentStudentData.Add(data[number1 - 1][index - 1][8]);
                                        currentStudentData.Add(data[number1 - 1][index - 1][9]);

                                        Double tmpTotalGrade =
                                            Double.Parse(data[number1 - 1][index - 1][4]) +
                                            Double.Parse(data[number1 - 1][index - 1][5]) +
                                            Double.Parse(data[number1 - 1][index - 1][6]) +
                                            Double.Parse(data[number1 - 1][index - 1][7]) +
                                            Double.Parse(data[number1 - 1][index - 1][8]) +
                                            Double.Parse(data[number1 - 1][index - 1][9]);

                                        tmpTotalGrade = tmpTotalGrade / 6;

                                        string totalGrade = $"{tmpTotalGrade:F1}";
                                        currentStudentData.Add(totalGrade);

                                        for (int i = 0; i < data.Count; i++)
                                        {
                                            if (newData == data[i][0][3])
                                            {
                                                data[i].Add(currentStudentData);
                                                check = true;
                                                break;
                                            }
                                        }

                                        if (check == false)
                                        {
                                            currentClass.Add(currentStudentData);
                                            data.Add(currentClass);
                                        }

                                        data.RemoveAt(number1 - 1);
                                        Console.WriteLine("-----------------------------");
                                        Console.WriteLine("---- Update Successfully ----");
                                        Console.WriteLine("-----------------------------");
                                        break;
                                    }
                                    else if (numberEdit >= 5 && numberEdit <= 10)
                                    {
                                        data[number1 - 1][index - 1][numberEdit - 1] = newData;

                                        Double tmpTotalGrade =
                                            Double.Parse(data[number1 - 1][index - 1][4]) +
                                            Double.Parse(data[number1 - 1][index - 1][5]) +
                                            Double.Parse(data[number1 - 1][index - 1][6]) +
                                            Double.Parse(data[number1 - 1][index - 1][7]) +
                                            Double.Parse(data[number1 - 1][index - 1][8]) +
                                            Double.Parse(data[number1 - 1][index - 1][9]);

                                        tmpTotalGrade = tmpTotalGrade / 6;

                                        string totalGrade = $"{tmpTotalGrade:F1}";
                                        data[number1 - 1][index - 1][10] = totalGrade;
                                        Console.WriteLine("-----------------------------");
                                        Console.WriteLine("---- Update Successfully ----");
                                        Console.WriteLine("-----------------------------");
                                    }
                                    else if (numberEdit == 1 || numberEdit == 3)
                                    {
                                        data[number1 - 1][index - 1][numberEdit - 1] = newData;
                                        Console.WriteLine("-----------------------------");
                                        Console.WriteLine("---- Update Successfully ----");
                                        Console.WriteLine("-----------------------------");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid number");
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Number");
                            }
                        }

                        break;
                    }
                    else if (number == 6)
                    {
                        checkEd = 6;
                        break;
                    }

                    Console.WriteLine("Invalid number");
                }

                if (checkEd == 6)
                {
                    break;
                }
            }
        }
    }
}