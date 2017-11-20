using DevExpress.Xpf.Core;
using GalaSoft.MvvmLight.Messaging;
using SuperSocket.ViewModel;

namespace SuperSocket.View
{
	/// <summary>
	/// Interaction logic for ConnectToServerView.xaml
	/// </summary>
	public partial class CreateClientView : DXWindow
    {
        public CreateClientView()
        {
			this.DataContext = new CreateClientViewModel();
            InitializeComponent();
			Messenger.Default.Register<object>(this, "CloseCreateClientView", CloseCreateClientView);
			this.Unloaded += (sender, e) => Messenger.Default.Unregister<object>(this, "CloseCreateClientView");
		}

		void CloseCreateClientView(object obj)
		{
			this.Close();
		}

		private void DXWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			txtServerAddress.Focus();
		}
	}
}
