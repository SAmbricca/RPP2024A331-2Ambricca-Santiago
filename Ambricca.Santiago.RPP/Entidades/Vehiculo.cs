using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class Vehiculo  //clase abstracta, lo que significa que no se puede instanciar directamente
    {
        protected Fabricante _fabricante;
        protected static Random _generadorDeVelocidades;
        protected string _modelo;
        protected float _precio;
        protected int _velocidadMaxima;

        public int VelocidadMaxima   //Propiedad que devuelve la velocidad máxima del vehículo. Si la velocidad máxima no ha sido establecida, se genera una velocidad aleatoria entre 100 y 280. 
        {
            get
            {
                if (_velocidadMaxima == 0)
                    _velocidadMaxima = _generadorDeVelocidades.Next(100, 281);

                return _velocidadMaxima;
            }
        }

        public Fabricante Fabricante    //propiedad que devuelve y establece el fabricante del vehículo
        {
            get { return _fabricante; }
            set { _fabricante = value; }
        }

        public string Modelo             //propiedad que devuelve y establece el modelo del vehículo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }

        public float Precio             //propiedad que devuelve y establece el precio del vehículo
        {
            get { return _precio; }
            set { _precio = value; }
        }

        public Vehiculo()               //Constructor vacío que no inicializa los atributos
        {

        }

        static Vehiculo()               //Constructor necesario para inicializar _generadorDeVelocidades
        {
            _generadorDeVelocidades = new Random();
        }

        public Vehiculo(string marca, EPais pais, string modelo, float precio)  //Constructor que inicializa los atributos  _fabricante, _modelo y _precio
        {
            _fabricante = new Fabricante(marca, pais);
            _modelo = modelo;
            _precio = precio;
        }

        public Vehiculo(string modelo, float precio, Fabricante fabricante)  //Igual al anterior que utiliza sus inicializadores para _modelo, _precio y _fabricante
            : this(modelo, fabricante.Pais, fabricante.Marca, precio)
        {

        }
        private string Mostrar()    //metodo privado que devuelva una cadena que contiene la informacion del veihculo
        {
            return $"FABRICANTE: {(string)_fabricante}\nMODELO: {_modelo}\nVELOCIDAD MAXIMA: {VelocidadMaxima}\nPRECIO: ${_precio}";
        }

        public static bool operator ==(Vehiculo vehiculo1, Vehiculo vehiculo2)  //operador que compara dos objetos Vehiculo y devuelve true si tienen el mismo modelo y fabricante.
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
        public virtual string ToString()  //Metodo que devuelve una cadena que representa la informacion del vehiculo, utilizando Mostrar().
        {
            return Mostrar();
        }

    }

}
