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
        public const int ROWS = 3;
        public const int COLUMNS = 3;

        public static readonly Random rand = new Random();

        /// <summary>
        /// Gets the minimum number of lines to play based on the selected line type.
        /// </summary>
        /// <param name="lineType">The type of line chosen by the player (horizontal, vertical, or diagonal).</param>
        /// <returns>The minimum number of lines to play.</returns>
        public static int GetMinLinesToPlay(char lineType)
        {
          return lineType == LINE_TYPE_DIAGONAL ? MIN_LINE_AMOUNT_DIAGONAL_PLAY : MIN_LINE_AMOUNT;
        }

        // <summary>
        /// Gets the maximum number of lines to play based on the selected line type and the dimensions of the slot machine.
        /// </summary>
        /// <param name="lineType">The type of line chosen by the player (horizontal, vertical, or diagonal).</param>
        /// <returns>The maximum number of lines to play.</returns>
        public static int GetMaxLinesToPlay(char lineType)
        {
            return Math.Max(ROWS, COLUMNS);
        }

        /// <summary>
        ///  Fills the slots with random vaules 
        /// </summary>
        /// <param name="slots">2D array filled with random vaules</param>
        /// <param name="rand">Random object that generates the random values</param>
        public static int[,] FillSlots()  // Updated method signature
        {
            int[,] slots = new int[ROWS, COLUMNS];
            for (int indexRow = 0; indexRow < ROWS; indexRow++)
            {
                for (int indexCol = 0; indexCol < COLUMNS; indexCol++)
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
                    winAmount += linesToPlay;
                }
            }
            return winAmount;
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

            if (linesToPlay >= 1)
            {
                bool firstDiagonalEqual = true;
                for (int indexRow = 0; indexRow < ROWS - 1; indexRow++)
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

            if (linesToPlay == 2)
            {
                bool secondDiagonalEqual = true;
                for (int indexRow = 0; indexRow < ROWS - 1; indexRow++)
                {
                    if (slots[indexRow, COLUMNS - 1 - indexRow] != slots[indexRow + 1, COLUMNS - 2 - indexRow])
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
