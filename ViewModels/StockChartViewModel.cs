using LiveCharts;
using StockChart.BaseControl;
using StockChart.CommonFunctions;
using StockChart.DTO;
using StockChart.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockChart.ViewModels
{
    /// <summary>
    /// 株価チャート画面のViewModel
    /// </summary>
    class StockChartViewModel : CommonINotifyPropertyChanged
    {

        #region Model

        /// <summary>
        /// チャートデータ
        /// </summary>
        private StockChartModel Model;

        #endregion

        #region プロパティ

        /// <summary>
        /// 株コード
        /// </summary>
        private string _SiteName;

        /// <summary>
        /// 株コード
        /// </summary>
        public string SiteName
        {
            get
            {
                return this._SiteName;
            }
            set
            {
                if (value != this._SiteName)
                {
                    this._SiteName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 株コード
        /// </summary>
        private string _StockCode;

        /// <summary>
        /// 株コード
        /// </summary>
        public string StockCode
        {
            get
            {
                return this._StockCode;
            }
            set
            {
                if (value != this._StockCode)
                {
                    this._StockCode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// チャートデータ
        /// </summary>
        private ChartDataDto _ChartData;

        /// <summary>
        /// チャートデータ
        /// </summary>
        public ChartDataDto ChartData
        {
            get
            {
                return this._ChartData;
            }
            set
            {
                if (value != this._ChartData)
                {
                    this._ChartData = value;
                    NotifyPropertyChanged();
                }
            }
        }




        #endregion

        #region イベント

        /// <summary>
        /// Drawボタンクリック
        /// </summary>
        private DelegateCommand _DrawButton;

        /// <summary>
        /// Drawボタンクリック
        /// </summary>
        public DelegateCommand DrawButton
        {
            get
            {
                return this._DrawButton ?? (this._DrawButton = new DelegateCommand(

                _ =>
                {
                    //株価のチャートデータを取得
                    Tuple<string,ChartDataDto> result = this.Model.SearchChartData(this.StockCode);

                    //取得結果を設定
                    this.SiteName = result.Item1;
                    this.ChartData = result.Item2;

                },
                _ =>
                {
                    
                    if (this.ChartData is object)
                    {
                        //再検索を注意
                        MessageBoxResult result = MessageBox.Show("再検索しますか？"
                                                                ,"メッセージボックス"
                                                                ,MessageBoxButton.YesNoCancel);
                        if (result == MessageBoxResult.Yes)
                        {
                            //「はい」ボタンを押した場合の処理
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }));
            }
        }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StockChartViewModel()
        {
            //モデルのインスタンス作成
            this.Model = new StockChartModel();
        }

        #endregion

    }
}
