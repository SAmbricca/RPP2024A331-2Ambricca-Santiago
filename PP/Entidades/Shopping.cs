using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    public class Shopping
    {
        private int _capacidadMaxima;
        private List<Comercio> _comercios;

        public int CapacidadMaxima
        {
            get { return _capacidadMaxima; }
            set { _capacidadMaxima = value; }
        }

        public List<Comercio> GetComercios()
        {
            return _comercios;
        }

        public double PrecioDeExportaciones
        {
            get { return ObtenerPrecio(EComercios.Exportador); }
        }
        public double PrecioDeImportaciones
        {
            get { return ObtenerPrecio(EComercios.Importador); }
        }

        public double PrecioTotal
        {
            get { return ObtenerPrecio(EComercios.Ambos); }
        }

        private Shopping()
        {

        }

        private Shopping(int capacidadMaxima)
        {
            capacidadMaxima = _capacidadMaxima;
        }

        public static implicit operator Shopping(int capacidadMaxima)  ///toma el valor int y retorna una nueva instancia de Shopping con la capacidadMaxima
        {
            return new Shopping(capacidadMaxima);
        }

        public static bool operator ==(Shopping shopping, Comercio comercio)
        {
            if (shopping != null && shopping._comercios != null)
            {
                if (shopping._comercios.Count() < shopping._capacidadMaxima)
                {
                    return shopping._comercios.Contains(comercio);
                }
            }
            return shopping._comercios.Contains(comercio);
        }

        public static bool operator !=(Shopping shopping, Comercio comercio)
        {
            return !object.Equals(shopping, comercio); 
        }


        public static Shopping operator +(Shopping shopping, Comercio comercio)
        {
            if (shopping != null && shopping._comercios != null)
            {
                if (shopping._comercios.Count() < shopping._capacidadMaxima)
                {
                    shopping._comercios.Add(comercio);
                }
                else if (!(shopping._comercios.Contains(comercio)))
                {
                    Console.WriteLine("No hay más lugar en el Shopping!!!");
                }

                else
                {
                    Console.WriteLine("El comercio ya está en el Shopping!!!");
                }
            }

            

            return shopping;
        }

        private double ObtenerPrecio(EComercios comercios)
        {
            double precio = 0;

            if (_comercios != null)
            {
                foreach (Comercio comercio in _comercios)
                {
                    if (comercio.GetType() == _comercios.GetType())
                    {
                        precio += comercio.PrecioAlquiler;
                    }
                }
            }

            return precio;
        }


        public static string Mostrar(Shopping shopping)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Capacidad del shopping: {shopping.CapacidadMaxima}");
            stringBuilder.AppendFormat($"Total por Importadores: $0:#.##", shopping.PrecioDeImportaciones);
            stringBuilder.AppendFormat($"Total por Exportadores: $0:#.##", shopping.PrecioDeExportaciones);
            stringBuilder.AppendFormat($"Total: $0:#.##", shopping.PrecioTotal);

            return stringBuilder.ToString();
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

            using (StreamWriter streamWriter = new StreamWriter(rutaArchivo))
            {
                streamWriter.WriteLine(Mostrar(this));
            }
        }

        public void SerializarShopping(string rutaArchivo)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Shopping));

            using (StreamWriter streamWriter = new StreamWriter(rutaArchivo))
            {
                xmlSerializer.Serialize(streamWriter, this);
            }
        }


        public static Shopping DeserializarShopping(string rutaArchivo)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Shopping));
            ///en vez de escribir, ahora lo leo
            using (StreamReader streamReader = new StreamReader(rutaArchivo))
            {
                return (Shopping)xmlSerializer.Deserialize(streamReader);
            }
        }
    }
}
