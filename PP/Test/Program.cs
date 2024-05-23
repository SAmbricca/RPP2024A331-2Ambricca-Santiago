using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Comerciante comerciante1 = new Comerciante("Domingo", "Caballo");
            Comerciante comerciante2 = new Comerciante("Alberto", "Samudio");
            Comerciante comerciante3 = new Comerciante("Joe", "Molleja");
            Exportador exportador1 = new Exportador("Electrónica 80", 15200,
            comerciante1, ETipoProducto.Tecnologico);
            Importador importador1 = new Importador("Granola S.A.", 23900,
            comerciante2, EPaises.UnionEuropea);
            Exportador exportador2 = new Exportador("Matarife", 29095, comerciante3,
            ETipoProducto.Rural);
            Importador importador2 = new Importador("Matarife", 203000, comerciante2,
            EPaises.Taiwan);
            Importador importador3 = new Importador("Matarife", 98000, comerciante1,
            EPaises.China);
            Shopping shopping = (Shopping)5;
            shopping += exportador1;
            shopping += importador1;
            shopping += exportador2;
            shopping += importador2;
            shopping += importador2;
            shopping += importador3;
            Importador importador4 = new Importador("Nuevo Comercio", 50000,
            comerciante1, EPaises.China);
            shopping += importador4;
            Console.WriteLine(Shopping.Mostrar(shopping));
            string rutaArchivoTxt = Path.Combine(Directory.GetCurrentDirectory(),
            "shopping.txt");
            shopping.GuardarShopping(rutaArchivoTxt);
            string rutaArchivoXml = Path.Combine(Directory.GetCurrentDirectory(),
            "shopping.xml");
            shopping.SerializarShopping(rutaArchivoXml);
            Shopping deserializedShopping =
            Shopping.DeserializarShopping(rutaArchivoXml);
            Console.WriteLine("\n---------------------------------");
            Console.WriteLine("Objeto deserealizado:");
            Console.WriteLine("---------------------------------\n");
            Console.WriteLine(Shopping.Mostrar(deserializedShopping));
            Console.ReadKey();
        }
    }
}
