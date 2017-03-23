using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_ChatServer
{
    public class DatabaseManager :IDisposable
    {

        //private readonly string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"baza_danych.mdf") + ";Integrated Security = True";
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dawid\Documents\GitHub\PWSIP_2016-17_ST_2\src\WPF_ChatServer\WPF_ChatServer\baza_danych.mdf;Integrated Security = True";

        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;


        public DatabaseManager()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand()
            {
                Connection = sqlConnection,
                CommandType = CommandType.Text
            };
        }

        public DataTable WczytajKsiazke(int idKlient)
        {
            DataTable table = new DataTable();

            sqlConnection.Open();

            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@id", idKlient);
            sqlCommand.CommandText = "select id_friend, nick " +
                "from ksiazka_adresowa k, [user] u " +
                "where id_owner = @id " +
                "and k.id_friend = u.iduser;";

            using (SqlDataAdapter da = new SqlDataAdapter(sqlCommand))
                da.Fill(table);

            sqlConnection.Close();

            return table;
        }

        public string DodajUzytkownika(string nick, string haslo, string mail)
        {
            sqlConnection.Open();

            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@nick", nick);
                sqlCommand.Parameters.AddWithValue("@mail", mail);

                sqlCommand.CommandText = "select [nick] " +
                    "from [user] " +
                    "where [nick] = @nick " +
                    "and [mail] = @mail";
                string ob = (string)sqlCommand.ExecuteScalar();

                if (ob != null)
                {
                    sqlConnection.Close();
                    return "Istnieje uzytkownik o podanych danych";
                }

                sqlCommand.CommandText = "insert into [profil] (miasto, plec, wiek, opis) values ('','','',''); " +
                    "select SCOPE_IDENTITY();";
                var index = Convert.ToInt32(sqlCommand.ExecuteScalar());
                sqlCommand.Parameters.AddWithValue("@id", index);
                sqlCommand.Parameters.AddWithValue("@hash", haslo);

                sqlCommand.CommandText = "insert into [user] (nick, mail, hash, id_profilu)" +
                    "values (@nick, @mail, @hash, @id)";
                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return "Pomyslnie utworzono uzytkowika";

            }
            catch (Exception e)
            {
                sqlConnection.Close();

                MessageBox.Show(e.Message);

                return "Wystapil blad podczas tworzenia uzytkownika";
            }
        }

        public int Logowanie(string nick, string haslo)
        {
            sqlConnection.Open();

            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@nick", nick);
                sqlCommand.Parameters.AddWithValue("@haslo", haslo);

                sqlCommand.CommandText = "select [iduser] " +
                    "from [user] " +
                    "where [nick] = @nick " +
                    "and [hash] = @haslo;";

                var ob = sqlCommand.ExecuteScalar();

                if (ob != null)
                {
                    sqlCommand.CommandText = "Update [user] " +
                        "set [connected] = 1 " +
                        "where [nick] = @nick " +
                        "and [hash] = @haslo";

                    sqlCommand.ExecuteNonQuery();
                }

                sqlConnection.Close();

                return ob == null ? 0 : (int)ob;
            }
            catch (Exception e)
            {
                sqlConnection.Close();

                MessageBox.Show(e.Message);

                return 0;
            }
        }

        public void Wylogowanie(int id)
        {
            sqlConnection.Open();

            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@id", id);

                sqlCommand.CommandText = "Update [user] " +
                         "set [connected] = 0 " +
                         "where [iduser] = @id;";

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
            catch (Exception e)
            {
                sqlConnection.Close();

                MessageBox.Show(e.Message);
            }
        }

        public bool CzyZalogowany(string nick, string haslo)
        {

            sqlConnection.Open();

            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@nick", nick);
                sqlCommand.Parameters.AddWithValue("@haslo", haslo);

                sqlCommand.CommandText = "select [connected] " +
                    "from [user] " +
                    "where [nick] = @nick " +
                    "and [hash] = @haslo;";

                var ob = (int)sqlCommand.ExecuteScalar();


                sqlConnection.Close();

                if (ob == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                sqlConnection.Close();

                MessageBox.Show(e.Message);

                return false;
            }
        }

        public bool SprawdzCzyIstniejeLogin(string login)
        {
            sqlConnection.Open();

            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@nick", login);

                sqlCommand.CommandText = "select [nick] " +
                    "from [user] " +
                    "where [nick] = @nick;";

                string ob = (string)sqlCommand.ExecuteScalar();

                sqlConnection.Close();

                if (ob != null)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                sqlConnection.Close();

                MessageBox.Show(e.Message);

                return true;
            }
        }

        public bool SprawdzCzyIstniejeMail(string mail)
        {
            sqlConnection.Open();

            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@mail", mail);

                sqlCommand.CommandText = "select [mail] " +
                    "from [user] " +
                    "where [mail] = @mail;";

                string ob = (string)sqlCommand.ExecuteScalar();

                sqlConnection.Close();

                if (ob != null)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                sqlConnection.Close();

                MessageBox.Show(e.Message);

                return true;
            }
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
                    if (sqlCommand != null)
                        sqlCommand.Dispose();
                    if (sqlConnection != null)
                    {
                        if (sqlConnection.State == ConnectionState.Open)
                            sqlConnection.Close();
                        sqlConnection.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DatabaseManager() {
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
