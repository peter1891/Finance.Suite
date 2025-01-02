using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Finance.Utilities.FormBuilder.Base
{
    public abstract class FormBuilderBase
    {
        public CheckBox BuildCheckBoxField(string binding)
        {
            CheckBox checkBox = new CheckBox();

            checkBox.SetBinding(CheckBox.IsCheckedProperty, new Binding(binding));

            return checkBox;
        }

        public ComboBox BuildComboBoxField()
        {
            ComboBox comboBox = new ComboBox();

            return comboBox;
        }

        public DatePicker BuildDatePickerField(int row)
        {
            DatePicker datePicker = new DatePicker();

            Grid.SetColumn(datePicker, 1);
            Grid.SetRow(datePicker, row);

            return datePicker;
        }

        public Grid BuildGrid(object datacontext, int rows)
        { 
            Grid grid = new Grid()
            {
                DataContext = datacontext,
            };

            for (int i = 0; i < 2; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions[i].Width = new GridLength(170);
            }

            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            return grid; 
        }

        public TextBlock BuildTextBlockField(string text, int row)
        {
            TextBlock textBlock = new TextBlock()
            {
                Text = text,
            };

            Grid.SetColumn(textBlock, 0);
            Grid.SetRow(textBlock, row);

            return textBlock;
        }

        public TextBox BuildTextBoxField(string binding, int row)
        {
            TextBox textBox = new TextBox();

            textBox.SetBinding(TextBox.TextProperty, new Binding(binding));

            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, row);

            return textBox;
        }
    }
}
