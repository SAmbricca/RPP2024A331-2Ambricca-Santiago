using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    //Interfaz que define un contrato para clases que puedan realizar la deserialización desde un archivo XML a un objeto de tipo Fruta
    public interface IDeserializar
    {
        //Metodo Xml que recibe un archivo (path) y devuelve un bool, indicando si se deserializo o no
        bool Xml(string path, out Fruta fruta);
    }
}
