using System;
using Newtonsoft.Json;
namespace MyNoteApp.Models

{
    public class UserData
    {
        public string FirstName;
        public string LastName;
        public string UserName;
        public string Password;
        public static List<UserData> UserList = new List<UserData>();
        public List<Note> Notes = new List<Note>();
        public List<string> Sessions = new List<string>();

        public UserData(string firstName, string lastName, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
        }

        public static UserData LogIn(string userName, string password)
        {
            for (int i = 0; i < UserList.Count; i++)
            {
                if (userName == UserList[i].UserName)
                {
                    if (password == UserList[i].Password)
                    {
                        return UserList[i];
                    }
                }
            }
            throw new Exception("The username or password is incorrect.");
        }

        public static UserData Register(string firstName, string lastName, string userName, string password, string confirmPassword) //Registry of users
        {
            if (firstName == null || userName == null || password == null || confirmPassword == null)
            {
                throw new ArgumentNullException("Fields required.");
            }


            for (int i = 0; i < UserList.Count; i++) 
            {
                if (userName == UserList[i].UserName)
                {
                    throw new ArgumentException("This user already exists.");
                }
            }

            if (password != confirmPassword)
            {
                throw new ArgumentException("Passwords do not match.");
            }
            UserData user = new UserData(firstName, lastName, userName, password);
            UserList.Add(user);
            return user;
        }

        public static UserData FindUser(string session)
        {
            for (int i = 0; i < UserList.Count; i++) //goes through the list of user 
            {
                for (int j = 0; j < UserList[i].Sessions.Count; j++) //goes through the user's session list
                {
                    if (session == UserList[i].Sessions[j]) //checks if the value of auth is inside the user's session list
                    {
                        return UserList[i]; 
                    }
                }
            }
            return null;
        }

        public static void Save()
        {
            File.WriteAllText("people.json", JsonConvert.SerializeObject(UserList));
        }

        public static void Load()
        {
            if (File.Exists("people.json")) //checks if the file exists
            {
                try
                {
                    UserList = JsonConvert.DeserializeObject<List<UserData>>(File.ReadAllText("people.json"));
                }
                catch (Exception ex) //if it throws an exception, it clears the UserList
                {
                    UserList = new List<UserData>(); 
                }
            }
            else //if the file does not exist, it clears the UserList
            {
                UserList = new List<UserData>(); 
            }
        }
    }
}
