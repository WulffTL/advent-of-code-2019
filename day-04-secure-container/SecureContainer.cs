using System;

namespace advent_of_code_2019
{
    class SecureContainer
    {
        string lowerBound;
        string upperBound;

        public void PrintAnswer()
        {
            var possibleValues = 0;
            SetRangeValues();
            for(int i = Int32.Parse(lowerBound); i < Int32.Parse(upperBound); i++)
            {
                if(IsCriteriaMet(i.ToString()))
                {
                    possibleValues++;
                }
            }
            Console.WriteLine(possibleValues);
        }

        private bool IsCriteriaMet(string password)
        {
            var firstDigit = 0;
            var secondDigit = 0;
            var currentConsecutiveDigitsCount = 0;
            var hasPairOfConsecutiveDigits = false;
            for(int i = 0; i < password.Length - 1; i++)
            {
                firstDigit = Int32.Parse(password[i].ToString());
                secondDigit = Int32.Parse(password[i+1].ToString());

                if (firstDigit > secondDigit)
                {
                    return false;
                }

                if(firstDigit == secondDigit)
                {
                    currentConsecutiveDigitsCount++;
                }
                else if(currentConsecutiveDigitsCount == 1)
                {
                    hasPairOfConsecutiveDigits = true;
                    currentConsecutiveDigitsCount = 0;
                }
                else
                {
                    currentConsecutiveDigitsCount = 0;
                }

            }
            return hasPairOfConsecutiveDigits || currentConsecutiveDigitsCount == 1;
        }

        private void SetRangeValues()
        {
            var text = System.IO.File.ReadAllText(@"day-04-secure-container/input.txt");
            var wires = text.Split("-");
            this.lowerBound = wires[0];
            this.upperBound = wires[1];
        }
    }
}