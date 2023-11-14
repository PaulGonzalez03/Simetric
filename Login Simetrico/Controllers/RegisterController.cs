using Login_Simetrico.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Login_Simetrico.Controllers
{
    public class RegisterController : Controller
    {
        DBContext db = new DBContext();
        // GET: Register
        public ActionResult Index()
        {
            //Aes

            return View();
        }
        public ActionResult Registrarse(FormCollection collection)
        {
            string name = collection[0];
            string email = collection[1];
            string password = collection[2];

            using (Aes myAes = Aes.Create())
            {
                // Encrypt the string to an array of bytes.
                string encrypted = GetSHA256(password);

                db.Usuario.Add(new Usuario() { NombreUsuario = name, Correo = email, Contrasena = encrypted });
                
                db.SaveChanges();         
            }



            return RedirectToAction("Index", "Login");
        }
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}