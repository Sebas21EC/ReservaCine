using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VentaBoletosCine
{
    public partial class boleto : Form
    {
        DBConnection conexionBD;
        List<Button> listaAsientos;
        public List<Label> listaColoresAsientos;
        public int boletosTotal;
        public int contaBoletos = 0;
        int numAsiento = 10;
        public delegate void delegadoPasaDato(int valor);
        public event delegadoPasaDato eventoPasaNumBoleto;

        /// <summary>
        /// Constructor; incializa las variables.
        /// </summary>
        /// <param name="conexion"></param> conexxion a la base de datos.
        /// <param name="lista"></param> lista con labels del form
        /// <param name="cantBoletos"></param> cantidad de boletos que se van a vender.
        public boleto(DBConnection conexion, List<Label> lista, int cantBoletos)
        {
            conexion = conexionBD;
            listaAsientos = new List<Button>(lista.Count);
            listaColoresAsientos = lista;
            boletosTotal = cantBoletos;
            InitializeComponent();
        }

        /// <summary>
        /// Metodo que revisa para cambiarlos de color.
        /// </summary>
        public void revisa()
        {
            for (int cont = 0; cont < 32; cont++)
            {
                if (listaColoresAsientos[cont].BackColor == listaAsientos[cont].BackColor)
                {
                    listaColoresAsientos[cont].BackColor = listaAsientos[cont].BackColor;
                    //MessageBox.Show("Ya esta ocupado");
                }
                else
                {
                    listaAsientos[cont].BackColor = Color.Blue;
                }
            }
        }

        /// <summary>
        /// Metodo que llena la lista de asientos copn los botones.
        /// </summary>
        public void llenaListaBotones()
        {
            listaAsientos.Add(bt1);
            listaAsientos.Add(bt2);
            listaAsientos.Add(bt3);
            listaAsientos.Add(bt4);
            listaAsientos.Add(bt5);
            listaAsientos.Add(bt6);
            listaAsientos.Add(bt7);
            listaAsientos.Add(bt8);
            listaAsientos.Add(bt9);
            listaAsientos.Add(bt10);

            listaAsientos.Add(bt11);
            listaAsientos.Add(bt12);
            listaAsientos.Add(bt13);
            listaAsientos.Add(bt14);
            listaAsientos.Add(bt15);
            listaAsientos.Add(bt16);
            listaAsientos.Add(bt17);
            listaAsientos.Add(bt18);
            listaAsientos.Add(bt19);
            listaAsientos.Add(bt20);

            listaAsientos.Add(bt21);
            listaAsientos.Add(bt22);
            listaAsientos.Add(bt23);
            listaAsientos.Add(bt24);
            listaAsientos.Add(bt25);
            listaAsientos.Add(bt26);
            listaAsientos.Add(bt27);
            listaAsientos.Add(bt28);
            listaAsientos.Add(bt29);
            listaAsientos.Add(bt30);

            listaAsientos.Add(bt31);
            listaAsientos.Add(bt32);

            for (int i = 0; i < listaAsientos.Count; i++)
            {
                listaAsientos[i].BackColor = listaColoresAsientos[i].BackColor;
                if (listaAsientos[i].BackColor == Color.Red)
                    listaAsientos[i].Enabled = false;
            }
        }

        /// <summary>
        /// Evento al cargar el Formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void boleto_Load(object sender, EventArgs e)
        {

            llenaListaBotones();
            revisa();

            //this.DoubleBuffered = true;
        }

        /// <summary>
        /// Evento ala acepclick en botón aceptar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            revisa();
            checaSeleecionAsiento();
            this.Close();


        }

        /// <summary>
        /// Metodo que checa los asientos seleccionaddos.
        /// </summary>
        public void checaSeleecionAsiento()
        {
            if (contaBoletos <= boletosTotal)
            {
                foreach (var listaAux in listaAsientos)
                {
                    if (listaAux.BackColor == Color.Green)
                    {

                        numAsiento = listaAsientos.IndexOf(listaAux);
                        eventoPasaNumBoleto(numAsiento);
                        //this.Close();
                    }

                }
            }

        }

        /// <summary>
        /// Evento usado oara los botones los cuales, cambiaran de color al hacer click y encuaran el nuemro del asiento.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.BackColor == Color.Blue)
            {
                if (contaBoletos <= boletosTotal - 1)
                {
                    contaBoletos++;
                    b.BackColor = Color.Green;
                    numAsiento = listaAsientos.IndexOf(b);
                    eventoPasaNumBoleto(Convert.ToInt32(numAsiento));
                }
            }
            else if (b.BackColor == Color.Green)
            {
                contaBoletos--;
                b.BackColor = Color.Blue;
                numAsiento = listaAsientos.IndexOf(b);
                eventoPasaNumBoleto(numAsiento);
            }
        }



    }
}
