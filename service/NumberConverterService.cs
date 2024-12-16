using NumberToWordConverter.Models.enums;

namespace NumberToWordConverter.service
{
    public class NumberConverterService
    {
        public string ToWord(double input)
        {
            int roundNumber = (int)input;
            int decimalNumber = (int)((input * 100) % 100);
            string? words = string.Empty; ;
            string? tempWord = null;

            if (decimalNumber > 0)
            {
                words = hundrendConverter(decimalNumber) + " CENTS"; ;
            }

            int count = 0;
            while (roundNumber > 0)
            {
                int currentNumber = roundNumber % 1000;
                tempWord = hundrendConverter(currentNumber);
                if (count == 0)
                {
                    if (words.Length > 0)
                    {
                        words = tempWord + " DOLLARS AND " + words;
                    }
                    else
                    {
                        words = tempWord + " DOLLARS";
                    }


                }
                else
                {
                    words = tempWord + " " + (ThousandsEnum)count + " " + words;
                }
                roundNumber = roundNumber / 1000;
                count++;
            }
            return words;
        }

        private string hundrendConverter(int roundNumber)
        {
            string numberToWords = null;
            int hundred = roundNumber / 100;
            if (hundred != 0)
            {
                numberToWords = (BelowTwentyEnum)hundred + " HUNDRED ";
            }

            return numberToWords + tensConverter(roundNumber);
        }

        private string tensConverter(int roundNumber)
        {
            string numberToWords = "";
            int tens = roundNumber % 100;
            if (tens > 19)
            {
                int temp = tens / 10;
                numberToWords = "" + (TensEnum)temp;
                int belowTwenty = tens % 10;
                if (belowTwenty != 0)
                {

                    numberToWords = numberToWords + "-" + (BelowTwentyEnum)belowTwenty;
                }
            }
            else
            {
                numberToWords = numberToWords + (BelowTwentyEnum)tens;
            }
            return numberToWords;
        }
    }
}
