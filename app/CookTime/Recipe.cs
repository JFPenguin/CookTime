using System.Collections.Generic;

namespace CookTime {
    /// <summary>
    /// The class for Recipe creations on the client side of the application.
    /// Used to store attributes for a recipe received in a serialized file throught REST requests.
    /// </summary>
    public class Recipe {
        public int id;
        public int businessId;
        public string authorEmail;
        public string authorName;
        public List<string> comments;
        public int difficulty;
        public List<string> dishTags;
        public string dishTime;
        public string dishType;
        public int duration;
        public List<string> ingredientsList;
        public List<string> instructions;
        public string name;
        public string photo;
        public int portions;
        public string postTimeString;
        public float price;
        public float score;
        public int scoreTimes;

        /// <summary>
        /// Constructor for the Recipe class
        /// </summary>
        /// <param name="email"> The email of the user that created the recipe </param>
        /// <param name="recipeName"> The recipe's name </param>
        /// <param name="phot">the selected picture in base64 string format</param>
        /// <param name="diff"> The recipe's difficulty </param>
        /// <param name="tags"> The recipe's tags </param>
        /// <param name="time">The recipe's dishTime </param>
        /// <param name="type"> The recipe's dish type </param>
        /// <param name="durat"> The recipe's duration </param>
        /// <param name="ingredients"> The recipe's ingredients </param>
        /// <param name="instruct"> The recipe's instructions </param>
        /// <param name="port"> The recipe's portions </param>
        /// <param name="bsns"> Business id </param>
        /// <param name="cost"> The recipe's price </param>
        
        public Recipe(string email, string recipeName,string phot, int diff, List<string> tags, string time, string type, int durat, 
            List<string> ingredients, List<string> instruct, int port, float cost, int bsns) {
            authorEmail = email;
            name = recipeName;
            difficulty = diff;
            photo = phot;
            dishTags = tags;
            dishTime = time;
            dishType = type;
            duration = durat;
            ingredientsList = ingredients;
            instructions = instruct;
            portions = port;
            price = cost;
            businessId = bsns;
        }
    }
}