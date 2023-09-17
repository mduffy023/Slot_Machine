using System;
using System.Threading;

namespace Slot_Machine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int ROWS = 3;
            const int COLUMNS = 3;

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
                        int shiftNums = rand.Next(0, ROWS);
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
                //row check
                for (int indexRow = 0; indexRow < slots.GetLength(0); indexRow++)
                {
                    int refValue = slots[indexRow, 0];
                    bool isMatchingRow = true;

                    for (int indexColumn = 0; indexColumn < slots.GetLength(1); indexColumn++)            
                    {
                        if(slots[indexRow, indexColumn] != refValue)
                        {
                            isMatchingRow = false;
                            break;
                        }                    
                    }                  
                    if (isMatchingRow) 
                    {
                        Console.WriteLine($"You WIN! Row matches on {(indexRow+1)}");
                        money ++;
                    }
                }
                //column check
                for (int indexColumn = 0; indexColumn < COLUMNS; indexColumn++)
                {
                    int refValue = slots[indexColumn, 0];
                    bool isMatchingColumn = true;
                    for (int indexRow = 0; indexRow < ROWS; indexRow++)
                    {
                        if (slots[indexRow, indexColumn] != refValue)
                        {
                            isMatchingColumn = false;
                            break;
                        }
                    }
                    if(isMatchingColumn)
                    {
                        Console.WriteLine($"You WIN! Columm matches on {(indexColumn + 1)}");
                        money++;
                    }
                }
                //main diagonal checks 
                bool MainDiagonalMatch = true;
                int mainDigonalRef = slots[0, 0];
                for (int indexRow = 0; indexRow < ROWS; indexRow++)
                {
                    if (slots[indexRow, indexRow] != mainDigonalRef)
                    {
                        MainDiagonalMatch = false;
                        break;
                    }
                }
                if (MainDiagonalMatch)
                {
                   Console.WriteLine("You WIN! Main Diagonal Matches!");
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