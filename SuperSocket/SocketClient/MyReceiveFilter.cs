using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocket.SocketClient
{
    public class MyReceiveFilter : FixedHeaderReceiveFilter<MyPackageInfo>
    {
        /// +-------+---+-------------------------------+
        /// |request| l |                               |
        /// | name  | e |    request body               |
        /// |  (2)  | n |                               |
        /// |       |(1)|                               |
        /// +-------+---+-------------------------------+
        public MyReceiveFilter()
        : base(3)
        {
            //回复格式：地址 功能码 字节数 数值……
            //01 03 02 00 01   地址:01 功能码:03 字节数:02 数据:00 01
        }
        protected override int GetBodyLengthFromHeader(IBufferStream bufferStream, int length)
        {
            ArraySegment<byte> buffers = bufferStream.Buffers[0];
            byte[] array = buffers.ToArray();
            int len = array[length - 1];
            return len;
        }
        public override MyPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            //第三个参数用0,1都可以
            byte[] header = bufferStream.Buffers[0].ToArray();
            byte[] bodyBuffer = bufferStream.Buffers[1].ToArray();
            byte[] allBuffer = bufferStream.Buffers[0].Array.CloneRange(0, (int)bufferStream.Length);
            return new MyPackageInfo(header, bodyBuffer, allBuffer);
        }
    }
}
