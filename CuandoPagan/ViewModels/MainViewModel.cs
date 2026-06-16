using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuandoPagan.Models;
using CuandoPagan.Services;

namespace CuandoPagan.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private string nombre = string.Empty;

    [ObservableProperty]
    private string mensaje = string.Empty;

    public MainViewModel()
    {
        _databaseService = new DatabaseService();
    }

    [RelayCommand]
    private async Task GuardarNombre()
    {
        if (string.IsNullOrWhiteSpace(Nombre))
        {
            Mensaje = "Debes ingresar tu nombre";
            return;
        }

        var usuario = new Usuario
        {
            Nombre = Nombre
        };

        await _databaseService.GuardarUsuarioAsync(usuario);

        Mensaje = $"Hola {Nombre}, tu nombre fue guardado";
    }
}