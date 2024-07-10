using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Archivo
{
    public class Xml : IArchivo
    {
        private string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "patentes.xml");
        /// <summary>
        /// guarda en un archivo XML llamado patentes.xml que se
        /// almacena en el Escritorio las patentes que reciba por parámetro.
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool Guardar(List<Patente> datos)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Patente>));
                using (StreamWriter sw = new StreamWriter(ruta))
                {
                    serializer.Serialize(sw, datos);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// lee el archivo XML patentes.xml y retorna el listado de
        /// las patentes.
        /// </summary>
        /// <returns></returns>
        public List<Patente> Leer()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Patente>));
                using (StreamReader sr = new StreamReader(ruta))
                {
                    return (List<Patente>)serializer.Deserialize(sr);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}

