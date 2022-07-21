using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    
    /// <summary>
    /// Esta clase es utilizada para buscar registros utilizando las columnas de la tabla.
    /// </summary>
    public partial class Buscador : Form
    {
        public int id;
        private DBConnection conexionBD;
        private string tabla;
        public string busqueda;
        private DataTable dT;


        /// <summary>
        /// Constructor: 
        /// </summary>
        /// <param name="conexionBD"></param> objeto que crea la conexión con la Base de Datos.
        /// <param name="table"></param> string que contiene el nombre de la tabla en la que se raliza la busqueda.
        public Buscador(DBConnection conexionBD, string table)
        {
            tabla = table;
            id = -1;
            this.conexionBD = conexionBD;
            InitializeComponent();
        }

        /// <summary>
        /// Metodo para llenar el combobox, este se encarga de llenar el comboBox de la ventana con las columnas de la tabla.
        /// </summary>
        /// <param name="bSource"></param> contiene la fuente de informacion de la tabla.
        public void LlenarComboBox(BindingSource bSource)
        {
            dataGridView1.DataSource = bSource;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                comboBox1.Items.Add(dataGridView1.Columns[i].Name);
            }
        }

        /// <summary>
        /// Evento del boton 1  el cual se encarga de terminar la busqueda y regresar los datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            id = comboBox1.SelectedIndex;
            busqueda = textBox1.Text;

            this.Close();
        }


        private void Buscador_Load(object sender, EventArgs e)
        {

        }
    }
}
