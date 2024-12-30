namespace Finance.Utilities.FormBuilder.Interface
{
    public interface IFormBuilder
    {
        void BuildForm();
        void BuildSubmitButton();
        void BuildCancelButton();

        Form GetForm();
    }
}
