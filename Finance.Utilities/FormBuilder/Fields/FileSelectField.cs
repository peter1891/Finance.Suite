using Finance.Core;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class FileSelectField : Grid
    {
        private TextBox _textBox;
        private Button _button;

        public ICommand ExecuteBrowseCommand { get; }

        public FileSelectField(string binding, int row)
        {
            this.HorizontalAlignment = HorizontalAlignment.Stretch;

            this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            this.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            this._textBox = SetTextBox(binding);
            this._button = SetButton();

            Grid.SetColumn(_textBox, 0);
            Grid.SetColumn(_button, 1);

            this.Children.Add(_textBox);
            this.Children.Add(_button);

            Grid.SetColumn(this, 1);
            Grid.SetRow(this, row);

            ExecuteBrowseCommand = new RelayCommand(ExecuteBrowse, obj => true);

            _button.Command = ExecuteBrowseCommand;
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
            return new Button()
            {
                Content = "Browse...",
            };
        }
    }
}
