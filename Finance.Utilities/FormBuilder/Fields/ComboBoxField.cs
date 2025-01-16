using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class ComboBoxField : ComboBox
    {
        public ComboBoxField(string itemsSource, string displayMemberPath, string binding, int row)
        {
            this.Style = (Style)Application.Current.Resources["ComboBoxField"];

            this.SetBinding(ComboBox.ItemsSourceProperty, new Binding(itemsSource));
            this.SetBinding(ComboBox.SelectedItemProperty, new Binding(binding));

            this.DisplayMemberPath = displayMemberPath;

            Grid.SetColumn(this, 1);
            Grid.SetRow(this, row);
        }
    }
}
