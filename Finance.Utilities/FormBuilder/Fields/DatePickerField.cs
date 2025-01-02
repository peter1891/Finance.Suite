﻿using System.Windows.Controls;
using System.Windows.Data;

namespace Finance.Utilities.FormBuilder.Fields
{
    public class DatePickerField : DatePicker
    {
        public DatePickerField(string binding, int row)
        {
            this.SetBinding(DatePicker.SelectedDateProperty, new Binding(binding));

            Grid.SetColumn(this, 1);
            Grid.SetRow(this, row);
        }
    }
}