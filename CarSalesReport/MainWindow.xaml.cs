using System.IO;
using System.Windows;
using System.Xml;
using CarSalesReport.Core;
using Microsoft.Win32;

namespace CarSalesReport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly SalesReader _reader = new();
        public readonly SalesCalculator _calculate = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "XML soubory (*.xml)|*.xml",
                InitialDirectory = Path.Combine(AppContext.BaseDirectory, "Data")
            };

            if(dialog.ShowDialog() != true)
            {
                return;
            }

            try
            {
                List<Sale> sales = _reader.Load(dialog.FileName);
                List<ModelSummary> summary = _calculate.Calculate(sales);

                SalesGrid.ItemsSource = sales;
                SummaryList.ItemsSource = summary;
                FilePathText.Text = dialog.FileName;
            }
            catch(Exception ex) when (ex is XmlException or FormatException or NullReferenceException)
            {
                MessageBox.Show($"Soubor se nepodarilo nacit: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}