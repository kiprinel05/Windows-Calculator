using System;

namespace WPFCalculator.Models
{
    public class ProgrammerCalculator
    {
        public string ConvertToBinary(string number)
        {
            if (long.TryParse(number, out long value))
            {
                return Convert.ToString(value, 2);
            }
            return "Error";
        }

        public string ConvertToHex(string number)
        {
            if (long.TryParse(number, out long value))
            {
                return Convert.ToString(value, 16).ToUpper();
            }
            return "Error";
        }

        public string ConvertToOctal(string number)
        {
            if (long.TryParse(number, out long value))
            {
                return Convert.ToString(value, 8);
            }
            return "Error";
        }

        public string BitwiseAND(string num1, string num2)
        {
            if (long.TryParse(num1, out long val1) && long.TryParse(num2, out long val2))
            {
                return (val1 & val2).ToString();
            }
            return "Error";
        }

        public string BitwiseOR(string num1, string num2)
        {
            if (long.TryParse(num1, out long val1) && long.TryParse(num2, out long val2))
            {
                return (val1 | val2).ToString();
            }
            return "Error";
        }

        public string BitwiseXOR(string num1, string num2)
        {
            if (long.TryParse(num1, out long val1) && long.TryParse(num2, out long val2))
            {
                return (val1 ^ val2).ToString();
            }
            return "Error";
        }

        public string BitwiseNOT(string number)
        {
            if (long.TryParse(number, out long value))
            {
                return (~value).ToString();
            }
            return "Error";
        }

        public string BitShiftLeft(string number, int shift)
        {
            if (long.TryParse(number, out long value))
            {
                return (value << shift).ToString();
            }
            return "Error";
        }

        public string BitShiftRight(string number, int shift)
        {
            if (long.TryParse(number, out long value))
            {
                return (value >> shift).ToString();
            }
            return "Error";
        }
    }
}