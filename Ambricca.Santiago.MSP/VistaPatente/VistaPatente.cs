using System;
using System.Windows.Forms;
using System.Threading;
using Entidades;

namespace Patentes
{
    public delegate void MostrarPatente(object patente);
    public delegate void FinExposicionPatente(VistaPatente vistaPatente);
    public partial class VistaPatente : UserControl
    {

        public event FinExposicionPatente finExposicion;

        public VistaPatente()
        {
            InitializeComponent();
            picPatente.Image = fondosPatente.Images[(int)Tipo.Mercosur];
        }
        /// <summary>
        /// se muestra la patente
        /// durante un tiempo X y luego notifica por medio de un evento que finalizó dicha
        /// exposición.
        /// </summary>
        /// <param name="patente"></param>
        public void MostrarPatente(object patente)
        {
            if (lblPatenteNro.InvokeRequired)
            {
                try
                {
                    // Llama al hilo principal para actualizar la interfaz de usuario.
                    lblPatenteNro.Invoke(new Action(() =>
                    {
                        picPatente.Image = fondosPatente.Images[(int)((Patente)patente).TipoCodigo];
                        lblPatenteNro.Text = patente.ToString();
                    }));

                    Thread.Sleep(1500);

                    finExposicion.Invoke(this);

                    Thread.Sleep(1500);

                    // Dispara el evento de que finalizó la exposición de la patente.

                }
                catch (Exception) { }
            }
            else
            {
                picPatente.Image = fondosPatente.Images[(int)((Patente)patente).TipoCodigo];
                lblPatenteNro.Text = patente.ToString();
                Thread.Sleep(1500);

                finExposicion.Invoke(this);
            }
        }
    }
}
