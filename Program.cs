namespace Slot_Machine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UIMethods.DisplayIntro();
            int remainingMoney = UIMethods.GetInitialMoney();

            int[,] slots = LogicMethods.FillSlots();

            while (remainingMoney > 0)
            {
                remainingMoney = UIMethods.DisplayBalance(remainingMoney);

                char lineType = UIMethods.GetLineType();
                int linesToPlay = UIMethods.GetLinesToPlay(lineType, remainingMoney);

                UIMethods.WaitForSpin();
                remainingMoney -= linesToPlay;
                int winnings = LogicMethods.CalculateWinnings(slots, lineType, linesToPlay);

                UIMethods.DisplaySlots(slots);
                remainingMoney += winnings;

                remainingMoney = UIMethods.DisplayWinningsAndBalance(winnings, remainingMoney);
            }
        }
    }
}



