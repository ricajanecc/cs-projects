using Microsoft.AspNetCore.Mvc;
using MyNoteApp.Models;

namespace MyNoteApp.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult Index()
        {
            if (Request.Cookies.ContainsKey("auth")) // if the user is already logged in to the system
            {
                return RedirectToAction("Index", "NotePage"); //redirects the user to the note page
            }
            return View("LogIn");
        }

        public IActionResult LogIn(string userName, string password)
        {
            try
            {
                UserData user = UserData.LogIn(userName, password);
                string session = Convert.ToBase64String(Guid.NewGuid().ToByteArray()); //generates random numbers & letters
                Response.Cookies.Append("auth", session); //creates a cookie that is called "auth" and sends it to the user

                user.Sessions.Add(session);
                UserData.Save();
                return RedirectToAction("Index", "NotePage");
            }
           catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View("LogIn");
            }
        }

        public IActionResult LogOut()
        {
            if (Request.Cookies.ContainsKey("auth")) //checks if the cookie "auth" exists
            {
                UserData user = UserData.FindUser(Request.Cookies["auth"]); //retrieves the user
                
                if (user != null)
                {
                    user.Sessions.Remove(Request.Cookies["auth"]); //deletes the cookie from the user 
                    UserData.Save();
                }

                Response.Cookies.Delete("auth"); //deletes the cookie for the browser
            }

            return RedirectToAction("Index");
        }
    }
}
