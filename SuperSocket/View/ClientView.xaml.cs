using GalaSoft.MvvmLight.Messaging;
using SuperSocket.ViewModel;
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

namespace SuperSocket.View
{
	/// <summary>
	/// Interaction logic for ClientView.xaml
	/// </summary>
	public partial class ClientView : UserControl
	{
		public ClientView()
		{
			this.DataContext = new ClientViewModel();

			InitializeComponent();

			Messenger.Default.Register<object>(this, "ShowCreateClientView",ShowCreateClientView);

			this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
		}

		void ShowCreateClientView(object obj)
		{
			new CreateClientView().Show();
		}
	}
}
