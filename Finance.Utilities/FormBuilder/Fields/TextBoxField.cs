using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class TextBoxField : TextBox
    {
        public TextBoxField(string binding, int row)
        {
            this.Style = (Style)Application.Current.Resources["TextBoxField"];

            this.SetBinding(TextBox.TextProperty, new Binding(binding));

            Grid.SetColumn(this, 1);
            Grid.SetRow(this, row);
        }
    }
}
