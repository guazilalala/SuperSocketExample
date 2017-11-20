using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocket.Model
{
	public class ReceiveMessage
	{
		/// <summary>
		/// 时间
		/// </summary>
		public DateTime MsgTime { get; set; }

		/// <summary>
		/// 服务名称
		/// </summary>
		public string ServerName { get; set; }

		/// <summary>
		/// 会话名称
		/// </summary>
		public string SessionName { get; set; }

		/// <summary>
		/// 客户地址
		/// </summary>
		public string RemoteAddress { get; set; }

		/// <summary>
		/// 消息内容
		/// </summary>
		public string MsgContent { get; set; }

		/// <summary>
		/// 消息类别
		/// </summary>
		public string MsgType { get; set; }
	}
}
