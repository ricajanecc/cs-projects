using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNoteApp.Models;
using System;


namespace MyNoteTests
{
    [TestClass]
   public class LogInTests
    {
        [TestMethod]
        public void LogInSuccess()
        {
            UserData user = new UserData("a", "b", "f", "123"); //creates a new user 
            UserData.UserList.Add(user); //adds user to the UserList

            Assert.AreEqual(user, UserData.LogIn("f", "123")); //tests if the user can log in with the correct credentials
        }

        [TestMethod]
        public void LogInError()
        {
            Assert.ThrowsException<Exception>(() => UserData.LogIn("xyz", "456")); //tests that the method throws an exception when the user logs in with incorrect credentials
           
        }
    }
}
