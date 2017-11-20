using DevExpress.Xpf.Core;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SuperSocket.ClientEngine;
using SuperSocket.Model;
using SuperSocket.SocketClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;

namespace SuperSocket.ViewModel
{
	public class ClientViewModel: GalaSoft.MvvmLight.ViewModelBase
	{
		private ObservableCollection<ClientConnectInfo> _ClientConnectInfoList;
		public ObservableCollection<ClientConnectInfo> ClientConnectInfoList
		{
			get
			{
				return _ClientConnectInfoList;
			}
			set
			{
				_ClientConnectInfoList = value;
				RaisePropertyChanged("ClientConnectInfoList");
			}
		}
		private ObservableCollection<ClientMessage> _ClientMessageList;

		public ObservableCollection<ClientMessage> ClientMessageList
		{
			get
			{
				return _ClientMessageList;
			}
			set
			{
				_ClientMessageList = value;
				RaisePropertyChanged("ClientMessageList");
			}
		}
		public Dictionary<string, ObservableCollection<ClientMessage>> AllClientMessageList { get; private set; }
		private List<EasyClient<MyPackageInfo>> ClientList { get; set; }
		public RelayCommand ShowCreateConnectViewCommand { get; private set; }
		public RelayCommand CloseCommand { get; private set; }
		public RelayCommand DeleteCommand { get; private set; }
		public RelayCommand ConnectCommand { get; set; }
		public RelayCommand SendCommand { get; set; }
		private bool _DisConnectEnabled;
		private bool _ConnectEnabled;
		private bool _DeleteEnabled;
		private bool _SendEnabled;
		public ClientConnectInfo SelectedClientConnectInfo
		{
			get
			{
				return _SelectedClientConnectInfo;
			}
			set
			{
				_SelectedClientConnectInfo = value;

				if (_SelectedClientConnectInfo != null)
				{
					DisConnectEnabled = _SelectedClientConnectInfo.IsConnected == "已连接";
					ConnectEnabled = _SelectedClientConnectInfo.IsConnected != "已连接";
					DeleteEnabled = true;
					ClientMessageList = AllClientMessageList[_SelectedClientConnectInfo.LocalEndPoint];
					SendEnabled = _SelectedClientConnectInfo.IsConnected == "已连接";
				}
				else
				{
					ConnectEnabled = false;
					DisConnectEnabled = false;
					DeleteEnabled = false;
					ClientMessageList = null;
					SendEnabled = false;
				}
				RaisePropertyChanged("SelectedClientConnectInfo");
			}
		}

		private string _SendMessageContent;
		public bool DisConnectEnabled
		{
			get
			{
				return _DisConnectEnabled;
			}
			set
			{
				_DisConnectEnabled = value;
				RaisePropertyChanged("DisConnectEnabled");
			}
		}
		public bool ConnectEnabled
		{
			get
			{
				return _ConnectEnabled;
			}
			set
			{
				_ConnectEnabled = value;
				RaisePropertyChanged("ConnectEnabled");
			}
		}
		public bool DeleteEnabled
		{
			get
			{
				return _DeleteEnabled;
			}
			set
			{
				_DeleteEnabled = value;
				RaisePropertyChanged("DeleteEnabled");
			}
		}
		public bool SendEnabled
		{
			get
			{
				return _SendEnabled;
			}
			set
			{
				_SendEnabled = value;
				RaisePropertyChanged("SendEnabled");
			}
		}

		public string SendMessageContent
		{
			get
			{
				return _SendMessageContent;
			}
			set
			{
				_SendMessageContent = value;

				RaisePropertyChanged("SendMessageContent");
			}
		}

