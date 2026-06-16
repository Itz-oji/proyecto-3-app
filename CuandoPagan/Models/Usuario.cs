using SQLite;

namespace CuandoPagan.Models;

public class Usuario
{
    [PrimaryKey, AutoIncrement]
    public int Id {get; set;}
    public string Nombre {get; set;} = string.Empty;
}