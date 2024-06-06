using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class Vehiculo
    {
        protected Fabricante _fabricante;
        protected static Random _generadorDeVelocidades;
        protected string _modelo;
        protected float _precio;
        protected int _velocidadMaxima;

        public int VelocidadMaxima
        {
            get
            {
                if (_velocidadMaxima == 0)
                    _velocidadMaxima = _generadorDeVelocidades.Next(100, 281);

                return _velocidadMaxima;
            }
        }

        public Fabricante Fabricante
        {
            get { return _fabricante; }
            set { _fabricante = value; }
        }

        public string Modelo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }

        public float Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }

        public Vehiculo()
        {

        }

        static Vehiculo()
        {
            _generadorDeVelocidades = new Random();
        }

        public Vehiculo(string marca, EPais pais, string modelo, float precio)
        {
            _fabricante = new Fabricante(marca, pais);
            _modelo = modelo;
            _precio = precio;
        }

        public Vehiculo(string modelo, float precio, Fabricante fabricante)
            : this(modelo, fabricante.Pais, fabricante.Marca, precio)
        {

        }
        private string Mostrar()
        {
            return $"FABRICANTE: {(string)_fabricante}\nMODELO: {_modelo}\nVELOCIDAD MAXIMA: {VelocidadMaxima}\nPRECIO: ${_precio}";
        }

        public static bool operator ==(Vehiculo vehiculo1, Vehiculo vehiculo2)
        {
            return vehiculo1._modelo == vehiculo2._modelo && vehiculo1.Fabricante == vehiculo2.Fabricante;  //con poner solo Fabricante analiza tanto la marca como el pais de la misma. reutilizando codigo
        }

        public static bool operator !=(Vehiculo vehiculo1, Vehiculo vehiculo2)
        {
            return !(vehiculo1 == vehiculo2); //reutiliza codigo verificando lo contrario a la linea 73
        }

        public static explicit operator string(Vehiculo vehiculo)
        {
            return vehiculo.Mostrar(); //reutiliza Mostrar()
        }
        public virtual string ToString()
        {
            return Mostrar();
        }

    }

}
