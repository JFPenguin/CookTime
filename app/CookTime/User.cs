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
        public List<string> followingEmails;
        public string lastName;
        public string password;
        public List<string> notifications;
        public bool isChef;
        public float chefScore;

        /// <summary>
        /// Constructor for the User class
        /// </summary>
        /// <param name="age"> The user's age </param>
        /// <param name="email"> The user's email </param>
        /// <param name="firstName"> The user's first name </param>
        /// <param name="lastName"> The user's last name </param>
        /// <param name="password"> The user's password </param>
        public User(int age, string email, string firstName, string lastName, string password) {
            this.age = age;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.password = password;
        }
    }
}