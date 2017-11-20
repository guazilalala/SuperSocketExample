using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.ViewModel;
using SuperSocket.Model;

namespace SuperSocket.SocketServer
{
    public class ECHO1 : CommandBase<CommonSession, StringRequestInfo>
    {
        public override void ExecuteCommand(CommonSession session, StringRequestInfo requestInfo)
        {


			ServerViewModel.AddRecevieGridControlItem(new ReceiveMessage
			{
				MsgTime = DateTime.Now,
				ServerName = session.AppServer.Name,
				SessionName = session.GetType().ToString(),
				RemoteAddress = session.RemoteEndPoint.ToString(),
				MsgContent = requestInfo.Body,
				MsgType = this.Name
			});
			session.Send(requestInfo.Body);
        }
    }
}
