using Cadeteria.Entidades;
using Cadeteria.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel();
            return View(usuarioViewModel);
        }

        [HttpPost]
        public IActionResult Login(UsuarioViewModel usuarioViewModel)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                if (ModelState.IsValid)
                {
                    string query = @"SELECT idUsuario, username, rol
                                     FROM Usuarios 
                                     INNER JOIN Roles USING(idRol)
                                     WHERE username = @Username AND password = @Password";
                    SQLiteData.OpenConnection();
                    SQLiteData.Sql_cmd.CommandText = query;
                    SQLiteData.Sql_cmd.Parameters.AddWithValue("@Username", usuarioViewModel.Username);
                    SQLiteData.Sql_cmd.Parameters.AddWithValue("@Password", usuarioViewModel.Password);
                    SQLiteDataReader data = SQLiteData.Sql_cmd.ExecuteReader();
                    if (data.HasRows)
                    {
                        data.Read();
                        HttpContext.Session.SetInt32("IdUsuario", Convert.ToInt32(data["idUsuario"]));
                        HttpContext.Session.SetString("Username", data["username"].ToString());
                        HttpContext.Session.SetString("Rol", data["rol"].ToString());
                        data.Close();
                        SQLiteData.CloseConnection();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        SQLiteData.CloseConnection();
                        ViewBag.Mensaje = "USUARIO NO EXISTENTE";
                        return View("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Rol");

            return RedirectToAction("Index", "Home");
        }
    }
}
