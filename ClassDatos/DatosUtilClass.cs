using System;
using System.Collections.Generic;
using System.Text;
using ClassEntidades;
using SQLite;

namespace ClassDatos
{
    public class DatosUtilClass
    {
        Conexion con = new Conexion();

        /// <summary>
        /// Guardo los errores que se produzcan en sqlite.
        /// </summary>
        /// <param name="ex"></param>
        public void Control_Errores(SQLiteException ex)
        {
            var error = new EntErroresClass
            {
                Mensaje = ex.Message,
                Fecha = DateTime.Now.ToString(),
                Usuario = DatosGlobalClass.Usuario
            };
            using (var conexao = new SQLiteConnection(System.IO.Path.Combine(con.Ruta, con.DbName)))
            {
                conexao.Insert(error);
            }
        }

        /// <summary>
        /// Guardo los errores que se produzcan por bloques try catch de la app.
        /// </summary>
        /// <param name="ex"></param>
        public void Control_Errores(Exception ex)
        {
            var error = new EntErroresClass
            {
                Mensaje = ex.Message,
                Fecha = DateTime.Now.ToString(),
                Usuario = DatosGlobalClass.Usuario
            };
            using (var conexao = new SQLiteConnection(System.IO.Path.Combine(con.Ruta, con.DbName)))
            {
                conexao.Insert(error);
            }
        }
    }
}
