using System.Xml.Linq;
using System.Globalization;

namespace CarSalesReport.Core;

public class SalesReader
{
    public List<Sale> Load(string path)
    {
        XDocument doc = XDocument.Load(path);

        var sales = new List<Sale>();

        foreach(XElement element in doc.Root!.Elements("sale"))
        {
            string model = element.Element("model")!.Value;
            string dateText = element.Element("date")!.Value;
            string priceText = element.Element("price")!.Value;
            string dphText = element.Element("DPH")!.Value;

            var sale = new Sale
            {
                Model = model,
                Date = DateOnly.Parse(dateText, CultureInfo.InvariantCulture),
                Price = decimal.Parse(priceText, CultureInfo.InvariantCulture),
                Dph = decimal.Parse(dphText, CultureInfo.InvariantCulture)
            };

            sales.Add(sale);
        }

        return sales;
    }
}
