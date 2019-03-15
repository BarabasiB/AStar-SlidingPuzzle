using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_SlidingPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] matrix = new char[9];
            string numbers = string.Empty;
            Console.Write("Enter the starting state in one line with no spaces between the numbers: ");
            Console.WriteLine();
            numbers = Console.ReadLine();
            matrix = numbers.ToArray();
            PrintMatrix(matrix);
            Console.ReadKey();
        }

        private static void PrintMatrix(char[] matrix)
        {
            for (int i = 0; i < 9; i++)
            {
                Console.Write(matrix[i]);
                Console.Write(" ");
                if ((i + 1) % 3 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
