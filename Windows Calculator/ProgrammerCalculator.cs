using System;
using System.Numerics;

namespace WPFCalculator.Models
{
    public class ProgrammerCalculator
    {
        public string ConvertToBinary(string number)
        {
            if (BigInteger.TryParse(number, out BigInteger value))
            {
                if (value < 0) return "Negative numbers not supported";
                return Convert.ToString((long)value, 2);
            }
            return "Error: Invalid input";
        }

        public string ConvertToHex(string number)
        {
            if (BigInteger.TryParse(number, out BigInteger value))
            {
                return value.ToString("X");
            }
            return "Error: Invalid input";
        }

        public string ConvertToOctal(string number)
        {
            if (BigInteger.TryParse(number, out BigInteger value))
            {
                if (value < 0) return "Negative numbers not supported";
                return Convert.ToString((long)value, 8);
            }
            return "Error: Invalid input";
        }

        public string BitwiseAND(string num1, string num2)
        {
            if (BigInteger.TryParse(num1, out BigInteger val1) && BigInteger.TryParse(num2, out BigInteger val2))
            {
                return (val1 & val2).ToString();
            }
            return "Error: Invalid input";
        }

        public string BitwiseOR(string num1, string num2)
        {
            if (BigInteger.TryParse(num1, out BigInteger val1) && BigInteger.TryParse(num2, out BigInteger val2))
            {
                return (val1 | val2).ToString();
            }
            return "Error: Invalid input";
        }

        public string BitwiseXOR(string num1, string num2)
        {
            if (BigInteger.TryParse(num1, out BigInteger val1) && BigInteger.TryParse(num2, out BigInteger val2))
            {
                return (val1 ^ val2).ToString();
            }
            return "Error: Invalid input";
        }

        public string BitwiseNOT(string number)
        {
            if (BigInteger.TryParse(number, out BigInteger value))
            {
                return (~value).ToString();
            }
            return "Error: Invalid input";
        }

        public string BitShiftLeft(string number, int shift)
        {
            if (BigInteger.TryParse(number, out BigInteger value))
            {
                if (shift < 0) return "Error: Negative shift not allowed";
                return (value << shift).ToString();
            }
            return "Error: Invalid input";
        }

        public string BitShiftRight(string number, int shift)
        {
            if (BigInteger.TryParse(number, out BigInteger value))
            {
                if (shift < 0) return "Error: Negative shift not allowed";
                return (value >> shift).ToString();
            }
            return "Error: Invalid input";
        }
    }
}
