using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    public class DBConnection
    {
        private DBConnection()
        {
        }

        /// <summary>
        /// databaseName: variable para nombre de base de datos
        /// </summary>
        private string databaseName = String.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }
        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        /// <summary>
        /// Conexxion a la base de datos.
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            bool result = true;
            if (Connection == null)
            {
                databaseName = "dbcine";
                connection = new MySqlConnection("Server = 127.0.0.1; Database = dbcine; Uid = root; Pwd = root;");
                connection.Open();
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Metodo que cierra la conexion.
        /// </summary>
        public void Close()
        {
            connection.Close();
        }
    }
}
