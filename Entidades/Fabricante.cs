using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Fabricante
    {

        private string _marca;
        private EPais _pais;

        public string Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        public EPais Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        public Fabricante()
        {

        }

        public Fabricante(string marca, EPais pais)
        {
            _marca = marca;
            _pais = pais;
        }

        public static bool operator ==(Fabricante fabricante1, Fabricante fabricante2)
        {
            return fabricante1._marca == fabricante2._marca && fabricante1._pais == fabricante2._pais;
        }
        public static bool operator !=(Fabricante fabricante1, Fabricante fabricante2)
        {
            return !(fabricante1 == fabricante2);
        }

        public static implicit operator string(Fabricante fabricante)
        {
            return $"{fabricante.Marca} - {fabricante.Pais}";
        }


    }
}
