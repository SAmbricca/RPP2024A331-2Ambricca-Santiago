using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Exportador : Comercio  ///deriva de Comercio
    {
        private ETipoProducto _tipo;

        public Exportador()  ///Constructor de instancia vacio
        {

        }

        public Exportador(string nombreComercio, float precioAlquiler, Comerciante comerciante,   ETipoProducto tipo)
            : base(nombreComercio, precioAlquiler, comerciante)
        {
            _tipo = tipo;
        }

        public static bool operator == (Exportador e1, Exportador e2)
        {
            return e1._nombre == e2._nombre && e1._tipo == e2._tipo;
        }

        public static bool operator !=(Exportador e1, Exportador e2)
        {
            return !(e1 == e2);
        }

        public static implicit operator string(Exportador exportador)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Tipo: {exportador._tipo}");

            return stringBuilder.ToString();
        }
    }
}
