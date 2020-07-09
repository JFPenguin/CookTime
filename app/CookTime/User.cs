using Android.Service.QuickSettings;

namespace CookTime {
    public class User {
        public int age;
        public string email;
        public string firstName;
        public string lastName;
        public string password;

        public User(int age, string email, string firstName, string lastName, string password) {
            this.age = age;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.password = password;
        }
    }
}