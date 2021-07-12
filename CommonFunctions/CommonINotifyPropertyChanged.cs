using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
