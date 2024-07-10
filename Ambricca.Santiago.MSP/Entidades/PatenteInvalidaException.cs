using System;

namespace Entidades
{
    public class PatenteInvalidaException: Exception
    {
        public PatenteInvalidaException(string mensaje) : base(mensaje) { }
        public PatenteInvalidaException(string mensaje, Exception innerException) : base(mensaje) { }

    }
}
