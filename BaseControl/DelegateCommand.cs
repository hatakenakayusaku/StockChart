using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StockChart.BaseControl
{
    class DelegateCommand : ICommand
    {
        #region フィールド

        /// <summary>
        /// コマンドを実行するためのメソッドを保持します。
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// コマンドの実行可能性を判別するためのメソッドを保持します。
        /// </summary>
        private Func<object, bool> _canExecute;
        
        #endregion フィールド

        #region コンストラクタ
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="execute">コマンドを実行するためのメソッドを指定します。</param>
        public DelegateCommand(Action<object> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="execute">コマンドを実行するためのメソッドを指定します。</param>
        /// <param name="canExecute">コマンドの実行可能性を判別するためのメソッドを指定します。</param>
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }
        #endregion コンストラクタ

        #region ICommand のメンバ
        /// <summary>
        /// コマンドの実行可能性を判別します。
        /// </summary>
        /// <param name="parameter">この処理に対するパラメータを指定します。</param>
        /// <returns>コマンドが実行可能である場合に true を返します。</returns>
        public bool CanExecute(object parameter)
        {
            return _execute != null;
        }

        /// <summary>
        /// コマンドの実行可能性が変更されたときに発生します。
        /// </summary>
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        /// <param name="parameter">この処理に対するパラメータを指定します。</param>
        public void Execute(object parameter)
        {
            if (this._execute != null)
                this._execute(parameter);
        }
        #endregion ICommand のメンバ
    }
}
