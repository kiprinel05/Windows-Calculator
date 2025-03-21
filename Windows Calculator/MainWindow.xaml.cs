﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFCalculator.Models;
using System.IO;
using System.Globalization;


namespace WPFCalculator.Views
{
    public partial class MainWindow : Window
    {
        private bool isDigitGroupingEnabled = false;

        /* STANDARD */

        private CalculatorLogic calculator = new CalculatorLogic();
        private string[,] standardButtonsMatrix =
{
            { "MC", "MR", "M+", "M-" },
            { "MS", "M>", "√", "x²" },
            { "7", "8", "9", "/" },
            { "4", "5", "6", "*" },
            { "1", "2", "3", "-" },
            { "+/-", "0", "=", "+" }
        };


        /* PROGRAMER */

        private int currentBase = 10;
        private bool isProgrammerMode = false;
        private ProgrammerCalculator programmerCalculator = new ProgrammerCalculator();


        private string[,] programmerButtonsMatrix =
        {
            { "AND", "OR", "XOR", "NOT", "<<" },
            { "A", "B", "C", "D", "E" },
            { "F", "7", "8", "9", "/" },
            { "4", "5", "6", "*", "Mod" },
            { "1", "2", "3", "-", "Rol" },
            { "0", "(", ")", "=", "+" }
        };

        private string currentExpression = "";
        public MainWindow()
        {
            InitializeComponent();
            setButtons_Standard();
            UpdateDisplay();
            this.KeyDown += MainWindow_KeyDown;

            int lastBase = LoadBaseFromFile();
            SetBase(lastBase);
        }

        private readonly Dictionary<Key, string> keyMappings = new()
        {
            { Key.D0, "0" }, { Key.NumPad0, "0" },
            { Key.D1, "1" }, { Key.NumPad1, "1" },
            { Key.D2, "2" }, { Key.NumPad2, "2" },
            { Key.D3, "3" }, { Key.NumPad3, "3" },
            { Key.D4, "4" }, { Key.NumPad4, "4" },
            { Key.D5, "5" }, { Key.NumPad5, "5" },
            { Key.D6, "6" }, { Key.NumPad6, "6" },
            { Key.D7, "7" }, { Key.NumPad7, "7" },
            { Key.D8, "8" }, { Key.NumPad8, "8" },
            { Key.D9, "9" }, { Key.NumPad9, "9" },
            { Key.OemMinus, "-" }, { Key.Subtract, "-" },
            { Key.OemQuestion, "/" }, { Key.Divide, "/" },
            { Key.Oem5, "\\" }, { Key.Multiply, "*" },
            { Key.Decimal, CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator },
            { Key.OemPeriod, CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator },
            { Key.Back, "⌫" },
            { Key.Enter, "=" },
            { Key.Escape, "C" }
        };
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (keyMappings.TryGetValue(e.Key, out string key))
            {
                Button_Click(new Button { Content = key }, new RoutedEventArgs());
                return;
            }

