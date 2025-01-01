using Finance.Core;
using Finance.Utilities.FormBuilder.Interface;
using System.Windows.Controls;
using System.Windows.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Finance.Utilities.FormBuilder
{
    public class Form : IForm
    {
        private Grid _grid;
        private Button _submitButton;
        private Button _cancelButton;

        private RelayCommand _submitButtonCommand;
        private RelayCommand _cancelButtonCommand;

        public Button GetCancelButton()
        {
            return _cancelButton;
        }

        public RelayCommand GetCancelCommand()
        {
            return _cancelButtonCommand;
        }

        public Grid GetGrid()
        {
            return _grid;
        }

        public Button GetSubmitButton()
        {
            return _submitButton;
        }

        public RelayCommand GetSubmitCommand()
        {
            return _submitButtonCommand;
        }

        public void SetCancelButton(string content)
        {
            Button cancelButton = new Button()
            {
                Content = content,
            };

            cancelButton.SetBinding(Button.CommandProperty, new Binding("ExecuteCancelCommand"));

            _cancelButton = cancelButton;
        }

        public void SetCancelCommand(RelayCommand relayCommand)
        {
            _cancelButtonCommand = relayCommand;
        }

        public void SetGrid(Grid grid)
        {
            _grid = grid;
        }

        public void SetSubmitButton(string content)
        {
            Button submitButton = new Button()
            {
                Content = content,
            };

            submitButton.SetBinding(Button.CommandProperty, new Binding("ExecuteSubmitCommand"));

            _submitButton = submitButton;
        }

        public void SetSubmitCommand(RelayCommand relayCommand)
        {
            _submitButtonCommand = relayCommand;
        }
    }
}
