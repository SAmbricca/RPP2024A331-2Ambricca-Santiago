using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Banana : Fruta
    {
        protected string _paisOrigen;

        public string Nombre
        {
            get { return "Banana"; }
        }

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

        public override bool TieneCarozo
        {
            get { return false; }
            set {; }
        }
        public Banana() { }

        public Banana(string color, double peso, string paisOrigen)
            : base(color, peso)
        {
            _paisOrigen = paisOrigen;
        }

        protected override string FrutasToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Fruta: {Nombre}");
            sb.AppendLine(base.FrutasToString());
            sb.AppendLine($"Pais de origen: {_paisOrigen}");

            return sb.ToString();
        }

        public override string ToString()
        {
            return FrutasToString();
        }
    }
}
