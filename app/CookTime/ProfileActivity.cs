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
        private Button _btnFollowers;
        private Button _btnFollowing;
        private Button _btnSettings;
        private Toast _toast;
        
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

            _btnFollowers = FindViewById<Button>(Resource.Id.btnFollowers);
            _btnFollowing = FindViewById<Button>(Resource.Id.btnFollowing);
            _btnSettings = FindViewById<Button>(Resource.Id.btnSettings);

            _nameView.Text = "Name: " + _loggedUser.firstName + " " + _loggedUser.lastName;
            _ageView.Text = "Age: " + _loggedUser.age;

            _btnFollowers.Text = "FOLLOWERS: " + _loggedUser.followerEmails.Count;
            _btnFollowing.Text = "FOLLOWING: " + _loggedUser.followingEmails.Count;
            
            _btnSettings.Click += (sender, args) =>
            {
                //Brings dialog fragment forward
                var transaction = SupportFragmentManager.BeginTransaction();
                var dialogSettings = new DialogSettings();
                dialogSettings.Show(transaction, "settings");

                dialogSettings.Email = _loggedUser.email;
                dialogSettings.EventHandlerPass += PassResult;
            };
        }
        
        /// <summary>
        /// This method is in charge of retrieving the data entered by the user in the Settings dialog fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void PassResult(object sender, SendPassEvent e) {
            string toastText;
            if (e.Message == "3") {
                toastText = "Please fill in all of the information";
            }
            else if (e.Message == "2") {
                toastText = "The new passwords do not match";
            }
            else if (e.Message == "0") {
                toastText = "Wrong current password";
            }
            else {
                toastText = "Password changed successfully";
            }

            _toast = Toast.MakeText(this, toastText, ToastLength.Short);
            _toast.Show();
        }
    }
}