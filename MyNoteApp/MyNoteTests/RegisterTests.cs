using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNoteApp.Models;
using System;

namespace MyNoteTests
{
    [TestClass]
    public class RegisterTests
    {
        [TestMethod] 
        public void SuccessRegister()
        {
            UserData firstUser = UserData.Register("a", "b", "ab", "123", "123");

            Assert.AreEqual(firstUser.FirstName, "a"); //tests that the firstName is correct 
        }

        [TestMethod]
        public void RequiredFields()
        {
            Assert.ThrowsException<ArgumentNullException>(() => UserData.Register(null, null, null, null, null)); //tests that the method throws an exception when the inputs are null
        }

        [TestMethod]
        public void UserAlreadyExists()
        {
            UserData.Register("b", "b", "b", "b", "b"); //created a user with a username b
            Assert.ThrowsException<ArgumentException>(() => UserData.Register("b", "b", "b", "b", "b")); //tests that the method throws an exception when the username b is repeated
        }

        [TestMethod]
        public void PasswordsNotMatch()
        {
            Assert.ThrowsException<ArgumentException>(() => UserData.Register("b", "b", "c", "b", "c")); //tests that the method throws an exception when the password and the confirm password are not matched
        }
    }
}
