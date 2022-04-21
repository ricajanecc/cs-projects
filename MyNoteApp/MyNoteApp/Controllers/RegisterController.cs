using MyNoteApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;
namespace MyNoteApp.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            if (Request.Cookies.ContainsKey("auth")) // if the user is already logged in to the system
            {
                return RedirectToAction("Index", "NotePage"); //redirects the user to the note page
            }

            return View("Register");
        }

        [HttpPost] 
        public IActionResult Register(string firstName, string lastName, string userName, string password, string confirmPassword)
        {
            try //if there are errors such as username already exist or passwords are not matched. 
            { 
                UserData user = UserData.Register(firstName, lastName, userName, password, confirmPassword);
                string session = Convert.ToBase64String(Guid.NewGuid().ToByteArray()); //generates random numbers & letters
                Response.Cookies.Append("auth", session); //creates a cookie that is called "auth" and sends it to the user

                user.Sessions.Add(session);
                UserData.Save();
                return RedirectToAction("Index", "NotePage");
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message; //error message gets printed on the view
                return View("Register");
            }
            
        }

        
    }
}
