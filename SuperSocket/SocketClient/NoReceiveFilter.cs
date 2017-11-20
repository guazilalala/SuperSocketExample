using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocket.SocketClient
{
    public class NoReceiveFilter : IReceiveFilter<MyPackageInfo>
    {
        public IReceiveFilter<MyPackageInfo> NextReceiveFilter { get; set; }

        public FilterState State { get; set; }

        public MyPackageInfo Filter(BufferList data, out int rest)
        {
            rest = 0;
            return new MyPackageInfo(null, null, data[0].ToArray());
        }

        public void Reset()
        {
            
        }
    }
}
