using System.Collections.Generic;

namespace CookTime {
    /// <summary>
    /// The class for Recipe creations on the client side of the application.
    /// Used to store attributes for a recipe received in a serialized file throught REST requests.
    /// </summary>
    public class Recipe {
        public string name;
        public string authorEmail;
        public string dishTime;
        public int portions;
        public int duration; //measured in minutes
        public string dishType;
        public int difficulty;
        public List<string> dishTags;
        public string picture;
        public List<string> ingredients;
        public List<string> instructions;
        public float price;
        public int id;
        public long realPostedTime;
        public string showTime;
        public float score;
        public int scoreTimes;
        public List<string> comments;
        public List<string> ratedBy;

        /// <summary>
        /// public constructor for Recipe class
        /// </summary>
        /// <param name="name"> the name of the recipe</param>
        /// <param name="authorEmail"> the reference to the creator's id email</param>
        /// <param name="dishTime">the time of the day category</param>
        /// <param name="portions">amount of portions category</param>
        /// <param name="duration">time it takes to follow the recipe</param>
        /// <param name="dishType">type of dish category</param>
        /// <param name="difficulty">rating of difficulty category</param>
        /// <param name="dishTags">tags such as vegan, keto in tag category</param>
        /// <param name="picture">a picture name to show the picture loaded from server</param>
        /// <param name="ingredients">list of ingredients needed for the recipe</param>
        /// <param name="instructions">list of steps to follow the recipe</param>
        /// <param name="price">estimated cost of the recipe in a business</param>
        /// <param name="id">numeric id to get recipe data from server</param>
        /// <param name="realPostedTime">numeric value of posted time</param>
        /// <param name="showTime">posted time string to show in recipe profile</param>
        /// <param name="score">rating value category</param>
        /// <param name="scoreTimes"></param>
        /// <param name="comments">list of comments left under the recipe profile</param>
        /// <param name="ratedBy">reference to the users that have rated the recipe</param>
        public Recipe(string name, string authorEmail, string dishTime, int portions, int duration, string dishType, int difficulty, List<string> dishTags,
            string picture, List<string> ingredients, List<string> instructions, float price, int id, long realPostedTime, string showTime, 
            float score, int scoreTimes, List<string> comments, List<string> ratedBy) {
            this.name = name; 
            this.authorEmail = authorEmail;
            this.dishTime = dishTime;
            this.portions = portions;
            this.duration = duration;
            this.dishType = dishType;
            this.difficulty = difficulty;
            this.dishTags = dishTags;
            this.picture = picture;
            this.ingredients = ingredients;
            this.instructions = instructions;
            this.price = price;
            this.id = id;
            this.realPostedTime = realPostedTime;
            this.showTime = showTime;
            this.score = score;
            this.scoreTimes = scoreTimes;
            this.comments = comments;
            this.ratedBy = ratedBy;
        }
    }
}