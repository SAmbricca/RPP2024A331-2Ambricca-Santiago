using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class Fruta
    {
        protected string _color;
        protected double _peso;
        public abstract bool TieneCarozo { get; set; }

        public string Color
        {
            get;
            set;
        }
        public double Peso
        {
            get;
            set;
        }
        public Fruta() { }

        public Fruta(string color, double peso)
        {
            _color = color;
            _peso = peso;
        }
        protected virtual string FrutasToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Color: {Color}");
            sb.AppendLine($"Peso: {Peso}");
            sb.AppendLine($"Tiene carozo: {(TieneCarozo ? "Si" : "No")}"); //condicion ? verdadero : falso. si ? es verdadero, cumple lo que esta dentro de verdadero. Analogo para falso

            return sb.ToString();
        }
    }
}
