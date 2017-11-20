using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocket.Model
{
	public class ServerInfo
	{
		public string Name { get; set; }

		public string IPAddress { get; set; }
		public string ListenerPort { get; set; }

		public int State { get; set; }

		public int Connections { get; set; }
		public string Description { get; set; }
	}
}
