using Finance.Utilities.FormBuilder.Interface;
using System.Windows.Controls;

namespace Finance.Utilities.FormBuilder
{
    public class Form : IForm
    {
        private Grid _grid;
        private Button _submitButton;
        private Button _cancelButton;

        public void SetCancel(Button cancelButton)
        {
            _cancelButton = cancelButton;
        }

        public void SetForm(Grid grid)
        {
            _grid = grid;
        }

        public void SetSubmit(Button submitButton)
        {
            _submitButton = submitButton;
        }
    }
}
