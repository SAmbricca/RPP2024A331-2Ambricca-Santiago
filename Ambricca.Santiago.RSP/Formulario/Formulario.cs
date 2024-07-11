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
        //Declaracion de variables para instanciar frutas y cajones
        private Manzana _manzana;
        private Banana _banana;
        private Durazno _durazno;

        public Cajon<Manzana> _cajonManzanas;
        public Cajon<Banana> _cajonBananas;
        public Cajon<Durazno> _cajonDuraznos;

        protected static SqlConnection conexion; //Designo conexion para conectar con la base de datos SQL
        protected static SqlCommand sqlCom; //Idem sqlCom para ejecutar comandos SQL

        //Constructor del formulario. Inicializa sus componentes.
        public Formulario()
        {
            InitializeComponent();
        }

        //Evento de carga de formulario. Muestra un mensaje con mi apellido y nombre.
        private void Formulario_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Ambricca, Santiago");
        }

        //Boton Punto 1: Crea instancias de frutas, las inicializa y muestra sus detalles
        private void btnPunto1_Click(object sender, EventArgs e)
        {
            _manzana = new Manzana("Verde", 2, "Río Negro");
            _banana = new Banana("Amarillo", 5, "Ecuador");
            _durazno = new Durazno("Rojo", 2, 53);

            MessageBox.Show(_manzana.ToString());
            MessageBox.Show(_banana.ToString());
            MessageBox.Show(_durazno.ToString());
        }

        //Boton Punto 2: Crea cajones de frutas, añade frutas a los cajones y muestra detalles de los cajones
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

        //Boton Punto 3: Serializa y deserializa frutas y cajones de frutas
        private void btnPunto3_Click(object sender, EventArgs e)
        {
            Fruta aux = null;

            //Serialización de una Manzana a XML
            if (_manzana.Xml("manzana.xml"))
            {
                MessageBox.Show("Manzana serializada correctamente");
            }
            else
            {
                MessageBox.Show("Error al serializar el objeto Manzana");
            }

            //Deserialización de una Manzana desde XML
            if (((IDeserializar)_manzana).Xml("manzana.xml", out aux))
            {
                MessageBox.Show(((Manzana)aux).ToString());
            }
            else
            {
                MessageBox.Show("Error en la deserialización el objeto Manzana");
            }

            //Serealizacion de cajon de manzanas

            if (_cajonManzanas.Xml("cajonManzanas.xml"))
            {
                MessageBox.Show("Cajón de manzanas serializada correctamente");
            }
            else
            {
                MessageBox.Show("Error en la serialización del cajón de manzanas");
            }
        }

        //Boton Punto 4: Intenta agregar un Durazno al cajon de duraznos (que se encontrará lleno) y maneja la excepcion CajonLlenoException
        private void btnPunto4_Click(object sender, EventArgs e)
        {
            try

            {
                _cajonDuraznos += _durazno;
            }

            catch (CajonLlenoException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        //Boton Punto 5: Intenta agregar Bananas al cajon de Bananas, mostrando la fecha + hora actual y el precio total del cajon de bananas. Maneja la excepcion CajonLlenoException
        private void btnPunto5_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.Now;
                _cajonBananas += new Banana("Verde", 2, "Argentina");
                _cajonBananas += new Banana("Amarilla", 4, "Ecuador");

                MessageBox.Show("Fecha: " + date.ToString("dd/MM/yyyy HH:mm:ss") + $" - Precio total: ${_cajonBananas.PrecioTotal.ToString()}");
            }
            catch (CajonLlenoException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Botón Punto 6: Obtiene un listado de frutas desde la base de datos y lo muestra
        //Utiliza el metodo ObtenerListadoFrutas()
        private void btnPunto6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ObtenerListadoFrutas());
        }

        //Boton Punto 7: Intenta agregar frutas a la base de datos y muestra un mensaje de exito o fracaso
        //Utiliza el metodo AgregarFrutas()
        private void btnPunto7_Click(object sender, EventArgs e)
        {
            if (AgregarFrutas(this))
            {
                MessageBox.Show("Se agregaron las frutas a la Base de Datos");
            }
            else
            {
                MessageBox.Show("No se agregaron las frutas a la Base de Datos");
            }
        }

        //Metodo estático para obtener un listado de frutas desde la base de datos
        private static string ObtenerListadoFrutas()
        {
            StringBuilder sb = new StringBuilder();
            conexion = new SqlConnection(@"Data Source=DESKTOP-QLG6O2R;Initial Catalog=lab_rsp;Integrated Security=True");

            try
            {
                sqlCom = new SqlCommand();
                sqlCom.Parameters.Clear();
                sqlCom.Connection = conexion;
                sqlCom.CommandType = System.Data.CommandType.Text;
                sqlCom.CommandText = "SELECT * FROM frutas"; //Obtiene todas las frutas desde una consulta a SQL

                conexion.Open();

                //Ejecuta la consulta y lee los resultados
                using (SqlDataReader sqlDataReader = sqlCom.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        //Cadena con los detalles de cada fruta obtenida en la consulta
                        sb.AppendFormat("Id: {0} - Nombre: {1} - Peso: {2}kg - Precio ${3}\n",
                        sqlDataReader["id"].ToString(),
                        sqlDataReader["nombre"].ToString(),
                        sqlDataReader["peso"].ToString(),
                        sqlDataReader["precio"].ToString());

                    }

                    sqlDataReader.Close();
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return sb.ToString();
        }

        //Metodo estático para agregar frutas a la base de datos
        private static bool AgregarFrutas(Formulario esteFormulario)
        {
            bool retorno = false;
            Random rand = new Random();

            //Acción para insertar una fruta (con precio aleatorio) en la base de datos
            Action<string, double> cargarBD = (nombre, peso) =>
            {
                sqlCom.CommandText = $"INSERT INTO frutas (nombre, peso, precio) VALUES (@nombre, @peso, @precio)";
                sqlCom.Parameters.AddWithValue("@nombre", nombre);
                sqlCom.Parameters.AddWithValue("@peso", peso);
                sqlCom.Parameters.AddWithValue("@precio", (float)rand.Next(1, 100));

                //Ejecuta la insercion en la base de datos y limpia los parámetros para el siguiente uso
                sqlCom.ExecuteNonQuery();
                sqlCom.Parameters.Clear();
            };

            try
            {
                conexion.Open();
                sqlCom.Connection = conexion;

                sqlCom.Parameters.Clear();

                //Recorre y agrega las frutas del formulario a la base de datos con la accion cargarBD. 
                //Comentario adicional: Seguro haya una forma de unificar los 3 'foreach', generando una lista con los cajones, pero no me salió. Por ahora lo dejo así
                foreach (Banana fruta in esteFormulario._cajonBananas.Elementos)
                {
                    cargarBD(fruta.Nombre, fruta.Peso);
                }
                foreach (Manzana fruta in esteFormulario._cajonManzanas.Elementos)
                {
                    cargarBD(fruta.Nombre, fruta.Peso);
                }
                foreach (Durazno fruta in esteFormulario._cajonDuraznos.Elementos)
                {
                    cargarBD(fruta.Nombre, fruta.Peso);
                }

                retorno = true;
            }
            catch (Exception)
            {
                retorno = false;
                throw;
            }
            finally
            {
                conexion.Close();
            }

            return retorno;
        }
    }
}
