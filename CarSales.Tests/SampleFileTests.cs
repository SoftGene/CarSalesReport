using CarSalesReport.Core;

namespace CarSales.Tests;

public class SampleFileTests
{
    [Fact]
    public void SampleFile_ExpectedWeekendsTotal()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "Data", "sales.xml");

        List<Sale> sales = new SalesReader().Load(path);
        List<ModelSummary> result = new SalesCalculator().Calculate(sales);

        Assert.Equal(13, sales.Count);
        Assert.Equal(5, result.Count);

        var oktavia = result.Single(r => r.Model == "Škoda Oktávia");
        Assert.Equal(1000000m, oktavia.Price);
        Assert.Equal(1200000m, oktavia.TotalPrice);

        var fabia = result.Single(r => r.Model == "Škoda Fabia");
        Assert.Equal(700000m, fabia.Price);
        Assert.Equal(840000m, fabia.TotalPrice);

        var felicia = result.Single(r => r.Model == "Škoda Felicia");
        Assert.Equal(630000m, felicia.Price);
        Assert.Equal(751800m, felicia.TotalPrice);
    }
}