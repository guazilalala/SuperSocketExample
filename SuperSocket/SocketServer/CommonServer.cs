using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketServer;
using SuperSocket.ViewModel;

namespace SuperSocket
{
	public class CommonServer:AppServer<CommonSession>
	{
		protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
		{
			return base.Setup(rootConfig, config);
		}
		protected override void OnStarted()
		{
			base.OnStarted();
		}
		protected override void OnStopped()
		{
			base.OnStopped();
		}
		protected override void OnNewSessionConnected(CommonSession session)
		{
			ServerViewModel.RefreshServerInfoList();


			ServerViewModel.AddClientToClientList(new Model.ClientInfo
			{
				SessionID = session.SessionID,
				IPAddress = session.RemoteEndPoint.ToString(),
				StartTime = session.StartTime
			});
			base.OnNewSessionConnected(session);
		}
		protected override void OnSessionClosed(CommonSession session, CloseReason reason)
		{
			ServerViewModel.RemoveClientFromClientList(session.SessionID);
		}

	}
}
