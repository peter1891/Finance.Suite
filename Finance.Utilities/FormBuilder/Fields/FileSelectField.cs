using Finance.Core;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class FileSelectField : StackPanel
    {
        private TextBox _textBox;
        private Button _button;

        public ICommand ExecuteBrowseCommand { get; }

        public FileSelectField(string binding, int row)
        {
            this.Orientation = Orientation.Horizontal;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;

            this._textBox = SetTextBox(binding);
            this._button = SetButton();

            this.Children.Add(_textBox);
            this.Children.Add(_button);

            Grid.SetColumn(this, 1);
            Grid.SetRow(this, row);

            ExecuteBrowseCommand = new RelayCommand(ExecuteBrowse, obj => true);
        }

        private void ExecuteBrowse(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV-bestand (*.csv)|*.csv|Alle bestanden (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
                _textBox.Text = openFileDialog.FileName;
        }

        private TextBox SetTextBox(string binding)
        {
            TextBox textBox = new TextBox()
            {
                Style = (Style)Application.Current.Resources["TextBoxField"],
            };

            textBox.SetBinding(TextBox.TextProperty, new Binding(binding)
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            });

            return textBox;
        }

        private Button SetButton()
        {
            Button button = new Button()
            {
                Content = "Browse...",
            };

            button.SetBinding(Button.CommandProperty, new Binding("ExecuteBrowseCommand"));

            return button;
        }
    }
}
