using SQLite;
using CuandoPagan.Models;

namespace CuandoPagan.Services;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;
    public DatabaseService()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Usuario>().Wait();
        _database.CreateTableAsync<Transaction>().Wait();
    }
    public async Task GuardarUsuarioAsync(Usuario usuario)
    {
        await _database.InsertAsync(usuario);
    }
    public Task<List<Usuario>> ObtenerUsuariosAsync()
    {
        return _database.Table<Usuario>().ToListAsync();
    }
    public async Task<Usuario?> ObtenerPrimerUsuarioAsync()
    {
        var usuarios = await _database.Table<Usuario>().ToListAsync();
        return usuarios.FirstOrDefault();
    }
    public async Task<Usuario?> ObtenerUsuarioAsync()
    {
        return await _database.Table<Usuario>().FirstOrDefaultAsync();
    }

    // Métodos para Transacciones
    public Task<List<Transaction>> ObtenerTransaccionesAsync()
    {
        return _database.Table<Transaction>().OrderByDescending(t => t.Date).ToListAsync();
    }

    public async Task GuardarTransaccionAsync(Transaction transaccion)
    {
        if (transaccion.Id != 0)
        {
            await _database.UpdateAsync(transaccion);
        }
        else
        {
            await _database.InsertAsync(transaccion);
        }
    }

    public async Task EliminarTransaccionAsync(Transaction transaccion)
    {
        await _database.DeleteAsync(transaccion);
    }
}