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
    public class LoginController : Controller
    {
        // GET: Login
        private DBContext db = new DBContext();

        public ActionResult Index()
        {
            if (Session["SesionIniciada"] != null)
            {
                if ((bool)Session["SesionIniciada"] == false) {
                    ViewBag.Error = "Error de usuario y/o contraseña";
                }
            }

            return View();
        }
        public ActionResult IniciarSesion(FormCollection collection)
        {
            var email = collection[0];
            var password = collection[1];

            string encrypted = GetSHA256(password);

            var user = db.Usuario.Where(x => x.Correo == email && x.Contrasena == encrypted).FirstOrDefault();


            if (user != null)
            {
                Session["SesionIniciada"] = true;
                Session["nombre"] = user.NombreUsuario;
                return RedirectToAction("Index", "Home");
            }

            Session["SesionIniciada"] = false;

            return RedirectToAction("Index");
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