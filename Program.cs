﻿namespace Slot_Machine
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
        }
    }
}