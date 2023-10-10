using System;
using System.Threading;

namespace Slot_Machine
{
    internal class Program
    {
        public const char LINE_TYPE_HORIZONTAL = 'H';
        public const char LINE_TYPE_VERTICAL = 'V';
        public const char LINE_TYPE_DIAGONAL = 'D';
        public const int MIN_LINE_AMOUNT = 1;
        public const int MIN_LINE_AMOUNT_DIAGONAL_PLAY = 2;
        public const int ROWS = 3;
        public const int COLUMNS = 3;

        static void Main(string[] args)
        {
            
            Console.WriteLine("Slot Machine Game");
            Console.WriteLine("Game Rules:");
            Console.WriteLine("1. The cost is $1 for 1 line, $2 for 2 lines, and $3 for 3 lines.");
            Console.WriteLine("2. You can choose to play Horizontal, Vertical, or Diagonal lines.");
            Console.WriteLine("3. If the line matches, you win the amount equivalent to the number of lines played.");
            Console.WriteLine("4. Press Enter to spin.");

            int[,] slots = new int[ROWS, COLUMNS];
            Random rand = new Random();

            Console.WriteLine("How much money would you like to insert? ");
            int remainingMoney = Convert.ToInt32(Console.ReadLine());

            while (remainingMoney > 0)
            {
                Console.WriteLine($"Balance ${remainingMoney}");

                char lineType;
                while (true)
                {
                    Console.WriteLine("Choose the line type you would like to play: H for Horizontal, V for Vertical, and D for Diagonal");
                    lineType = char.ToUpper(Console.ReadKey(true).KeyChar);
                    if (lineType == LINE_TYPE_HORIZONTAL || lineType == LINE_TYPE_VERTICAL || (lineType == LINE_TYPE_DIAGONAL && ROWS >= 2))
                        break;
                    else
                        Console.WriteLine("Invalid input. Please enter H, V, or D.");
                }

                int minLinesToPlay = lineType == LINE_TYPE_DIAGONAL ? MIN_LINE_AMOUNT_DIAGONAL_PLAY : MIN_LINE_AMOUNT;
                int maxLinesToPlay = Math.Min(ROWS, COLUMNS);
                Console.WriteLine($"Choose the number of lines you would like to play ({minLinesToPlay} to {maxLinesToPlay}) ");

                int linesToPlay;
                while (true)
                {
                    try
                    {
                        linesToPlay = Convert.ToInt32(Console.ReadLine());
                  
                        if (linesToPlay >= 1 && linesToPlay <= maxLinesToPlay && linesToPlay <= remainingMoney)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Invalid input. Please enter a number 1 to {maxLinesToPlay}, and ensure you have enough balance.");
                        }
                    }
                    catch 
                    {
                        Console.WriteLine("Please enter a valid intger.");
                    }
                }

                Console.WriteLine("Press Enter to start spin");
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { Console.WriteLine("Please only press Enter."); }

                remainingMoney -= linesToPlay;

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
                if (lineType == LINE_TYPE_HORIZONTAL)
                {
                    for (int indexRow = 0; indexRow < linesToPlay; indexRow++)
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
                            winnings += linesToPlay;
                        }
                    }
                }

                if (lineType == LINE_TYPE_VERTICAL)
                {
                    for (int indexCol = 0; indexCol < linesToPlay; indexCol++)
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
                            winnings += linesToPlay;
                        }
                    }
                }
                else 
                {
                    if (linesToPlay >= 2)
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
                            winnings += linesToPlay;
                        }
                    }
                }

                if(lineType == LINE_TYPE_DIAGONAL)
                {
                    if (linesToPlay >= 1)
                    {
                        bool firstDigonalEqual = true;
                        for (int indexRow = 0; indexRow < ROWS - 1; indexRow++)
                        {
                            if (slots[indexRow, indexRow] != slots[indexRow + 1, indexRow + 1])
                            {
                                firstDigonalEqual = false;
                                break;
                            }
                        }
                        if (firstDigonalEqual)
                        {
                            winnings += linesToPlay;
                        }
                    }

                    if( lineType >= 2)
                    {
                        bool secondDigonalEqual = true;
                        for (int indexRow = 0; indexRow < ROWS - 1; indexRow++)
                        {
                            if (slots[indexRow, COLUMNS - 1 - indexRow] != slots[indexRow + 1, COLUMNS -2 - indexRow])
                            {
                                secondDigonalEqual = false;
                                break;
                            }
                        }
                        if (secondDigonalEqual)
                        {
                            winnings += linesToPlay;
                        }
                    }
                }

                remainingMoney += winnings;

                int rows = slots.GetLength(0);
                int columns = slots.GetLength(1);

                for (int indexRow = 0; indexRow < rows; indexRow++)
                {
                    for (int indexCol = 0; indexCol < columns; indexCol++)
                    {
                        Console.Write(slots[indexRow, indexCol] + " ");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine($"You have won ${winnings}. Current Balance: ${remainingMoney}");

                if (remainingMoney == 0)
                {
                    Console.WriteLine("You Lose! Would you like to insert more money or press any key to exit?");
                    if (int.TryParse(Console.ReadLine(), out int additionalMoney))
                    {
                        remainingMoney += additionalMoney;
                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
