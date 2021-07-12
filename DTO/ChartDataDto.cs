using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockChart.DTO
{
    /// <summary>
    /// チャートデータ
    /// </summary>
    public class ChartDataDto
    {
        /// <summary>
        /// 日付リスト
        /// </summary>
        public List<string> DayList { get; set; } = new List<string>();

        /// <summary>
        /// 価格リスト
        /// </summary>
        public SeriesCollection CostList { get; set; } = new SeriesCollection();

        /// <summary>
        /// 出来高リスト
        /// </summary>
        public SeriesCollection DekidakaList { get; set; } = new SeriesCollection();

        /// <summary>
        /// 日毎の価格差
        /// </summary>
        public SeriesCollection DiffList { get; set; } = new SeriesCollection();

    }
}
