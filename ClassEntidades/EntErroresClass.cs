using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ClassEntidades
{
    [Table("Errores")]
    public class EntErroresClass
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Mensaje { get; set; }
        public string Fecha { get; set; }
        public string Usuario { get; set; }
    }
}
