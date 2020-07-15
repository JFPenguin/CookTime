using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;

namespace CookTime {
    /// <summary>
    /// This class represents the Newsfeed view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class NewsfeedActivity : AppCompatActivity {
        private User _loggedUser;
        private List<string> _followedMails;
        private List<User> _followed;
        

        /// <summary>
        /// This method is called when the activity is starting.
        /// It contains the logic for the buttons shown in the first view.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Newsfeed);

            string json = Intent.GetStringExtra("User");
            _loggedUser = JsonConvert.DeserializeObject<User>(json);
            _followedMails = _loggedUser.followingEmails;
            //TODO get each user from the list via API requests to create the user list that allows to obtain the recipes from each user.
        }
    }
}