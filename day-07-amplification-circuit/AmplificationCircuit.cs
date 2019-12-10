using System;
using System.Collections.Generic;

namespace advent_of_code_2019
{
    class AmplificationCircuit
    {
        IntcodeProgram[] amplifiers;
        HashSet<string> phaseSettings;

        public AmplificationCircuit()
        {
            this.amplifiers = new IntcodeProgram[5];
            for(int i = 0; i < 5; i++)
            {
                this.amplifiers[i] = new IntcodeProgram(new long[]{1}, "day-07-amplification-circuit/input.txt");
            }
            this.phaseSettings = new HashSet<string>();
            this.Permute("98765", 0, 4);
        }

        public void PrintAnswer()
        {
            long maxOutput = 0;
            var maxPhaseSetting = "";
            foreach(var phaseSetting in this.phaseSettings)
            {
                for(long i = 0; i < 5; i++)
                {
                    this.amplifiers[i].SetListFromInput();
                    this.amplifiers[i].CurrentInputIndex = 0;
                    this.amplifiers[i].IsHalted = false;
                    this.amplifiers[i].PointerPosition = 0;
                }
                long previousOutput = 0;
                while(!this.amplifiers[4].IsHalted)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var ps = int.Parse(phaseSetting[i].ToString());
                        this.amplifiers[i].Input = new long[] { ps, previousOutput };
                        var output = this.amplifiers[i].Test();
                        previousOutput = output[output.Count - 1];
                    }
                }
                if (previousOutput > maxOutput)
                {
                    maxOutput = previousOutput;
                    maxPhaseSetting = phaseSetting;
                }
            }
            Console.WriteLine(maxPhaseSetting);
            Console.WriteLine(maxOutput);
        }

        private void Permute(string str, int leftBound, int rightBound) 
        {
            HashSet<string> permutations = new HashSet<string>();
            if(leftBound == rightBound)
            {
                this.phaseSettings.Add(str);
            }
            for(int i = leftBound; i <= rightBound; i++)
            { 
                str = Swap(str, leftBound, i); 
                Permute(str, leftBound + 1, rightBound); 
                str = Swap(str, leftBound, i); 
            }
        }

        public static string Swap(string a, int i, int j) 
        { 
            char temp; 
            char[] charArray = a.ToCharArray(); 
            temp = charArray[i]; 
            charArray[i] = charArray[j]; 
            charArray[j] = temp; 
            string s = new string(charArray); 
            return s; 
        }

    } 
}