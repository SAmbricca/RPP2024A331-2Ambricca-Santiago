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

        public string Marca            //propiedad que devuelve y establece la marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        public EPais Pais              //propiedad que devuelve y establece el pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        public Fabricante()            //constructor vacío que no inicializa nada
        {

        }

        public Fabricante(string marca, EPais pais)     //constructor que inicializa la instancia con una marca y un pais
        {
            _marca = marca;
            _pais = pais;
        }

        public static bool operator ==(Fabricante fabricante1, Fabricante fabricante2)              //operador que verifica si las marcas y paises de los dos fabricantes son iguales
        {
            return fabricante1._marca == fabricante2._marca && fabricante1._pais == fabricante2._pais;
        }
        public static bool operator !=(Fabricante fabricante1, Fabricante fabricante2)              //operador que vefirica lo contrario al anterior
        {
            return !(fabricante1 == fabricante2);
        }

        public static implicit operator string(Fabricante fabricante)     //operador de implicito que devuelve una cadena que representa la marca y el pais del fabricante
        {
            return $"{fabricante.Marca} - {fabricante.Pais}";
        }


    }
}
