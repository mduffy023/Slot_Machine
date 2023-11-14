namespace Slot_Machine
{
    public class LogicMethods
    {
        public const char LINE_TYPE_HORIZONTAL = 'H';
        public const char LINE_TYPE_VERTICAL = 'V';
        public const char LINE_TYPE_DIAGONAL = 'D';
        public const int SLOT_MIN_VALUE = 0;
        public const int SLOT_MAX_VALUE = 3;
        public const int MIN_LINE_AMOUNT = 1;
        public const int MIN_LINE_AMOUNT_DIAGONAL_PLAY = 2;
        public const int ROW_COUNT = 3;
        public const int COLUMN_COUNT = 3;

        public static readonly Random rand = new Random();

        /// <summary>
        /// Determines the minimum number of lines a player needs to play based on the selected line type.
        /// </summary>
        /// <param name="lineType">A character representing the chosen line type by the player. Expected values are LINE_TYPE_HORIZONTAL, LINE_TYPE_VERTICAL, or LINE_TYPE_DIAGONAL.</param>
        /// <returns>Returns the constant MIN_LINE_AMOUNT if a valid line type is provided.</returns>
        /// <exception cref="ArgumentException">Throws an exception if an invalid line type is provided.</exception>
        public static int GetMinLinesToPlay(char lineType)
        {
          if(lineType == LINE_TYPE_HORIZONTAL || lineType == LINE_TYPE_VERTICAL || lineType == LINE_TYPE_DIAGONAL)
            {
                return MIN_LINE_AMOUNT;
            }
            else
            {
                throw new ArgumentException($"Invalid line type: {lineType}");
            }       
       }

        /// <summary>
        /// Determines the maximum number of lines a player can play based on the selected line type and the structure of the slot machine.
        /// </summary>
        /// <param name="lineType">A character representing the chosen line type by the player. Expected values are LINE_TYPE_HORIZONTAL, LINE_TYPE_VERTICAL, or LINE_TYPE_DIAGONAL.</param>
        /// <returns>
        /// Returns the number of ROWS if lineType is LINE_TYPE_HORIZONTAL, the number of COLUMNS if lineType is LINE_TYPE_VERTICAL, and 2 for LINE_TYPE_DIAGONAL (accounting for both left-to-right and right-to-left diagonals).
        /// </returns>
        /// <exception cref="ArgumentException">Throws an exception if an invalid line type is provided.</exception>
        public static int GetMaxLinesToPlay(char lineType)
        {
            if (lineType == LINE_TYPE_HORIZONTAL)
            {
                return ROW_COUNT;
            }
            else if (lineType == LINE_TYPE_VERTICAL)
            {
                return COLUMN_COUNT;
            }
            else if (lineType == LINE_TYPE_DIAGONAL)
            {
                return MIN_LINE_AMOUNT_DIAGONAL_PLAY; // Left diagonal and right diagonal.
            }
            else
            {
                throw new ArgumentException($"Invalid line type: {lineType}");
            }
        }
        
    /// <summary>
    ///  Fills the slots with random vaules 
    /// </summary>
    /// <param name="slots">2D array filled with random vaules</param>
    /// <param name="rand">Random object that generates the random values</param>
    public static int[,] FillSlots()  // Updated method signature
        {
            int[,] slots = new int[ROW_COUNT, COLUMN_COUNT];
            for (int indexRow = 0; indexRow < ROW_COUNT; indexRow++)
            {
                for (int indexCol = 0; indexCol < COLUMN_COUNT; indexCol++)
                {
                    slots[indexRow, indexCol] = rand.Next(SLOT_MIN_VALUE, SLOT_MAX_VALUE + 1);  // Use global Random object
                }
            }
            return slots;  // Return the newly created and filled array
        }


        /// <summary>
        /// Calculates the winnings based on the line type and number of lines played.
        /// </summary>
        /// <param name="slots">The 2D array representing the slot machine grid.</param>
        /// <param name="lineType">The type of line (horizontal, vertical, or diagonal) to check for winning combinations.</param>
        /// <param name="linesToPlay">The number of lines the player has chosen to play.</param>
        /// <returns>The total winnings based on matching elements along the specified line
        public static int CalculateWinnings(int[,] slots, char lineType, int linesToPlay)
        {
            int winnings = 0;

            if (lineType == LINE_TYPE_HORIZONTAL)
            {
                winnings = CheckHorizontal(slots, linesToPlay);
            }
            if (lineType == LINE_TYPE_VERTICAL)
            {
                winnings += CheckVertical(slots, linesToPlay);
            }
            if (lineType == LINE_TYPE_DIAGONAL)
            {
                winnings += CheckDiagonal(slots, linesToPlay);
            }

            return winnings;
        }

        /// <summary>
        /// Checks horizontal lines for winning combinations in the slot machine grid.
        /// </summary>
        /// <param name="slots">The 2D array representing the slot machine grid.</param>
        /// <param name="linesToPlay">The number of lines the player has chosen to play.</param>
        /// <returns>The total winnings from horizontal lines based on matching elements and the number of lines played.</returns>
        public static int CheckHorizontal(int[,] slots, int linesToPlay)
        {
            int winAmount = 0;
            for (int indexRow = 0; indexRow < linesToPlay; indexRow++)
            {
                bool allEqual = true;
                for (int indexCol = 0; indexCol < COLUMN_COUNT - 1; indexCol++)
                {
                    if (slots[indexRow, indexCol] != slots[indexRow, indexCol + 1])
                    {
                        allEqual = false;
                        break;
                    }
                }
                if (allEqual)
                {
                    winAmount += linesToPlay;
                }
            }
            return winAmount;
        }

        /// <summary>
        /// Checks vertical lines for winning combinations in the slot machine grid.
        /// </summary>
        /// <param name="slots">The 2D array representing the slot machine grid.</param>
        /// <param name="linesToPlay">The number of lines the player has chosen to play.</param>
        /// <returns>The total winnings from vertical lines based on matching elements and the number of lines played.</returns>
        public static int CheckVertical(int[,] slots, int linesToPlay)
        {
            int winAmount = 0;
            for (int indexCol = 0; indexCol < linesToPlay; indexCol++)
            {
                bool allEqual = true;
                for (int indexRow = 0; indexRow < ROW_COUNT - 1; indexRow++)
                {
                    if (slots[indexRow, indexCol] != slots[indexRow + 1, indexCol])
                    {
                        allEqual = false;
                        break;
                    }
                }
                if (allEqual)
                {
                    winAmount += linesToPlay;
                }
            }
            return winAmount;
        }

        
        public static int getNewBetAmount(int winnings, int remainingMoney)
        {
            if (remainingMoney == 0)
            {
                Console.WriteLine();
                Console.WriteLine("You Lose! Would you like to insert more money or press any key to exit?");
                if (int.TryParse(Console.ReadLine(), out int additionalMoney))
                {
                    remainingMoney += additionalMoney;
                }
            }
            System.Threading.Thread.Sleep(500);
            return remainingMoney;
        }

        /// <summary>
        /// Checks diagonal lines for winning combinations in the slot machine grid.
        /// </summary>
        /// <param name="slots">The 2D array representing the slot machine grid.</param>
        /// <param name="linesToPlay">The number of lines the player has chosen to play.</param>
        /// <returns>The total winnings from diagonal lines based on matching elements and the number of lines played.</returns>
        public static int CheckDiagonal(int[,] slots, int linesToPlay)
        {
            int winAmount = 0;

            if (linesToPlay >= MIN_LINE_AMOUNT)
            {
                bool firstDiagonalEqual = true;
                for (int indexRow = 0; indexRow < ROW_COUNT - 1; indexRow++)
                {
                    if (slots[indexRow, indexRow] != slots[indexRow + 1, indexRow + 1])
                    {
                        firstDiagonalEqual = false;
                        break;
                    }
                }
                if (firstDiagonalEqual)
                {
                    winAmount += linesToPlay;
                }
            }

            if (linesToPlay == MIN_LINE_AMOUNT_DIAGONAL_PLAY)
            {
                bool secondDiagonalEqual = true;
                for (int indexRow = 0; indexRow < ROW_COUNT - 1; indexRow++)
                {
                    if (slots[indexRow, COLUMN_COUNT - 1 - indexRow] != slots[indexRow + 1, COLUMN_COUNT - 2 - indexRow])
                    {
                        secondDiagonalEqual = false;
                        break;
                    }
                }
                if (secondDiagonalEqual)
                {
                    winAmount += linesToPlay;
                }
            }

            return winAmount;
        }
    }
}
