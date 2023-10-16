using System;

namespace Slot_Machine
{
    public class UIMethods
    {
        /// <summary>
        /// Displays for the rules and introduction  
        /// </summary>
        public void DisplayIntro()
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
        public int GetInitialMoney()
        {
            Console.WriteLine("How much money would you like to insert? ");
            int remainingMoney = Convert.ToInt32(Console.ReadLine());
            return remainingMoney;
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
        public char GetLineType()
        {
            char lineType;
            while (true)
            {
                Console.WriteLine("Choose the line type you would like to play: H for Horizontal, V for Vertical, and D for Diagonal");
                lineType = char.ToUpper(Console.ReadKey(true).KeyChar);
                if (lineType == LogicMethods.LINE_TYPE_HORIZONTAL || lineType == LogicMethods.LINE_TYPE_VERTICAL || (lineType == LogicMethods.LINE_TYPE_DIAGONAL && LogicMethods.ROWS >= 2))
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
        public int GetLinesToPlay(char lineType, int remainingMoney)
        {
            int minLinesToPlay = lineType == LogicMethods.LINE_TYPE_DIAGONAL ? LogicMethods.MIN_LINE_AMOUNT_DIAGONAL_PLAY : LogicMethods.MIN_LINE_AMOUNT;
            int maxLinesToPlay = Math.Min(LogicMethods.ROWS, LogicMethods.COLUMNS);
            if (lineType == LogicMethods.LINE_TYPE_DIAGONAL)
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
        public void WaitForSpin()
        {
            Console.WriteLine("Press Enter to start spin");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                Console.WriteLine("Please only press Enter.");
            }
        }

        /// <summary>
        /// display the contents of a 2D array to the console 
        /// </summary>
        /// <param name="slots">The 2D array to be displayed</param>
        public void DisplaySlots(int[,] slots)
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