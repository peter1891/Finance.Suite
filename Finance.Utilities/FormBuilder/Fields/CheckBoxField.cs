using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class CheckBoxField : CheckBox
    {
        public CheckBoxField(string binding, int row)
        {
            this.Style = (Style)Application.Current.Resources["CheckBoxField"];

            this.SetBinding(CheckBox.IsCheckedProperty, new Binding(binding));

            Grid.SetColumn(this, 1);
            Grid.SetRow(this, row);
        }
    }
}
