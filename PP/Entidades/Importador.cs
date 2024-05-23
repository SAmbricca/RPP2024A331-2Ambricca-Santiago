using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Importador : Comercio  ///deriva de Comercio
    {
        private EPaises _paisOrigen;

        public Importador()  ///Constructor de instancia vacio
        {

        }

        public Importador(string nombreComercio, float precioAlquiler, Comerciante comerciante, EPaises paisOrigen)
            : base(nombreComercio, precioAlquiler, comerciante)
        {
            _paisOrigen = paisOrigen;
        }

        public static bool operator ==(Importador i1, Importador i2)
        {
            return i1._nombre == i2._nombre && i1._paisOrigen == i2._paisOrigen;
        }

        public static bool operator !=(Importador i1, Importador i2)
        {
            return !(i1 == i2);
        }

        public static implicit operator string(Importador importador)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Pais de Origen: {importador._paisOrigen}");

            return stringBuilder.ToString();
        }
    }
}
