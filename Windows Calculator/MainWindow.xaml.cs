using System;
using System.Windows;
using System.Windows.Controls;
using WPFCalculator.Models;

namespace WPFCalculator.Views
{
    public partial class MainWindow : Window
    {
        private CalculatorLogic calculator = new CalculatorLogic();
        private string[,] buttonsMatrix =
 {
    { "MC", "MR", "M+", "M-" },
    { "MS", "M>", "√", "x²" },
    { "7", "8", "9", "/" },
    { "4", "5", "6", "*" },
    { "1", "2", "3", "-" },
    { "+/-", "0", "=", "+" }
};

        public MainWindow()
        {
            InitializeComponent();
            GenerateButtons();
            UpdateDisplay();
        }

        private void GenerateButtons()
        {
            int rows = buttonsMatrix.GetLength(0);
            int cols = buttonsMatrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button button = new Button
                    {
                        Content = buttonsMatrix[i, j],
                        FontSize = 20,
                        Margin = new Thickness(5)
                    };

                    button.Click += Button_Click;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    ButtonGrid.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string content = button.Content.ToString();

                if (int.TryParse(content, out _))
                {
                    calculator.AddDigit(content);
                }
                else if (content == "C")
                {
                    calculator.Clear();
                }
                else if (content == "=")
                {
                    calculator.Calculate();
                }
                else if (content == "√")
                {
                    calculator.SquareRoot();
                }
                else if (content == "x²")
                {
                    calculator.Square();
                }
                else if (content == "1/x")
                {
                    calculator.Reciprocal();
                }
                else if (content == "%")
                {
                    calculator.CalculatePercentage();
                }
                else if (content == "+/-")
                {
                    calculator.Negate();
                }
                else if (content == "MC")
                {
                    calculator.MemoryClear();
                }
                else if (content == "MR")
                {
                    calculator.MemoryRecall();
                }
                else if (content == "M+")
                {
                    calculator.MemoryAdd();
                }
                else if (content == "M-")
                {
                    calculator.MemorySubtract();
                }
                else if (content == "MS")
                {
                    calculator.MemoryStore();
                }
                else if (content == "M>")
                {
                    MessageBox.Show("Memorie: " + calculator.MemoryView(), "Memory Stack");
                }
                else
                {
                    calculator.SetOperator(content);
                }

                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            txtDisplay.Text = calculator.CurrentDisplay;
            txtOperator.Text = calculator.CurrentOperator;
        }

    }
}