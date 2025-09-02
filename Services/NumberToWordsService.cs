using System.Globalization;

namespace NumberToWordsApp.Services
{
    public class NumberToWordsService
    {
        private readonly string[] ones = 
        {
            "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE",
            "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN",
            "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };

        private readonly string[] tens = 
        {
            "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };

        private readonly string[] scales = 
        {
            "", "THOUSAND", "MILLION", "BILLION", "TRILLION"
        };

        public string ConvertToWords(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be null or empty");

            if (!decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal number))
                throw new ArgumentException("Invalid number format");

            if (number < 0)
                throw new ArgumentException("Negative numbers are not supported");

            if (number > 999999999999.99m)
                throw new ArgumentException("Number too large (maximum 999,999,999,999.99)");

            if (number == 0)
                return "ZERO DOLLARS";

            var parts = input.Split('.');
            var wholePart = Convert.ToInt64(Math.Floor(number));
            var fractionalPart = 0;

            if (parts.Length > 1 && parts[1].Length > 0)
            {
                var centsPart = parts[1].PadRight(2, '0').Substring(0, 2);
                fractionalPart = Convert.ToInt32(centsPart);
            }

            var result = "";

            if (wholePart > 0)
            {
                result += ConvertWholeNumberToWords(wholePart) + " DOLLAR";
                if (wholePart != 1)
                    result += "S";
            }

            if (fractionalPart > 0)
            {
                if (!string.IsNullOrEmpty(result))
                    result += " AND ";
                
                result += ConvertWholeNumberToWords(fractionalPart) + " CENT";
                if (fractionalPart != 1)
                    result += "S";
            }
            else if (wholePart > 0)
            {
                // No cents, just dollars
            }
            else
            {
                result = "ZERO DOLLARS";
            }

            return result;
        }

        private string ConvertWholeNumberToWords(long number)
        {
            if (number == 0)
                return "";

            var result = "";
            var scaleIndex = 0;

            while (number > 0)
            {
                var chunk = number % 1000;
                if (chunk != 0)
                {
                    var chunkWords = ConvertChunkToWords((int)chunk);
                    if (scaleIndex > 0)
                        chunkWords += " " + scales[scaleIndex];
                    
                    if (!string.IsNullOrEmpty(result))
                        result = chunkWords + " " + result;
                    else
                        result = chunkWords;
                }
                
                number /= 1000;
                scaleIndex++;
            }

            return result;
        }

        private string ConvertChunkToWords(int number)
        {
            var result = "";

            var hundreds = number / 100;
            if (hundreds > 0)
            {
                result += ones[hundreds] + " HUNDRED";
                number %= 100;
                if (number > 0)
                    result += " AND ";
            }

            if (number >= 20)
            {
                var tensDigit = number / 10;
                var onesDigit = number % 10;
                result += tens[tensDigit];
                if (onesDigit > 0)
                    result += "-" + ones[onesDigit];
            }
            else if (number > 0)
            {
                result += ones[number];
            }

            return result;
        }
    }
}