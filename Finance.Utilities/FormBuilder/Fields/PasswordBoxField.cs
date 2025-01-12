using Finance.CustomControls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class PasswordBoxField
    {
        private readonly BindablePasswordBox _passwordBox;

        public BindablePasswordBox PasswordBox => _passwordBox;

        public PasswordBoxField(string binding, int row)
        {
            _passwordBox = new BindablePasswordBox();

            _passwordBox.Style = (Style)Application.Current.Resources["PasswordBoxField"];

            _passwordBox.SetBinding(BindablePasswordBox.PasswordProperty, new Binding(binding)
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            });

            Grid.SetColumn(_passwordBox, 1);
            Grid.SetRow(_passwordBox, row);
        }
    }
}
