using System.Windows;

namespace WPFCalculator.Views
{
    public partial class ModeSelectionDialog : Window
    {
        private bool _isProgrammerMode;

        public ModeSelectionDialog(bool isProgrammerMode)
        {
            InitializeComponent();
            _isProgrammerMode = isProgrammerMode;
            InitializeModeSelection();
        }

        private void InitializeModeSelection()
        {
            if (_isProgrammerMode)
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
            _isProgrammerMode = ProgrammerModeRadio.IsChecked == true;
            DialogResult = true;
            Close();
        }

        public bool IsProgrammerModeSelected => _isProgrammerMode;
    }
}
