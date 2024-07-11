using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    //Clase Durazno que hereda de la clase Fruta
    public class Durazno : Fruta
    {
        protected int _cantPelusa;

        //Propiedad que retorne el nombre de la fruta como 'Durazno'
        public string Nombre
        {
            get 
            {
                return "Durazno";
            }
        }

        //Propiedad de lectura virtual que indica si la fruta tiene carozo ('durazno' tiene, retorna true)
        public override bool TieneCarozo
        {
            get 
            {
                return true; 
            }
            set { }
        }

        //Propiedad que permite acceder y modificar la cantidad de pelusa del durazno
        public int CantidadPelusa
        {
            get 
            {
                return _cantPelusa;
            }
            set 
            {
                _cantPelusa = value;
            }
        }

        //Constructor de instancia vacío
        public Durazno()
        {

        }

        //Constructor que inicializa la banana con un color, peso y la cantidad de pelusa. Reutiliza constructor de Fruta
        public Durazno(string color, double peso, int cantPelusa)
            : base(color, peso)
        {
            _cantPelusa = cantPelusa;
        }

        //Metodo protegido que sobreescribe el metodo protegido 'FrutasToString()' de Fruta (reutiliza codigo). Agrega a la cadena la informacion del durazno
        protected override string FrutasToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Fruta: {Nombre}");
            sb.Append(base.FrutasToString()); //Llama al metodo FrutasToString de la clase base (Fruta) para detalles generales
            sb.AppendFormat("Cantidad pelusa: {0}", _cantPelusa);

            return sb.ToString();
        }

        //Sobreescribe el metodo 'ToString()' para devolver la representacion de la cadena del durazno
        //Llama al metodo FrutasToString para obtener la representación detallada de la Manzana
        public override string ToString()
        {
            return FrutasToString();
        }
    }
}