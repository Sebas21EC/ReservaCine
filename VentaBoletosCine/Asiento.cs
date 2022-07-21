using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    class Asiento
    {

        /// <summary>
        /// Declaracion de variables y propiedades set y get.
        /// </summary>
        public int id_asiento { get; set; }
        public bool disponible { get; set; }
        public int num_sala { get; set;}
        public int id_funcion { get; set; }
        public int num_asiento { get; set; }



        /// <summary>
        /// Metodo para ocupar un asiento
        /// </summary>
        /// <param name="conexionBD"></param> Recibe la conexxion con la base de datos
        /// <returns></returns> Retorna verdadero si se ejecuto el query o falso si hubo un error.
        public bool Ocupar(DBConnection conexionBD)
        {
            string commandtxt = "UPDATE asiento SET disponibilidad=" + false + " WHERE id_asiento=" + id_asiento;
            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
            catch (Exception exception)
            {
                return false;
            }
            return true;
        }
    }
}
