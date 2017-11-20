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
using SuperSocket.View;

namespace DXApplication24
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : DXRibbonWindow
	{
		ServerView serverView;
		ClientView clientView;
		public MainWindow()
		{
			InitializeComponent();
			InitView();
		}

		private void InitView()
		{
			serverView = new ServerView()
			{
				Name = "服务端"
			};

			clientView = new ClientView()
			{
				Name = "客户端",

				Visibility = Visibility.Hidden
			};

			gridPanel.Children.Add(serverView);
			gridPanel.Children.Add(clientView);
		}
		private void serverItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (serverView.Visibility == Visibility.Hidden)
			{
				SetViewVisibility("服务端");
			}
		}

		private void clientItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (clientView.Visibility == Visibility.Hidden)
			{
				SetViewVisibility("客户端");
			}
		}

		private void SetViewVisibility(string controlName)
		{
			foreach (UserControl item in gridPanel.Children)
			{
				if (item.Name == controlName)
				{
					item.Visibility = Visibility.Visible;
				}
				else
				{
					item.Visibility = Visibility.Hidden;
				}
			}
		}
	}
}
