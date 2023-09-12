using System;
using System.Threading;

namespace Slot_Machine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int UPPER_LIMIT = 3;

            Console.WriteLine("Slot Machine Game");

            int[,] slots = new int[3, 3];
            Random rand = new Random();

            Console.WriteLine("How much money would you like to insert? ");
            int money = Convert.ToInt32(Console.ReadLine());

            while (money > 0)
            {
                Console.WriteLine($"Balance ${money}");
                Console.WriteLine($"Press Enter to start spin");

                if (Console.ReadKey(true).Key != ConsoleKey.Enter)
                {
                    break;
                }
                money--;
                //loops through each row of the slots array
                for (int i = 0; i < slots.GetLength(0); i++)
                {
                    //loops through each colum of the slots array for the curret row 
                    for (int j = 0; j < slots.GetLength(1); j++)
                    {
                        //shift the nums between 0, 1, 2
                        int shiftNums = rand.Next(0, UPPER_LIMIT);
                        slots[i, j] = (slots[i, j] + shiftNums) % 3;
                    }
                    Console.Clear();
                }
                for (int i = 0; i < slots.GetLength(0); i++)
                {
                    for (int j = 0; j < slots.GetLength(1); j++)
                    {

                        Console.Write(slots[i, j] + " ");

                    }

                    Console.WriteLine();
                }
                for (int i = 0; i < UPPER_LIMIT; i++)
                    if (slots[i, 0] == slots[i, 1] && slots[i, 1] == slots[i, 2])
                    {
                        Console.WriteLine("YOU WIN!");
                        money++;
                    }
                if (money == 0)
                {
                    Console.WriteLine("YOU LOSE! Would you like to insert more money or preess any key to exit");

                    money = Convert.ToInt32(Console.ReadLine());
                }
                Thread.Sleep(500);
            }
        }
    }
}