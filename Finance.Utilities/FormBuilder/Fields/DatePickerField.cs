using System.Windows.Controls;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class DatePickerField : DatePicker
    {
        public DatePickerField(int row)
        {
            Grid.SetColumn(this, 1);
            Grid.SetRow(this, row);
        }
    }
}
