using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNoteApp.Models;
using System;

namespace MyNoteTests
{
    [TestClass]
    public class FindUserTests
    {
        [TestMethod]
        public void FindUser()
        {
            UserData user = new UserData("a", "b", "c", "d"); //creates a new user 
            user.Sessions.Add("Hello"); //adds a session to the user 
            UserData.UserList.Add(user); //adds the user to the UserList

            Assert.AreEqual(user, UserData.FindUser("Hello")); //tests if the user can be found by its session
        }

        [TestMethod]
        public void NotFoundUser()
        {
            Assert.IsNull(UserData.FindUser("hej")); //tests that the user cannot be found if the session does not exist
        }
    }
}
