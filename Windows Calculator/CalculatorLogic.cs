using System;

namespace WPFCalculator.Models
{
    public class CalculatorLogic
    {
        private double _currentValue = 0;
        private double _memoryValue = 0;
        private string _currentOperator = "";
        private bool _isNewInput = true;

        public bool LastActionWasOperator { get; set; } = false;
        public string StoredValue { get; set; } = "0";
        private MemoryManager memoryManager = new MemoryManager();
        public string CurrentOperator { get; private set; } = "";
        public string CurrentDisplay { get; private set; } = "0";

        public void AddDigit(string digit)
        {
            string[] invalidCharacters = { "AND", "OR", "XOR", "NOT", "<<", ">>", "Mod", "Rol" };
            if (invalidCharacters.Contains(digit))
            {
                return;
            }

            if (_isNewInput)
            {
                CurrentDisplay = digit;
                _isNewInput = false;
            }
            else
            {
                CurrentDisplay += digit;
            }
            LastActionWasOperator = false;
        }

        public void SetOperator(string op)
        {
            CurrentOperator = op;
            if (!string.IsNullOrEmpty(_currentOperator))
                Calculate();

            _currentValue = double.Parse(CurrentDisplay);
            _currentOperator = op;
            _isNewInput = true;
        }

        public void Calculate()
        {
            double newValue = double.Parse(CurrentDisplay);
            switch (_currentOperator)
            {
                case "+": _currentValue += newValue; break;
                case "-": _currentValue -= newValue; break;
                case "*": _currentValue *= newValue; break;
                case "/":
                    if (newValue != 0)
                        _currentValue /= newValue;
                    else
                        CurrentDisplay = "Eroare";
                    break;
            }
            CurrentDisplay = _currentValue.ToString();
            _currentOperator = "";
            _isNewInput = true;
        }

        public void Clear()
        {
            _currentValue = 0;
            _currentOperator = "";
            CurrentDisplay = "0";
            _isNewInput = true;
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
            CurrentDisplay = "0";
            _isNewInput = true;
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