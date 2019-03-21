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
            int[,] matrix = CreateMatrix();
            PrintMatrix(matrix);
            List<int[,]> movesAvailable = MovesAvailable(matrix).OrderBy(x => CalculateDistance(x)).ToList();
            /*foreach (var state in MovesAvailable(matrix))
            {
                Console.WriteLine();
                PrintMatrix(state);
            }*/
            List<int[,]> olderStates = new List<int[,]>();
            olderStates.Add(matrix.Clone() as int[,]);
            if (IsSolvable(matrix))
            {
                FindSolution(matrix, 0, olderStates);
            }
            else
            {
                Console.WriteLine("Unsolvable startingh state!");
            }
            Console.ReadKey();
        }

        private static bool FindSolution(int[,] matrix, int moves, List<int[,]> olderStates)
        {
            if (CalculateDistance(matrix) == 0)
            {
                PrintMatrix(matrix);
                Console.WriteLine();
                Console.WriteLine("Moves made: " + moves);
                return true;
            }
            else
            {
                List<int[,]> movesAvailable = MovesAvailable(matrix).OrderBy(x => CalculateDistance(x)).ToList();
                foreach (var state in movesAvailable)
                {
                    if (StateChecker(olderStates, state))
                    {
                        olderStates.Add(state.Clone() as int[,]);
                        if (FindSolution(state, moves + 1, olderStates))
                        {
                            //PrintMatrix(matrix);
                            return true;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                return false;
            }
            
        }

        private static bool StateChecker(List<int[,]> states, int[,] state)
        {
            foreach (var item in states)
            {
                if (item.Rank == state.Rank &&
                    Enumerable.Range(0, item.Rank).All(dimension => item.GetLength(dimension) == state.GetLength(dimension)) &&
                    item.Cast<int>().SequenceEqual(state.Cast<int>()))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsSolvable(int[,] matrix)
        {
            int inversions = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = i; k < 3; k++)
                    {
                        for (int l = j; l < 3; l++)
                        {
                            if (matrix[i,j] > matrix[k,l])
                            {
                                inversions += 1;
                            }
                        }
                    }
                }
            }
            if (inversions % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int[,] CreateMatrix()
        {
            int[,] matrix = new int[3,3];
            Console.WriteLine("Enter the starting state in one line with no spaces between the numbers: ");
            string numbers = Console.ReadLine();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i,j] = int.Parse(numbers.Substring(0,1));
                    numbers = numbers.Substring(1);
                }
            }
            return matrix;
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(matrix[i,j]);
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

        private static int CalculateDistance(int[,] matrix)
        {
            int distance = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (matrix[i,0] != 1)
                        {
                            distance += 1;
                        }
                        if (matrix[i,1] != 2)
                        {
                            distance += 1;
                        }
                        if (matrix[i,2] != 3)
                        {
                            distance += 1;
                        }
                        break;
                    case 1:
                        if (matrix[i,0] != 4)
                        {
                            distance += 1;
                        }
                        if (matrix[i,1] != 5)
                        {
                            distance += 1;
                        }
                        if (matrix[i,2] != 6)
                        {
                            distance += 1;
                        }
                        break;
                    case 2:
                        if (matrix[i,0] != 7)
                        {
                            distance += 1;
                        }
                        if (matrix[i,1] != 8)
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

        private static List<int[,]> MovesAvailable(int[,] matrix)
        {
            List<int[,]> movesAvailable = new List<int[,]>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matrix[i,j] == 0)
                    {
                        if (i == 0)
                        {
                            if (j == 0)
                            {
                                matrix[i,j] = matrix[i,j + 1];
                                matrix[i,j + 1] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i,j + 1] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i + 1,j];
                                matrix[i + 1,j] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i + 1,j] = matrix[i,j];
                                matrix[i,j] = 0;
                                return movesAvailable;
                            }
                            else if (j == 1)
                            {
                                matrix[i,j] = matrix[i,j + 1];
                                matrix[i,j + 1] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i,j + 1] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i + 1,j];
                                matrix[i + 1,j] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i + 1,j] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i,j-1];
                                matrix[i,j - 1] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i,j - 1] = matrix[i,j];
                                matrix[i,j] = 0;
                                return movesAvailable;
                            }
                            else
                            {
                                matrix[i,j] = matrix[i,j - 1];
                                matrix[i,j - 1] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i,j - 1] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i + 1,j];
                                matrix[i + 1,j] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i + 1,j] = matrix[i,j];
                                matrix[i,j] = 0;
                                return movesAvailable;
                            }
                        }
                        else if (i == 1)
                        {
                            if (j == 0)
                            {
                                matrix[i,j] = matrix[i,j + 1];
                                matrix[i,j + 1] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i,j + 1] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i + 1,j];
                                matrix[i + 1,j] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i + 1,j] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i - 1,j];
                                matrix[i - 1,j] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i - 1,j] = matrix[i,j];
                                matrix[i,j] = 0;
                                return movesAvailable;
                            }
                            else if (j == 1)
                            {
                                matrix[i,j] = matrix[i,j - 1];
                                matrix[i,j - 1] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i,j - 1] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i + 1,j];
                                matrix[i + 1,j] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i + 1,j] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i - 1,j];
                                matrix[i - 1,j] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i - 1,j] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i,j + 1];
                                matrix[i,j + 1] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i,j + 1] = matrix[i,j];
                                matrix[i,j] = 0;
                                return movesAvailable;
                            }
                            else
                            {
                                matrix[i,j] = matrix[i,j - 1];
                                matrix[i,j - 1] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i,j - 1] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i + 1,j];
                                matrix[i + 1,j] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i + 1,j] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i - 1,j];
                                matrix[i - 1,j] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i - 1,j] = matrix[i,j];
                                matrix[i,j] = 0;
                                return movesAvailable;
                            }
                        }
                        else
                        {
                            if (j == 0)
                            {
                                matrix[i,j] = matrix[i,j + 1];
                                matrix[i,j + 1] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i,j + 1] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i - 1,j];
                                matrix[i - 1,j] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i - 1,j] = matrix[i,j];
                                matrix[i,j] = 0;
                                return movesAvailable;
                            }
                            else if (j == 1)
                            {
                                matrix[i,j] = matrix[i,j + 1];
                                matrix[i,j + 1] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i,j + 1] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i - 1,j];
                                matrix[i - 1,j] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i - 1,j] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i,j - 1];
                                matrix[i,j - 1] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i,j - 1] = matrix[i,j];
                                matrix[i,j] = 0;
                                return movesAvailable;
                            }
                            else
                            {
                                matrix[i,j] = matrix[i,j - 1];
                                matrix[i,j - 1] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i,j - 1] = matrix[i,j];
                                matrix[i,j] = 0;
                                matrix[i,j] = matrix[i - 1,j];
                                matrix[i - 1,j] = 0;
                                movesAvailable.Add(matrix.Clone() as int[,]);
                                matrix[i - 1,j] = matrix[i,j];
                                matrix[i,j] = 0;
                                return movesAvailable;
                            }
                        }
                    }
                }
            }
            return movesAvailable;
        }
       
    }
}
