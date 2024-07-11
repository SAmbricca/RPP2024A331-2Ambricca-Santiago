using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{

    //Clase utilizada para representar una excepción que se lanza cuando se intenta agregar un elemento a un cajón que ya habia alcanzado su capacidad máxima
    //Hereda de Exception, lo que la convierte en una excepción personalizada
    public class CajonLlenoException : Exception
    {
        public CajonLlenoException(string message)
            : base(message)
        {

        }
    }
}