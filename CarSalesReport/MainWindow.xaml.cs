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
        private readonly SalesReader _reader = new();
        private readonly SalesCalculator _calculator = new();

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
                List<ModelSummary> summary = _calculator.Calculate(sales);

                SalesGrid.ItemsSource = sales;
                SummaryList.ItemsSource = summary;
                FilePathText.Text = dialog.FileName;
            }
            catch(Exception ex) when (ex is XmlException or FormatException or NullReferenceException)
            {
                MessageBox.Show($"Soubor se nepodařilo načíst: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}