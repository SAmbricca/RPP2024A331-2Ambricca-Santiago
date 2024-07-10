using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entidades.Vehiculo;

namespace Entidades
{
    public class Program
    {
        static void Main(string[] args)
        {
            Concesionaria miConcesionaria = 6;

            Fabricante fabricante1 = new Fabricante("RENAULT", EPais.Francia);
            Fabricante fabricante2 = new Fabricante("CITROEN", EPais.Francia);

            Auto auto1 = new Auto("R9", 65000, fabricante1, ETipo.Sedan);
            Auto auto2 = new Auto("C4", 285900, fabricante2, ETipo.Deportivo);
            Auto auto3 = new Auto("C4", 390500, fabricante2, ETipo.Familiar);
            Auto auto4 = new Auto("C4", 96300, fabricante1, ETipo.Deportivo);

            Moto moto1 = new Moto("DUCATI", EPais.Italia, "MONSTER", 450000, ECilindrada.cc500);
            Moto moto2 = new Moto("BMW", EPais.Alemania, "G 310 GS", 437500, ECilindrada.cc125);
            Moto moto3 = new Moto("DUCATI", EPais.Italia, "MONSTER", 150000, ECilindrada.cc125);

            miConcesionaria += auto1;

            //YA INGRESADO
            miConcesionaria += auto1;

            miConcesionaria += moto1;
            miConcesionaria += moto2;
            miConcesionaria += moto3;

            miConcesionaria += auto2;
            miConcesionaria += auto3;
            //SIN LUGAR
            miConcesionaria += auto4;

            //AGREGO UN LUGAR SACANDO UN VEHICULO
            miConcesionaria -= auto3;

            //NO ESTA MÁS EN LA CONCESIONARIA
            miConcesionaria -= auto3;

            miConcesionaria += auto4;

            Console.WriteLine();
            Console.WriteLine(Concesionaria.Mostrar(miConcesionaria));

            string rutaEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string rutaArchivoTxt = Path.Combine(rutaEscritorio, "Concesionaria.txt");

            miConcesionaria.GuardarConcesionaria(rutaArchivoTxt);

            string rutaArchivoXml = Path.Combine(rutaEscritorio, "Concesionaria.xml");
            miConcesionaria.SerializarConcesionaria(rutaArchivoXml);

            Concesionaria deserializedConcesionaria = Concesionaria.DeserializarConcesionaria(rutaArchivoXml);

            Console.WriteLine("\n***********************");
            Console.WriteLine("Objeto deserealizado:");
            Console.WriteLine("***********************\n");

            Console.WriteLine(Concesionaria.Mostrar(deserializedConcesionaria));

            Console.ReadLine();
        }
    }
}
