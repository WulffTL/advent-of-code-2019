using System;
using System.Collections.Generic;

namespace advent_of_code_2019
{
    class IntcodeProgram
    {
        int[] instructions;
        int[] input;
        int output;
        string inputFilePath;
        int currentInputIndex;
        bool isHalted;

        bool isPaused;
        int pointerPosition;
        public int[] Input { get => input; set => input = value; }
        public int Output { get => output; set => output = value; }
        public int CurrentInputIndex { get => currentInputIndex; set => currentInputIndex = value; }
        public bool IsHalted { get => isHalted; set => isHalted = value; }
        public int PointerPosition { get => pointerPosition; set => pointerPosition = value; }

        public IntcodeProgram(int[] input, string inputFilePath)
        {
            this.inputFilePath = inputFilePath;
            this.Input = input;
            this.CurrentInputIndex = 0;
            this.Output = 0;
            this.IsHalted = false;
            this.PointerPosition = 0;
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

        public int Test()
        {
            PerformIntCodes();
            return this.Output;
        }
        private void Alter1202ProgramAlaram(int val1, int val2)
        {
            this.instructions[1] = val1;
            this.instructions[2] = val2;
        }

        private void PerformIntCodes()
        {
            this.isPaused = false;
            while (!this.isPaused && !this.isHalted)
            {
                var pointerMovementlength = PerformOperation();
                this.PointerPosition += pointerMovementlength + 1;
            }
        }

        private int PerformOperation()
        {
            var optCode = this.instructions[this.PointerPosition];
            var operation = GetOperation(optCode);
            var modes = GetModes(optCode);

            switch(operation)
            {
                case 1:
                    return Add(modes);
                case 2:
                    return Product(modes);
                case 3:
                    return PlaceInput();
                case 4:
                    return SetOutput(modes);
                case 5:
                    return JumpIfTrue(modes);
                case 6:
                    return JumpIfFalse(modes);
                case 7:
                    return IsLessThan(modes);
                case 8:
                    return IsEquals(modes);
                case 99:
                    this.IsHalted = true;
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

        private int JumpIfTrue(int[] modes)
        {
            var firstParam = this.instructions[this.PointerPosition + 1];
            var secondParam = this.instructions[this.PointerPosition + 2];

            var factor1 = modes[0] == 0 ? this.instructions[firstParam] : firstParam;
            var factor2 = modes[1] == 0 ? this.instructions[secondParam] : secondParam;

            if (factor1 != 0)
            {
                return factor2 - this.PointerPosition - 1;
            }
            return 2;
        }

        private int JumpIfFalse(int[] modes)
        {
            var firstParam = this.instructions[this.PointerPosition + 1];
            var secondParam = this.instructions[this.PointerPosition + 2];

            var factor1 = modes[0] == 0 ? this.instructions[firstParam] : firstParam;
            var factor2 = modes[1] == 0 ? this.instructions[secondParam] : secondParam;

            if (factor1 == 0)
            {
                return factor2 - this.PointerPosition - 1;
            }
            return 2;
        }

        private int IsLessThan(int[] modes)
        {
            var firstParam = this.instructions[this.PointerPosition + 1];
            var secondParam = this.instructions[this.PointerPosition + 2];
            var thirdParam = this.instructions[this.PointerPosition + 3];

            var factor1 = modes[0] == 0 ? this.instructions[firstParam] : firstParam;
            var factor2 = modes[1] == 0 ? this.instructions[secondParam] : secondParam;
            this.instructions[thirdParam] = factor1 < factor2 ? 1 : 0;
            return 3;
        }

        private int IsEquals(int[] modes)
        {
            var firstParam = this.instructions[this.PointerPosition + 1];
            var secondParam = this.instructions[this.PointerPosition + 2];
            var thirdParam = this.instructions[this.PointerPosition + 3];

            var factor1 = modes[0] == 0 ? this.instructions[firstParam] : firstParam;
            var factor2 = modes[1] == 0 ? this.instructions[secondParam] : secondParam;
            this.instructions[thirdParam] = factor1 == factor2 ? 1 : 0;
            return 3;
        }

        private int SetOutput(int[] modes)
        {
            var outputIndex = this.instructions[this.PointerPosition + 1];
            var factor1 = modes[0] == 0 ? this.instructions[outputIndex] : outputIndex;
            this.Output = factor1;
            this.isPaused = true;
            return 1;
        }

        private int PlaceInput()
        {
            var newIndex = this.instructions[this.PointerPosition + 1];
            this.instructions[newIndex] = this.Input[this.CurrentInputIndex];
            if(this.CurrentInputIndex < this.input.Length - 1)
            {
                this.CurrentInputIndex++;
            }
            return 1;
        }

        private int Add(int[] modes)
        {
            var summandIndex1 = this.instructions[this.PointerPosition + 1];
            var summandIndex2 = this.instructions[this.PointerPosition + 2];
            var resultIndex = this.instructions[this.PointerPosition + 3];

            var summand1 = modes[0] == 0 ? this.instructions[summandIndex1] : summandIndex1;
            var summand2 = modes[1] == 0 ? this.instructions[summandIndex2] : summandIndex2;
            this.instructions[resultIndex] = summand1 + summand2;

            return 3;
        }

        private int Product(int[] modes)
        {
            var factorIndex1 = this.instructions[this.PointerPosition + 1];
            var factorIndex2 = this.instructions[this.PointerPosition + 2];
            var resultIndex = this.instructions[this.PointerPosition + 3];

            var factor1 = modes[0] == 0 ? this.instructions[factorIndex1] : factorIndex1;
            var factor2 = modes[1] == 0 ? this.instructions[factorIndex2] : factorIndex2;
            this.instructions[resultIndex] = factor1 * factor2;

            return 3;
        }

        public void SetListFromInput()
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