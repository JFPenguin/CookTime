using System.Collections.Generic;

namespace CookTime {
    /// <summary>
    /// The class for Recipe creations on the client side of the application.
    /// Used to store attributes for a recipe received in a serialized file throught REST requests.
    /// </summary>
    public class Recipe
    {
        public string authorEmail;
        public List<string> comments;
        public int difficulty;
        public List<string> dishTags;
        public string dishTime;
        public string dishType;
        public int duration;
        public int id;
        public List<string> ingredientsList;
        public List<string> instructions;
        public string name;
        public List<string> photos;
        public int portions;
        public long postTime;
        public string postTimeString;
        public float price;
        public List<string> ratedBy;
        public float score;
        public int scoreTimes;
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
        public Recipe(string authorEmail, int difficulty, string dishTime, string dishType, int duration,
            int id, string name, int portions, long postTime, string postTimeString, float price, float score, int scoreTimes,
            List<string> dishTags, List<string> ratedBy, List<string> photos, List<string> instructions, List<string> ingredientsList, List<string> comments)
        {
            this.authorEmail = authorEmail;
            this.comments = comments;
            this.difficulty = difficulty;
            this.dishTags = dishTags;
            this.dishTime = dishTime;
            this.dishType = dishType;
            this.duration = duration;
            this.id = id;
            this.ingredientsList = ingredientsList;
            this.instructions = instructions;
            this.name = name;
            this.photos = photos;
            this.portions = portions;
            this.postTime = postTime;
            this.postTimeString = postTimeString;
            this.price = price;
            this.ratedBy = ratedBy;
            this.score = score;
            this.scoreTimes = scoreTimes;

        }
    }
}