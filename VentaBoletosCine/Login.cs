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

    public partial class Login : Form
    {
        DBConnection conexionBD;
        MySqlCommand comando;
        MySqlDataReader reader;
        String query;

        public Login()
        {
            InitializeComponent();
            IniciarConexion();
        }


        /// <summary>
        /// metodo que  hace la conexion con la base de datos he informa al usuario si se logro la conexión
        /// </summary>
        private void IniciarConexion()
        {
            conexionBD = DBConnection.Instance();
            comando = new MySqlCommand();

            if (conexionBD.IsConnected())
            {
                MessageBox.Show("Conexion lograda","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Conexion fallida", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }


         /// <summary>
         /// método sirve para la autentificación y el acceso de usuarios mediante la base de datos haciendo
         /// conexión en la tabla miembros en la cual se ubican los los campos de nombre de usuario y contraseña,
         /// también valida que el nombre de usuario y contraseña esten correctos.
         /// </summary>
        public void login()
        {
            if ((textBox1.Text != "") &&
                 (textBox2.Text != ""))
            {
                string usuario = textBox1.Text.ToString();
                string contrasena = textBox2.Text.ToString();
                query = "SELECT * FROM Usuario WHERE usuario = '" + usuario + "'";
                comando = new MySqlCommand(query, conexionBD.Connection);

                try
                {
                    reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((reader.GetString("contrasena")).Equals(contrasena))
                        {
                            Usuario user = new Usuario();
                            user.nombreUsusario = usuario;
                            user.contraseña = reader.GetString("contrasena");
                            user.permisos = reader.GetInt32("permisos");
                            MessageBox.Show("Has accesado al sistema", "Bienvenido(a)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Menu ventana = new Menu(conexionBD, user);
                            this.Hide();
                            reader.Close();
                            ventana.ShowDialog(this);
                            this.Show();
                            this.Focus();
                        }
                        else
                        {
                            reader.Close();
                            MessageBox.Show("Contraseña incorrecta");
                        }
                    }
                    else {
                        reader.Close();
                        MessageBox.Show("Contraseña/usuario incorrecta");
                    }
                }
                catch (Exception exception)
                {
                    reader.Close();
                    MessageBox.Show(exception.Message);
                    Close();
                }
            }
            else
                MessageBox.Show("Ingresar nombre y contraseña",
                                "Error de autentificación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login();

        }

        
        private void Login_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                login();
            }
            if ((int)e.KeyChar == (int)Keys.Escape)
            {
                this.Close();
            }
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Escape)
            {
                this.Close();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Escape)
            {
                this.Close();
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
