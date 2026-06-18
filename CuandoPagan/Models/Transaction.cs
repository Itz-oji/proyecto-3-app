using SQLite;

namespace CuandoPagan.Models;

public class Transaction
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    public string Description { get; set; } = string.Empty;
    
    public double Amount { get; set; }
    
    public bool IsIncome { get; set; } // true = Ingreso, false = Gasto
    
    public DateTime Date { get; set; }

    [Ignore]
    public string DisplayAmount => IsIncome ? $"+ ${Amount:N2}" : $"- ${Amount:N2}";

    [Ignore]
    public string DisplayColor => IsIncome ? "#27AE60" : "#C0392B";
}
