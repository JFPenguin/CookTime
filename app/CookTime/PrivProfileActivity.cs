using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;

namespace CookTime {
    /// <summary>
    /// This class represents the Private Profile view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class PrivProfileActivity : AppCompatActivity {
        private User _user;
        private string _loggedId;
        private TextView _nameView;
        private TextView _ageView;
        private Button _btnFollowers;
        private Button _btnFollowing;
        private Button _btnFollow;

        /// <summary>
        /// This method is called when the activity is starting.
        /// It contains the logic for the buttons shown in this activity.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.PrivateProfile);

            _loggedId = Intent.GetStringExtra("LoggedId");
            
            var json = Intent.GetStringExtra("User");
            _user = JsonConvert.DeserializeObject<User>(json);
            
            _nameView = FindViewById<TextView>(Resource.Id.nameView);
            _ageView = FindViewById<TextView>(Resource.Id.ageView);

            _btnFollowers = FindViewById<Button>(Resource.Id.btnFollowers);
            _btnFollowing = FindViewById<Button>(Resource.Id.btnFollowing);
            _btnFollow = FindViewById<Button>(Resource.Id.btnFollow);

            _nameView.Text = "Name: " + _user.firstName + " " + _user.lastName;
            _ageView.Text = "Age: " + _user.age;

            _btnFollowers.Text = "FOLLOWERS: " + _user.followerEmails.Count;
            _btnFollowing.Text = "FOLLOWING: " + _user.followingEmails.Count;

            _btnFollowers.Click += (sender, args) =>
            {
                Intent intent = new Intent(this, typeof(FollowActivity));
                intent.PutExtra("Title", "Followers");
                intent.PutExtra("LoggedId", _loggedId);
                intent.PutStringArrayListExtra("FollowList", _user.followerEmails);
                
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            };
            
            _btnFollowing.Click += (sender, args) =>
            {
                Intent intent = new Intent(this, typeof(FollowActivity));
                intent.PutExtra("Title", "Following");
                intent.PutExtra("LoggedId", _loggedId);
                intent.PutStringArrayListExtra("FollowList", _user.followingEmails);
                
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            };
        }
    }
}