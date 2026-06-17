using CuandoPagan.Models;
using CuandoPagan.Services;
using CuandoPagan.Views;

namespace CuandoPagan;

public partial class App : Application
{
    private readonly DatabaseService _databaseService;

    public App()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var shell = new AppShell();

        Task.Run(async () =>
        {
            var usuario = await _databaseService.ObtenerPrimerUsuarioAsync();

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (usuario != null)
                {
                    await Shell.Current.GoToAsync(nameof(HomePage), new Dictionary<string, object>
                    {
                        { "nombre", usuario.Nombre }
                    });
                }
            });
        });

        return new Window(shell);
    }
}