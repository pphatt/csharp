using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Making Three dimensional List for storing data.
            // Why is it 3D array ? -> Because we are making a program to store students data and sorted by each classes.
            // First layer of array is like the whole school.
            // Second layer is multiple classes in it.
            // Third layer is multiple students data in specific class.
            List<List<List<string>>> data = new List<List<List<string>>>();

            // checkEd variable here is for the exit.
            int checkEd = 0;

            // We are using while true for making the menu.
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
                    // Making two List:
                    // One is a normal List for storing current student data when user input.
                    // Two is a two dimensional List for current student class.
                    // Why is it 2D array ? -> Because we are making a place to store multiple students data not just a single student and 2D can help to solve that problem.
                    List<string> currentStudentData = new List<string>();
                    List<List<string>> currentClass = new List<List<string>>();
                    
                    // Accepting the user input.
                    Console.Write("Input to use: ");
                    int number = int.Parse(Console.ReadLine());
                    
                    // Checking if the user input is number 1 or not.
                    if (number == 1)
                    {
                        // We get student data like: ID, Name, Age, Class, and Grade of some Subjects
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
                        
                        // In here, we want name get capitalized in each specific name.
                        // So we butcher the name by Split(). (1)
                        // And we looking for if there is any unwanted-white-space in out array and remove it. (2)
                        // Next, we capitalize all the name and concat the name together. (3)
                        
                        string[] name1 = name.Split(' '); // (1)
                        
                        name1 = name1.Where(x => x != "").ToArray(); // (2)
                        
                        for (int i = 0; i < name1.Length; i++) // (3)
                        {
                            name1[i] = name1[i].First().ToString().ToUpper() + name1[i].Substring(1);
                        }

                        name = string.Join(" ", name1); // (3)
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
                        
                        // We capitalize the first character in the class name.
                        classroom = classroom.First().ToString().ToUpper() + classroom.Substring(1);
                        
                        // the check variable here is for checking if the class is already exist or not. (1)
                        // if the class is exist, we just add in the current class and we don't need the making the new ones. (2)
                        // And if not we make a new class. (3)
                        bool check = false; // (1)

                        for (int i = 0; i < data.Count; i++)
                        {
                            if (classroom == data[i][0][3]) // (2)
                            {
                                currentStudentData.Add(classroom);
                                data[i].Add(currentStudentData);
                                check = true;
                                break;
                            }
                        }

                        if (check == false) // (3)
                        {
                            currentStudentData.Add(classroom);
                            currentClass.Add(currentStudentData);
                            data.Add(currentClass);
                        }
                        
                        // Here we get all the subjects grade.
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
                        
                        // We calculate the total grade to calculate the status of the student. Is that student pass or failed or something based on the student's status. (1)
                        Double tmpTotalGrade = Double.Parse(Math_grade) + Double.Parse(Physics_grade) +
                                               Double.Parse(Chemistry_grade) + Double.Parse(Literature_grade) +
                                               Double.Parse(History_grade) + Double.Parse(English_grade); // (1)

                        tmpTotalGrade = tmpTotalGrade / 6; // (1)

                        string totalGrade = $"{tmpTotalGrade:F1}"; // (1)
                        currentStudentData.Add(totalGrade);

                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("---- Update Successfully ----");
                        Console.WriteLine("-----------------------------");

                        break;
                    }
                    else if (number == 2)
                    {
                        // So option 2 is that show the student's data in the database.
                        Console.WriteLine("-----------------------------");
                        if (data.Count == 0)
                        {
                            Console.WriteLine("There are no data currently");
                            Console.WriteLine("-----------------------------");
                            break;
                        }
                        
                        // In here, we display any available class in the database.
                        for (int i = 0; i < data.Count; i++)
                        {
                            Console.WriteLine($"-> {i + 1}. {data[i][0][3]}");
                        }

                        Console.Write("Input to use: ");
                        int number1 = int.Parse(Console.ReadLine());
                        
                        // Showing student's data based on user option.
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
                            
                            // So the way to draw table in the console is not that hard. We just need to use the string.format output by microsoft.
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
                            
                            // In here we calculate the status of the student.
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
                        // Option 3 is the remove student.
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
                            
                            // We input to remove a specific student. (1)
                            Console.Write("Input to remove: ");
                            int index = int.Parse(Console.ReadLine());

                            if (index > 0 && index <= data[number1 - 1].Count) // (1)
                            {
                                if (index == data[number1 - 1].Count && data[number1 - 1].Count == 1) // (1)
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
                        // Option 4 is used to remove class
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
                        
                        // Like section number 3: remove student.
                        // Here we will remove a specific class out of the database.
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
                        // Option 5 is used to edit student's data.
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
                            
                            // We accept the user input as an integer.
                            Console.Write("Input to edit: ");
                            int index = int.Parse(Console.ReadLine());
                            
                            // The titleInfo array is just for custom output
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