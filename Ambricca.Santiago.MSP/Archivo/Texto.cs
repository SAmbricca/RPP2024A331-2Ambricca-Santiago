using Entidades;
using System;
using System.Collections.Generic;
using System.IO;

namespace Archivo
{
    public class Texto : IArchivo
    {
        private string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "patentes.txt");
        /// <summary>
        /// guardar en un archivo llamado patentes.txt que se
        /// almacenará en el Escritorio las patentes que recibe por parámetro.
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool Guardar(List<Patente> datos)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(ruta, true))
                {
                    foreach (var patente in datos)
                    {
                        sw.WriteLine($"{patente.CodigoPatente},{patente.TipoCodigo}");
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Lee el archivo patentes.txt y retorna el listado de las
        /// patentes
        /// </summary>
        /// <returns></returns>
        public List<Patente> Leer()
        {
            List<Patente> patentes = new List<Patente>();
            try
            {
                using (StreamReader sr = new StreamReader(ruta))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var data = line.Split(',');
                        patentes.Add(data[0].ValidarPatente());
                    }
                }
                return patentes;
            }
            catch
            {
                return null;
            }
        }
    }
}
