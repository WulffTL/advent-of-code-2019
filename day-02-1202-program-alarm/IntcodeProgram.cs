using System;

namespace advent_of_code_2019
{
    class IntcodeProgram
    {
        public static void PrintAnswer()
        {
           var list = GetListFromInput(); 
           for(int i = 0; i < 100; i++)
           {
               for(int j = 0; j < 100; j++)
               {
                    list = Alter1202ProgramAlaram(list, i, j);
                    list = PerformIntCodes(list);
                    if(list[0] == 19690720)
                    {
                        Console.WriteLine($"ANSWER: {i}, {j}");
                        return;
                    }
                    list = GetListFromInput();
               }
           }
        }

        private static int[] Alter1202ProgramAlaram(int[] list, int val1, int val2)
        {
            list[1] = val1;
            list[2] = val2;
            return list;
        }

        private static int[] PerformIntCodes(int[] list)
        {
            var index = 0;
            while (index <= list.Length - 1 && list[index] != 99)
            {
                list = PerformOperation(list, index);
                index += 4;
            }
            return list;
        }

        private static int[] PerformOperation(int[] list, int index)
        {
            if(index > list.Length - 1)
            {
                return list;
            }

            var operation = list[index];

            switch(operation)
            {
                case 1:
                    return Add(list, index);
                case 2:
                    return Product(list, index);
                case 99:
                    return list;
                default:
                    throw new Exception("Invalid operator");
            }
        }

        private static int[] Add(int[] list, int startIndex)
        {
            var summandIndex1 = list[startIndex + 1];
            var summandIndex2 = list[startIndex + 2];
            var resultIndex = list[startIndex + 3];
            list[resultIndex] = list[summandIndex1] + list[summandIndex2];
            return list;
        }

        private static int[] Product(int[] list, int startIndex)
        {
            var factorIndex1 = list[startIndex + 1];
            var factorIndex2 = list[startIndex + 2];
            var resultIndex = list[startIndex + 3];
            list[resultIndex] = list[factorIndex1] * list[factorIndex2];
            return list;
        }

        private static int[] GetListFromInput()
        {
            var text = System.IO.File.ReadAllText(@"day-02-1202-program-alarm/input.txt");
            var list = text.Split(",");
            var intList = new int[list.Length];
            for(int i = 0; i < intList.Length; i++)
            {
                intList[i] = Int32.Parse(list[i]);
            }
            return intList;  
        }
    }
}