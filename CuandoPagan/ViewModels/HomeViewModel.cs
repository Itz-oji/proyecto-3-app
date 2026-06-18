using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuandoPagan.Models;
using CuandoPagan.Services;
using CuandoPagan.Views;
using System.Collections.ObjectModel;

namespace CuandoPagan.ViewModels;

[QueryProperty(nameof(Nombre), "nombre")]
public partial class HomeViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private string nombre = string.Empty;

    [ObservableProperty]
    private double balance;

    [ObservableProperty]
    private double totalIngresos;

    [ObservableProperty]
    private double totalGastos;

    [ObservableProperty]
    private ObservableCollection<Transaction> transacciones = new();

    public HomeViewModel()
    {
        _databaseService = new DatabaseService();
    }

    [RelayCommand]
    private async Task IrAAgregarTransaccionAsync()
    {
        if (Shell.Current != null)
        {
            await Shell.Current.GoToAsync(nameof(AddTransactionPage));
        }
    }

    [RelayCommand]
    public async Task CargarDatosAsync()
    {
        try
        {
            var lista = await _databaseService.ObtenerTransaccionesAsync();
            
            Transacciones.Clear();
            double ingresosTemp = 0;
            double gastosTemp = 0;

            foreach (var t in lista)
            {
                Transacciones.Add(t);
                if (t.IsIncome)
                {
                    ingresosTemp += t.Amount;
                }
                else
                {
                    gastosTemp += t.Amount;
                }
            }

            TotalIngresos = ingresosTemp;
            TotalGastos = gastosTemp;
            Balance = TotalIngresos - TotalGastos;
        }
        catch (Exception ex)
        {
            if (Shell.Current != null)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"No se pudieron cargar los datos: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    public async Task EliminarTransaccionAsync(Transaction transaccion)
    {
        if (transaccion == null) return;

        if (Shell.Current != null)
        {
            bool confirm = await Shell.Current.DisplayAlertAsync("Confirmar", "¿Deseas eliminar esta transacción?", "Sí", "No");
            if (confirm)
            {
                await _databaseService.EliminarTransaccionAsync(transaccion);
                await CargarDatosAsync();
            }
        }
    }
}
