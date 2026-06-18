using CuandoPagan.ViewModels;

namespace CuandoPagan.Views;

[QueryProperty(nameof(Nombre), "nombre")]
public partial class HomePage : ContentPage
{
    public string Nombre
    {
        set
        {
            if (BindingContext is HomeViewModel vm)
            {
                vm.Nombre = value;
            }
        }
    }

    public HomePage()
    {
        InitializeComponent();
        BindingContext = new HomeViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is HomeViewModel vm)
        {
            await vm.CargarDatosCommand.ExecuteAsync(null);
        }
    }
}