using System.Windows.Controls;
using System.Windows;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class LabelField : TextBlock
    {
        public LabelField(string text, int row)
        {
            this.Style = (Style)Application.Current.Resources["LabelField"];

            this.Text = text;

            Grid.SetColumn(this, 0);
            Grid.SetRow(this, row);
        }
    }
}
