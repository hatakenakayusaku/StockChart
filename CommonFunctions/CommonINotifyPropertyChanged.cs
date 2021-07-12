using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StockChart.CommonFunctions
{
    /// <summary>
    /// プロパティチェンジの基底クラス
    /// </summary>
    public class CommonINotifyPropertyChanged : INotifyPropertyChanged
    {
        //プロパティ値の変更時にViewに通知
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// プロパティチェンジメソッド
        /// </summary>
        /// <param name="propertyName">プロパティ名称</param>
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
