using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    class Venta
    {
        public int id_venta { get; set; }
        public string usuario { get; set; }
        public int id_funcion { get; set; }
        public string precio { get; set; }
        private MySqlDataReader reader;

        public Venta()
        {

        }

        /// <summary>
        /// Metood que inserta un nuevo registro en la base de datps.
        /// </summary>
        /// <param name="conexionBD"></param>
        /// <returns></returns>
        public bool Registrar(DBConnection conexionBD)
        {
            string commandtxt = "INSERT INTO venta (usuario_venta, id_funcion, precio) VALUES ('" + usuario + "'," + id_funcion + "," + precio + ")";
            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
            catch (Exception exception)
            {
                reader.Close();
                return false;
            }
            return true;
        }

    }
}
