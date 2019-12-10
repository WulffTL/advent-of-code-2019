using System;
using System.Collections.Generic;

namespace advent_of_code_2019
{
    class IntcodeProgram
    {
        long[] instructions;
        long[] input;
        List<long> output;
        string inputFilePath;
        long currentInputIndex;
        bool isHalted;

        bool isPaused;
        long pointerPosition;
        long relativeBase;
        public long[] Input { get => input; set => input = value; }
        public List<long> Output { get => output; set => output = value; }
        public long CurrentInputIndex { get => currentInputIndex; set => currentInputIndex = value; }
        public bool IsHalted { get => isHalted; set => isHalted = value; }
        public long PointerPosition { get => pointerPosition; set => pointerPosition = value; }

        public IntcodeProgram(long[] input, string inputFilePath)
        {
            this.inputFilePath = inputFilePath;
            this.Input = input;
            this.CurrentInputIndex = 0;
            this.Output = new List<long>();
            this.IsHalted = false;
            this.pointerPosition = 0;
            this.relativeBase = 0;
            SetListFromInput();
        }

        public void PrintAnswer()
        {
           for(long i = 0; i < 100; i++)
           {
               for(long j = 0; j < 100; j++)
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

        public List<long> Test()
        {
            PerformIntCodes();
            return this.Output;
        }

        public List<long> Run()
        {
            PerformIntCodesUntilHalt();
            return this.Output;
        }
        private void Alter1202ProgramAlaram(long val1, long val2)
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
                this.pointerPosition += pointerMovementlength + 1;
            }
        }

        private void PerformIntCodesUntilHalt()
        {
            while (!this.isHalted)
            {
                var pointerMovementlength = PerformOperation();
                this.pointerPosition += pointerMovementlength + 1;
            }
        }

        private long PerformOperation()
        {
            var optCode = this.instructions[this.pointerPosition];
            var operation = GetOperation(optCode);
            var modes = GetModes(optCode);

            switch(operation)
            {
                case 1:
                    return Add(modes);
                case 2:
                    return Product(modes);
                case 3:
                    return PlaceInput(modes);
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
                case 9:
                    return AdjustRelativeBase(modes);
                case 99:
                    this.IsHalted = true;
                    return 0;
                default:
                    throw new Exception($"Invalid operator: {operation}");
            }
        }

        private Mode[] GetModes(long optCode)
        {
            Mode[] modes = new Mode[3];
            optCode /= 100;
            long i = 0;
            while(optCode > 0)
            {
                modes[i] = (Mode)(optCode % 10);
                optCode /= 10;
                i++;
            }
            return modes;
        }

        private long GetOperation(long optCode)
        {
            return optCode % 100;
        }

        private long GetFactor(Mode mode, long param)
        {
            switch(mode)
            {
                case Mode.Position:
                    return this.instructions[param];
                case Mode.Immediate:
                    return param;
                case Mode.Relative:
                    return this.instructions[this.relativeBase + param];
                default:
                    return -1;
            }
        }

        private long GetAddress(Mode mode, long param)
        {
            if(mode == Mode.Position)
            {
                return param;
            }
            if(mode == Mode.Relative)
            {
                return param + this.relativeBase;
            }
            return -1;
        }

        private long JumpIfTrue(Mode[] modes)
        {
            var firstParam = this.instructions[this.pointerPosition + 1];
            var secondParam = this.instructions[this.pointerPosition + 2];

            var factor1 = GetFactor(modes[0], firstParam);
            var factor2 = GetFactor(modes[1], secondParam);

            if (factor1 != 0)
            {
                return factor2 - this.pointerPosition - 1;
            }
            return 2;
        }

        private long JumpIfFalse(Mode[] modes)
        {
            var firstParam = this.instructions[this.pointerPosition + 1];
            var secondParam = this.instructions[this.pointerPosition + 2];

            var factor1 = GetFactor(modes[0], firstParam);
            var factor2 = GetFactor(modes[1], secondParam);

            if (factor1 == 0)
            {
                return factor2 - this.pointerPosition - 1;
            }
            return 2;
        }

        private long IsLessThan(Mode[] modes)
        {
            var firstParam = this.instructions[this.pointerPosition + 1];
            var secondParam = this.instructions[this.pointerPosition + 2];
            var thirdParam = this.instructions[this.pointerPosition + 3];

            var factor1 = GetFactor(modes[0], firstParam);
            var factor2 = GetFactor(modes[1], secondParam);
            var write = GetAddress(modes[2], thirdParam);
            this.instructions[write] = factor1 < factor2 ? 1 : 0;
            return 3;
        }

        private long IsEquals(Mode[] modes)
        {
            var firstParam = this.instructions[this.pointerPosition + 1];
            var secondParam = this.instructions[this.pointerPosition + 2];
            var thirdParam = this.instructions[this.pointerPosition + 3];

            var factor1 = GetFactor(modes[0], firstParam);
            var factor2 = GetFactor(modes[1], secondParam);
            var write = GetAddress(modes[2], thirdParam);
            this.instructions[write] = factor1 == factor2 ? 1 : 0;
            return 3;
        }

        private long SetOutput(Mode[] modes)
        {
            var firstParam = this.instructions[this.pointerPosition + 1];
            var address = GetFactor(modes[0], firstParam);
            this.Output.Add(address);
            this.isPaused = true;
            return 1;
        }

        private long PlaceInput(Mode[] modes)
        {
            var firstParam = this.instructions[this.pointerPosition + 1];
            var factor1 = GetAddress(modes[0], firstParam);
            this.instructions[factor1] = this.Input[this.CurrentInputIndex];
            if(this.CurrentInputIndex < this.input.Length - 1)
            {
                this.CurrentInputIndex++;
            }
            return 1;
        }

        private long AdjustRelativeBase(Mode[] modes)
        {
            var firstParam = this.instructions[this.pointerPosition + 1];
            var factor1 = GetFactor(modes[0], firstParam);
            this.relativeBase += factor1;
            return 1;
        }

        private long Add(Mode[] modes)
        {
            var summandIndex1 = this.instructions[this.pointerPosition + 1];
            var summandIndex2 = this.instructions[this.pointerPosition + 2];
            var resultIndex = this.instructions[this.pointerPosition + 3];

            var summand1 = GetFactor(modes[0], summandIndex1);
            var summand2 = GetFactor(modes[1], summandIndex2);
            var write = GetAddress(modes[2], resultIndex);
            this.instructions[write] = summand1 + summand2;

            return 3;
        }

        private long Product(Mode[] modes)
        {
            var factorIndex1 = this.instructions[this.pointerPosition + 1];
            var factorIndex2 = this.instructions[this.pointerPosition + 2];
            var resultIndex = this.instructions[this.pointerPosition + 3];

            var factor1 = GetFactor(modes[0], factorIndex1);
            var factor2 = GetFactor(modes[1], factorIndex2);
            var write = GetAddress(modes[2], resultIndex);
            this.instructions[write] = factor1 * factor2;

            return 3;
        }

        public void SetListFromInput()
        {
            var text = System.IO.File.ReadAllText(this.inputFilePath);
            var list = text.Split(",");
            var longList = new long[list.Length*100];
            for(long i = 0; i < list.Length; i++)
            {
                longList[i] = Int64.Parse(list[i]);
            }
            this.instructions = longList;  
        }
    }
}