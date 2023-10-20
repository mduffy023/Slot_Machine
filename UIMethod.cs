namespace Slot_Machine
{
    public class UIMethods
    {
        /// <summary>
        /// Displays for the rules and introduction  
        /// </summary>
        public static void DisplayIntro()
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
        public static int GetInitialMoney()
        {
            Console.WriteLine();
            Console.WriteLine("How much money would you like to insert? ");
            int remainingMoney = Convert.ToInt32(Console.ReadLine());
            return remainingMoney;
        }

        public static int DisplayBalance(int remainingMoney)
        {
            Console.WriteLine();
            Console.WriteLine($"Balance ${remainingMoney}");
            return remainingMoney;
        }

        public static int DisplayWinningsAndBalance(int winnings, int remainingMoney)
        {
            Console.WriteLine();
            Console.WriteLine($"You have won ${winnings}. Current Balance: ${remainingMoney}");
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
        public static char GetLineType()
        {
            char lineType;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"Choose the line type you would like to play: {LogicMethods.LINE_TYPE_HORIZONTAL} for Horizontal, {LogicMethods.LINE_TYPE_VERTICAL} for Vertical, and {LogicMethods.LINE_TYPE_DIAGONAL} for Diagonal");
                Console.WriteLine();
               lineType = char.ToUpper(Console.ReadKey(true).KeyChar);
                if (lineType == LogicMethods.LINE_TYPE_HORIZONTAL || lineType == LogicMethods.LINE_TYPE_VERTICAL || (lineType == LogicMethods.LINE_TYPE_DIAGONAL && LogicMethods.ROW_COUNT >= 2))
                {
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please enter H, V, or D.");
                }
            }
            return lineType;
        }


        /// <summary>
        /// Interactively prompts the player to determine the number of lines they wish to play for a given game round.
        /// The method considers the line type (diagonal or other) and the player's remaining money to ensure valid input.
        /// </summary>
        /// <param name="lineType">A character indicating the line type the player wishes to play. Can represent diagonal lines or other line types.</param>
        /// <param name="remainingMoney">An integer representing the player's remaining money. This is used to ensure the player does not choose to play more lines than they can afford.</param>
        /// <returns>A validated integer indicating the number of lines the player wishes to play. This value will always be positive, within the permitted range based on the line type, and will not exceed the player's available balance.</returns>
        public static int GetLinesToPlay(char lineType, int remainingMoney)
        {
            int minLinesToPlay = LogicMethods.GetMinLinesToPlay(lineType);
            int maxLinesToPlay = LogicMethods.GetMaxLinesToPlay(lineType);

            Console.WriteLine();
            Console.WriteLine($"Choose the number of lines you would like to play ({minLinesToPlay} to {maxLinesToPlay}) ");

            int linesToPlay;
            while (true)
            {
                bool isValidInput = int.TryParse(Console.ReadLine(), out linesToPlay);

                if (!isValidInput)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a valid integer.");
                    continue;
                }

                if (linesToPlay >= minLinesToPlay && linesToPlay <= maxLinesToPlay && linesToPlay <= remainingMoney)
                {
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Invalid input. Please enter a number {minLinesToPlay} to {maxLinesToPlay}, and ensure you have enough balance.");
                }
            }
            return linesToPlay;
        }

        /// <summary>
        /// waits for the user to initiate a spain by pressing the Enter key
        /// </summary>
        public static void WaitForSpin()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to start spin");
            Console.WriteLine();
            Console.ReadKey(true);       
        }

        /// <summary>
        /// display the contents of a 2D array to the console 
        /// </summary>
        /// <param name="slots">The 2D array to be displayed</param>
        public static void DisplaySlots(int[,] slots)
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