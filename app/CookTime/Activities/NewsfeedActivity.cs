using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.App.Usage;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;

namespace CookTime.Activities {
    /// <summary>
    /// This class represents the Newsfeed view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class NewsfeedActivity : AppCompatActivity {
        private User _loggedUser;
        private List<string> _followedMails;
        private List<User> _followed;
        private Toast _toast;
        private Button _profileButton;
        

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

            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            var url = "resources/newsfeed?id=" + _loggedUser.email;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var request = webClient.DownloadString(url);
            var response = JsonConvert.DeserializeObject<string>(request);
            //TODO solve request-response relation to properly get the newsfeed from server
        }

        private void ProfileClick(object sender, UsageEvents.Event e)
        {
            string toastText = "opening MyProfile...";
            _toast = Toast.MakeText(this, toastText, ToastLength.Short);
            _toast.Show();
            
            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

            var url = "resources/getUser?id=" + _loggedUser.email;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var send = webClient.DownloadString(url);
            Intent profileIntent = new Intent(this, typeof(MyProfileActivity));
            Intent.PutExtra("User", send);
            StartActivity(profileIntent);
            OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
            Finish();
            
        }
    }
}