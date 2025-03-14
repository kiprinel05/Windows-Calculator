using System.Windows;

namespace WPFCalculator.Views
{
    public partial class ModeSelectionDialog : Window
    {
        private bool isProgrammerMode;

        public ModeSelectionDialog(bool isProgrammerMode)
        {
            InitializeComponent();
            this.isProgrammerMode = isProgrammerMode;
            InitializeModeSelection();
        }

        private void InitializeModeSelection()
        {
            if (isProgrammerMode)
            {
                ProgrammerModeRadio.IsChecked = true;
            }
            else
            {
                StandardModeRadio.IsChecked = true;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            isProgrammerMode = ProgrammerModeRadio.IsChecked == true;
            DialogResult = true;
            Close();
        }

        public bool IsProgrammerModeSelected => isProgrammerMode;
    }
}
