using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using ClassDatos;
using ClassEntidades;

namespace ClassNegocio
{
    public class NegUtilClass
    {
        Conexion con = new Conexion();
        DatosUtilClass Dutil = new DatosUtilClass();

        /// <summary>
        /// Crea la base de datos de la aplicación.
        /// </summary>
        /// <returns></returns>
        public bool IniciaBD()
        {
            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(con.Ruta, con.DbName)))
                {
                    conexion.CreateTable<EntUsuarioClass>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Dutil.Control_Errores(ex);
                return false;
            }
        }

        /// <summary>
        /// guarda los errores producidos por Sqlite.
        /// </summary>
        /// <param name="ex"></param>
        public void Control_Errores(SQLiteException ex)
        {
            Dutil.Control_Errores(ex);
        }

        /// <summary>
        /// guarda los errores por bloque try catch de la app.
        /// </summary>
        /// <param name="ex"></param>
        public void Control_Errores(Exception ex)
        {
            Dutil.Control_Errores(ex);
        }
    }
}
