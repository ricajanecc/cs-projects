using Microsoft.AspNetCore.Mvc;
using MyNoteApp.Models;

namespace MyNoteApp.Controllers
{
    public class NotePageController : Controller
    {
        public IActionResult Index()
        {
            if (Request.Cookies.ContainsKey("auth")) //checks if the cookie "auth" exists
            {
                UserData user = UserData.FindUser(Request.Cookies["auth"]);

                if(user == null)
                {
                    return RedirectToAction("LogOut", "LogIn");
                }

                ViewData["Name"] = user.FirstName; //if user is found, the user's nane will be mentioned on the view.
                return View("NotePage");
            }
            else //else, the user is redirected to the main page and has to sign in
            {
                return RedirectToAction("Index", "Home"); 
            }
        }

        [HttpPost]
        public IActionResult NoteWrite(string title, string text, string id)
        {
            if (title == null || text == null)
            {
                return Content("Note cannot be empty!");
            }

            if (Request.Cookies.ContainsKey("auth")) 
            {
                UserData user = UserData.FindUser(Request.Cookies["auth"]);
                if (user == null)
                {
                    return RedirectToAction("LogOut", "LogIn");
                }

                if (id == null)
                {
                    Note newNote = new Note(title, DateTime.Now, text); //creates new note
                    user.Notes.Add(newNote); //adds to the user's note list
                    UserData.Save();
                    return Content("Note created!");
                }
                else
                {
                    for (int i = 0; i < user.Notes.Count; i++) //goes through the notes of the user 
                    {
                        Note editNote = user.Notes[i]; //assigns the note to editNote
                        if (editNote.NoteId == id) //if the note id is equals to the id of the request 
                        {
                            editNote.Title = title; //changes the note's title to the title of the request
                            editNote.Text = text; //note's text to the text of the request
                            editNote.Date = DateTime.Now;
                            UserData.Save();
                            return Content("note edited"); //returns note edited when the note has been edited
                        }
                    }
                    return Content("note cannot be found."); //if the note is not found, it returns "note cannot be found"
                }
              
            }
            else 
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ListNote()
        {
            if (Request.Cookies.ContainsKey("auth"))
            {
                UserData user = UserData.FindUser(Request.Cookies["auth"]);
                if (user == null)
                {
                    return RedirectToAction("LogOut", "LogIn");
                }

                return Json(user.Notes); //sends json of the user's list 
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost] 
        public IActionResult NoteDelete(string id)
        {
            if (Request.Cookies.ContainsKey("auth"))
            {
                UserData user = UserData.FindUser(Request.Cookies["auth"]);
                if (user == null)
                {
                    return RedirectToAction("LogOut", "LogIn");
                }

                for (int i = 0; i < user.Notes.Count; i++) 
                {
                    Note editNote = user.Notes[i]; 
                    if (editNote.NoteId == id) 
                    {
                        user.Notes.RemoveAt(i);
                        UserData.Save();
                        return Content("Note deleted.");
                    }
                }
                return Content("note cannot be found."); //if the note is not found, it returns "note cannot be found"
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
