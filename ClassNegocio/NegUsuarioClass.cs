using System;
using System.Collections.Generic;
using System.Text;
using ClassEntidades;
using ClassDatos;
using SQLite;

namespace ClassNegocio
{
    public class NegUsuarioClass
    {
        DatosUsuarioClass Dusu = new DatosUsuarioClass();
        DatosUtilClass Dutil = new DatosUtilClass();

        /// <summary>
        /// Recibo la clase de usuarios para insertar nuevos usuarios.
        /// </summary>
        /// <param name="usu"></param>
        /// <returns></returns>
        public bool Insertar_Usuario(EntUsuarioClass usu)
        {
            try
            {
                Dusu.Insertar_Usuario(usu);
                return true;
            }
            catch (Exception ex)
            {
                Dutil.Control_Errores(ex);
                return false;
            }
        }

        /// <summary>
        /// Obtengo los usuarios de la base de datos
        /// </summary>
        /// <returns></returns>
        public List<EntUsuarioClass> Obtener_usuarios()
        {
            List<EntUsuarioClass> lusuario = new List<EntUsuarioClass>();

            try
            {
                lusuario= Dusu.Obtener_usuarios();
                return lusuario;
            }
            catch (Exception ex)
            {
                Dutil.Control_Errores(ex);
                return null;
            }
        }

        /// <summary>
        /// Actualizo los datos del usuario según el registro de la primera vez.
        /// </summary>
        /// <param name="usu"></param>
        /// <returns></returns>
        public bool Actualizar_registro_usuario(EntUsuarioClass usu)
        {
            try
            {
                Dusu.Insertar_Usuario(usu);
                return true;
            }
            catch (Exception ex)
            {
                Dutil.Control_Errores(ex);
                return false;
            }
        }
    }
}
