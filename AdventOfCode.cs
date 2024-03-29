using System;

namespace advent_of_code_2019
{
    class AdventOfCode
    {
        static void Main(string[] args)
        {
            var day = 9;
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
                    var intcodeProgram = new IntcodeProgram(new long[]{1}, "day-02-1202-program-alarm/input.txt");
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
                    var intcodeProgramTEST = new IntcodeProgram(new long[]{5}, "day-05-sunny-with-a-chance-of-asteroids/input.txt");
                    Console.WriteLine(intcodeProgramTEST.Test());
                    break;
                case 6:
                    var orbits = new Orbits();
                    orbits.PrintAnswer();
                    break;
                case 7:
                    var amplificationCircuit = new AmplificationCircuit();
                    amplificationCircuit.PrintAnswer();
                    break;
                case 8:
                    var spaceImageFormat = new SpaceImageFormat("day-08-space-image-format/input.txt", 25, 6);
                    spaceImageFormat.PrintAnswer2();
                    break;
                case 9:
                    var sensorBoost = new SensorBoost("day-09-sensor-boost/input.txt");
                    sensorBoost.PrintAnswer1();
                    break;
                default:
                    Console.WriteLine("Unexpected input failure");
                    break;
            }
        }
    }
}
