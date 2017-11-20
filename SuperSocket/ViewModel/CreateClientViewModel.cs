using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.Command;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Net;

namespace SuperSocket.ViewModel
{
	public class CreateClientViewModel: GalaSoft.MvvmLight.ViewModelBase
	{
		private string _IPAddress;
		public string IPAddress
		{
			get
			{
				return _IPAddress;
			}
			set
			{
				if (_IPAddress != value)
				{
					_IPAddress = value;
					RaisePropertyChanged("IPAddress");
				}
			}
		}

		private int _Port;
		public int Port
		{
			get
			{
				return _Port;
			}
			set
			{
				if (_Port != value)
				{
					_Port = value;
					RaisePropertyChanged("Port");
				}
			}
		}

		private bool _ViewDialogResult;
		public bool ViewDialogResult
		{
			get
			{
				return _ViewDialogResult;
			}
			set
			{
				if (_ViewDialogResult != value)
				{
					_ViewDialogResult = value;
					RaisePropertyChanged("ViewDialogResult");
				}
			}
		}

		public RelayCommand<object> ConfirmCommand { get; set; }
		public CreateClientViewModel()
		{
			IPAddress = "192.168.5.25";
			Port = 2020;
			ConfirmCommand = new RelayCommand<object>(Confirm);
		}

		void Confirm(object parameter)
		{
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("IPAddress", IPAddress);
			dic.Add("Port", Port.ToString());

			Messenger.Default.Send(dic, "ConnectToServer");
			Messenger.Default.Send<object>(null, "CloseCreateClientView");
		}
	}
}
