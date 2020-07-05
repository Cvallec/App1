using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ClassEntidades
{
    [Table("Usuario")]
    public class EntUsuarioClass
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string guid { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string EMail { get; set; }
        public DateTime Fecha_creacion { get; set; }
    }
}
