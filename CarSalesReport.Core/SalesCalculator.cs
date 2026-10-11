namespace CarSalesReport.Core;

public class SalesCalculator
{
    public List<ModelSummary> Calculate(List<Sale> sales)
    {
        return sales.GroupBy(sale => sale.Model).Select(group => new ModelSummary(group.Key, group.Where(IsWeekend).Sum(sale => sale.Price), group.Where(IsWeekend).Sum(sale => sale.TotalPrice)))
            .OrderBy(r => r.Model).ToList();
    }

    private static bool IsWeekend(Sale sale) => sale.Date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
}