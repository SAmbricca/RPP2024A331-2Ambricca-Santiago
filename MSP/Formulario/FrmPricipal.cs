using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Entidades;
using Archivo;
using System.Threading;

namespace Formulario
{
    /// <summary>
    /// Formulario principal para la gestión de patentes.
    /// </summary>
    public partial class FrmPricipal : Form
    {
        List<Patente> patentes;
        List<Thread> hilos;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FrmPricipal"/>.
        /// </summary>
        public FrmPricipal()
        {
            InitializeComponent();
            patentes = new List<Patente>();
            hilos = new List<Thread>();
        }

        /// <summary>
        /// Manejador del evento Load del formulario.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void FrmPricipal_Load(object sender, EventArgs e)
        {
            vistaPatente.finExposicion += ProximaPatente;
        }

        /// <summary>
        /// Manejador del evento FormClosing del formulario.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void FrmPricipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            FinalizarSimulacion();
        }

        /// <summary>
        /// Manejador del evento Click del botón para agregar más patentes.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnMas_Click(object sender, EventArgs e)
        {
            try
            {
                List<Patente> listPatente = new List<Patente>
                {
                    new Patente("CP709WA", Tipo.Mercosur),
                    new Patente("DIB009", Tipo.Vieja),
                    new Patente("FD010GC", Tipo.Mercosur)
                };

                Sql sql = new Sql();
                if (sql.Guardar(listPatente))
                {
                    MessageBox.Show("¡Patentes guardadas en la base de datos!");
                }
                else
                {
                    MessageBox.Show("¡Error al guardar en la base de datos!");
                }

                Xml xml = new Xml();
                if (xml.Guardar(listPatente))
                {
                    MessageBox.Show("¡Patentes guardadas en el archivo xml!");
                }
                else
                {
                    MessageBox.Show("¡Error al guardar en el archivo xml!");
                }

                Texto texto = new Texto();
                if (texto.Guardar(listPatente))
                {
                    MessageBox.Show("¡Patentes guardadas en el archivo de texto!");
                }
                else
                {
                    MessageBox.Show("¡Error al guardar en el archivo de texto!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Manejador del evento Click del botón para leer patentes desde la base de datos SQL.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnSql_Click(object sender, EventArgs e)
        {
            try
            {
                Sql sql = new Sql();
                patentes = sql.Leer();
                IniciarSimulacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Manejador del evento Click del botón para leer patentes desde un archivo XML.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnXml_Click(object sender, EventArgs e)
        {
            try
            {
                Xml xml = new Xml();
                patentes = xml.Leer();
                IniciarSimulacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Manejador del evento Click del botón para leer patentes desde un archivo de texto.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnTxt_Click(object sender, EventArgs e)
        {
            try
            {
                Texto texto = new Texto();
                patentes = texto.Leer();
                IniciarSimulacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Inicia la simulación de visualización de patentes.
        /// </summary>
        private void IniciarSimulacion()
        {
            // Finalizará los hilos activos
            FinalizarSimulacion();
            ProximaPatente(vistaPatente);
        }

        /// <summary>
        /// Finaliza la simulación de visualización de patentes.
        /// </summary>
        private void FinalizarSimulacion()
        {
            if (hilos.Count > 0)
            {
                foreach (Thread item in hilos)
                {
                    if (item.IsAlive)
                        item.Abort();
                }
            }
        }

        /// <summary>
        /// Muestra la próxima patente en la vista.
        /// </summary>
        /// <param name="vistaPatente">La vista de la patente.</param>
        private void ProximaPatente(Patentes.VistaPatente vistaPatente)
        {
            if (patentes.Count > 0)
            {
                // Inicializará un hilo parametrizado para el método MostrarPatente del objeto VistaPatente recibido.
                Thread thread = new Thread(new ParameterizedThreadStart(vistaPatente.MostrarPatente));
                thread.Start(patentes.First());
                hilos.Add(thread);
                patentes.RemoveAt(0);
            }
        }
    }
}
