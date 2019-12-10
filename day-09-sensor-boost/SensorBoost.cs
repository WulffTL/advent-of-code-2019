using System;
using System.Collections.Generic;

namespace advent_of_code_2019
{
    class SensorBoost
    {
        string inputFilePath;
        IntcodeProgram intcodeProgram;

        public SensorBoost(string inputFilePath)
        {
            this.inputFilePath = inputFilePath;
            this.intcodeProgram = new IntcodeProgram(new long[]{2}, this.inputFilePath);
        }

        public void PrintAnswer1()
        {
            var output = this.intcodeProgram.Run();
            foreach(var a in output)
            {
                Console.Write($"{a},");
            }
        }
    }
}