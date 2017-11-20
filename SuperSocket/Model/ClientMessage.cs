using SuperSocket.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocket.Model
{
   public class ClientMessage
    {
		public DateTime MsgTime { get; set; }

		public MessageType MsgType { get; set; }

		public string MsgContent { get; set; }
	}



}
