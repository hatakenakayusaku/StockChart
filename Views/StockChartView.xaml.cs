using AngleSharp.Html.Parser;
using LiveCharts;
using LiveCharts.Wpf;
using StockChart.DTO;
using StockChart.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
