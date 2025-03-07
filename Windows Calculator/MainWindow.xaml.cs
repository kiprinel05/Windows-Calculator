using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFCalculator.Models;

namespace WPFCalculator.Views
{
    public partial class MainWindow : Window
    {
        private CalculatorLogic calculator = new CalculatorLogic();
        private ProgrammerCalculator programmerCalculator = new ProgrammerCalculator();
        private bool isProgrammerMode = false;
        private int currentBase = 10; // Baza implicită este DEC

        private string[,] standardButtonsMatrix =
        {
            { "MC", "MR", "M+", "M-" },
            { "MS", "M>", "√", "x²" },
            { "7", "8", "9", "/" },
            { "4", "5", "6", "*" },
            { "1", "2", "3", "-" },
            { "+/-", "0", "=", "+" }
        };

        private string[,] programmerButtonsMatrix =
        {
            { "AND", "OR", "XOR", "NOT", "<<" },
            { "A", "B", "C", "D", "E" },
            { "F", "7", "8", "9", "/" },
            { "4", "5", "6", "*", "Mod" },
            { "1", "2", "3", "-", "Rol" },
            { "0", "(", ")", "=", "+" }
        };

        public MainWindow()
        {
            InitializeComponent();
            GenerateStandardButtons();
            UpdateDisplay();
        }

        private void GenerateStandardButtons()
        {
            int rows = standardButtonsMatrix.GetLength(0);
            int cols = standardButtonsMatrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button button = new Button
                    {
                        Content = standardButtonsMatrix[i, j],
                        FontSize = 20,
                        Margin = new Thickness(5)
                    };

                    button.Click += Button_Click;
                    Grid.SetRow(button, i + 1); // Adăugăm 1 pentru a face loc butoanelor suplimentare
                    Grid.SetColumn(button, j);
                    StandardButtonGrid.Children.Add(button);
                }
            }
        }

        private void GenerateProgrammerButtons()
        {
            int rows = programmerButtonsMatrix.GetLength(0);
            int cols = programmerButtonsMatrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button button = new Button
                    {
                        Content = programmerButtonsMatrix[i, j],
                        FontSize = 20,
                        Margin = new Thickness(5)
                    };

                    button.Click += Button_Click;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    ProgrammerButtonGrid.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string content = button.Content.ToString();

                if (isProgrammerMode)
                {
                    HandleProgrammerInput(content);
                }
                else
                {
                    HandleStandardInput(content);
                }

                UpdateDisplay();
            }
        }

        private void HandleStandardInput(string content)
        {
            if (int.TryParse(content, out _))
            {
                calculator.AddDigit(content);
            }
            else if (content == "C")
            {
                calculator.Clear();
            }
            else if (content == "CE")
            {
                calculator.ClearEntry();
            }
            else if (content == "⌫")
            {
                calculator.Backspace();
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
        }

        private void HandleProgrammerInput(string content)
        {
            if (IsValidInputForCurrentBase(content))
            {
                calculator.AddDigit(content);
            }
            else if (content == "C")
            {
                calculator.Clear();
            }
            else if (content == "CE")
            {
                calculator.ClearEntry();
            }
            else if (content == "⌫")
            {
                calculator.Backspace();
            }
            else if (content == "=")
            {
                calculator.Calculate();
            }
            else
            {
                // Logica pentru operații pe biți (AND, OR, XOR, NOT, shiftare biți, etc.)
            }
        }

        private bool IsValidInputForCurrentBase(string input)
        {
            switch (currentBase)
            {
                case 16: // HEX
                    return "0123456789ABCDEF".Contains(input);
                case 10: // DEC
                    return "0123456789".Contains(input);
                case 8: // OCT
                    return "01234567".Contains(input);
                case 2: // BIN
                    return "01".Contains(input);
                default:
                    return false;
            }
        }

        private void UpdateDisplay()
        {
            if (isProgrammerMode)
            {
                ProgrammerDisplay.Visibility = Visibility.Visible;
                txtDisplay.Visibility = Visibility.Collapsed;

                string hex = programmerCalculator.ConvertToHex(calculator.CurrentDisplay);
                string dec = calculator.CurrentDisplay;
                string oct = programmerCalculator.ConvertToOctal(calculator.CurrentDisplay);
                string bin = programmerCalculator.ConvertToBinary(calculator.CurrentDisplay);

                txtHex.Text = $"HEX: {hex}";
                txtDec.Text = $"DEC: {dec}";
                txtOct.Text = $"OCT: {oct}";
                txtBin.Text = $"BIN: {bin}";

                // Highlight pentru baza selectată
                txtHex.Style = currentBase == 16 ? (Style)FindResource("SelectedBaseStyle") : null;
                txtDec.Style = currentBase == 10 ? (Style)FindResource("SelectedBaseStyle") : null;
                txtOct.Style = currentBase == 8 ? (Style)FindResource("SelectedBaseStyle") : null;
                txtBin.Style = currentBase == 2 ? (Style)FindResource("SelectedBaseStyle") : null;

                // Dezactivează butoanele inaccesibile
                foreach (Button button in ProgrammerButtonGrid.Children.OfType<Button>())
                {
                    if (!IsValidInputForCurrentBase(button.Content.ToString()))
                    {
                        button.Style = (Style)FindResource("DisabledButtonStyle");
                    }
                    else
                    {
                        button.Style = null; // Resetează stilul pentru butoanele accesibile
                    }
                }
            }
            else
            {
                ProgrammerDisplay.Visibility = Visibility.Collapsed;
                txtDisplay.Visibility = Visibility.Visible;
                txtDisplay.Text = calculator.CurrentDisplay;
            }
        }

        private void ModeSwitchButton_Click(object sender, RoutedEventArgs e)
        {
            ModeSelectionDialog dialog = new ModeSelectionDialog();
            if (dialog.ShowDialog() == true)
            {
                isProgrammerMode = dialog.IsProgrammerModeSelected;
                calculator.SetMode(isProgrammerMode ? CalculatorLogic.CalculatorMode.Programmer : CalculatorLogic.CalculatorMode.Standard);

                if (isProgrammerMode)
                {
                    StandardButtonGrid.Visibility = Visibility.Collapsed;
                    ProgrammerButtonGrid.Visibility = Visibility.Visible;
                    GenerateProgrammerButtons();
                }
                else
                {
                    StandardButtonGrid.Visibility = Visibility.Visible;
                    ProgrammerButtonGrid.Visibility = Visibility.Collapsed;
                }

                UpdateDisplay();
            }
        }

        private void BaseDisplay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null)
            {
                string baseText = textBlock.Text.Split(':')[0];
                switch (baseText)
                {
                    case "HEX":
                        currentBase = 16;
                        break;
                    case "DEC":
                        currentBase = 10;
                        break;
                    case "OCT":
                        currentBase = 8;
                        break;
                    case "BIN":
                        currentBase = 2;
                        break;
                }

                UpdateDisplay();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            calculator.Clear();
            UpdateDisplay();
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            calculator.ClearEntry();
            UpdateDisplay();
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            calculator.Backspace();
            UpdateDisplay();
        }
    }
}