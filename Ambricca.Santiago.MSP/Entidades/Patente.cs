using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public enum Tipo
    {
        Vieja,
        Mercosur
    }
    public class Patente
    {
        private string _codigoPatente;
        private Tipo _tipoCodigo;
        /// <summary>
        /// prop para obtener y setear el codigo de la patente
        /// </summary>
        public string CodigoPatente
        {
            get { return _codigoPatente; }
            set { _codigoPatente = value; }
        }
        /// <summary>
        /// Prop para obtener y setear el tipo de la patente Vieja o Mercosur
        /// </summary>
        public Tipo TipoCodigo
        {
            get { return _tipoCodigo; }
            set { _tipoCodigo = value; }
        }
        /// <summary>
        /// ctor vacio de la patente
        /// </summary>
        public Patente()
        {
            
        }
        /// <summary>
        /// ctor que inicializa el codigo de la patente y el tipo de patente (vieja o mercosur)
        /// </summary>
        /// <param name="codigoPatente"></param>
        /// <param name="tipoCodigo"></param>
        public Patente(string codigoPatente, Tipo tipoCodigo)
        {
            _codigoPatente = codigoPatente;
            _tipoCodigo = tipoCodigo;
        }
        /// <summary>
        /// Metodo que devuelve el codigo de la patente
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _codigoPatente;
        }
    }
}
