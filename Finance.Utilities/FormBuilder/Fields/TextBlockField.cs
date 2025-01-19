using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class TextBlockField : TextBlock
    {
        public TextBlockField(string binding, int row)
        {
            this.Style = (Style)Application.Current.Resources["TextBlockField"];

            this.SetBinding(TextBlock.TextProperty, new Binding(binding));

            Grid.SetColumn(this, 1);
            Grid.SetRow(this, row);
        }
    }
}
