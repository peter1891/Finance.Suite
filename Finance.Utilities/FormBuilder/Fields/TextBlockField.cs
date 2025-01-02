using System.Windows.Controls;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class TextBlockField : TextBlock
    {
        public TextBlockField(string text, int row)
        {
            this.Text = text;

            Grid.SetColumn(this, 0);
            Grid.SetRow(this, row);
        }
    }
}
