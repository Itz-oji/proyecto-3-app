using CuandoPagan.ViewModels;

namespace CuandoPagan.Views;

public partial class AddTransactionPage : ContentPage
{
    public AddTransactionPage()
    {
        InitializeComponent();
        BindingContext = new AddTransactionViewModel();
    }
}
