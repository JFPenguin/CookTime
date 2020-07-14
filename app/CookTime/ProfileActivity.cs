using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;

namespace CookTime {
    /// <summary>
    /// This class represents the MyMenu view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class ProfileActivity : AppCompatActivity {
        private User _loggedUser;
        private TextView _nameView;
        private TextView _ageView;
        private TextView _emailView;
        private Button _btnFollowers;
        private Button _btnFollowing;
        
        /// <summary>
        /// This method is called when the activity is starting.
        /// It contains the logic for the buttons shown in the first view.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Profile);

            var json = Intent.GetStringExtra("User");
            _loggedUser = JsonConvert.DeserializeObject<User>(json);
            
            _nameView = FindViewById<TextView>(Resource.Id.nameView);
            _ageView = FindViewById<TextView>(Resource.Id.ageView);
            _emailView = FindViewById<TextView>(Resource.Id.emailView);

            _btnFollowers = FindViewById<Button>(Resource.Id.btnFollowers);
            _btnFollowing = FindViewById<Button>(Resource.Id.btnFollowing);

            _nameView.Text = "Name: " + _loggedUser.firstName + " " + _loggedUser.lastName;
            _ageView.Text = "Age: " + _loggedUser.age;
            _emailView.Text = "Email: " + _loggedUser.email;

            _btnFollowers.Text = "FOLLOWERS: " + _loggedUser.followerEmails.Count;
            _btnFollowing.Text = "FOLLOWERS: " + _loggedUser.followingEmails.Count;
        }
    }
}