using System;
using System.Collections.Generic;

namespace advent_of_code_2019
{
    class IntcodeProgram
    {
        int[] instructions;
        int input;
        string inputFilePath;

        public IntcodeProgram(int input, string inputFilePath)
        {
            this.inputFilePath = inputFilePath;
            this.input = input;
            SetListFromInput();
        }

        public void PrintAnswer()
        {
           for(int i = 0; i < 100; i++)
           {
               for(int j = 0; j < 100; j++)
               {
                    Alter1202ProgramAlaram(i, j);
                    PerformIntCodes();
                    if(this.instructions[0] == 19690720)
                    {
                        Console.WriteLine($"ANSWER: {i}, {j}");
                        return;
                    }
                    SetListFromInput();
               }
           }
        }

        public void Test()
        {
            PerformIntCodes();
        }
        private void Alter1202ProgramAlaram(int val1, int val2)
        {
            this.instructions[1] = val1;
            this.instructions[2] = val2;
        }

        private void PerformIntCodes()
        {
            var index = 0;
            while (index <= this.instructions.Length - 1 && this.instructions[index] != 99)
            {
                var pointerMovementlength = PerformOperation(index);
                index += pointerMovementlength + 1;
            }
        }

        private int PerformOperation(int index)
        {
            if(index > this.instructions.Length - 1)
            {
                return 0;
            }

            var optCode = this.instructions[index];
            var operation = GetOperation(optCode);
            var modes = GetModes(optCode);

            switch(operation)
            {
                case 1:
                    return Add(index, modes);
                case 2:
                    return Product(index, modes);
                case 3:
                    return PlaceInput(index);
                case 4:
                    return Output(index, modes);
                case 5:
                    return JumpIfTrue(index, modes);
                case 6:
                    return JumpIfFalse(index, modes);
                case 7:
                    return IsLessThan(index, modes);
                case 8:
                    return IsEquals(index, modes);
                case 99:
                    return 0;
                default:
                    throw new Exception($"Invalid operator: {operation}");
            }
        }

        private int[] GetModes(int optCode)
        {
            int[] modes = new int[3];
            optCode /= 100;
            int i = 0;
            while(optCode > 0)
            {
                modes[i] = optCode % 10;
                optCode /= 10;
                i++;
            }
            return modes;
        }

        private int GetOperation(int optCode)
        {
            return optCode % 100;
        }

        private int JumpIfTrue(int index, int[] modes)
        {
            var firstParam = this.instructions[index + 1];
            var secondParam = this.instructions[index + 2];

            var factor1 = modes[0] == 0 ? this.instructions[firstParam] : firstParam;
            var factor2 = modes[1] == 0 ? this.instructions[secondParam] : secondParam;

            if (factor1 != 0)
            {
                return factor2 - index - 1;
            }
            return 2;
        }

        private int JumpIfFalse(int index, int[] modes)
        {
            var firstParam = this.instructions[index + 1];
            var secondParam = this.instructions[index + 2];

            var factor1 = modes[0] == 0 ? this.instructions[firstParam] : firstParam;
            var factor2 = modes[1] == 0 ? this.instructions[secondParam] : secondParam;

            if (factor1 == 0)
            {
                return factor2 - index - 1;
            }
            return 2;
        }

        private int IsLessThan(int index, int[] modes)
        {
            var firstParam = this.instructions[index + 1];
            var secondParam = this.instructions[index + 2];
            var thirdParam = this.instructions[index + 3];

            var factor1 = modes[0] == 0 ? this.instructions[firstParam] : firstParam;
            var factor2 = modes[1] == 0 ? this.instructions[secondParam] : secondParam;
            this.instructions[thirdParam] = factor1 < factor2 ? 1 : 0;
            return 3;
        }

        private int IsEquals(int index, int[] modes)
        {
            var firstParam = this.instructions[index + 1];
            var secondParam = this.instructions[index + 2];
            var thirdParam = this.instructions[index + 3];

            var factor1 = modes[0] == 0 ? this.instructions[firstParam] : firstParam;
            var factor2 = modes[1] == 0 ? this.instructions[secondParam] : secondParam;
            this.instructions[thirdParam] = factor1 == factor2 ? 1 : 0;
            return 3;
        }

        private int Output(int index, int[] modes)
        {
            var outputIndex = this.instructions[index + 1];
            var factor1 = modes[0] == 0 ? this.instructions[outputIndex] : outputIndex;
            Console.WriteLine(factor1);
            return 1;
        }

        private int PlaceInput(int startIndex)
        {
            var newIndex = this.instructions[startIndex + 1];
            this.instructions[newIndex] = this.input;
            return 1;
        }

        private int Add(int startIndex, int[] modes)
        {
            var summandIndex1 = this.instructions[startIndex + 1];
            var summandIndex2 = this.instructions[startIndex + 2];
            var resultIndex = this.instructions[startIndex + 3];

            var summand1 = modes[0] == 0 ? this.instructions[summandIndex1] : summandIndex1;
            var summand2 = modes[1] == 0 ? this.instructions[summandIndex2] : summandIndex2;
            this.instructions[resultIndex] = summand1 + summand2;

            return 3;
        }

        private int Product(int startIndex, int[] modes)
        {
            var factorIndex1 = this.instructions[startIndex + 1];
            var factorIndex2 = this.instructions[startIndex + 2];
            var resultIndex = this.instructions[startIndex + 3];

            var factor1 = modes[0] == 0 ? this.instructions[factorIndex1] : factorIndex1;
            var factor2 = modes[1] == 0 ? this.instructions[factorIndex2] : factorIndex2;
            this.instructions[resultIndex] = factor1 * factor2;

            return 3;
        }

        private void SetListFromInput()
        {
            var text = System.IO.File.ReadAllText(this.inputFilePath);
            var list = text.Split(",");
            var intList = new int[list.Length];
            for(int i = 0; i < intList.Length; i++)
            {
                intList[i] = Int32.Parse(list[i]);
            }
            this.instructions = intList;  
        }
    }
}