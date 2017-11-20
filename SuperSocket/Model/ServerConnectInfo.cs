using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocket.Model
{
	public class ClientConnectInfo:INotifyPropertyChanged
	{
		private string _ServerAddress;
		private int _ServerPort;
		private string _LocalEndPoint;
		private string _IsConnected;

		public string ServerAddress
		{
			get { return _ServerAddress; }
			set
			{
				_ServerAddress = value;
				RaisePropertyChanged("ServerAddress");
			}
		}
		public string LocalEndPoint
		{
			get { return _LocalEndPoint; }
			set
			{
				_LocalEndPoint = value;
				RaisePropertyChanged("LocalEndPoint");
			}
		}
		public string IsConnected
		{
			get { return _IsConnected; }
			set
			{
				_IsConnected = value;
				RaisePropertyChanged("IsConnected");
			}
		}

		public int ServerPort
		{
			get { return _ServerPort; }
			set
			{
				_ServerPort = value;
				RaisePropertyChanged("ServerPort");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
