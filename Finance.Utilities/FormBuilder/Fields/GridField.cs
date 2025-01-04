using System.Windows;
using System.Windows.Controls;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class GridField : Grid
    {
        public GridField(object dataContext, int rows)
        {
            this.DataContext = dataContext;

            this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200) });
            this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200) });

            for (int i = 0; i < rows; i++)
                this.RowDefinitions.Add(new RowDefinition());
        }
    }
}
