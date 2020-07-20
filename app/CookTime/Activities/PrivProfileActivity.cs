using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using CookTime.Adapters;
using Newtonsoft.Json;

namespace CookTime.Activities {
    /// <summary>
    /// This class represents the Private Profile view.
    /// It is used to see profiles from other users.
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
        private ListView _menuListView;
        private IList<string> _menuList;

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

            _menuListView = FindViewById<ListView>(Resource.Id.menuListView);

            _nameView.Text = "Name: " + _user.firstName + " " + _user.lastName;
            _ageView.Text = "Age: " + _user.age;

            _btnFollowers.Text = "FOLLOWERS: " + _user.followerEmails.Count;
            _btnFollowing.Text = "FOLLOWING: " + _user.followingEmails.Count;

            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

            var url = "resources/isFollowing?ownEmail=" + _loggedId + "&followingEmail=" + _user.email;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = webClient.DownloadString(url);

            url = "resources/myMenu?email=" + _user.email + "&filter=date";
            var send = webClient.DownloadString(url);
            
            _menuList = JsonConvert.DeserializeObject<IList<string>>(send);
            RecipeAdapter adapter = new RecipeAdapter(this, _menuList);
            _menuListView.Adapter = adapter;
            _menuListView.ItemClick += ListClick;
            
            _btnFollow.Text = response == "0" ? "FOLLOW" : "UNFOLLOW";
            
            _btnFollow.Click += (sender, args) =>
            {
                using var webClient2 = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                var followUrl = "resources/followUser?ownEmail=" + _loggedId + "&followingEmail=" + _user.email;
                webClient2.Headers[HttpRequestHeader.ContentType] = "application/json";
                var answer = webClient2.DownloadString(followUrl);

                followUrl = "resources/getUser?id=" + _loggedId;
                webClient2.Headers[HttpRequestHeader.ContentType] = "application/json";
                var userJson = webClient2.DownloadString(followUrl);
                
                _btnFollow.Text = answer == "0" ? "FOLLOW" : "UNFOLLOW";

                var toastText = answer == "0" ? "unfollowed" : "followed";
                
                Toast toast = Toast.MakeText(this, "User " + toastText + ". Redirecting to MyProfile...", ToastLength.Short);
                toast.Show();
                
                Intent intent = new Intent(this, typeof(MyProfileActivity));
                intent.PutExtra("User", userJson);
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);

            };
            
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
        
        /// <summary>
        /// This method handles the clicking on a list view item event
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="eventArgs"> Contains the event data </param>
        private void ListClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            var recipeId = _menuList[eventArgs.Position].Split(';')[0];
            
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
        
            var url = "resources/getRecipe?id=" + recipeId;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var request = webClient.DownloadString(url);
            
            Intent recipeIntent = new Intent(this, typeof(RecipeActivity));
            recipeIntent.PutExtra("Recipe", request);
            recipeIntent.PutExtra("LoggedId", _loggedId);
            StartActivity(recipeIntent);
            OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
        }
    }
}