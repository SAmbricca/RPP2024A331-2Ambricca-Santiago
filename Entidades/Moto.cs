using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Moto : Vehiculo //Deriva de la clase Vehiculo
    {
        private ECilindrada _cilindrada;

        public ECilindrada Cilindrada               //propiedad que devuelve y establece la cilindrada de la moto.
        {
            get { return _cilindrada; }
            set { _cilindrada = value; }
        }

        public Moto()                               //Constructor vacío que no inicializa los atributos.
        {

        }

        public Moto(string marca, EPais pais, string modelo, float precio, ECilindrada cilindrada)      //constructor que inicializa el atributo _cilindrada y llama al constructor de la clase base Vehiculo para inicializar los atributos comunes
            : base(marca, pais, modelo, precio)
        {
            _cilindrada = cilindrada;
        }

        public static bool operator ==(Moto moto1, Moto moto2)                  //operador que compara dos parametros (objetos) Moto y devuelve true si tienen el mismo modelo, fabricante y cilindrada
        {
            return (Vehiculo)moto1 == (Vehiculo)moto2 && moto1._cilindrada == moto2._cilindrada;  //mismo procedimiento que con auto
        }
        public static bool operator !=(Moto moto1, Moto moto2)
        {
            return !(moto1 == moto2);
        }

        public static explicit operator float(Moto moto)                        //operador que convierte un objeto Moto en un float que representa el precio de la moto
        {
            return moto._precio;
        }

        public override bool Equals(object obj)                                //Metodo que compara un objeto Moto con otro obejto y devuelve true si son iguales
        {
            return obj is Moto && (Moto)obj == this; 
        }

        public override string ToString()                                      //Metodo que devuelve una cadena con la informacion de la moto, utilizando ToString() de la clase base
        {
            return base.ToString() + $"\nCILINDRADA: {_cilindrada}\n"; //utiliza toda la informacion dada por Mostrar y le agrega la linea 'TIPO: ___'
        }

    }
}
