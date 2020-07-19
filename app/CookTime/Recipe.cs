using System.Collections.Generic;

namespace CookTime {
    /// <summary>
    /// The class for Recipe creations on the client side of the application.
    /// Used to store attributes for a recipe received in a serialized file throught REST requests.
    /// </summary>
    public class Recipe {
        public int id;
        public string authorEmail;
        public List<string> comments;
        public int difficulty;
        public List<string> dishTags;
        public string dishTime;
        public string dishType;
        public int duration;
        public List<string> ingredientsList;
        public List<string> instructions;
        public string name;
        public string photos;
        public int portions;
        public string postTimeString;
        public float price;
        public float score;
        public int scoreTimes;
    }
}