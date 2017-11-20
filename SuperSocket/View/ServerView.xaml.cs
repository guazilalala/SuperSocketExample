using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Ribbon;
using SuperSocket.ViewModel;
using System.Collections.ObjectModel;
using SuperSocket.Model;
using SuperSocket.SocketBase;

namespace SuperSocket.View
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class ServerView : UserControl
	{
		public ServerView()
		{
			DevExpress.Xpf.Core.DXGridDataController.DisableThreadingProblemsDetection = true;
			InitializeComponent();
			this.DataContext = new ServerViewModel();

			lueServerStateName.ItemsSource = new List<ServerStateName>
			{
				new ServerStateName{ StateCode = (int)ServerState.Initializing,StateName = "初始化"},
				new ServerStateName{ StateCode = (int)ServerState.NotInitialized,StateName = "未初始化"},
				new ServerStateName{ StateCode = (int)ServerState.NotStarted,StateName = "未启动"},
				new ServerStateName{ StateCode = (int)ServerState.Running,StateName = "运行中 "},
				new ServerStateName{ StateCode = (int)ServerState.Starting,StateName = "启动中"},
				new ServerStateName{ StateCode = (int)ServerState.Stopping,StateName = "停止中"},

			};

		}
	}
}
