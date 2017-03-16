using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_ChatServer
{
    public class Client 
    {
        private Socket SocketClient;
        private MainWindow Parent;
        private Thread ThreadRequest;
        private bool Wait;

        public string IP
        {
            get { return SocketClient.LocalEndPoint.ToString(); }
        }

        public Client(Socket client,MainWindow parent)
        {
            SocketClient = client;
            Parent = parent;
            Wait = true;
            ThreadRequest = new Thread(() => WaitForRequest());
            ThreadRequest.Start();
        }

        private void WaitForRequest()
        {
            while (true)
            {
                if (!Wait) break;
                string request = SocketDataTransfer.Recive(ref SocketClient);
                if (!Wait) break;
                string[] command = request.Split('|');
                DoRequest(command);
            }
        }

        private void DoRequest(string[] command)
        {
            switch (command[0])
            {
                case "login":
                    Login(command[1], command[2]);
                    break;
                case "dc":
                    Disconnect();
                    break;
                default:
                    break;
            }
        }

        public void Disconnect()
        {
            Wait = false;
            SocketClient.Shutdown(SocketShutdown.Receive);
            SocketDataTransfer.Send(ref SocketClient, "dc");
            SocketClient.Shutdown(SocketShutdown.Send);
            SocketClient.Close();
            Parent.DisconnectClient(this);
        }

        private void Login(string log, string pass)
        {
            string msg;

            //znajdz w bazie czy dane logowania sa poprawne
            string login = "login";
            string haslo = "haslo";
            
            //odeslij czy dane sa poprawne
            bool czyDobre;

            if (log == login && pass == haslo)
                czyDobre = true;
            else
                czyDobre = false;

            //wyslanie informacji o poprawnosci danych
            if (czyDobre)
                msg = "polaczono";
            else
                msg = "niepolaczono";

            SocketDataTransfer.Send(ref SocketClient, msg);
        }
    }
}
