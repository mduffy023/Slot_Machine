using System;
using System.Threading;

namespace Slot_Machine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int ROWS = 3;
            const int COLUMNS = 3;

            Console.WriteLine("Slot Machine Game");
            Console.WriteLine("Game Rules:");
            Console.WriteLine("1. The cost is $1 for 1 line, $2 for 2 lines, and $3 for 3 lines.");
            Console.WriteLine("2. You can choose to play Horizontal, Vertical, or Diagonal lines.");
            Console.WriteLine("3. If the line matches, you win the amount equivalent to the number of lines played.");
            Console.WriteLine("4. Press Enter to spin.");

            int[,] slots = new int[ROWS, COLUMNS];
            Random rand = new Random();

            Console.WriteLine("How much money would you like to insert? ");
            int money = Convert.ToInt32(Console.ReadLine());

            while (money > 0)
            {
                Console.WriteLine($"Balance ${money}");

                char lineType;
                while (true)
                {
                    Console.WriteLine("Choose the line type you would like to play: H for Horizontal, V for Vertical, and D for Diagonal");
                    lineType = char.ToUpper(Console.ReadKey(true).KeyChar);
                    if (lineType == 'H' || lineType == 'V' || (lineType == 'D' && ROWS >= 2))
                        break;
                    else
                        Console.WriteLine("Invalid input. Please enter H, V, or D.");
                }

                Console.WriteLine($"Choose the number of lines you would like to play (1 to {Math.Min(ROWS, COLUMNS)}) ");
                int lineCount;
                while (true)
                {
                    try
                    {
                        lineCount = Convert.ToInt32(Console.ReadLine());
                  
                        if (lineCount >= 1 && lineCount <= Math.Min(ROWS, COLUMNS) && lineCount <= money)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Invalid input. Please enter a number 1 to {Math.Min(ROWS, COLUMNS)}, and ensure you have enough balance.");
                        }
                    }
                    catch 
                    {
                        Console.WriteLine("Please enter a valid intger.");
                    }
                }

                Console.WriteLine("Press Enter to start spin");
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { Console.WriteLine("Please only press Enter."); }

                money -= lineCount;

                // Generate slot values
                for (int indexRow = 0; indexRow < ROWS; indexRow++)
                {
                    for (int indexCol = 0; indexCol < COLUMNS; indexCol++)
                    {
                        slots[indexRow, indexCol] = rand.Next(1, 4);
                    }
                }

                int winnings = 0;

                // Check the selected lines for matches and calculate winnings
                if (lineType == 'H')
                {
                    for (int indexRow = 0; indexRow < lineCount; indexRow++)
                    {
                        bool allEqual = true;
                        for (int indexCol = 0; indexCol < COLUMNS - 1; indexCol++)
                        {
                            if (slots[indexRow, indexCol] != slots[indexRow, indexCol + 1])
                            {
                                allEqual = false;
                                break;
                            }
                        }
                        if (allEqual)
                        {
                            winnings += lineCount;
                        }
                    }
                }
                else if (lineType == 'V')
                {
                    for (int indexCol = 0; indexCol < lineCount; indexCol++)
                    {
                        bool allEqual = true;
                        for (int indexRow = 0; indexRow < ROWS - 1; indexRow++)
                        {
                            if (slots[indexRow, indexCol] != slots[indexRow + 1, indexCol])
                            {
                                allEqual = false;
                                break;
                            }
                        }
                        if (allEqual)
                        {
                            winnings += lineCount;
                        }
                    }
                }
                else 
                {
                    if (lineCount >= 2)
                    {
                        // Check both diagonals for a match
                        bool firstDiagonalEqual = true;
                        bool secondDiagonalEqual = true;
                        for (int i = 0; i < ROWS - 1; i++)
                        {
                            if (slots[i, i] != slots[i + 1, i + 1])
                            {
                                firstDiagonalEqual = false;
                            }
                            if (slots[i, COLUMNS - 1 - i] != slots[i + 1, COLUMNS - 2 - i])
                            {
                                secondDiagonalEqual = false;
                            }
                        }
                        if (firstDiagonalEqual || secondDiagonalEqual)
                        {
                            winnings += lineCount;
                        }
                    }
                }

                money += winnings;

                // Display the selected lines and the winnings
                DisplaySelectedLines(slots, lineType, lineCount);
                Console.WriteLine($"You have won ${winnings}. Current Balance: ${money}");

                if (money == 0)
                {
                    Console.WriteLine("You Lose! Would you like to insert more money or press any key to exit?");
                    if (int.TryParse(Console.ReadLine(), out int additionalMoney))
                    {
                        money += additionalMoney;
                    }
                    else
                    {
                        break;
                    }
                }

                Thread.Sleep(500);
            }
        }

        // Helper method to display the selected lines
        static void DisplaySelectedLines(int[,] slots, char lineType, int lineCount)
        {
            int rows = slots.GetLength(0);
            int columns = slots.GetLength(1);

            if (lineType == 'H')
            {
                for (int indexRow = 0; indexRow < lineCount; indexRow++)
                {
                    for (int indexCol = 0; indexCol < columns; indexCol++)
                    {
                        Console.Write(slots[indexRow, indexCol] + " ");
                    }
                    Console.WriteLine();
                }
            }
            else if (lineType == 'V')
            {
                for (int indexRow = 0; indexRow < rows; indexRow++)
                {
                    for (int indexCol = 0; indexCol < lineCount; indexCol++)
                    {
                        Console.Write(slots[indexRow, indexCol] + " ");
                    }
                    Console.WriteLine();
                }
            }
            //else // Diagonal
            //{
            //    for (int i = 0; i < rows; i++)
            //    {
            //        for (int j = 0; j < columns; j++)
            //        {
            //            if (i == j || i + j == columns - 1)
            //            {
            //                Console.Write(slots[i, j] + " ");
            //            }
            //            else
            //            {
            //                Console.Write("  ");
            //            }
            //        }
            Console.WriteLine();
                }
            }
        }
//    }
//}
