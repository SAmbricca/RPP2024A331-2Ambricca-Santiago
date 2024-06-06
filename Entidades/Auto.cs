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

        public ETipo Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public Auto()
        {

        }

        public Auto(string modelo, float precio, Fabricante fabricante, ETipo tipo)
            : base(fabricante.Marca, fabricante.Pais, modelo, precio)
        {
            _tipo = tipo;
        }

        public static bool operator ==(Auto auto1, Auto auto2)
        {
            return (Vehiculo)auto1 == (Vehiculo)auto2 && auto1._tipo == auto2._tipo;
        }
        //auto1 == auto2 compara si sus modelos, precios y fabricantes son iguales, reutilizando el codigo de la clase Vehiculo
        public static bool operator !=(Auto auto1, Auto auto2)
        {
            return !(auto1 == auto2);
        }

        public static explicit operator float(Auto auto)
        {
            return auto._precio;
        }

        public override bool Equals(object obj)
        {
            return obj is Auto && (Auto)obj == this;
        }
        public override string ToString()
        {
            return base.ToString() + $"\nTipo: {_tipo}\n"; //utiliza toda la informacion dada por Mostrar y le agrega la linea 'TIPO: ___'
        }
    }
}
