using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    //Clase Banana que hereda de la clase Fruta
    public class Banana : Fruta
    {
        protected string _paisOrigen;

        //Propiedad que retorne el nombre de la fruta como 'Banana'
        public string Nombre           
        {
            get 
            { 
                return "Banana"; 
            }
        }

        //Propiedad de lectura virtual que indica si la fruta tiene carozo ('banana' no tiene, retorna false)
        public override bool TieneCarozo
        {
            get 
            { 
                return false; 
            }
            set { }
        }

        //Propiedad que permite acceder y modificar el país de origen de la banana
        public string PaisOrigen  
        {
            get 
            { 
                return _paisOrigen;
            }
            set 
            { 
                _paisOrigen = value; 
            
            }
        }

        //Constructor de instancia vacío
        public Banana()     
        {

        }

        //Constructor que inicializa la banana con un color, peso y país de origen específicos. Reutiliza constructor de Fruta
        public Banana(string color, double peso, string paisOrigen) 
            : base(color, peso)
        {
            _paisOrigen = paisOrigen;
        }

        //Metodo protegido que sobreescribe el metodo protegido 'FrutasToString()' de Fruta (reutiliza codigo). Agrega a la cadena la informacion de Banana
        protected override string FrutasToString()      
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Fruta: {Nombre}");
            sb.Append(base.FrutasToString()); //Llama al metodo FrutasToString de la clase base (Fruta) para detalles generales
            sb.AppendFormat("Pais de origen: {0}", _paisOrigen);

            return sb.ToString();
        }

        //Sobreescribe el metodo 'ToString()' para devolver la representacion de la cadena de Banana
        //Llama al metodo FrutasToString para obtener la representación detallada de la Manzana
        public override string ToString()  
        {
            return FrutasToString();
        }
    }
}