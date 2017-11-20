using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocket.Model
{
	public class ClientInfo
	{
		/// <summary>
		/// 名称
		/// </summary>
		public string SessionID { get; set; }

		/// <summary>
		/// 地址
		/// </summary>
		public string IPAddress { get; set; }

		/// <summary>
		/// 连接时间
		/// </summary>
		public DateTime StartTime { get; set; }
	}
}
