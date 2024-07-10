using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Entidades
{
    public delegate void PrecioExtendido(object sender);
    public class Cajon<T>
    {
        protected int _capacidad;
        protected List<T> _elementos;
        protected double _precioUnitario;
        public event PrecioExtendido eventoPrecio;

        public List<T> Elementos
        {
            get { return _elementos; }
            set { _elementos = value; }
        }

        public double PrecioTotal
        {
            get
            {
                double precioTotal = _precioUnitario * _elementos.Count();

                if (precioTotal > 55)
                {
                    eventoPrecio(precioTotal);
                }
                return precioTotal;
            }
            set
            {

            }
        }

        public Cajon()
        {
            _elementos = new List<T>();
        }

        public Cajon(int capacidad)
            : this()
        {
            _capacidad = capacidad;
        }

        public Cajon(double precio, int capacidad)
            : this()
        {
            _precioUnitario = precio;
        }

        public bool Xml(string path)
        {
            bool resultado = false;

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Cajon<T>));

                using (StreamWriter sw = new StreamWriter(path))
                {
                    xmlSerializer.Serialize(sw, this);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return resultado;
        }

        public static Cajon<T> operator +(Cajon<T> cajon, T elemento)
        {
            if (cajon._elementos.Count < cajon._capacidad)
            {
                cajon._elementos.Add(elemento);
            }
            else
            {
                throw new CajonLlenoException("El cajón ya se encuentra lleno");
            }

            return cajon;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Capacidad: {_capacidad}");
            sb.AppendLine($"Cantidad total de elementos: {_elementos.Count}");
            sb.AppendLine($"Precio total: ${PrecioTotal}");
            sb.AppendLine($"*******************************");
            sb.AppendLine($"        Lista de frutas");
            sb.AppendLine($"*******************************");

            foreach (T elemento in _elementos)
            {
                sb.AppendLine(elemento.ToString());
                sb.AppendLine($"------------------------");
            }

            return sb.ToString();
        }




    }
}
