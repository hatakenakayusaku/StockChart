using StockChart.ViewModels;
using System.Windows;

namespace StockChart.Views
{
    /// <summary>
    /// StockChartView.xaml の相互作用ロジック
    /// </summary>
    public partial class StockChartView : Window
    {
        public StockChartView()
        {
            InitializeComponent();
            DataContext = new StockChartViewModel();
        }
    }
}
