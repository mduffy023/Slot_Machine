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

            slots[0, 0] = 1;
            slots[1, 0] = 0;
            slots[2, 0] = 2;
            slots[0, 1] = 2;
            slots[1, 1] = 0;
            slots[2, 1] = 1;
            slots[0, 2] = 0;
            slots[1, 2] = 2;
            slots[2, 2] = 1;

            Random  rand = new Random();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    break;
                }


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
                Thread.Sleep(1000);
            }          
        }
    }
}