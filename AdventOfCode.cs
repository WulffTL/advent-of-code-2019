using System;

namespace advent_of_code_2019
{
    class AdventOfCode
    {
        static void Main(string[] args)
        {
            var day = 1;
            if(args.Length > 1)
            {
                day = Int32.Parse(args[1]);
            }

            switch(day)
            {
                case 1:
                    RocketEquation.PrintTotalFuelRequiredForAllModules();
                    break;
                case 2:
                    IntcodeProgram.PrintAnswer();
                    break;
                default:
                    Console.WriteLine("Unexpected input failure");
                    break;
            }
        }
    }
}
