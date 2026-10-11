using CarSalesReport.Core;

namespace CarSales.Tests;

public class SalesCalculatorTests
{
    private readonly SalesCalculator _calculator = new();

    [Fact]
    public void Dph_IsCalculatedSeparately()
    {
        var sales = new List<Sale>
        {
            new() { Model = "Škoda Felicia", Date = new DateOnly(2000, 12, 3), Price = 210000, Dph = 20 }, // nedele
            new() { Model = "Škoda Felicia", Date = new DateOnly(2000, 12, 3), Price = 210000, Dph = 19 }, // nedele
            new() { Model = "Škoda Felicia", Date = new DateOnly(2000, 12, 2), Price = 210000, Dph = 19 }, // sobota
        };

        var result = _calculator.Calculate(sales);

        var felicia = Assert.Single(result);
        Assert.Equal(630000m, felicia.Price);
        Assert.Equal(751800m, felicia.TotalPrice);
    }

    [Fact]
    public void Workday_NotCounted()
    {
        var sales = new List<Sale>
        {
            new() { Model = "Škoda Oktávia", Date = new DateOnly(2010, 12, 2), Price = 500000, Dph = 20 }, // ctvrtek
            new() { Model = "Škoda Oktávia", Date = new DateOnly(2010, 12, 4), Price = 500000, Dph = 20 }, // sobota
        };

        var result = _calculator.Calculate(sales);

        var oktavia = Assert.Single(result);
        Assert.Equal(500000m, oktavia.Price);
        Assert.Equal(600000m, oktavia.TotalPrice);
    }

    [Fact]
    public void ModelWithoutWeekend_HasZero()
    {
        var sale = new List<Sale>
        {
            new(){ Model = "Škoda Forman", Date = new DateOnly(2000, 12, 4), Price = 100000, Dph = 19 } // pondeli
        };

        var result = _calculator.Calculate(sale);

        var forman = Assert.Single(result);
        Assert.Equal(0m, forman.Price);
        Assert.Equal(0m, forman.TotalPrice);
    }
}
