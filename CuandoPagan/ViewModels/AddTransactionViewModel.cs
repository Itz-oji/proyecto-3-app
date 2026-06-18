using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuandoPagan.Models;
using CuandoPagan.Services;

namespace CuandoPagan.ViewModels;

public partial class AddTransactionViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private string descripcion = string.Empty;

    [ObservableProperty]
    private string montoText = string.Empty;

    [ObservableProperty]
    private DateTime fecha = DateTime.Today;

    [ObservableProperty]
    private bool esIngreso;

    public AddTransactionViewModel()
    {
        _databaseService = new DatabaseService();
    }

    [RelayCommand]
    private async Task GuardarAsync()
    {
        if (string.IsNullOrWhiteSpace(Descripcion))
        {
            await Shell.Current.DisplayAlertAsync("Error", "Por favor ingresa una descripción (glosa).", "OK");
            return;
        }

        if (!double.TryParse(MontoText, out double montoVal) || montoVal <= 0)
        {
            await Shell.Current.DisplayAlertAsync("Error", "Por favor ingresa una cantidad válida mayor a 0.", "OK");
            return;
        }

        var nuevaTransaccion = new Transaction
        {
            Description = Descripcion,
            Amount = montoVal,
            IsIncome = EsIngreso,
            Date = Fecha
        };

        await _databaseService.GuardarTransaccionAsync(nuevaTransaccion);

        // Volver a la página anterior
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task CancelarAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
