using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_ChatServer
{
    public class Client :IDisposable
    {
        public string IP
        {
            get { return SocketClient.LocalEndPoint.ToString(); }
        }
        private Socket SocketClient;
        private MainWindow Parent;
        private Thread ThreadRequest;
        private bool Wait;
        private DatabaseManager dbManager;
        private int Index;


        public Client(Socket client, MainWindow parent)
        {
            dbManager = new DatabaseManager();
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
                    Logowanie(command[1], command[2]);
                    break;
                case "dc":
                    Wyloguj();
                    break;
                case "reg":
                    RejestracjaNowegoUzytkownika(command[1], command[2], command[3]);
                    break;
                case "check":
                    SprawdzCzyIstnieje(command[1], command[2]);
                    break;
                case "list":
                    WczytajListe();
                    break;
                case "":
                    break;
                default:
                    break;
            }
        }

        private void Logowanie(string log, string pass)
        {
            string msg;

            bool czyZalogowany = dbManager.CzyZalogowany(log, pass);

            if (czyZalogowany)
            {
                msg = "log|null";
                SocketDataTransfer.Send(ref SocketClient, msg);
                return;
            }

            //znajdz w bazie czy dane logowania sa poprawne
            int id = dbManager.Logowanie(log, pass);

            //wyslanie informacji o poprawnosci danych
            if (id == 0)
                msg = "log|" + bool.FalseString;
            else
            {
                Index = id;
                msg = "log|" + bool.TrueString + "|" + id.ToString();
            }

            SocketDataTransfer.Send(ref SocketClient, msg);
        }

        public void Wyloguj()
        {
            Wait = false;
            SocketClient.Shutdown(SocketShutdown.Receive);
            SocketDataTransfer.Send(ref SocketClient, "dc");
            dbManager.Wylogowanie(Index);
            SocketClient.Shutdown(SocketShutdown.Send);
            SocketClient.Close();
            Parent.DisconnectClient(this);
        }

        private void WczytajListe()
        {
            DataTable b = dbManager.WczytajKsiazke(Index);

            string msg = "list";

            foreach (DataRow item in b.Rows)
            {
                int id = (int)item[0];
                string nazwa = (string)item[1];
                msg += "|" + id.ToString() + ":" + nazwa;
            }

            SocketDataTransfer.Send(ref SocketClient, msg);
        }

        private void RejestracjaNowegoUzytkownika(string v1, string v2, string v3)
        {
            string  a = dbManager.DodajUzytkownika(v1, v2, v3);

            SocketDataTransfer.Send(ref SocketClient, a);

        }

        private void SprawdzCzyIstnieje(string v1, string v2)
        {
            bool a;
            if (v1 == "login")
                a = dbManager.SprawdzCzyIstniejeLogin(v2);
            else
                a = dbManager.SprawdzCzyIstniejeMail(v2);



            SocketDataTransfer.Send(ref SocketClient, a.ToString());
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (dbManager != null)
                        dbManager.Dispose();
                    if (SocketClient != null)
                    {
                        //rozlacz jezeli trzeba
                        //dispose
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Client() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
