using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Exportador : Comercio
    {
        private ETipoProducto _tipo;

        public Exportador()
        {

        }
        public Exportador(string nombreComercio, float precioAlquiler, Comerciante comerciante, ETipoProducto tipo)
            : base(nombreComercio, comerciante, precioAlquiler)
        {
            _tipo = tipo;
        }

        public string Mostrar()
        {
            return $"{(string)this}Tipo: {_tipo}\n";
        }

        public static bool operator == (Exportador exportador1, Exportador exportador2)
        {
            return exportador1 == exportador2 && exportador1._tipo == exportador2._tipo; //e1 == e2 compara si sus nombres y comerciantes son iguales, reutilizando el codigo de la clase Comercio
        }
        public static bool operator !=(Exportador exportador1, Exportador exportador2)
        {
            return !(exportador1 == exportador2); 
        }

        public static implicit operator ETipoProducto(Exportador exportador)
        {
            return exportador._tipo;
        }

    }
}
