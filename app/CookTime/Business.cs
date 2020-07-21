using System.Collections.Generic;

namespace CookTime {
    /// <summary>
    /// This class represents a Business created by app users.
    /// used for JSON serialization into client-side workable objects.
    /// </summary>
    public class Business {
        private int id;
        private string name;
        private float rating;
        private int scoreTimes;
        private List<string> raters;
        private List<int> privateList;
        private List<int> publicList;
        private string location;
        private List<string> employeeList;
        private string contact;
        private string photo;
        private string businessHours;
        
        /// <summary>
        /// constructor for the Business class
        /// </summary>
        /// <param name="id">unique number to identify the business in the server data</param>
        /// <param name="name">the business name given by the user creator group</param>
        /// <param name="rating">numeric value of the given rating by app users</param>
        /// <param name="scoreTimes">amount of times a specific business has been rated</param>
        /// <param name="raters">reference to which users have rated the business</param>
        /// <param name="privateList">a list of recipes that only the business can manipulate</param>
        /// <param name="publicList">a list of recipes that any app user can see on the recipe profile</param>
        /// <param name="location">a string that represent the given location for a business in GPS coordinates</param>
        /// <param name="employeeList"></param>
        /// <param name="contact"></param>
        /// <param name="photo"></param>
        /// <param name="businessHours"></param>
        public Business(int id, string name, float rating, int scoreTimes, List<string> raters, List<int> privateList,
            List<int> publicList, string location, List<string> employeeList, string contact, string photo, string businessHours)
        {
            this.id = id;
            this.name = name;
            this.rating = rating;
            this.scoreTimes = scoreTimes;
            this.raters = raters;
            this.privateList = privateList;
            this.publicList = publicList;
            this.location = location;
            this.employeeList = employeeList;
            this.contact = contact;
            this.photo = photo;
            this.businessHours = businessHours;
        }
    }
}