		private ClientConnectInfo _SelectedClientConnectInfo;
		public ClientViewModel()
		{
			ClientConnectInfoList = new ObservableCollection<ClientConnectInfo>();
			ClientMessageList = new ObservableCollection<ClientMessage>();
			AllClientMessageList = new Dictionary<string, ObservableCollection<ClientMessage>>();
			ClientList = new List<EasyClient<MyPackageInfo>>();
			ShowCreateConnectViewCommand = new RelayCommand(ShowCreateConnectView);
			CloseCommand = new RelayCommand(Close);
			DeleteCommand = new RelayCommand(Delete);
			ConnectCommand = new RelayCommand(Connect);
			SendCommand = new RelayCommand(Send);
		}
		private void ShowCreateConnectView ()
		{
			Messenger.Default.Send<object>(null, "ShowCreateClientView");
			Messenger.Default.Register<Dictionary<string, string>>(this, "ConnectToServer", ConnectToServer);
		}
		private void Connect()
		{
			for (int i = 0; i < ClientList.Count; i++)
			{
				if (ClientList[i].IsConnected == false)
				{
					if (ClientList[i].LocalEndPoint.ToString() == SelectedClientConnectInfo.LocalEndPoint)
					{
						string localEndPoint = SelectedClientConnectInfo.LocalEndPoint;
						if (ClientList[i].ConnectAsync(new IPEndPoint ( IPAddress.Parse(SelectedClientConnectInfo.ServerAddress), SelectedClientConnectInfo.ServerPort)).Result)
						{
							for (int k = 0; k < ClientConnectInfoList.Count; k++)
							{
								if (ClientConnectInfoList[k].LocalEndPoint == localEndPoint)
								{
									ClientConnectInfoList[k].IsConnected = "已连接";
								}
							}
						}
					}
				}
			}
		}
		private void Close()
		{
			for (int i = 0; i < ClientList.Count; i++)
			{
				if (ClientList[i].IsConnected == true)
				{
					if (ClientList[i].LocalEndPoint.ToString() == SelectedClientConnectInfo.LocalEndPoint)
					{
						string localEndPoint = SelectedClientConnectInfo.LocalEndPoint;
						if (ClientList[i].Close().Result)
						{
							ClientList.Remove(ClientList[i]);

							for (int k = 0; k < ClientConnectInfoList.Count; k++)
							{
								if (ClientConnectInfoList[k].LocalEndPoint == localEndPoint)
								{
									ClientConnectInfoList.Remove(ClientConnectInfoList[k]);
									AllClientMessageList.Remove(localEndPoint);
								}
							}
						}					
					}
				}
			}
		}
		private void Delete()
		{
			for (int i = 0; i < ClientList.Count; i++)
			{
				if (ClientList[i].IsConnected == true)
				{
					if (ClientList[i].LocalEndPoint.ToString() == SelectedClientConnectInfo.LocalEndPoint)
					{
						string localEndPoint = SelectedClientConnectInfo.LocalEndPoint;
						if (ClientList[i].Close().Result)
						{
							ClientList.Remove(ClientList[i]);

							for (int k = 0; k < ClientConnectInfoList.Count; k++)
							{
								if (ClientConnectInfoList[k].LocalEndPoint == localEndPoint)
								{
									ClientConnectInfoList.Remove(ClientConnectInfoList[k]);
									AllClientMessageList.Remove(localEndPoint);
								}
							}
						}
					}
				}
				else
				{
					if (ClientList[i].LocalEndPoint.ToString() == SelectedClientConnectInfo.LocalEndPoint)
					{
						string localEndPoint = SelectedClientConnectInfo.LocalEndPoint;	
							ClientList.Remove(ClientList[i]);
							for (int k = 0; k < ClientConnectInfoList.Count; k++)
							{
								if (ClientConnectInfoList[k].LocalEndPoint == localEndPoint)
								{
									ClientConnectInfoList.Remove(ClientConnectInfoList[k]);
								}
							}						
					}
				}
			}
		}

		private void Send()
		{

			if (SendMessageContent.Length > 0)
			{
				var query = ClientList.Find(p => p.LocalEndPoint.ToString() == SelectedClientConnectInfo.LocalEndPoint);
				query.Send(Encoding.Default.GetBytes(SendMessageContent));
			}


		}
		private async void ConnectToServer(Dictionary<string, string> obj)
		{

			var client = new EasyClient<MyPackageInfo>();
			client.Initialize(new NoReceiveFilter());
			client.Connected += OnClientConnected;
			client.NewPackageReceived += OnPackageReceived;
			client.Error += OnClientError;
			client.Closed += OnClientClosed;
			var connected = await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(obj["IPAddress"]), Convert.ToInt32(obj["Port"])));



			if (connected)
			{
				ClientConnectInfo sci = new ClientConnectInfo
				{
					ServerAddress = obj["IPAddress"] + ":" + obj["Port"],
					ServerPort = Convert.ToInt32(obj["Port"]),
					LocalEndPoint = client.LocalEndPoint.ToString(),
					IsConnected = "已连接"
				};

				ClientConnectInfoList.Add(sci);
				ClientList.Add(client);
			}
			else
			{
				DXMessageBox.Show("连接服务端失败！","提示");
			}

			Messenger.Default.Unregister<Dictionary<string, string>>(this, "ConnectToServer", ConnectToServer);


		}
		private void OnPackageReceived(object sender, PackageEventArgs<MyPackageInfo> e)
		{
			var client = sender as EasyClient<MyPackageInfo>;
			if (AllClientMessageList.ContainsKey(client.LocalEndPoint.ToString()))
			{
				AllClientMessageList[client.LocalEndPoint.ToString()].Add(new ClientMessage
				{
					MsgType = MessageType.Receive,
					MsgTime = DateTime.Now,
					MsgContent = Encoding.Default.GetString(e.Package.AllData)
				});
			}
			else
			{
				ClientMessage cmsg = new ClientMessage
				{								
					MsgType = MessageType.Receive,
					MsgTime = DateTime.Now,
					MsgContent = Encoding.Default.GetString(e.Package.AllData)
	};
				AllClientMessageList.Add(client.LocalEndPoint.ToString(), new ObservableCollection<ClientMessage> { cmsg });
			}
		}
		private void OnClientConnected(object sender, EventArgs e)
		{

		}
		private void OnClientClosed(object sender, EventArgs e)
		{
			var clinet = sender as EasyClient<MyPackageInfo>;
			for (int i = 0; i < ClientConnectInfoList.Count; i++)
			{
				if (ClientConnectInfoList[i].LocalEndPoint == clinet.LocalEndPoint.ToString())
				{
					ClientConnectInfoList[i].IsConnected = "断开";
				}
			}
		}
		private void OnClientError(object sender, ErrorEventArgs e)
		{
		}

	}	

	public enum MessageType
	{
		System = 1,
		Send = 2,
		Receive = 3
	}

	public class MessageTypeName
	{
		public MessageType MsgType { get; set; }

		public string  MsgTypeName { get; set; }
	}
}