using CommunityToolkit.Mvvm.ComponentModel;

namespace CuandoPagan.Views;

[QueryProperty(nameof(Nombre), "nombre")]
public partial class HomePage : ContentPage
{
    public string Nombre
    {
        set
        {
            BindingContext = new
            {
                Nombre = value
            };
        }
    }

    public HomePage()
    {
        InitializeComponent();
    }
}