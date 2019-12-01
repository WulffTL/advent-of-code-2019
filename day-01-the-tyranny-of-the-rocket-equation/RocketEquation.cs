using System;

namespace advent_of_code_2019
{
    class RocketEquation
    {
        public static void PrintTotalFuelRequiredForAllModules()
        {
            var moduleMasses = GetModuleMassesFromInput();
            Console.WriteLine(GetTotalFuelRequired(moduleMasses));
        }

        public static int GetTotalFuelRequired(int[] moduleMasses)
        {
            var totalFuelRequiredForAllModules = 0;
            foreach (var mass in moduleMasses)
            {
                totalFuelRequiredForAllModules += GetTotalFuelRequired(mass);
            }
            return totalFuelRequiredForAllModules;
        }

        public static int GetTotalFuelRequired(int moduleMass)
        {
            var initialFuelRequired = GetFuelRequired(moduleMass);
            var additionalFuelRequired = GetAdditionalFuelRequired(initialFuelRequired);
            return initialFuelRequired + additionalFuelRequired;
        }

        private static int GetAdditionalFuelRequired(int initialFuelRequired)
        {
            int additionalFuelRequired = 0;
            int previousValue = initialFuelRequired;
            while(previousValue != 0)
            {
                previousValue = GetFuelRequired(previousValue);
                additionalFuelRequired += previousValue;
            }
            return additionalFuelRequired;
        }

        private static int GetFuelRequired(int moduleMass)
        {
            var fuelMassRequired = moduleMass/3 - 2;
            return fuelMassRequired > 0 ? fuelMassRequired : 0;
        }

        private static int[] GetModuleMassesFromInput()
        {
            var text = System.IO.File.ReadAllLines(@"day-01-the-tyranny-of-the-rocket-equation/input.txt");
            var fuelLevels = new int[text.Length];
            for(int i = 0; i < text.Length; i++)
            {
                fuelLevels[i] = Int32.Parse(text[i]);
            }
            return fuelLevels;
        }

    }
}
