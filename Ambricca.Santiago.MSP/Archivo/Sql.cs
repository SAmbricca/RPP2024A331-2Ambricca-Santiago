using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using System.CodeDom;
namespace Archivo
{
    public class Sql : IArchivo
    {
        private SqlConnection _conexion;
        private SqlCommand _comando;
        /// <summary>
        /// Ctor para inicializar la conexion con la base de datos
        /// </summary>
        public Sql()
        {
            _conexion = new SqlConnection("Data Source=DESKTOP-BQJDIM2\\SQLEXPRESS;Initial Catalog=lab_sp;Integrated Security=True");
            _comando = new SqlCommand
            {
                Connection = _conexion,
                CommandType = System.Data.CommandType.Text
            };
        }
        /// <summary>
        /// Guarda en la base de datos en la tabla de patentes los datos proporcionados por parametro.
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool Guardar(List<Patente> datos)
        {
            try
            {
                _conexion.Open();
                foreach (var patente in datos)
                {
                    _comando.CommandText = $"INSERT INTO patentes (CodigoPatente, TipoCodigo) VALUES ('{patente.CodigoPatente}', '{patente.TipoCodigo}')";
                    _comando.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _conexion.Close();
            }
        }
        /// <summary>
        /// lee los datos de la tabla patentes y retorna la
        /// información obtenida.
        /// </summary>
        /// <returns></returns>
        public List<Patente> Leer()
        {
            List<Patente> patentes = new List<Patente>();
            try
            {
                _conexion.Open();
                _comando.CommandText = "SELECT * FROM patentes";
                using (var reader = _comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string codigoDePatente = reader["patente"].ToString();
                        Tipo tipoPatente=(Tipo)Enum.Parse(typeof(Tipo), reader["tipo"].ToString());
                        patentes.Add(new Patente(codigoDePatente, tipoPatente));
                    }
                }
                return patentes;
            }
            catch
            {
                return null;
            }
            finally
            {
                _conexion.Close();
            }
        }
    }
}
