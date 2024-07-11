using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    //Interfaz que define un contrato para que la clase pueda realizar la serialización a XML
    public interface ISerializar
    {
        //Metodo que recibe la ruta del archivo (path) donde va a serializar el objeto. Devolvera un booleano que indica si fue exitosa o no la serialización.
        bool Xml(string path);
    }
}
