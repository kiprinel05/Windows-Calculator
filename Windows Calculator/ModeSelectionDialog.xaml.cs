using System.Windows;

namespace WPFCalculator.Views
{
    public partial class ModeSelectionDialog : Window
    {
        public bool IsProgrammerModeSelected { get; private set; } = false;

        public ModeSelectionDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            IsProgrammerModeSelected = ProgrammerModeRadio.IsChecked == true;
            DialogResult = true;
            Close();
        }
    }
}