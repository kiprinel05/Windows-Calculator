using System;

namespace WPFCalculator.Models
{
    public class CalculatorLogic
    {
        private double currentValue = 0;
        private double memoryValue = 0;
        private string currentOperator = "";
        private bool isNewInput = true;

        public bool LastActionWasOperator { get; set; } = false;
        public string StoredValue { get; set; } = "0";
        private MemoryManager memoryManager = new MemoryManager();
        public string CurrentOperator { get; private set; } = "";
        public string CurrentDisplay { get; private set; } = "0";

        public void AddDigit(string digit)
        {
            if (!double.TryParse(digit, out _) && !IsValidOperator(digit))
                return;

            if (isNewInput)
            {
                CurrentDisplay = digit;
                isNewInput = false;
            }
            else
            {
                CurrentDisplay += digit;
            }
            LastActionWasOperator = false;
        }
        private bool IsValidOperator(string input)
        {
            string[] validOperators = { "+", "-", "*", "/", "Mod", "AND", "OR", "XOR", "NOT", "<<", ">>", "Rol" };
            return Array.Exists(validOperators, op => op == input);
        }
        public void SetOperator(string op)
        {
            if (!IsValidOperator(op)) return;

            if (!string.IsNullOrEmpty(currentOperator))
                Calculate();

            if (double.TryParse(CurrentDisplay, out double parsedValue))
                currentValue = parsedValue;

            currentOperator = op;
            isNewInput = true;
        }

        public void Calculate()
        {
            if (!double.TryParse(CurrentDisplay, out double newValue))
            {
                CurrentDisplay = "Eroare";
                return;
            }

            try
            {
                switch (currentOperator)
                {
                    case "+": currentValue += newValue; break;
                    case "-": currentValue -= newValue; break;
                    case "*": currentValue *= newValue; break;
                    case "/":
                        if (newValue != 0)
                            currentValue /= newValue;
                        else
                            throw new DivideByZeroException();
                        break;
                }

                CurrentDisplay = currentValue.ToString();
            }
            catch (Exception ex)
            {
                CurrentDisplay = ex.Message;
            }

            currentOperator = "";
            isNewInput = true;
        }
        public void Clear()
        {
            currentValue = 0;
            currentOperator = "";
            CurrentDisplay = "0";
            isNewInput = true;
        }

        public void CalculatePercentage()
        {
            if (double.TryParse(CurrentDisplay, out double value))
            {
                CurrentDisplay = (value / 100).ToString();
            }
        }

        public void SquareRoot()
        {
            if (double.TryParse(CurrentDisplay, out double value) && value >= 0)
            {
                CurrentDisplay = Math.Sqrt(value).ToString();
            }
        }

        public void Square()
        {
            if (double.TryParse(CurrentDisplay, out double value))
            {
                CurrentDisplay = (value * value).ToString();
            }
        }

        public void Reciprocal()
        {
            if (double.TryParse(CurrentDisplay, out double value) && value != 0)
            {
                CurrentDisplay = (1 / value).ToString();
            }
        }

        public void Negate()
        {
            if (double.TryParse(CurrentDisplay, out double value))
            {
                CurrentDisplay = (-value).ToString();
            }
        }
        public void MemoryClear()
        {
            memoryManager.MemoryClear();
        }

        public void MemoryRecall()
        {
            string display = memoryManager.MemoryView();
            if (!string.IsNullOrEmpty(display))
            {
                CurrentDisplay = display;
            }
        }

        public void MemoryAdd()
        {
            if (double.TryParse(CurrentDisplay, out double value))
            {
                memoryManager.MemoryAdd(value);
            }
        }

        public void MemorySubtract()
        {
            if (double.TryParse(CurrentDisplay, out double value))
            {
                memoryManager.MemorySubtract(value);
            }
        }

        public void MemoryStore()
        {
            if (double.TryParse(CurrentDisplay, out double value))
            {
                memoryManager.MemoryStore(value);
            }
        }

        public string MemoryView()
        {
            return memoryManager.MemoryView();
        }
        public enum CalculatorMode
        {
            Standard,
            Programmer
        }

        private CalculatorMode currentMode = CalculatorMode.Standard;

        public void SetMode(CalculatorMode mode)
        {
            currentMode = mode;
        }
        public void ClearEntry()
        {
            if (!isNewInput)
            {
                CurrentDisplay = "0";
                isNewInput = true;
            }
        }

        public void Backspace()
        {
            if (CurrentDisplay.Length > 1)
            {
                CurrentDisplay = CurrentDisplay.Substring(0, CurrentDisplay.Length - 1);
            }
            else
            {
                CurrentDisplay = "0";
            }
        }

        public void SetDisplay(string value)
        {
            CurrentDisplay = value;
        }


    }
}