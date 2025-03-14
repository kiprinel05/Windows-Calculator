using System.Collections.Generic;
using System.Linq;

namespace WPFCalculator.Models
{
    public class MemoryManager
    {
        private double memory = 0;
        private bool hasMemory = false;

        public string MemoryDisplay { get; private set; } = "";

        public void MemoryClear()
        {
            memory = 0;
            hasMemory = false;
            MemoryDisplay = "";
        }

        public void MemoryRecall(ref string currentDisplay)
        {
            if (hasMemory)
            {
                currentDisplay = memory.ToString();
            }
        }

        public void MemoryAdd(double value)
        {
            if (hasMemory)
            {
                memory += value;
                UpdateMemoryDisplay();
            }
        }

        public void MemorySubtract(double value)
        {
            if (hasMemory)
            {
                memory -= value;
                UpdateMemoryDisplay();
            }
        }

        public void MemoryStore(double value)
        {
            memory = value;
            hasMemory = true;
            UpdateMemoryDisplay();
        }

        public string MemoryView()
        {
            return hasMemory ? memory.ToString() : "";
        }

        private void UpdateMemoryDisplay()
        {
            MemoryDisplay = MemoryView();
        }
    }
}
