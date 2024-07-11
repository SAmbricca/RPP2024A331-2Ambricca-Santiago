using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class Comercio
    {
        protected string _nombre;
        protected int _cantidadDeEmpleados;
        protected static Random _generadorDeEmpleados;
        protected float _precioAlquiler;
        protected Comerciante _comerciante;

        public int CantidadDeEmpleados 
        {
            get
            {
                if (_cantidadDeEmpleados == 0)
                    _cantidadDeEmpleados = _generadorDeEmpleados.Next(1, 101);

                return _cantidadDeEmpleados;
            }
            set
            {
 
            }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public float PrecioAlquiler
        {
            get { return _precioAlquiler; }
            set { _precioAlquiler = value; }
        }
        public Comerciante Comerciante
        {
            get { return _comerciante; }
            set { _comerciante = value; }
        }

        static Comercio()
        {
            _generadorDeEmpleados = new Random();
        }

        public Comercio()
        {

        }

        public Comercio(float  precioAlquiler, string nombreComercio, string nombre, string apellido)
        {
            _nombre = nombreComercio;
            _comerciante = new Comerciante(nombre, apellido);
            _precioAlquiler = precioAlquiler;
        }

        public Comercio(string nombre, Comerciante comerciante, float precioAlquiler)
            :this (precioAlquiler, nombre, comerciante.Nombre, comerciante.Apellido)
        {

        }
        private string Mostrar()
        {
            return $"Nombre: {_nombre}\nComerciante: {(string)_comerciante}\nCantidad de Empleados: {CantidadDeEmpleados}\n";
        }

        public static bool operator == (Comercio comercio1, Comercio comercio2)
        {
            return comercio1._nombre == comercio2._nombre && comercio1.Comerciante == comercio2.Comerciante;
        }

        public static bool operator !=(Comercio comercio1, Comercio comercio2)
        {
            return !(comercio1 == comercio2);
        }

        public override bool Equals(object obj)
        {
            return obj is Comercio && (Comercio)obj == this;
            //es lo mismo que la que use yo
        }

        public static explicit operator string (Comercio comercio)
        {
            return comercio.Mostrar();
        }

    }
}
