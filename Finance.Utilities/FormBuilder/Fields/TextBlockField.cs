using System.Windows;
using System.Windows.Controls;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class TextBlockField : TextBlock
    {
        public TextBlockField(string text, int row)
        {
            this.Style = (Style)Application.Current.Resources["TextBlockField"];

            this.Text = text;

            Grid.SetColumn(this, 0);
            Grid.SetRow(this, row);
        }
    }
}
