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
    public class Manzana : Fruta, ISerializar, IDeserializar
    {
        protected string _provinciaOrigen;

        //Propiedad que devuelve el nombre de la fruta como 'Manzana'
        public string Nombre
        {
            get
            {
                return "Manzana";
            }
        }

        //Propiedad de lectura virtual TieneCarozo que indica si la fruta tiene carozo (manzana no tiene, retorna false)
        public override bool TieneCarozo
        {
            get
            {
                return false;
            }
            set { }
        }

        //Propiedad para leer y escribir la provincia de origen de la manzana
        public string ProvinciaOrigen
        {
            get
            {
                return _provinciaOrigen;
            }
            set
            {
                _provinciaOrigen = value;
            }
        }

        //Constructor de instancia vacio
        public Manzana()
        {

        }

        //Constructor para inicializar color, peso y provincia de origen de la manzana. Reutiliza constructor de Fruta
        public Manzana(string color, double peso, string provinciaOrigen)
            : base(color, peso)
        {
            _provinciaOrigen = provinciaOrigen;
        }

        //Metodo de la interfaz ISerializar para serializar la manzana a XML
        public bool Xml(string path)
        {
            bool retorno = false;
            try
            {
                // Se crea un XmlTextWriter para escribir en el archivo XML especificado en 'path'
                using (XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8))
                {
                    // Se indica el tipo de objeto a serializar (Manzana)
                    XmlSerializer serializer = new XmlSerializer(typeof(Manzana));

                    // Se serializa el objeto actual (this, que es una instancia de Manzana) en el archivo
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

        //Metodo de la interfaz IDeserializar para deserializar un objeto Fruta desde XML
        bool IDeserializar.Xml(string path, out Fruta fruta)
        {
            bool retorno = false;
            fruta = null;

            try
            {
                //Obtiene la ruta completa del archivo desde el escritorio del sistema
                string rutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + path;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Manzana)); // Crea un XmlSerializer para tipo Manzana

                //Lee el archivo XML y deserializa su contenido a un objeto Manzana
                using (StreamReader streamReader = new StreamReader(rutaArchivo))
                {
                    fruta = (Manzana)xmlSerializer.Deserialize(streamReader); // Convierte el objeto deserializado a Manzana
                }

                retorno = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return retorno;
        }

        //Metodo protegido que sobreescribe el metodo protegido 'FrutasToString()' de Fruta (reutiliza codigo). Agrega a la cadena la informacion del durazno
        protected override string FrutasToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Fruta: {Nombre}");
            sb.Append(base.FrutasToString()); //Llama al metodo FrutasToString de la clase base (Fruta) para detalles generales
            sb.AppendFormat("Provincia de origen: {0}", _provinciaOrigen);

            return sb.ToString();
        }

        //Sobrescribe el metodo ToString() para obtener una representación de cadena de la Manzana
        //Llama al metodo FrutasToString para obtener la representación detallada de la Manzana
        public override string ToString()
        {
            return FrutasToString();
        }
    }
}