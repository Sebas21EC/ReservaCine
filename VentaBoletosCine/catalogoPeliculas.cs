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
    /// Clase encargada de la captura de información de peliculas.
    /// </summary>
    public partial class Capturista : Form
    {

        /*  
         *      conexionBD: objeto conector con la Base de Datos.
         *      comando: objeto contenedor de comandos SQL.
         *      reader: objeto lector de Base de Datos SQL.
         *      DA: objeto adaptador de datos de SQL.
        */
        DBConnection conexionBD;
        String nombrePelicula;
        String categoriaPelicula;
        String duracionPelicula;    
        String creditosRepPelicula;
        String sinopsis;
        MySqlCommand comando;
        MySqlDataReader reader;
        MySqlDataAdapter DA;
        String cuentaDuracion="";

        private Pelicula pelicula;

        /// <summary>
        /// Constrcutor: constructor de objeto capturista el cual es el encargado de cargar las peliculas.
        /// </summary>
        /// <param name="conexion"></param> referencia de  objeto conector con la Base de Datos de SQL,
        public Capturista(DBConnection conexion)
        {
            
            conexionBD = conexion;
            InitializeComponent();
            dataGridView1.Visible = false;
            LlenarTablaAuxiliar();
            btGuardar.Enabled = false;
            btActualizar.Enabled = false;
        }

             /// <summary>
        /// Metodo encargado de llenar la tabla de registros para auxiliar en la navegación.
        /// </summary>
        private void LlenarTablaAuxiliar()
        {
            DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from pelicula";
            DA.SelectCommand = new MySqlCommand(sqlSelectAll, conexionBD.Connection);

            DataTable dataTable = new DataTable();
            BindingSource bS = new BindingSource();
            DA.Fill(dataTable);
            bS.DataSource = dataTable;
            dataGridView1.DataSource = bS;
        }

        /// <summary>
        /// metodo que sirve para configurar la longitud de los datos
        /// </summary>
        public void configuraTamTexBox()
        {
            tbNombre.MaxLength = 14;
            tbDuracion.MaxLength = 3;
            tbSipnosis.MaxLength = 100;
            tbcategoria.MaxLength = 16;
            tbCreditosRep.MaxLength = 50;
            
        }
        private void Capturista_Load(object sender, EventArgs e)
        {

            configuraTamTexBox();
            this.DoubleBuffered = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// evento del boton encargado de la eliminacion en la Base de Datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DataGrid dG = new DataGrid();
            dG.GetData((BindingSource)dataGridView1.DataSource);
            dG.ShowDialog();
            pelicula = new Pelicula();

            if (dG.id != -1)
            {
                if (pelicula.Eliminar(conexionBD, dG.id) == true)
                {
                    MessageBox.Show("Eliminación exitosa");
                    LlenarTablaAuxiliar();
                }
            }
        }


        /// <summary>
        ///  evento del boton encargado de la generación de un nuevo registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            pelicula = new Pelicula();
            limpiaRegistro();
            btGuardar.Enabled = true;
            btActualizar.Enabled = false;
        }

        /// <summary>
        ///  método que sirva para limpiar todos los textbox y el richtextbox
        /// </summary>
        public void limpiaRegistro()
        {
            tbNombre.Clear();
            tbcategoria.Clear();
            tbDuracion.Clear();
            tbSipnosis.Clear();
            tbCreditosRep.Clear();
        }

        /// <summary>
        /// evento del boton encargado del registro de datos en la Base de Datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if ((tbNombre.Text != "") &&
                (tbcategoria.Text != "") &&
                (tbDuracion.Text != "") &&
                (tbSipnosis.Text != "")&&
                (tbCreditosRep.Text !="")
             )
            {
                nombrePelicula = tbNombre.Text;
                categoriaPelicula = tbcategoria.Text;
                duracionPelicula = tbDuracion.Text;
                creditosRepPelicula = tbCreditosRep.Text;
                sinopsis =tbSipnosis.Text ;

                this.Refresh();
                pelicula = new Pelicula();

                pelicula.nombre = nombrePelicula;
                pelicula.genero = categoriaPelicula;
                pelicula.duracion = int.Parse(duracionPelicula);
                pelicula.reparto = creditosRepPelicula;
                pelicula.sinopsis = sinopsis;

                if (pelicula.Registrar(conexionBD) == true)
                {
                    MessageBox.Show("Registro exitoso");
                    LlenarTablaAuxiliar();
                }
                else
                {
                    MessageBox.Show("Error de registro");
                }
            }
            else
                MessageBox.Show("Ingrese todos los campos","No se puedo guardar",
                                MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Metodo que muestra los registros de la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            MySqlDataAdapter DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from pelicula";
            DA.SelectCommand = new MySqlCommand(sqlSelectAll, conexionBD.Connection);

            DataTable table = new DataTable();
            DA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;

            DataGrid DG = new DataGrid();
            DG.ShowData(bSource);
            DG.Show();

        }


        /// <summary>
        /// evento del boton encargado de mostrar los registros que actualmente se encuentran en la Base de Datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            MySqlDataAdapter DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from pelicula";
            DA.SelectCommand = new MySqlCommand(sqlSelectAll, conexionBD.Connection);

            DataTable table = new DataTable();
            DA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;

            DataGrid DG = new DataGrid();
            DG.ShowData(bSource);
            DG.Show();
        }


        /// <summary>
        /// evento al presionar el textbox, es el encargado de hacer validación de caracteres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDuracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Back)))
            {
                MessageBox.Show("Solo esta permitido ingresar números");
                e.Handled = true;
                return;            
            }
            cuentaDuracion = tbDuracion.Text;
        }


       
        /// <summary>
        /// evento del boton encargado de llamar la pantalla de busqueda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click_1(object sender, EventArgs e)
        {
            Buscador busca;
            busca = new Buscador(conexionBD, "miembro");
            busca.LlenarComboBox((BindingSource)dataGridView1.DataSource);
            busca.ShowDialog();
            if (busca.id != -1)
            {
                LlenaCampos(busca.id, busca.busqueda);
                btActualizar.Enabled = true;
                btGuardar.Enabled = false;
            }
            else
                MessageBox.Show("Registro no encontrado");
        }


        /// <summary>
        /// método encargado de rellenar los textBox con los valores recuperados de busqueda.
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="busqueda"></param>
        private void LlenaCampos(int opcion, string busqueda)
        {
            pelicula = new Pelicula();
            pelicula.Recuperar(conexionBD, opcion, busqueda);

            tbNombre.Text = pelicula.nombre;
            tbDuracion.Text = pelicula.duracion.ToString();
            tbCreditosRep.Text = pelicula.reparto;
            tbSipnosis.Text = pelicula.sinopsis;
            tbcategoria.Text = pelicula.genero;

        }

        
        /// <summary>
        /// evento del botpon encargado de actualizar un registro de la Base de Datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click_1(object sender, EventArgs e)
        {
            if (pelicula.id_pelicula != -1)
            {
                pelicula.nombre = tbNombre.Text;
                pelicula.genero = tbcategoria.Text;
                pelicula.duracion = int.Parse(tbDuracion.Text);
                pelicula.sinopsis = tbSipnosis.Text;
                pelicula.reparto = tbCreditosRep.Text;

                if (pelicula.Actualizar(conexionBD) == true)
                {
                    MessageBox.Show("Actualización exitosa");
                }
            }
        }
    }
}
