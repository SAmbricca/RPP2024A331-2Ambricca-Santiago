using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    //Clase utilizada como base para representar las distintas frutas.
    //Es una clase abstracta, lo que significa que no puede ser instanciada directamente. Debe ser heredada por otras clases que implementen sus miembros
    public abstract class Fruta
    {
        protected string _color;
        protected double _peso;

        //Propiedad abstracta que DEBE ser utilizada por las clases derivadas, definiendo si tienen carozo o no
        public abstract bool TieneCarozo 
        { 
            get; 
            set; 
        }

        //Propiedad para leer y escribir el color de la fruta
        public string Color
        {
            get 
            { 
                return _color; 
            }
            set 
            { 
                _color = value; 
            }
        }

        //Propiedad para leer y escribir el peso de la fruta
        public double Peso
        {
            get 
            { 
                return _peso;
            }
            set 
            {
                _peso = value; 
            }
        }

        //Constructor de instancia vacío
        public Fruta()
        {

        }

        //Constructor de instancia que inicializa los atributos
        public Fruta(string color, double peso)
        {
            _color = color;
            _peso = peso;
        }

        //Metodo virtual protegido que devolvera una cadena con el color y el peso de la fruta.
        //Sera sobreescrito por las clases derivadas para que incluyan su información adicional
        protected virtual string FrutasToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Color: {_color}");
            sb.AppendLine($"Peso: {_peso}kg");
            return sb.ToString();
        }
    }
}
