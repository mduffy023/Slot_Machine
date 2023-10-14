using System;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Slot_Machine
{
    internal class Program
    {
        public const char LINE_TYPE_HORIZONTAL = 'H';
        public const char LINE_TYPE_VERTICAL = 'V';
        public const char LINE_TYPE_DIAGONAL = 'D';
        public const int SLOT_MIN_VALUE = 1;
        public const int SLOT_MAX_VALUE = 4;
        public const int MIN_LINE_AMOUNT = 1;
        public const int MIN_LINE_AMOUNT_DIAGONAL_PLAY = 2;
        public const int ROWS = 3;
        public const int COLUMNS = 3;

        static void Main(string[] args)
        {
            DisplayIntro();
            int remainingMoney = GetInitialMoney();
            PlayGame(remainingMoney);
        }

        /// <summary>
        /// Displays for the rules and introduction  
        /// </summary>
        private static void DisplayIntro()
        {
            Console.WriteLine("Slot Machine Game");
            Console.WriteLine("Game Rules:");
            Console.WriteLine("1. The cost is $1 for 1 line, $2 for 2 lines, and $3 for 3 lines.");
            Console.WriteLine("2. You can choose to play Horizontal, Vertical, or Diagonal lines.");
            Console.WriteLine("3. If the line matches, you win the amount equivalent to the number of lines played.");
            Console.WriteLine("4. Press Enter to spin.");
        }

        /// <summary>
        /// This method prompts the player to specify a monetary amount by displaying a message on the console.
        /// It then reads the player's input from the console, attempts to convert this input to an integer,
        /// and returns the resulting integer value. If the conversion is successful, this integer value
        /// represents the amount of money the player has inserted.
        /// </summary>
        /// <returns>
        /// The amount of money inserted by the player, as an integer.
        /// </returns>
        private static int GetInitialMoney()
        {
            Console.WriteLine("How much money would you like to insert? ");
            int remainingMoney = Convert.ToInt32(Console.ReadLine());
            return remainingMoney;
        }

        /// <summary>
        /// main game loops which continues  as long as the player has money 
        /// </summary>
        /// <param name="remainingMoney">Equals the money left for the player</param>
        private static void PlayGame(int remainingMoney)
        {
            int[,] slots = new int[ROWS, COLUMNS];
            Random rand = new Random();

            while (remainingMoney > 0)
            {
                Console.WriteLine($"Balance ${remainingMoney}");
                char lineType = GetLineType();
                int linesToPlay = GetLinesToPlay(lineType, remainingMoney);
                WaitForSpin();
                remainingMoney -= linesToPlay;
                FillSlots(slots, rand);
                int winnings = CalculateWinnings(slots, lineType, linesToPlay);
                DisplaySlots(slots);
                remainingMoney += winnings;
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

        /// <summary>
        /// Prompts the user to choose a line type for gameplay by entering a character: 'H' for Horizontal, 
        /// 'V' for Vertical, or 'D' for Diagonal. The method ensures valid input is received, and continues 
        /// to prompt the user in case of invalid input. Note: Diagonal is only considered a valid choice if 
        /// the number of rows is two or more.
        /// </summary>
        /// <returns>
        /// A character representing the chosen line type: 'H', 'V', or 'D'.
        /// </returns>
        private static char GetLineType()
        {
            char lineType;
            while (true)
            {
                Console.WriteLine("Choose the line type you would like to play: H for Horizontal, V for Vertical, and D for Diagonal");
                lineType = char.ToUpper(Console.ReadKey(true).KeyChar);
                if (lineType == LINE_TYPE_HORIZONTAL || lineType == LINE_TYPE_VERTICAL || (lineType == LINE_TYPE_DIAGONAL && ROWS >= 2))
                {
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid input. Please enter H, V, or D.");
                }
            }
            return lineType;
        }

        /// <summary>
        /// Determines the number of lines a player wishes to play in a game based on the type of line (diagonal or other) 
        /// and the amount of money remaining. The method prompts the user to input their choice and validates this input 
        /// to ensure it's a valid integer within the allowed range and does not exceed the player's remaining money.
        /// </summary>
        /// <param name="lineType">The type of line the player wishes to play. 
        /// Character value representing either diagonal lines or other types of lines.</param>
        /// <param name="remainingMoney">The amount of money the player has remaining. 
        /// Used to validate the player's choice does not exceed their balance.</param>
        /// <returns>The number of lines the player wishes to play. 
        /// Validated to be a positive integer within the allowed range and not exceeding the player's remaining money.</returns>
        private static int GetLinesToPlay(char lineType, int remainingMoney)
        {
            int minLinesToPlay = lineType == LINE_TYPE_DIAGONAL ? MIN_LINE_AMOUNT_DIAGONAL_PLAY : MIN_LINE_AMOUNT;
            int maxLinesToPlay = Math.Min(ROWS, COLUMNS);
            if (lineType == LINE_TYPE_DIAGONAL)
            {
                Console.WriteLine($"Choose the number of diagonal lines you would like to play (1 for left diagonal, 2 for both): ");
            }
            else
            {
                Console.WriteLine($"Choose the number of lines you would like to play ({minLinesToPlay} to {maxLinesToPlay}) ");
            }

            int linesToPlay;
            while (true)
            {
                bool isValidInput = int.TryParse(Console.ReadLine(), out linesToPlay);

                if (!isValidInput)
                {
                    Console.WriteLine("Please enter a valid integer.");
                    continue;
                }

                if (linesToPlay >= 1 && linesToPlay <= maxLinesToPlay && linesToPlay <= remainingMoney)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Invalid input. Please enter a number 1 to {maxLinesToPlay}, and ensure you have enough balance.");
                }
            }
            return linesToPlay;
        }

        /// <summary>
        /// waits for the user to initiate a spain by pressing the Enter key
        /// </summary>
        private static void WaitForSpin()
        {
            Console.WriteLine("Press Enter to start spin");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { Console.WriteLine("Please only press Enter."); }
        }

        /// <summary>
        ///  Fills the slots with random vaules 
        /// </summary>
        /// <param name="slots">2D array filled with random vaules</param>
        /// <param name="rand">Random object that generates the random values</param>
        private static void FillSlots(int[,] slots, Random rand)
        {
            for (int indexRow = 0; indexRow < ROWS; indexRow++)
            {
                for (int indexCol = 0; indexCol < COLUMNS; indexCol++)
                {
                    slots[indexRow, indexCol] = rand.Next(SLOT_MIN_VALUE, SLOT_MAX_VALUE);
                }
            }
        }

        /// <summary>
        /// Calculates the winnings based on matching elements in a slot machine grid.
        /// </summary>
        /// <param name="slots">2D array filled with random vaules.</param>
        /// <param name="lineType">The type of line (horizontal, vertical, or diagonal) to check for matching elements.</param>
        /// <param name="linesToPlay">The number of lines to play in the slot machine.</param>
        /// <returns>The total winnings based on the number of matching lines found.</returns>
        private static int CalculateWinnings(int[,] slots, char lineType, int linesToPlay)
        {
            int winnings = 0;

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
            if (linesToPlay >= 2)
            {
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
            if (lineType == LINE_TYPE_DIAGONAL)
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
                if (linesToPlay == 2)
                {
                    bool secondDigonalEqual = true;
                    for (int indexRow = 0; indexRow < ROWS - 1; indexRow++)
                    {
                        if (slots[indexRow, COLUMNS - 1 - indexRow] != slots[indexRow + 1, COLUMNS - 2 - indexRow])
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
           return winnings;
        }

        /// <summary>
        /// display the contents of a 2D array to the console 
        /// </summary>
        /// <param name="slots">The 2D array to be displayed</param>
        private static void DisplaySlots(int[,] slots)
        {
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
        }
    }
}
