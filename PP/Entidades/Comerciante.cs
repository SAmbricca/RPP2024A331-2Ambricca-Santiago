using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Comerciante
    {
        private string _apellido;
        private string _nombre;

        public string Nombre 
        {
            get { return _nombre; }
            set { _nombre = value;}
        }

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        public Comerciante()
        {

        }

        public Comerciante(string nombre, string apellido)
        {
            _nombre = nombre;
            _apellido = apellido;
        }

        public static bool operator == (Comerciante comerciante1, Comerciante comerciante2)
        {
            return comerciante1._nombre == comerciante2._nombre && comerciante1._apellido == comerciante2._apellido;
        }

        public static bool operator !=(Comerciante comerciante1, Comerciante comerciante2)
        {
            return !(comerciante1 == comerciante2);
        }

        public static implicit operator string (Comerciante comerciante)
        {
            return $"{comerciante.Apellido}, {comerciante.Nombre}";
        }




    }


}
