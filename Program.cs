using System;
using System.Threading;

namespace Slot_Machine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Slot Machine Game");

            int[,] slots = new int [3, 3];

            Random  rand = new Random();

            int Money;

            Console.WriteLine("how much money would you like to insert? ");

            Money = Convert.ToInt32(Console.ReadLine());

            while (Money > 0)
            {    
             
                
             Console.WriteLine($"Balance ${Money}");

             Console.WriteLine($"would you like to play? press enter to continue");

            if (Console.ReadKey(true).Key != ConsoleKey.Enter)
               {
                 break;
               }

               Money--;
                  
                //loops through each row of the slots array
            for (int i = 0; i < slots.GetLength(0); i++)
            {
                 //loops through each colum of the slots array for the curret row 
                for (int j = 0; j < slots.GetLength(0); j++)
                {
                        //shift the nums between 0, 1, 2
                        int shiftNums = rand.Next(0, 3);
                        slots[i, j] = (slots[i,j] + shiftNums) % 3 ;                   
                }

                Console.Clear();
            }
             
            for (int i = 0; i < slots.GetLength(1); i++)
            {
               for (int j = 0; j < slots.GetLength(1); j++)
               {

                Console.Write(slots[i, j] + " ");

               }

                Console.WriteLine();
            }

                if (slots[1,0] == slots[1,1] && slots[1,1] == slots[1,2])
                {
                    Console.WriteLine("YOU WIN!");
                    Money++;
                }
                if(Money == 0)
                {
                    Console.WriteLine("YOU LOSE! Would you like to insert more money or preess any key to exit");

                    Money = Convert.ToInt32(Console.ReadLine());
                }
                Thread.Sleep(500);
            }          
        }
    }
}