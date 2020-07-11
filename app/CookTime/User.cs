using System.Collections.Generic;

namespace CookTime {
    /// <summary>
    /// This class represents a user of the app.
    /// It is used for JSON serialization.
    /// </summary>
    public class User { 
        public int Age { get; set; }
        public string Email { get; set; }
        public string  FirstName { get; set; }
        public List<string> FollowerEmails { get; set; }
        public  List<string> FollowingEmails { get; set; } 
        public string LastName { get; set; }
        public string Password { get; set; }
        public List<Recipe> RecipeList { get; set; }
        
    }
}