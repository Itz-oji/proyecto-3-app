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
    }
    public async Task GuardarUsuarioAsync(Usuario usuario)
    {
        await _database.InsertAsync(usuario);
    }
    public async Task<Usuario?> ObtenerUsuarioAsync()
    {
        return await _database.Table<Usuario>().FirstOrDefaultAsync();
    }
}