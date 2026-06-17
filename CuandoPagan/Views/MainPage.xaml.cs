using CuandoPagan.ViewModels;

namespace CuandoPagan.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        try
        {
            BindingContext = new MainViewModel();
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
    }
}