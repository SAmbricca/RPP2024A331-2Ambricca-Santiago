using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    public class Manzana : Fruta, ISerializar, IDeserializar
    {
        protected string _provinciaOrigen;

        public override bool TieneCarozo //override porque la hereda y es abstracta
        {
            get { return false; }
            set { }
        }
        public string ProvinciaOrigen
        {
            get { return _provinciaOrigen; }
            set { _provinciaOrigen = value; }
        }
        public string Nombre
        {
            get { return GetType().Name; }
        }
        public Manzana(string color, double peso, string provinciaOrigen)
            : base(color, peso)
        {
            _provinciaOrigen = provinciaOrigen;
        }

        public bool Xml(string path)
        {
            bool retorno = false;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Manzana));

                using (StreamWriter sw = new StreamWriter(path))
                {
                    xmlSerializer.Serialize(sw, this);
                }

                retorno = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retorno;
        }

        bool IDeserializar.Xml(string path, out Fruta fruta)
        {
            bool retorno = false;
            fruta = null;

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Manzana));

                using (StreamReader sw = new StreamReader(path))
                {
                    fruta = (Manzana)xmlSerializer.Deserialize(sw);
                }
                retorno = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return retorno;
        }

        protected override string FrutasToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Fruta: {Nombre}");
            sb.AppendLine(base.FrutasToString());
            sb.AppendLine($"Pais de origen: {_provinciaOrigen}");

            return sb.ToString();
        }
        public override string ToString()
        {
            return FrutasToString();
        }
    }
}
