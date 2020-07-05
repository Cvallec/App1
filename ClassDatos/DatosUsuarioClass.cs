using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using ClassEntidades;

namespace ClassDatos
{
    public class DatosUsuarioClass
    {
        Conexion con = new Conexion();
        DatosUtilClass Dutil = new DatosUtilClass();

        /// <summary>
        /// Inserto un usuario en la base de datos.
        /// </summary>
        /// <param name="usu"></param>
        /// <returns></returns>
        public bool Insertar_Usuario(EntUsuarioClass usu)
        {
            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(con.Ruta, con.DbName)))
                {
                    conexion.Insert(usu);
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
        /// Listo todos los usuarios de la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<EntUsuarioClass> Obtener_usuarios()
        {
            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(con.Ruta, con.DbName)))
                {
                    return conexion.Table<EntUsuarioClass>().ToList();
                    //return conexion.Query<EntUsuarioClass>("SELECT * FROM Usuario");
                }
            }
            catch (SQLiteException ex)
            {
                Dutil.Control_Errores(ex);
                return null;
            }
        }

        /// <summary>
        /// Actualizo los datos del usuario.
        /// </summary>
        /// <param name="usu"></param>
        /// <returns></returns>
        public bool Actualizar_registro_usuario(EntUsuarioClass usu)
        {
            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(con.Ruta, con.DbName)))
                {
                    conexion.Query<EntUsuarioClass>("UPDATE Usuario set Nombre=?, Apellidos=?, Email=? Where Usuario=?", usu.Nombre, usu.Apellidos, usu.EMail, usu.Usuario);
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
        /// Elimino un usuario de la base de datos.
        /// </summary>
        /// <param name="usu"></param>
        /// <returns></returns>
        public bool Eliminar_usuario(EntUsuarioClass usu)
        {
            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(con.Ruta, con.DbName)))
                {
                    conexion.Delete(usu);
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
        /// Listo los datos de un usuario en particular.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Listar_usuario(int Id)
        {
            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(con.Ruta, con.DbName)))
                {
                    conexion.Query<EntUsuarioClass>("SELECT * FROM Usuario Where Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Dutil.Control_Errores(ex);
                return false;
            }
        }
    }
}