            if (e.Key == Key.OemPlus)
            {
                key = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift) ? "+" : "=";
                Button_Click(new Button { Content = key }, new RoutedEventArgs());
            }
        }
        private void setButtons_Standard()
        {
            int rows = standardButtonsMatrix.GetLength(0);
            int cols = standardButtonsMatrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    string buttonText = standardButtonsMatrix[i, j];

                    Button button = new Button
                    {
                        Content = buttonText,
                        FontSize = 20,
                        Margin = new Thickness(5),
                        Foreground = Brushes.White
                    };

                    SolidColorBrush backgroundColor = IsOperator(buttonText)
                        ? new SolidColorBrush(Color.FromRgb(45, 51, 61))
                        : new SolidColorBrush(Color.FromRgb(56, 59, 66));

                    SolidColorBrush hoverBackground = new SolidColorBrush(Color.FromRgb(80, 80, 90));

                    var borderFactory = new FrameworkElementFactory(typeof(Border));
                    borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(10));
                    borderFactory.SetValue(Border.BackgroundProperty, backgroundColor);
                    borderFactory.Name = "borderElement"; 

                    var contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
                    contentPresenterFactory.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                    contentPresenterFactory.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);

                    borderFactory.AppendChild(contentPresenterFactory);

                    ControlTemplate buttonTemplate = new ControlTemplate(typeof(Button))
                    {
                        VisualTree = borderFactory
                    };

                    Trigger hoverTrigger = new Trigger
                    {
                        Property = UIElement.IsMouseOverProperty,
                        Value = true
                    };
                    hoverTrigger.Setters.Add(new Setter(Border.BackgroundProperty, hoverBackground, "borderElement"));

                    buttonTemplate.Triggers.Add(hoverTrigger);

                    button.Template = buttonTemplate;
                    button.Click += Button_Click;

                    Grid.SetRow(button, i + 1);
                    Grid.SetColumn(button, j);
                    StandardButtonGrid.Children.Add(button);
                }
            }
        }

        private bool IsOperator(string text)
        {
            return text == "+" || text == "-" || text == "*" || text == "/" || text == "=" || text == "MC" || text == "MR" || text == "M+" || text == "M-" || text == "x²" || text == "MS" || text == "M>" || text == "+/-" || text == "√";
        }

        private void setButtons_Programmer()
        {
            int rows = programmerButtonsMatrix.GetLength(0);
            int cols = programmerButtonsMatrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    string buttonText = programmerButtonsMatrix[i, j];

                    Button button = new Button
                    {
                        Content = buttonText,
                        FontSize = 20,
                        Margin = new Thickness(5),
                        Foreground = Brushes.White,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch
                    };

                    SolidColorBrush backgroundColor = new SolidColorBrush(Color.FromRgb(57, 59, 63));

                    SolidColorBrush hoverBackground = new SolidColorBrush(Color.FromRgb(80, 80, 90));

                    var borderFactory = new FrameworkElementFactory(typeof(Border));
                    borderFactory.SetValue(Border.BackgroundProperty, backgroundColor);
                    borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(10));
                    borderFactory.Name = "borderElement";

                    var contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
                    contentPresenterFactory.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                    contentPresenterFactory.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);

                    borderFactory.AppendChild(contentPresenterFactory);

                    ControlTemplate buttonTemplate = new ControlTemplate(typeof(Button))
                    {
                        VisualTree = borderFactory
                    };

                    Trigger hoverTrigger = new Trigger
                    {
                        Property = UIElement.IsMouseOverProperty,
                        Value = true
                    };
                    hoverTrigger.Setters.Add(new Setter(Border.BackgroundProperty, hoverBackground, "borderElement"));
                    hoverTrigger.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.WhiteSmoke));

                    buttonTemplate.Triggers.Add(hoverTrigger);

                    button.Template = buttonTemplate;

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
                currentExpression += content;
            }
            else if (content == "C")
            {
                calculator.Clear();
                currentExpression = "";
            }
            else if (content == "CE")
            {
                calculator.ClearEntry();
            }
            else if (content == "⌫")
            {
                calculator.Backspace();
                if (currentExpression.Length > 0)
                {
                    currentExpression = currentExpression.Substring(0, currentExpression.Length - 1);
                }
            }
            else if (content == "=")
            {
                calculator.Calculate();
                currentExpression += " = " + calculator.CurrentDisplay;
            }
            else if (content == "√")
            {
                calculator.SquareRoot();
                currentExpression += " √";
            }
            else if (content == "x²")
            {
                calculator.Square();
                currentExpression += " x²";
            }
            else if (content == "+/-")
            {
                calculator.Negate();
                currentExpression += " +/-";
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
                if (calculator.LastActionWasOperator)
                {
                    return;
                }
                calculator.SetOperator(content);
                currentExpression += " " + content + " ";
                calculator.LastActionWasOperator = true;
            }
            txtOperation.Text = currentExpression;
        }


        private void HandleProgrammerInput(string content)
        {

            string[] operators = { "AND", "OR", "XOR", "NOT", "<<", ">>", "Mod", "Rol" };

            if (isValidforBase(content))
            {
                calculator.AddDigit(content);
            }
            else if (operators.Contains(content))
            {
                string num1 = calculator.StoredValue;
                string num2 = calculator.CurrentDisplay;

                if (num1 == "0")
                {
                    num1 = num2;
                    calculator.StoredValue = num1;
                    calculator.ClearEntry();
                    return;
                }

                switch (content)
                {
                    case "AND":
                        calculator.SetDisplay(programmerCalculator.BitwiseAND(num1, num2));
                        break;
                    case "OR":
                        calculator.SetDisplay(programmerCalculator.BitwiseOR(num1, num2));
                        break;
                    case "XOR":
                        calculator.SetDisplay(programmerCalculator.BitwiseXOR(num1, num2));
                        break;
                    case "NOT":
                        calculator.SetDisplay(programmerCalculator.BitwiseNOT(num2));
                        break;
                    case "<<":
                        calculator.SetDisplay(programmerCalculator.BitShiftLeft(num1, int.Parse(num2)));
                        break;
                    case ">>":
                        calculator.SetDisplay(programmerCalculator.BitShiftRight(num1, int.Parse(num2)));
                        break;
                    case "Mod":
                        calculator.SetDisplay((long.Parse(num1) % long.Parse(num2)).ToString());
                        break;
                    case "Rol":
                        calculator.SetDisplay(BitwiseRotateLeft(num1, int.Parse(num2)));
                        break;
                    default:
                        break;
                }
                calculator.StoredValue = "0";
                calculator.LastActionWasOperator = true;
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

            UpdateDisplay();
        }


        private string BitwiseRotateLeft(string number, int shift)
        {
            if (long.TryParse(number, out long value))
            {
                int bitCount = (int)Math.Log(value, 2) + 1;
                shift = shift % bitCount;
                long result = (value << shift) | (value >> (bitCount - shift));
                return result.ToString();
            }
            return "Error";
        }

        private bool isValidforBase(string input)
        {
            string[] operators = { "AND", "OR", "XOR", "NOT", "<<", ">>", "Mod", "Rol" };

            if (operators.Contains(input))
            {
                return false; 
            }

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
                txtDec.Text = $"DEC: {FormatNumber(dec)}";
                txtOct.Text = $"OCT: {oct}";
                txtBin.Text = $"BIN: {bin}";

                foreach (Button button in ProgrammerButtonGrid.Children.OfType<Button>())
                {
                    if (!isValidforBase(button.Content.ToString()))
                    {
                        button.Style = (Style)FindResource("DisabledButtonStyle");
                    }
                    else
                    {
                        button.Style = null;
                    }
                }
            }
            else
            {
                ProgrammerDisplay.Visibility = Visibility.Collapsed;
                txtDisplay.Visibility = Visibility.Visible;
                txtDisplay.Text = FormatNumber(calculator.CurrentDisplay);
            }
        }


        private void ModeSwitchButton_Click(object sender, RoutedEventArgs e)
        {
            ModeSelectionDialog dialog = new ModeSelectionDialog(isProgrammerMode);
            if (dialog.ShowDialog() == true)
            {
                isProgrammerMode = dialog.IsProgrammerModeSelected;
                calculator.SetMode(isProgrammerMode ? CalculatorLogic.CalculatorMode.Programmer : CalculatorLogic.CalculatorMode.Standard);

                if (isProgrammerMode)
                {
                    StandardButtonGrid.Visibility = Visibility.Collapsed; 
                    ProgrammerButtonGrid.Visibility = Visibility.Visible;   
                    ProgrammerDisplay.Visibility = Visibility.Visible; 
                    setButtons_Programmer();                     
                }
                else
                {
                    StandardButtonGrid.Visibility = Visibility.Visible;  
                    ProgrammerButtonGrid.Visibility = Visibility.Collapsed;
                    ProgrammerDisplay.Visibility = Visibility.Collapsed;
                }

                ModeLabel.Text = isProgrammerMode ? "Programmer" : "Standard";

                UpdateDisplay();
            }
        }



        private TextBlock selectedBase = null;

        private void BaseDisplay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                if (selectedBase != null)
                {
                    selectedBase.Style = (Style)FindResource("BaseTextStyle");
                }

                selectedBase = textBlock;
                selectedBase.Style = (Style)FindResource("SelectedBaseStyle");

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

                SaveBaseToFile(currentBase);

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
        private void SaveBaseToFile(int baseValue)
        {
            File.WriteAllText("config.txt", baseValue.ToString());
        }

        private int LoadBaseFromFile()
        {
            if (File.Exists("config.txt"))
            {
                string baseText = File.ReadAllText("config.txt");
                if (int.TryParse(baseText, out int savedBase))
                {
                    return savedBase;
                }
            }
            return 10;
        }

        private void SetBase(int baseValue)
        {
            string baseText = baseValue switch
            {
                16 => "HEX",
                10 => "DEC",
                8 => "OCT",
                2 => "BIN",
                _ => "DEC"
            };

            foreach (var child in ProgrammerDisplay.Children)
            {
                if (child is TextBlock textBlock && textBlock.Text.StartsWith(baseText))
                {
                    if (selectedBase != null)
                    {
                        selectedBase.Style = (Style)FindResource("BaseTextStyle");
                    }

                    selectedBase = textBlock;
                    selectedBase.Style = (Style)FindResource("SelectedBaseStyle");
                    currentBase = baseValue;

                    UpdateDisplay();
                    break;
                }
            }
        }
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.ShowDialog();
        }

        private void CutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDisplay.Text))
            {
                Clipboard.SetText(txtDisplay.Text);
                txtDisplay.Clear();
            }
        }

        private void CopyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDisplay.Text))
            {
                Clipboard.SetText(txtDisplay.Text);
            }
        }

        private void PasteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                string clipboardText = Clipboard.GetText();
                if (double.TryParse(clipboardText, out _))
                {
                    txtDisplay.Text = clipboardText;
                    calculator.SetDisplay(clipboardText);
                }
            }
        }

        private void DigitGroupingMenuItem_Click(object sender, RoutedEventArgs e)
        {
            isDigitGroupingEnabled = !isDigitGroupingEnabled;
            UpdateDisplay();
        }
        private string FormatNumber(string number)
        {
            if (!isDigitGroupingEnabled || string.IsNullOrEmpty(number))
                return number;

            CultureInfo culture = CultureInfo.CurrentCulture;

            if (number.Contains(".") || number.Contains(","))
            {
                string[] parts = number.Split('.', ',');
                return string.Format(culture, "{0:N0}", long.Parse(parts[0])) + culture.NumberFormat.NumberDecimalSeparator + parts[1];
            }

            return string.Format(culture, "{0:N0}", long.Parse(number));
        }


        private void ShowMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (FileMenu.Visibility == Visibility.Visible)
            {
                FileMenu.Visibility = Visibility.Collapsed;
            }
            else
            {
                FileMenu.Visibility = Visibility.Visible;
            }
        }
        private void OrderOfOperationsMenuItem_Click(object sender, RoutedEventArgs e) { }
    }
}