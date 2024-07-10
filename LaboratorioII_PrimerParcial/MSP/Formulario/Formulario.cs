using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using System.Data.SqlClient;

namespace Formulario
{
    public partial class Formulario : Form
    {
        private Manzana _manzana;
        private Banana _banana;
        private Durazno _durazno;

        public Cajon<Manzana> _cajonManzanas;
        public Cajon<Banana> _cajonBananas;
        public Cajon<Durazno> _cajonDuraznos;

        public Formulario()
        {
            InitializeComponent();
        }

        private void Formulario_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Apellido, Nombre");
        }

        // No realizar ningun cambio en este evento
        // pero debe de mostrar el mismo formato indicado
        private void btnPunto1_Click(object sender, EventArgs e)
        {
            _manzana = new Manzana("Verde", 2, "Río Negro");
            _banana = new Banana("Amarillo", 5, "Ecuador");
            _durazno = new Durazno("Rojo", 2, 53);

            MessageBox.Show(_manzana.ToString());
            MessageBox.Show(_banana.ToString());
            MessageBox.Show(_durazno.ToString());
        }

        // No realizar ningun cambio en este evento
        // pero debe de mostrar el mismo formato indicado
        private void btnPunto2_Click(object sender, EventArgs e)
        {
            _cajonManzanas = new Cajon<Manzana>(1.58, 3);
            _cajonBananas = new Cajon<Banana>(15.96, 4);
            _cajonDuraznos = new Cajon<Durazno>(21.5, 1);

            _cajonManzanas += new Manzana("Roja", 1, "Neuquén");
            _cajonManzanas += _manzana;
            _cajonManzanas += new Manzana("Amarilla", 3, "San Juan");

            _cajonBananas += new Banana("Verde", 3, "Brasil");
            _cajonBananas += _banana;

            _cajonDuraznos += _durazno;

            MessageBox.Show(_cajonManzanas.ToString());
            MessageBox.Show(_cajonBananas.ToString());
            MessageBox.Show(_cajonDuraznos.ToString());
        }

        // Se debe de serializar la clase Manzana, indicando un mensaje con lo sucedido
        // Se debe de deserializar la clase Manzana y mostrar el objeto obtenido
        // Se debe de serializar el cajon de Manzanas
        private void btnPunto3_Click(object sender, EventArgs e)
        {
            Fruta aux = null;

            // AGREGAR
            // Serealizacion implicita de manzana
            if ()
            {
                MessageBox.Show("Manzana serializada correctamente");
            }
            else
            {
                MessageBox.Show("Error al serializar el objeto Manzana");
            }

            if ()
            {
                
            }
            else
            {
                MessageBox.Show("Error en la deserialización el objeto Manzana");
            }

            // Serealizacion de cajon de manzanas
            if ()
            {
                MessageBox.Show("Cajón de manzanas serializada correctamente");
            }
            else
            {
                MessageBox.Show("Error en la serialización del cajón de manzanas");
            }
        }

        // Si se intenta agregar frutas al cajón y se supera la cantidad máxima,
        // se lanzará un CajonLlenoException, cuyo mensaje explicará lo sucedido.
        private void btnPunto4_Click(object sender, EventArgs e)
        {
            
        }

        //Si el precio total del cajon supera los 55 pesos, se disparará el evento EventoPrecio. 
        //caso contrario, mostrará la fecha (con hora, minutos y segundos) y el total del precio del cajón.
        private void btnPunto5_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.Now;
                _cajonBananas += new Banana("Verde", 2, "Argentina");
                _cajonBananas += new Banana("Amarilla", 4, "Ecuador");
                //_cajonBananas += new Banana("Amarilla", 2, "Ecuador");

                MessageBox.Show("Fecha: " + date.ToString("dd/MM/yyyy HH:mm:ss") + " - Precio total: $" + _cajonBananas.PrecioTotal.ToString());
            }
        }

        //Obtener de la base de datos (lab_msp) el listado de frutas
        //Implementar el método ObtenerListadoFrutas():string utilizando excepciones. 
        private void btnPunto6_Click(object sender, EventArgs e)
        {
            
        }

        //Agregar en la base de datos las frutas del formulario (_manzana, _banana y _durazno).
        //Implementar el método AgregarFrutas():bool utilizando excepciones. 
        //Indicar con un mensaje lo sucedo, si se pudo agregar datos o no.
        private void btnPunto7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
