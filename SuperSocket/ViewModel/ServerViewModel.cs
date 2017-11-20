using SuperSocket.Command;
using SuperSocket.Model;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketEngine;
using SuperSocket.SocketServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SuperSocket.ViewModel
{

	public class ServerViewModel : GalaSoft.MvvmLight.ViewModelBase
	{
		/// <summary>
		/// 接收信息GridControl数据源
		/// </summary>
		public static ObservableCollection<ReceiveMessage> RecMsgDateSource { get; set; }
		/// <summary>
		/// 服务列表
		/// </summary>
		public static ObservableCollection<ServerInfo> ServerList { get; set; }

		/// <summary>
		/// 客户端列表
		/// </summary>
		public static ObservableCollection<ClientInfo> ClientList { get; set; }

		private ServerInfo selectedServerInfo;
		private ClientInfo selectedClientInfo;
		private bool startButtonEnable;
		private bool stopButtonEnable;
		public ClientInfo SelectedClientInfo
		{
			get
			{
				return selectedClientInfo;
			}
			set
			{
				if (selectedClientInfo != value)
				{
					selectedClientInfo = value;

					RaisePropertyChanged("SelectedClientInfo");
				}
			}
		}

		/// <summary>
		/// 启动按钮的状态标记
		/// </summary>
		public bool StartButtonEnable
		{
			get
			{
				return startButtonEnable;
			}
			set
			{
				if (startButtonEnable != value)
				{
					startButtonEnable = value;
					RaisePropertyChanged("StartButtonEnable");
				}
			}
		}
		/// <summary>
		/// 停止按钮的状态标记
		/// </summary>
		public bool StopButtonEnable
		{
			get
			{
				return stopButtonEnable;
			}
			set
			{
				if (stopButtonEnable != value)
				{
					stopButtonEnable = value;
					RaisePropertyChanged("StopButtonEnable");
				}
			}
		}

		/// <summary>
		/// 服务状态数据源
		/// </summary>
		public ObservableCollection<ServerStateName> ServerStateNameList;

		/// <summary>
		/// 当前选中的服务
		/// </summary>
		public ServerInfo SelectedServerInfo
		{
			get
			{
				return selectedServerInfo;
			}
			set
			{
				if (selectedServerInfo != value)
				{
					selectedServerInfo = value;

					StartButtonEnable = (selectedServerInfo == null?false: selectedServerInfo.State < 4);
					StopButtonEnable = !StartButtonEnable;
					RaisePropertyChanged("SelectedServerInfo");
				}
			}
		}

		public static IBootstrap Bootstrap { get; set; }

		/// <summary>
		/// 监听端口
		/// </summary>
		public int ListenPort { get; set; }

		/// <summary>
		/// 启动某服务按钮Command
		/// </summary>
		public DelegateCommand<object> StartServer { get; set; }

		/// <summary>
		/// 停止某服务按钮Command
		/// </summary>
		public DelegateCommand<object> StopServer { get; set; }

		public ServerViewModel()
		{
			StartServer = new DelegateCommand<object>(AppServerStart);
			StopServer = new DelegateCommand<object>(AppServerStop);
			ServerList = new ObservableCollection<ServerInfo>();
			ClientList = new ObservableCollection<ClientInfo>();
			RecMsgDateSource = new ObservableCollection<ReceiveMessage>();

			InitServer();

			Bootstrap.Start();

			RefreshServerInfoList();

		}

		/// <summary>
		/// 启动某服务
		/// </summary>
		/// <param name="parmamter"></param>
		private void AppServerStart(object parmamter)
		{
			if (SelectedServerInfo != null)
			{
				string serverName = SelectedServerInfo.Name;
				foreach (var item in Bootstrap.AppServers)
				{
					if (item.Name == SelectedServerInfo.Name && item.State == ServerState.NotStarted )
					{
						item.Start();
					}
				}

				RefreshServerInfoList();

				SelectedServerInfo = ServerList.Where(p => p.Name == serverName).FirstOrDefault();
			}

		}

		/// <summary>
		/// 初始化服务
		/// </summary>
		private void InitServer()
		{
			Bootstrap = BootstrapFactory.CreateBootstrap();
			
			if (Bootstrap.Initialize())
			{
				RefreshServerInfoList();
			}
		}

		/// <summary>
		/// 刷新UI服务信息列表
		/// </summary>
		public static void RefreshServerInfoList()
		{
			ServerList.Clear();

			var servers = Bootstrap.AppServers;

			foreach (var item in servers)
			{

				string listenerPort;
				string ipAddress;
				if (item.Config.Listeners == null)
				{
					listenerPort = item.Config.Port.ToString();
					ipAddress = item.Config.Ip;
				}
				else
				{
					List<string> ipList = new List<string>();

					List<string> portList = new List<string>();

					var query = item.Config.Listeners.ToList();

					foreach (var listener in query)
					{
						ipList.Add(listener.Ip);
						portList.Add(listener.Port.ToString());
					}


					if (ipList.Contains("Any"))
					{
						ipAddress = "Any";
					}
					else
					{
						ipAddress = string.Join(",", ipList);
					}

					listenerPort = string.Join(",", portList);

				}

				ServerInfo serInfo = new ServerInfo
				{
					Name = item.Name,
					IPAddress = ipAddress,
					ListenerPort = listenerPort,
					State = (int)item.State,
					Connections = item.SessionCount
				};
					ServerList.Add(serInfo);
			}
		}

		/// <summary>
		/// 添加新连接到客户端列表
		/// </summary>
		public static void AddClientToClientList(ClientInfo clientInfo)
		{
			ClientList.Add(clientInfo);
		}

		/// <summary>
		/// 从客户端列表删除
		/// </summary>
		public static void RemoveClientFromClientList(string sessionId)
		{
			for (int i = 0; i < ClientList.Count; i++)
			{
				if (ClientList[i].SessionID == sessionId)
				{
					ClientList.Remove(ClientList[i]);
				}
			}
		}

		/// <summary>
		/// 停止某服务
		/// </summary>
		/// <param name="parmamter"></param>
		private void AppServerStop(object parmamter)
		{
			if (SelectedServerInfo != null)
			{
				string serverName = SelectedServerInfo.Name;

				foreach (var item in Bootstrap.AppServers)
				{
					if (item.Name == SelectedServerInfo.Name && item.State == ServerState.Running)
					{
						item.Stop();
					}

					RefreshServerInfoList();
					SelectedServerInfo = ServerList.Where(p => p.Name == serverName).FirstOrDefault();
				}

			}

		}
		private void appServer_NewRequestReceived(CommonSession session, StringRequestInfo requestInfo)
		{
			switch (requestInfo.Key.ToUpper())
			{
				case ("ECHO"):
					session.Send(requestInfo.Body);
					break;

				case ("ADD"):
					session.Send(requestInfo.Parameters.Select(p => Convert.ToInt32(p)).Sum().ToString());
					break;

				case ("MULT"):

					var result = 1;

					foreach (var factor in requestInfo.Parameters.Select(p => Convert.ToInt32(p)))
					{
						result *= factor;
					}

					session.Send(result.ToString());
					break;
			}
		}
		private void appServer_NewSessionConnected(CommonSession session)
		{
			session.Send("Welcome to SuperSocket Server");
		}
		private	void appServer_SessionClosed(CommonSession session, CloseReason reason)
		{
			session.Send(string.Format("A session is closed for {0}.", reason));
		}

		public static void AddRecevieGridControlItem(string receviceMessage,string messageType)
		{
			var msg = new ReceiveMessage
			{
				MsgTime = DateTime.Now,
				MsgContent = receviceMessage,
				MsgType = messageType
			};
			RecMsgDateSource.Add(msg);
		}

		public static void AddRecevieGridControlItem(ReceiveMessage receviceMessage)
		{
			RecMsgDateSource.Add(receviceMessage);
		}
	}

	/// <summary>
	/// 服务状态名称ServerState转换成中文
	/// </summary>
	public class ServerStateName
	{
		public int StateCode { get; set; }
		public string StateName { get; set; }
	}


}
