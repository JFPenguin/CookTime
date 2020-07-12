using System.Collections.Generic;

namespace CookTime {
    /// <summary>
    /// This class represents a user of the app.
    /// It is used for JSON serialization.
    /// </summary>
    public class User {
        public int age;
        public string email;
        public string firstName;
        public List<string> followerEmails;
        public List<string> follingEmails;
        public string lastName;
        public string password;
        public List<Recipe> recipeList;
        
        /// <summary>
        /// Constructor for the User class
        /// </summary>
        /// <param name="age"> The user's age </param>
        /// <param name="email"> The user's email </param>
        /// <param name="firstName"> The user's first name </param>
        /// <param name="followerEmails"> The user's followers' emails </param>
        /// <param name="followingEmails"> The emails of the user that follow this user </param>
        /// <param name="lastName"> The user's last name </param>
        /// <param name="password"> The user's password </param>
        /// <param name="recipeList"> The user's recipe list </param>
        public User(int age, string email, string firstName, List<string> followerEmails, List<string> followingEmails, 
            string lastName, string password, List<Recipe> recipeList)
        {
            this.age = age;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.password = password;
            this.followerEmails = followerEmails;
            this.follingEmails = followingEmails;
            this.recipeList = recipeList;
        }
        
    }
}