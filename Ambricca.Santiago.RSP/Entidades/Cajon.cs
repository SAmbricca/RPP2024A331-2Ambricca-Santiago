using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Entidades
{
    //Delegado destinado al uso de evento PrecioExtendido
    public delegate void PrecioExtendido(object sender);  

    public class Cajon<T>
    {
        protected int _capacidad;                       
        protected List<T> _elementos;
        protected double _precioUnitario;

        //Evento que se disparará cuando el precioTotal sea mayor a $55, invocando los suscriptores del evento
        public event PrecioExtendido eventoPrecio;

        //Propiedad para acceder a la lista de elementos del cajón
        public List<T> Elementos                    
        {
            get 
            { 
                return _elementos; 
            }
            set 
            { 
                _elementos = value; 
            }
        }

        //Propiedad que calcula y retorna el precio total del cajon. Utiliza el evento de ser necesario
        public double PrecioTotal                  
        {
            get
            {
                double precioTotal = _precioUnitario * _elementos.Count;

                if (eventoPrecio != null && precioTotal > 55)
                {
                    eventoPrecio(precioTotal);
                }
                return precioTotal;
            }
            set
            {

            }
        }

        //Constructor de instancia que inicializa la lista _elementos cuando se instancia un nuevo objeto Cajon
        public Cajon()
        {
            _elementos = new List<T>();
        }

        //Constructor de instancia que inicializa el atributo _capacidad
        public Cajon(int capacidad)
            : this()
        {
            _capacidad = capacidad;
        }

        //Constructor de instancia que inicializa los atributos atributo _capacidad (utilizando el constructor anterior) y _precioUnitario
        public Cajon(double precio, int capacidad)
            : this(capacidad)
        {
            _precioUnitario = precio;
        }

        //Serializa el objeto Cajon<T> a XML y lo guarda en el archivo especificado por path. Utiliza XmlSerializer para la serialización
        public bool Xml(string path)
        {
            bool retorno = false;
            try
            {
                //Para serializar XML necesita metodos publicos
                //Se crea un xmlTextWriter para escribir en el archivo XML
                using (XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8))
                {
                    //Se indica el tipo de objeto a serializar:
                    XmlSerializer serializer = new XmlSerializer(typeof(Cajon<T>));

                    //Se serializa el objeto this en el archivo contenido en el Writer
                    serializer.Serialize(writer, this);
                }

                retorno = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retorno;
        }

        // Sobrecarga del operador + que permite agregar un elemento T al cajón. Si el cajón no alcanzó su capacidad máxima, agrega el elemento a la lista _elementos.
        // Si está lleno, lanza una excepción CajonLlenoException
        public static Cajon<T> operator +(Cajon<T> cajon, T elem)
        {
            try
            {
                if (cajon.Elementos.Count < cajon._capacidad)
                {
                    cajon._elementos.Add(elem);
                    return cajon;
                }
                else
                {
                    throw new CajonLlenoException("¡El cajón ya se encuentra lleno!");
                }
            }
            catch (CajonLlenoException cajonLLenoException)
            {
                throw cajonLLenoException;
            }
        }

        //Sobrescribe el método ToString() para devolver una representación de cadena con la información del cajón
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Capacidad: {_capacidad}");
            sb.AppendLine($"Cantidad total de elementos: {_elementos.Count}");
            sb.AppendLine($"Precio total: ${PrecioTotal}");
            sb.AppendLine($"*****************************");
            sb.AppendLine($"      Lista de frutas");
            sb.AppendLine($"*****************************\n");

            foreach (T elem in _elementos)
            {
                sb.AppendLine(elem.ToString());
                sb.AppendLine($"------------------------------------------");
            }

            return sb.ToString();
        }
    }
}
