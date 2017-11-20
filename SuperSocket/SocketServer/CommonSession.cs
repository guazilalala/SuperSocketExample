using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase.Protocol;


namespace SuperSocket.SocketServer
{
	public class CommonSession:AppSession<CommonSession>
	{


		protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
		{
			this.Send("Unknow request");
		}

		protected override void HandleException(Exception e)
		{
			this.Send("Application error: {0}", e.Message);
		}

		protected override void OnSessionStarted()
		{
			this.Send("Welcome to SuperSocket Server");
		}
		protected override void OnSessionClosed(CloseReason reason)
		{
			this.Send("Close");
		}


	}
}
