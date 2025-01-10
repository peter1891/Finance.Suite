using Finance.CustomControls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class PasswordBoxField : BindablePasswordBox
    {
        public PasswordBoxField(string binding, int row)
        {
            this.Style = (Style)Application.Current.Resources["PasswordBoxField"];

            this.SetBinding(BindablePasswordBox.dependencyProperty, new Binding(binding)
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            });

            Grid.SetColumn(this, 1);
            Grid.SetRow(this, row);
        }
    }
}
