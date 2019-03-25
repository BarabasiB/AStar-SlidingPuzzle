using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AStar_SlidingPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = CreateMatrix();
            PrintMatrix(matrix);
            List<int[,]> movesAvailable = MovesAvailable(matrix).OrderBy(x => HammingDistance(x)).ToList();
            /*foreach (var state in MovesAvailable(matrix))
            {
                Console.WriteLine();
                PrintMatrix(state);
            }*/
            List<Tuple<int,int[,]>> olderStates = new List<Tuple<int,int[,]>>();
            olderStates.Add(Tuple.Create<int, int[,]>(HammingDistance(matrix), matrix.Clone() as int[,]));
            if (IsSolvable(matrix))
            {
                FindSolution(matrix, 0, olderStates);
            }
            else
            {
                Console.WriteLine("Unsolvable starting state!");
            }
            Console.ReadKey();
        }

        private static bool FindSolution(int[,] matrix, int moves, List<Tuple<int,int[,]>> olderStates)
        {
            if (HammingDistance(matrix) == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Goal node: ");
                PrintMatrix(matrix);
                Console.WriteLine();
                Console.WriteLine("Nodes visited: " + olderStates.Where(x => HammingDistance(x.Item2) == 0).First().Item1);
                return true;
            }
            else
            {
                List<int[,]> movesAvailable = MovesAvailable(matrix).OrderBy(x => HammingDistance(x)).ToList();
                foreach (var state in movesAvailable)
                {
                    if (IsNewState(olderStates, state))
                    {
                        olderStates.Add(Tuple.Create<int, int[,]>(moves + HammingDistance(state),state.Clone() as int[,]));
                        if (FindSolution(state, moves + 1, olderStates))
                        {
                            //PrintMatrix(matrix);
                            return true;
                        };
                    }
                    else
                    {
                        if (olderStates.Where(x => x.Item2 == state).Count() > 0)
                        {
                            if (HammingDistance(state) + moves < olderStates.Where(x => x.Item2 == state).First().Item1)
                            {
                                olderStates.Remove(olderStates.Where(x => x.Item2 == state).First());
                                olderStates.Add(Tuple.Create<int, int[,]>(HammingDistance(state) + moves, state.Clone() as int[,]));
                            }
                        }
                    }
                }
                return false;
            }
            
        }

        private static bool IsNewState(List<Tuple<int,int[,]>> states, int[,] state)
        {
            foreach (var item in states)
            {
                if (item.Item2.Rank == state.Rank &&
                    Enumerable.Range(0, item.Item2.Rank).All(dimension => item.Item2.GetLength(dimension) == state.GetLength(dimension)) &&
                    item.Item2.Cast<int>().SequenceEqual(state.Cast<int>()))
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
            Console.WriteLine("Enter the starting state in one line with no spaces between the numbers or a path or \"rand:\" and a number ");
            string numbers = Console.ReadLine();
            if (numbers.IndexOf("\\") != -1)
            {
                numbers = File.ReadAllText(numbers);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        matrix[i, j] = int.Parse(numbers.Substring(0, 1));
                        numbers = numbers.Substring(1);
                    }
                }
            }
            else if(numbers.IndexOf("rand") != -1)
            {
                matrix = Shuffle(int.Parse(numbers.Substring(numbers.IndexOf(":") + 1)));
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        matrix[i, j] = int.Parse(numbers.Substring(0, 1));
                        numbers = numbers.Substring(1);
                    }
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

        private static int ManhattanDistance(int[,] matrix)
        {
            return 0;
        }

        private static int HammingDistance(int[,] matrix)
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

        private static int[] FindZero(int[,] matrix)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matrix[i,j] == 0)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return null;
        }

        private static int[,] Shuffle(int m)
        {
            int[,] matrix = new int[,] { { 1,2,3 },
                                         { 4,5,6 },
                                         { 7,8,0 } };
            Random rand = new Random();
            for (int i = 0; i < m; i++)
            {
                int randNumber = rand.Next(0, 12);
                int[] positions = FindZero(matrix);
                if (randNumber <= 3)
                {
                    if (positions[1] > 0)
                    {
                        matrix[positions[0], positions[1]] = matrix[positions[0], positions[1] - 1];
                        matrix[positions[0], positions[1] - 1] = 0;
                    }
                    /*else
                    {
                        matrix[positions[0], positions[1]] = matrix[positions[0], 2];
                        matrix[positions[0], 2] = 0;
                    }*/
                }
                else if (randNumber > 3 && randNumber <= 6)
                {
                    if (positions[0] > 0)
                    {
                        matrix[positions[0], positions[1]] = matrix[positions[0] - 1, positions[1]];
                        matrix[positions[0] - 1, positions[1]] = 0;
                    }
                    /*else
                    {
                        matrix[positions[0], positions[1]] = matrix[2, positions[1]];
                        matrix[2, positions[1]] = 0;
                    }*/
                }
                else if (randNumber > 6 && randNumber <= 9)
                {
                    if (positions[1] < 2)
                    {
                        matrix[positions[0], positions[1]] = matrix[positions[0], positions[1] + 1];
                        matrix[positions[0], positions[1] + 1] = 0;
                    }
                    /*else
                    {
                        matrix[positions[0], positions[1]] = matrix[positions[0], 0];
                        matrix[positions[0], 0] = 0;
                    }*/
                }
                else
                {
                    if (positions[0] < 2)
                    {
                        matrix[positions[0], positions[1]] = matrix[positions[0] + 1, positions[1]];
                        matrix[positions[0] + 1, positions[1]] = 0;
                    }
                    /*else
                    {
                        matrix[positions[0], positions[1]] = matrix[0, positions[1]];
                        matrix[0, positions[1]] = 0;
                    }*/
                }
            }
            return matrix;
        }
       
    }
}
