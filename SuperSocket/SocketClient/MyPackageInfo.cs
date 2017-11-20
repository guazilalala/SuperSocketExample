using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocket.SocketClient
{
    public class MyPackageInfo : IPackageInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header">头部</param>
        /// <param name="bodyBuffer">数据</param>
        /// <param name="allBuffer">整个数据包</param>
        public MyPackageInfo(byte[] header, byte[] bodyBuffer,byte[] allBuffer)
        {
            Header = header;
            Data = bodyBuffer;
            AllData = allBuffer;
        }
        /// <summary>
        /// 服务器返回的字节数据头部
        /// </summary>
        public byte[] Header { get; set; }
        public string HexHeader
        {
            get
            {
                return DataHelper.ByteToHex(Header, Header.Length);
            }
        }
        /// <summary>
        /// 服务器返回的字节数据
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// 服务器返回的整个数据包
        /// </summary>
        public byte[] AllData { get; set; }
        /// <summary>
        /// 服务器返回的数据长度
        /// </summary>
        public int DataLength
        {
            get
            {
                if(Data != null)
                {
                    return Data.Length;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 服务器返回的整个数据包长度
        /// </summary>
        public int AllLength
        {
            get
            {
                if (Header != null && Data != null)
                {
                    return Header.Length + Data.Length;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 服务器返回的字符串数据
        /// </summary>
        public string Body
        {
            get
            { 
                return Encoding.Default.GetString(Data);
            }
        }
        /// <summary>
        /// 服务器返回的16进制数据
        /// </summary>
        public string HexData
        {
            get
            {
                return DataHelper.ByteToHex(Data, DataLength);
            }
        }
        /// <summary>
        /// 服务器返回的整个数据包(16进制)
        /// </summary>
        public string HexAllData
        {
            get
            {
                return DataHelper.ByteToHex(AllData, AllLength);
            }
        }
    }
}
