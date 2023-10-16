using System;
using Slot_Machine;
using static System.Reflection.Metadata.BlobBuilder;

namespace Slot_Machine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UIMethods uiMethods = new UIMethods();
            LogicMethods logicMethods = new LogicMethods();
            uiMethods.DisplayIntro();
            int remainingMoney = uiMethods.GetInitialMoney();
            PlayGame(remainingMoney, uiMethods, logicMethods);
        }

        /// <summary>
        /// Simulates the gameplay of the slot machine. The method initiates a game loop that continues until the player
        /// runs out of money. Within the loop, the method handles user interactions, calculates winnings,
        /// displays slot results, and manages the player's balance.
        /// </summary>
        /// <param name="remainingMoney">The amount of money the player has remaining.</param>
        /// <param name="uiMethods">An instance of the UIMethods class to handle user interactions.</param>
        /// <param name="logicMethods">An instance of the LogicMethods class to handle game logic.</param>
        private static void PlayGame(int remainingMoney, UIMethods uiMethods, LogicMethods logicMethods)
        {
            int[,] slots = new int[LogicMethods.ROWS, LogicMethods.COLUMNS];

            while (remainingMoney > 0)
            {
                Console.WriteLine($"Balance ${remainingMoney}");
                char lineType = uiMethods.GetLineType();
                int linesToPlay = uiMethods.GetLinesToPlay(lineType, remainingMoney);
                uiMethods.WaitForSpin();
                remainingMoney -= linesToPlay;
                int winnings = LogicMethods.CalculateWinnings(slots, lineType, linesToPlay);
                uiMethods.DisplaySlots(slots);
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
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}



