using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPath
{
    public static class Map
    {
        public static int[,] TwoDimensionalaps = 
        {
            { 1,1,1,1,1,1,1,1,1,1}, 
            { 1,1,1,1,1,1,1,1,1,1}, 
            { 1,1,1,1,1,0,1,1,1,1}, 
            { 1,1,1,1,1,0,1,1,1,1}, 
            { 1,3,1,1,1,0,1,1,1,1}, 
            { 1,1,1,1,1,0,1,1,4,1}, 
            { 1,1,1,1,1,0,1,1,1,1}, 
            { 1,1,1,1,1,0,1,1,1,1}, 
            { 1,1,1,1,1,1,1,1,1,1}, 
            { 1,1,1,1,1,1,1,1,1,1}, 
        };

        public static void PrintMap()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (TwoDimensionalaps[i, j] == 1)
                    {
                        Console.Write(" * ");
                    }
                    else if (TwoDimensionalaps[i, j] == 0)
                    {
                        Console.Write(" & ");
                    }
                    else if (TwoDimensionalaps[i, j] == 3 || TwoDimensionalaps[i, j] == 4)
                    {
                        Console.Write(" # ");
                    }
                    else
                    {
                        Console.Write(" @ ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
