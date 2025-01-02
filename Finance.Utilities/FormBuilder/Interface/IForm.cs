using Finance.Core;
using System.Windows.Controls;

namespace Finance.Utilities.FormBuilder.Interface
{
    public interface IForm
    {
        void SetTitle(string title);
        void SetGrid(Grid grid);
        void SetSubmitButton(string content);
        void SetCancelButton(string content);
        void SetSubmitCommand(RelayCommand relayCommand);
        void SetCancelCommand(RelayCommand relayCommand);

        string GetTitle();
        Grid GetGrid();
        Button GetSubmitButton();
        Button GetCancelButton();
        RelayCommand GetSubmitCommand();
        RelayCommand GetCancelCommand();
    }
}
