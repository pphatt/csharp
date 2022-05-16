using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 5;
            int b = 5;
            if (a == b)
            {
                a = 1;
                b = 0;
                Console.WriteLine(a + b);
            } else
            {
                a = 5;
                b = 5;
            }
        }
    }
}
