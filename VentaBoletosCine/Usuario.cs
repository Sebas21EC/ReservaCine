using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    public class Usuario
    {
        public string nombreUsusario{ get; set;}
        public string contraseña { get; set; }
        public int permisos { get; set; }
        private MySqlDataReader reader;

        MySqlDataAdapter DA;
        Funcion funcion;
        int precio;
        private int index;

        public Usuario()
        {

        }

        /// <summary>
        /// Metood que agrega un nuevo registro a la base de datos.
        /// </summary>
        /// <param name="conexionBD"></param> Referencia de objeto con la conexxiona la base datos.
        /// <returns></returns> Devuelve verdadero si se ejecuto el query.
        public bool Registrar(DBConnection conexionBD)
        {
            string commandtxt = "INSERT INTO usuario (usuario, contrasena, permisos) VALUES ('" + nombreUsusario + "','" + contraseña + "'," + permisos + ")";
            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
            catch (Exception exception)
            {
                //reader.Close();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Metodo que elimina un registro de la base detos,
        /// </summary>
        /// <param name="conexionBD"></param>
        /// <param name="id"></param> Referencia de objeto con la conexxiona la base datos.
        /// <returns></returns>devuelve verdadero si se ejecuto el query.
        public bool Eliminar(DBConnection conexionBD, string id)
        {
            string commandtxt = "DELETE FROM usuario WHERE usuario='" + id + "'";
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
