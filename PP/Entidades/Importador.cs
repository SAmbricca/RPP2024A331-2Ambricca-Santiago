using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Importador : Comercio
    {
        private EPaises _paisOrigen;

        public Importador()
        {

        }

        public Importador(string nombreComercio, float precioAlquiler, Comerciante comerciante, EPaises paisOrigen)
            :base(nombreComercio, comerciante, precioAlquiler)
        {
            _paisOrigen = paisOrigen;
        }

        public string Mostrar()
        {
            return $"{(string)this}País de Origen: {_paisOrigen}\n";
        }

        public static bool operator == (Importador importador1, Importador importador2)
        {
            return importador1 == importador2 && importador1._paisOrigen == importador2._paisOrigen;
        }
        public static bool operator !=(Importador importador1, Importador importador2)
        {
            return !(importador1 == importador2);
        }

        public static implicit operator EPaises(Importador importador)
        {
            return importador._paisOrigen;
        }
    }
}
