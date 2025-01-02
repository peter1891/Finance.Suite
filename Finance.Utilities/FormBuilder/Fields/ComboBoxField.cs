using System.Windows.Controls;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class ComboBoxField : ComboBox
    {
        public ComboBoxField(int row)
        {
            Grid.SetColumn(this, 1);
            Grid.SetRow(this, row);
        }
    }
}
