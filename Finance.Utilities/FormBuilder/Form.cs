using Finance.Utilities.FormBuilder.Interface;
using System.Windows.Controls;

namespace Finance.Utilities.FormBuilder
{
    public class Form : IForm
    {
        private Grid _grid;
        private Button _submitButton;
        private Button _cancelButton;

        public Button GetCancel()
        {
            return _cancelButton;
        }

        public Grid GetGrid()
        {
            return _grid;
        }

        public Button GetSubmit()
        {
            return _submitButton;
        }

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
