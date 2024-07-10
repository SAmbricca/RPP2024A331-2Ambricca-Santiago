using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Auto : Vehiculo //Deriva de la clase Vehiculo
    {
        private ETipo _tipo;

        public ETipo Tipo                        //propiedad que devuelve y establece el tipo de auto.
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public Auto()                           //constructor vacio que no inicializa nada
        {

        }

        public Auto(string modelo, float precio, Fabricante fabricante, ETipo tipo)   ////constructor que inicializa el atributo _tipo y llama al constructor de la clase base Vehiculo para inicializar los atributos comunes
            : base(fabricante.Marca, fabricante.Pais, modelo, precio)
        {
            _tipo = tipo;
        }

        public static bool operator ==(Auto auto1, Auto auto2)  //operador que verifica si losautos son iguales
        {
            return (Vehiculo)auto1 == (Vehiculo)auto2 && auto1._tipo == auto2._tipo; //auto1 == auto2 compara si sus modelos, precios y fabricantes son iguales, reutilizando el codigo de la clase Vehiculo
        }
        
        public static bool operator !=(Auto auto1, Auto auto2) //operador contrario al de igualdad
        {
            return !(auto1 == auto2);
        }

        public static explicit operator float(Auto auto) //operador de explicito float que devuelve el precio del auto
        {
            return auto._precio;
        }

        public override bool Equals(object obj)    //Metodo que compara un objeto Auto con otro obejto y devuelve true si son iguales
        {
            return obj is Auto && (Auto)obj == this;
        }
        public override string ToString()          ////Metodo que devuelve una cadena con la informacion del auto, utilizando ToString() de la clase base
        {
            return base.ToString() + $"\nTipo: {_tipo}\n"; //utiliza toda la informacion dada por Mostrar y le agrega la linea 'TIPO: ___'
        }
    }
}
