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
            int[][] matrix = CreateMatrix();
            PrintMatrix(matrix);
            Console.WriteLine(CalculateDistance(matrix));
            Console.ReadKey();
        }

        private static int[][] CreateMatrix()
        {
            int[][] matrix = new int[3][];
            for (int i = 0; i < 3; i++)
            {
                matrix[i] = new int[3];
            }
            Console.WriteLine("Enter the starting state in one line with no spaces between the numbers: ");
            //Console.WriteLine();
            string numbers = Console.ReadLine();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i][j] = int.Parse(numbers.Substring(0,1));
                    numbers = numbers.Substring(1);
                }
            }
            return matrix;
        }

        private static void PrintMatrix(int[][] matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var column in row)
                {
                    Console.Write(column);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        private static void PrintMatrix(int[] matrix)
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

        private static int CalculateDistance(int[][] matrix)
        {
            int distance = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (matrix[i][0] != 1)
                        {
                            distance += 1;
                        }
                        if (matrix[i][1] != 2)
                        {
                            distance += 1;
                        }
                        if (matrix[i][2] != 3)
                        {
                            distance += 1;
                        }
                        break;
                    case 1:
                        if (matrix[i][0] != 4)
                        {
                            distance += 1;
                        }
                        if (matrix[i][1] != 5)
                        {
                            distance += 1;
                        }
                        if (matrix[i][2] != 6)
                        {
                            distance += 1;
                        }
                        break;
                    case 2:
                        if (matrix[i][0] != 7)
                        {
                            distance += 1;
                        }
                        if (matrix[i][1] != 8)
                        {
                            distance += 1;
                        }
                        if (matrix[i][2] != 0)
                        {
                            distance += 1;
                        }
                        break;
                    default:
                        break;
                }
            }
            return distance;
        }
       
    }
}
