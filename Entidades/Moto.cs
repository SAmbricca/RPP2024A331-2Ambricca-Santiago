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

        public ECilindrada Cilindrada
        {
            get { return _cilindrada; }
            set { _cilindrada = value; }
        }

        public Moto()
        {

        }

        public Moto(string marca, EPais pais, string modelo, float precio, ECilindrada cilindrada)
            : base(marca, pais, modelo, precio)
        {
            _cilindrada = cilindrada;
        }

        public static bool operator ==(Moto moto1, Moto moto2)
        {
            return (Vehiculo)moto1 == (Vehiculo)moto2 && moto1._cilindrada == moto2._cilindrada;
        }
        public static bool operator !=(Moto moto1, Moto moto2)
        {
            return !(moto1 == moto2);
        }

        public static explicit operator float(Moto moto)
        {
            return moto._precio;
        }

        public override bool Equals(object obj)
        {
            return obj is Moto && (Moto)obj == this;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nCILINDRADA: {_cilindrada}\n"; //utiliza toda la informacion dada por Mostrar y le agrega la linea 'TIPO: ___'
        }

    }
}
