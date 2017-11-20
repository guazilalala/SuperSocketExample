using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuperSocket.Command
{
	public class DelegateCommand<T> : ICommand
	{
		private readonly Action<T> _executeMethod;
		private readonly Func<T,bool> _canExecuteMethod;

		public DelegateCommand(Action<T> execute) : this(execute, null)
		{

		}

		public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
		{
			_executeMethod = executeMethod ?? throw new ArgumentException("executeMethod");
			_canExecuteMethod = canExecuteMethod;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(T parameter)
		{
			return _canExecuteMethod == null ? true : _canExecuteMethod(parameter);
		}

		public void Execute(T parameter)
		{
			_executeMethod(parameter);
		}

		public bool CanExecute(object parameter)
		{
			if (parameter == null && typeof(T).IsValueType)
			{
				return (_canExecuteMethod == null);
			}
			return CanExecute((T)parameter);
		}

		public void Execute(object parameter)
		{
			Execute((T)parameter);
		}
	}
}
