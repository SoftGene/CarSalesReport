namespace CarSalesReport.Core;

public class Sale
{
    public string Model { get; init; } = string.Empty;
    public DateOnly Date { get; init; }
    public decimal Price { get; init; }
    public decimal Dph { get; init; }
    public decimal TotalPrice => Price * (1 + Dph / 100);
}
