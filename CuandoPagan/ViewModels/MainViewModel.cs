using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuandoPagan.Models;
using CuandoPagan.Services;
using CuandoPagan.Views;
using System.Collections.ObjectModel;

namespace CuandoPagan.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private string nombre = string.Empty;

    [ObservableProperty]
    private string mensaje = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Usuario> usuarios = new();

    [ObservableProperty]
    private Usuario? usuarioSeleccionado;

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

        await Shell.Current.GoToAsync(nameof(HomePage), new Dictionary<string, object>
        {
            { "nombre", Nombre }
        });
    }

    [RelayCommand]
    private async Task CargarUsuarios()
    {
        var lista = await _databaseService.ObtenerUsuariosAsync();

        Usuarios.Clear();

        foreach (var usuario in lista)
        {
            Usuarios.Add(usuario);
        }

        Mensaje = $"Usuarios cargados: {Usuarios.Count}";
    }
}