using AngleSharp.Html.Parser;
using LiveCharts;
using LiveCharts.Wpf;
using StockChart.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace StockChart.Models
{
    public class StockChartModel
    {
        public Tuple<string, ChartDataDto> SearchChartData(string stockCode)
        {
            // HtmlParserクラスをインスタンス化
            var parser = new HtmlParser();
            string url = ConfigurationManager.AppSettings["URL"] + stockCode + "/";

            // 指定されたURLに対してのRequestを作成します。
            var req = (HttpWebRequest)WebRequest.Create(url);

            // html取得文字列
            List<string[]> stockDataList = new List<string[]>();

            //サイト名
            string siteName = string.Empty;

            // 指定したURLに対してReqestを投げてResponseを取得します。
            using (var res = (HttpWebResponse)req.GetResponse())
            {
                using (var resSt = res.GetResponseStream())
                {
                    // 取得した文字列をUTF8でエンコードします。
                    using (var sr = new StreamReader(resSt, Encoding.UTF8))
                    {
                        // HTMLを取得する。
                        string tempStream = sr.ReadToEnd();
                        // HtmlParserクラスのParserメソッドを使用してパースする。
                        // Parserメソッドの戻り値の型はIHtmlDocument
                        var htmlDocument = parser.ParseDocument(tempStream);
                        var urlElements = htmlDocument.QuerySelectorAll("tbody");
                        //サイト名を設定
                        siteName = htmlDocument.Title;

                        foreach (var element in urlElements)
                        {
                            stockDataList.Add(Regex.Replace(element.TextContent
                                                   .Replace("\n    \n", "")
                                                   .Replace(" ", ""), "^[\r\n]+", string.Empty, RegexOptions.Multiline)
                                         .Split('\n'));
                        }
                    }
                }

            }

            //価格
            var costList = new ChartValues<int>();
            //出来高
            var dekidakaList = new ChartValues<int>();
            //日毎の価格差
            var diffList = new ChartValues<int>();
            //日付
            var dayList = new List<string>();


            stockDataList.Select((row, index) => new { Index = index, Cost = row[4], Dekidaka = row[5], Day = row[0] })
                         .Reverse()
                         .ToList()
                         .ForEach(row =>
                         {
                             costList.Add(int.Parse(row.Cost));
                             dekidakaList.Add(int.Parse(row.Dekidaka));
                             dayList.Add(row.Day);
                             if (row.Index == 0)
                             {
                                 diffList.Add(0);
                             }
                             else
                             {
                                 diffList.Add(int.Parse(stockDataList[row.Index][4])
                                        - int.Parse(stockDataList[row.Index - 1][4]));
                             }

                         });

            //チャートデータに変換
            ChartDataDto chartDataDto = new ChartDataDto
            {
                //価格チャート
                CostList = new SeriesCollection
                                {
                                    new LineSeries
                                    {
                                        Values = costList
                                    } ,
                                },
                //出来高チャート
                DekidakaList = new SeriesCollection
                                {
                                    new LineSeries
                                    {
                                        Values = dekidakaList
                                    } ,
                                },
                //日付の価格差
                DiffList = new SeriesCollection
                                {
                                    new LineSeries
                                    {
                                        Values = diffList
                                    } ,
                                },
                //日付
                DayList = dayList
            };

            return Tuple.Create(siteName, chartDataDto);

        }
    }
}
