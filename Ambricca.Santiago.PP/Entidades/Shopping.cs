using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [XmlInclude(typeof(Importador))]
    [XmlInclude(typeof(Exportador))]
    [XmlInclude(typeof(Comerciante))]
    [XmlInclude(typeof(Comercio))]
    public class Shopping
    {
        private int _capacidadMaxima;
        private List<Comercio> _comercios;

        public int CapacidadMaxima
        {
            get { return _capacidadMaxima;}
            set {  _capacidadMaxima = value;}
        }

        public List<Comercio> Comercios
        {
            get { return _comercios; }
            set { _comercios = value; }
        }

        public double PrecioExportadores
        {
            get { return ObtenerPrecio(EComercios.Exportador); }
            set { }
        }

        public double PrecioImportadores
        {
            get { return ObtenerPrecio(EComercios.Importador); }
            set { }
        }

        public double PrecioTotal
        {
            get { return ObtenerPrecio(EComercios.Ambos);}
            set { }
        }
        private Shopping()
        {
            _comercios = new List<Comercio>();
        }

        private Shopping(int capacidadMaxima)
            :this()
        {
            _capacidadMaxima = capacidadMaxima;
        }

        public static string Mostrar(Shopping shopping)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Capacidad del Shopping: {shopping.CapacidadMaxima}");
            sb.AppendLine($"Total por Importadores: ${shopping.PrecioImportadores}");
            sb.AppendLine($"Total por Exportadores: ${shopping.PrecioExportadores}");
            sb.AppendLine($"Total: ${shopping.PrecioTotal}");
            sb.AppendLine($"*********************");
            sb.AppendLine($"Listado de Comercios");
            sb.AppendLine($"*********************");

            foreach (Comercio comercio in shopping._comercios)
            {
                if (comercio is Exportador exportador)
                {
                    sb.AppendLine(exportador.Mostrar());
                }
                else if (comercio is Importador importador)
                {
                    sb.AppendLine(importador.Mostrar());
                }
            }

            return sb.ToString();
        }

        private double ObtenerPrecio(EComercios tipo)
        {
            double total = 0;

            foreach (Comercio comercio in _comercios)
            {
                if(tipo == EComercios.Ambos)
                {
                    total += comercio.PrecioAlquiler;
                }
                else if (tipo.ToString() == comercio.GetType().Name)
                {
                    total += comercio.PrecioAlquiler;
                }
            }

            return total;
        }

        public static implicit operator Shopping(int capacidad)
        {
            return new Shopping(capacidad);
        }

        public static bool operator == (Shopping shopping, Comercio comercio)
        {
            return shopping._comercios.Contains(comercio);
        }
        public static bool operator !=(Shopping shopping, Comercio comercio)
        {
            return !(shopping == comercio);
        }

        public static Shopping operator +(Shopping shopping, Comercio comercio)
        {
            if (shopping._comercios.Count() >= shopping._capacidadMaxima)
            {
                Console.WriteLine("No hay más lugar en el Shopping!!!");
            }
            else if (shopping == comercio)
            {
                Console.WriteLine("El comercio ya está en el Shopping!!!");
            }
            else
            {
                shopping._comercios.Add(comercio);
            }

            return shopping;
        }
        public void GuardarShopping(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
            {
                using (FileStream fileStream = File.Create(rutaArchivo))
                {
                    fileStream.Close();
                }
            }
            using (StreamWriter sw = new StreamWriter(rutaArchivo))
                {
                    sw.WriteLine(Mostrar(this));
                }
        }
        public void SerializarShopping(string path)
        { 
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Shopping));

            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                xmlSerializer.Serialize(streamWriter, this);
            }
        }
        public static Shopping DeserializarShopping(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Shopping));
            ///en vez de escribir, ahora lo leo
            using (StreamReader streamReader = new StreamReader(path))
            {
                return (Shopping)xmlSerializer.Deserialize(streamReader);
            }
        }
    }
}
