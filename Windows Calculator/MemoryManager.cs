using System.Collections.Generic;

namespace WPFCalculator.Models
{
    public class MemoryManager
    {
        private double memory = 0;
        private List<double> memoryStack = new List<double>();

        public string MemoryDisplay { get; private set; } = "";

        public void MemoryClear()
        {
            memory = 0;
            memoryStack.Clear();
            MemoryDisplay = "";
        }

        public void MemoryRecall(ref string currentDisplay)
        {
            currentDisplay = memory.ToString();
        }

        public void MemoryAdd(double value)
        {
            memory += value;
        }

        public void MemorySubtract(double value)
        {
            memory -= value;
        }

        public void MemoryStore(double value)
        {
            memory = value;
            memoryStack.Add(value);
        }

        public string MemoryView()
        {
            return string.Join(", ", memoryStack);
        }
    }
}
