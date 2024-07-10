using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Entidades;
using Archivo;
using System.Threading;
using Archivo;
using Entidades;
using System.Timers;
using System.Xml.Linq;
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
            vistaPatente.finExposicion += ProximaPatente;// lo tengo que asociar con += pero no se bien a que o como porque ProximaPatente no me lo toma
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

                // Implementar acá el punto del botón +
                Sql sql = new Sql();
                Xml xml = new Xml();
                Texto texto = new Texto();
                try
                {
                    sql.Guardar(listPatente);
                    MessageBox.Show("¡Patentes guardadas en la base de datos!");
                }
                catch (Exception)
                {
                    MessageBox.Show("¡Error al guardar en la base de datos!");
                }

                try
                {
                    xml.Guardar(listPatente);
                    MessageBox.Show("¡Patentes guardadas en el archivo XML!");
                }
                catch (Exception)
                {
                    MessageBox.Show("¡Error al guardar en el archivo XML!");
                }

                try
                {
                    texto.Guardar(listPatente);
                    MessageBox.Show("¡Patentes guardadas en el archivo de texto!");
                }
                catch (Exception)
                {
                    MessageBox.Show("¡Error al guardar en el archivo de texto!");
                }

                patentes.AddRange(listPatente);
                IniciarSimulacion();
            
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
                List<Patente> patentesDesdeSql = sql.Leer();
                if (patentesDesdeSql != null)
                {
                    patentes.AddRange(patentesDesdeSql);
                    IniciarSimulacion();
                }
                else
                {
                    MessageBox.Show("No se pudieron leer las patentes desde la base de datos SQL.");
                }
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
                List<Patente> patentesDesdeXml = xml.Leer();
                if (patentesDesdeXml != null)
                {
                    patentes.AddRange(patentesDesdeXml);
                    IniciarSimulacion();
                }
                else
                {
                    MessageBox.Show("No se pudieron leer las patentes desde el archivo XML.");
                }
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
                List<Patente> patentesDesdeTxt = texto.Leer();
                if (patentesDesdeTxt != null)
                {
                    patentes.AddRange(patentesDesdeTxt);
                    IniciarSimulacion();
                }
                else
                {
                    MessageBox.Show("No se pudieron leer las patentes desde el archivo de texto.");
                }
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
            // Implementar el método FinalizarSimulación
            // que se encarga de finalizar todos los hilos activos
            FinalizarSimulacion();
            ProximaPatente(vistaPatente);
        }
        /// <summary>
        /// Verifica los hilos activos y los desactiva
        /// </summary>
        private void FinalizarSimulacion()
        {
            foreach (Thread hilo in hilos)
            {
                if (hilo.IsAlive)
                {
                    hilo.Abort();
                }
            }
            hilos.Clear();
        }
        /// <summary>
        /// Muestra la próxima patente en la vista.
        /// </summary>
        /// <param name="vistaPatente">La vista de la patente.</param>
        private void ProximaPatente(Patentes.VistaPatente vistaPatente)
        {
            if (patentes.Count > 0)
            {
                //Inicializará el hilo recién creado con el próximo elemento de la lista(tomar el
                //primero y eliminarlo una vez agregado al hilo).
                
                Thread thread = new Thread(new ParameterizedThreadStart(vistaPatente.MostrarPatente));
                thread.Start(patentes.First());
                
                hilos.Add(thread);
                patentes.RemoveAt(0);
            }
            // Inicializará un hilo parametrizado para el método MostrarPatente del objeto VistaPatente recibido.

            //Implementar acá el manejo de hilos
        }
    }
}
