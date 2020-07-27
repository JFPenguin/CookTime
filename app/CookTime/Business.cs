using System.Collections.Generic;

namespace CookTime {
    /// <summary>
    /// This class represents a Business created by app users.
    /// used for JSON serialization into client-side workable objects.
    /// </summary>
    public class Business {
        public int id;
        public string name;
        public float rating;
        public int scoreTimes;
        public List<string> raters = new List<string>();
        public List<int> publicList = new List<int>();
        public List<int> privateList = new List<int>();
        public string location;
        public List<string> employeeList;
        public string contact;
        public string photo;
        public string businessHours;
        public List<string> followers = new List<string>();
        
        /// <summary>
        /// constructor for the Business class
        /// </summary>
        /// <param name="name">the business name given by the user creator group</param>
        /// <param name="contact"> the email of contact </param>
        /// <param name="businessHours"> the business hours </param>
        /// <param name="employeeList"> list of employees </param>
        public Business(string name, string contact, string photo,  string businessHours, string location, List<string> employeeList)
        {
            this.name = name;
            this.contact = contact;
            this.photo = photo;
            this.businessHours = businessHours;
            this.employeeList = employeeList;
            this.location = location;
        }
    }
}