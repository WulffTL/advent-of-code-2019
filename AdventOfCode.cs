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
                    var intcodeProgram = new IntcodeProgram(0);
                    intcodeProgram.PrintAnswer();
                    break;
                case 3:
                    var crossedWires = new CrossedWires();
                    crossedWires.PrintAnswer();
                    break;
                case 4:
                    var secureContainer = new SecureContainer();
                    secureContainer.PrintAnswer();
                    break;
                case 5:
                    var intcodeProgramTEST = new IntcodeProgram(5);
                    intcodeProgramTEST.Test();
                    break;
                default:
                    Console.WriteLine("Unexpected input failure");
                    break;
            }
        }
    }
}
