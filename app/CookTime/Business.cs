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
        /// <param name="name">the business name given by the user creator group</param>
        /// <param name="contact"></param>
        /// <param name="businessHours"></param>
        public Business(string name, string contact, string businessHours)
        {
            this.name = name;
            this.contact = contact;
            this.businessHours = businessHours;
        }
    }
}