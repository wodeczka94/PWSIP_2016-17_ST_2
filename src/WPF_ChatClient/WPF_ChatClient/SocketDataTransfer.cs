using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ChatClient
{
    public static class SocketDataTransfer
    {
        public static string Recive()
        {
            byte[] buffer = new byte[1024];
            int length = App.server.Receive(buffer);
            string message = Encoding.UTF8.GetString(buffer);
            message = message.Remove(length);
            return message;
        }

        public static void Send(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            App.server.Send(buffer);
        }
    }
}
