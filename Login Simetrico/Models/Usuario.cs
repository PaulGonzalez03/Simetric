using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Login_Simetrico.Models
{
    
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Ekey { get; set; }
    }

    public class DBContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
    }
    
}