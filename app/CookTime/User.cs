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
        public string photo;
        public List<string> notifications;
        public bool chef;
        public float chefScore;
        public int business;

        /// <summary>
        /// Constructor for the User class
        /// </summary>
        /// <param name="age"> The user's age </param>
        /// <param name="email"> The user's email </param>
        /// <param name="firstName"> The user's first name </param>
        /// <param name="photo">the user's phooto reference</param>
        /// <param name="lastName"> The user's last name </param>
        /// <param name="password"> The user's password </param>
        public User(int age, string email, string firstName, string photo, string lastName, string password) {
            this.age = age;
            this.email = email;
            this.photo = photo;
            this.firstName = firstName;
            this.lastName = lastName;
            this.password = password;
        }
    }
}