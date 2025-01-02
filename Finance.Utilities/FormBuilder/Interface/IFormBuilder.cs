﻿using Finance.Services.Navigation.Interface;

namespace Finance.Utilities.FormBuilder.Interface
{
    public interface IFormBuilder
    {
        void BuildTitle();
        void BuildForm();
        void BuildSubmitButton();
        void BuildCancelButton();

        Form GetForm();
    }
}
