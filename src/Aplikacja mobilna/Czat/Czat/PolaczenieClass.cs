using System.IO;
using Windows.Networking.Sockets;

namespace Czat
{
    static class PolaczenieClass
    {
        public static StreamSocket streamSocket = new StreamSocket();
        public static StreamWriter streamWriter = new StreamWriter(streamSocket.OutputStream.AsStreamForWrite());
        public static StreamReader streamReader = new StreamReader(streamSocket.InputStream.AsStreamForRead());
    }
}
