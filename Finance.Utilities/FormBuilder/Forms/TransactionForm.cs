using Finance.Utilities.FormBuilder.Interface;
using System.Windows.Controls;

namespace Finance.Utilities.FormBuilder.Forms
{
    public class TransactionForm : IFormBuilder
    {
        private Form _form;

        public TransactionForm()
        {
            _form = new Form();

            BuildForm();
            BuildSubmitButton();
            BuildCancelButton();
        }

        public void BuildCancelButton()
        {
            Button button = new Button();

            _form.SetCancel(button);
        }

        public void BuildForm()
        {
            Grid grid = new Grid();

            _form.SetForm(grid);
        }

        public void BuildSubmitButton()
        {
            Button button = new Button();

            _form.SetSubmit(button);
        }

        public Form GetForm()
        {
            return _form;
        }
    }
}
