using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Durazno : Fruta
    {
        protected int _cantPelusa;

        public string Nombre
        {
            get { return "Durazno"; }
        }

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

        public override bool TieneCarozo
        {
            get { return true; }
            set {; }
        }
        public Durazno() { }

        public Durazno(string color, double peso, int cantPelusa)
            : base(color, peso)
        {
            _cantPelusa = cantPelusa;
        }

        protected override string FrutasToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Fruta: {Nombre}");
            sb.AppendLine(base.FrutasToString());
            sb.AppendLine($"Pais de origen: {_cantPelusa}");

            return sb.ToString();
        }

        public override string ToString()
        {
            return FrutasToString();
        }
    }
}
