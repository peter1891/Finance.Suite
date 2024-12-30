using System.Windows.Controls;

namespace Finance.Utilities.FormBuilder.Interface
{
    public interface IForm
    {
        void SetForm(Grid grid);
        void SetSubmit(Button submitButton);
        void SetCancel(Button cancelButton);
    }
}
