﻿using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CookTime.Adapters;
using CookTime.DialogFragments;
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
        private string pictureUrl;
        private TextView _nameView;
        private TextView _ageView;
        private TextView _chefView;
        private TextView _scoreView;
        private ImageView _pfp;
        private Button _btnFollowers;
        private Button _btnFollowing;
        private Button _btnFollow;
        private Button _btnDate;
        private Button _btnScore;
        private Button _btnDiff;
        private Button _btnRateChef;
        private string sortStr;
        private ListView _menuListView;
        private IList<string> _menuList;
        private RecipeAdapter _adapter;

        /// <summary>
        /// This method is implemented to prompt the user with location permissions request.
        /// </summary>
        /// <param name="requestCode">the return code from the request</param>
        /// <param name="permissions">the permissions requested to the user</param>
        /// <param name="grantResults">communication to system with the permission requests</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
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
            _chefView = FindViewById<TextView>(Resource.Id.chefPText);
            _scoreView = FindViewById<TextView>(Resource.Id.scorePText);

            _pfp = FindViewById<ImageView>(Resource.Id.profilePic);
            _btnFollowers = FindViewById<Button>(Resource.Id.btnFollowers);
            _btnFollowing = FindViewById<Button>(Resource.Id.btnFollowing);
            _btnFollow = FindViewById<Button>(Resource.Id.btnCFollow);
            _btnDate = FindViewById<Button>(Resource.Id.btnPDate);
            _btnScore = FindViewById<Button>(Resource.Id.btnPScore);
            _btnDiff = FindViewById<Button>(Resource.Id.btnPDiff);
            _btnRateChef = FindViewById<Button>(Resource.Id.btnRateChef);
            
            _menuListView = FindViewById<ListView>(Resource.Id.menuListView);

            _nameView.Text = "Name: " + _user.firstName + " " + _user.lastName;
            _ageView.Text = "Age: " + _user.age;
            

            if (!string.IsNullOrEmpty(_user.photo)) {
                pictureUrl = $"http://{MainActivity.Ipv4}:8080/CookTime_war/cookAPI/resources/getPicture?id={_user.photo}";
                Bitmap bitmap = GetImageBitmapFromUrl(pictureUrl);
                _pfp.SetImageBitmap(bitmap);
            }
            if (_user.chef) {
                _chefView.Text = "Chef: yes";
                _scoreView.Text = "Score: " + _user.chefScore;
                _btnRateChef.Visibility = ViewStates.Visible;
            }
            else {
                _chefView.Text = "Chef: no";
                _scoreView.Text = "";
                _btnRateChef.Visibility = ViewStates.Gone;
            }

            _btnFollowers.Text = "FOLLOWERS: " + _user.followerEmails.Count;
            _btnFollowing.Text = "FOLLOWING: " + _user.followingEmails.Count;

            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

            var url = "resources/isFollowing?ownEmail=" + _loggedId + "&followingEmail=" + _user.email;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = webClient.DownloadString(url);

            _btnFollow.Text = response == "0" ? "FOLLOW" : "UNFOLLOW";
            
            url = "resources/myMenu?email=" + _user.email + "&filter=date";
            var send = webClient.DownloadString(url);
            
            _menuList = JsonConvert.DeserializeObject<IList<string>>(send);
            _adapter = new RecipeAdapter(this, _menuList);
            _menuListView.Adapter = _adapter;
            _menuListView.ItemClick += ListClick;

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
                
                var toast = Toast.MakeText(this, "User " + toastText + ". Redirecting to MyProfile...", ToastLength.Short);
                toast.Show();
                
                var intent = new Intent(this, typeof(MyProfileActivity));
                intent.PutExtra("User", userJson);
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);

            };
            
            _btnFollowers.Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(FollowActivity));
                intent.PutExtra("Title", "Followers");
                intent.PutExtra("LoggedId", _loggedId);
                intent.PutStringArrayListExtra("FollowList", _user.followerEmails);
                
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            };
            
            _btnFollowing.Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(FollowActivity));
                intent.PutExtra("Title", "Following");
                intent.PutExtra("LoggedId", _loggedId);
                intent.PutStringArrayListExtra("FollowList", _user.followingEmails);
                
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            };
            
            _btnDate.Click += (sender, args) =>
            {
                sortStr = "date";
                SortMenu();
            };
            
            _btnScore.Click += (sender, args) =>
            {
                sortStr = "score";
                SortMenu();
            };
            
            _btnDiff.Click += (sender, args) =>
            {
                sortStr = "difficulty";
                SortMenu();
            };
            
            _btnRateChef.Click += (sender, args) =>
            {
                //Brings dialog fragment forward
                var transaction = SupportFragmentManager.BeginTransaction();
                var dialogRate = new DialogRate();
                
                dialogRate.Show(transaction, "rate");
                dialogRate.LoggedId = _loggedId;
                dialogRate.ChefId = _user.email;
                dialogRate.Type = 1;
                    
                dialogRate.EventHandlerRate += RateResult;
            };

            _pfp.Click += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(_user.photo)) {
                    var transaction = SupportFragmentManager.BeginTransaction();
                    var dialogPShow = new DialogPShow();

                    dialogPShow.Url = pictureUrl;
                    dialogPShow.TypeText = "Profile Picture";
                    dialogPShow.Show(transaction, "privPfp");
                }
                else {
                    string toastText = "user has not set a profile picture you can view.";
                    Toast _toast = Toast.MakeText(this, toastText, ToastLength.Short);
                    _toast.Show();
                }
            };
        }
        
        /// <summary>
        /// this method is used to obtain an image bitmap from a url.
        /// </summary>
        /// <param name="url">the string url that displays the image.</param>
        /// <returns>a Bitmap type object representing the image to cache it into memory</returns>
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient()){
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return imageBitmap;
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
            
            var recipeIntent = new Intent(this, typeof(RecipeActivity));
            recipeIntent.PutExtra("Recipe", request);
            recipeIntent.PutExtra("LoggedId", _loggedId);
            StartActivity(recipeIntent);
            OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
        }
        
        /// <summary>
        /// This method sorts the Menu of the user according to the sortStr attribute
        /// </summary>
        private void SortMenu()
        {
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            var url = "resources/myMenu?email=" + _user.email + "&filter=" + sortStr;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var request = webClient.DownloadString(url);

            _menuList = JsonConvert.DeserializeObject<IList<string>>(request);
            _adapter.RecipeItems = _menuList;
            _menuListView.Adapter = _adapter;
        }
        
        /// <summary>
        /// This method is in charge of retrieving the result of the Rate Chef dialog fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void RateResult(object sender, SendRateEvent e) {
            Toast toast;
            string toastText;

            switch (e.Message)
            {
                case "-1":
                    toastText = "Please choose a rating";
                    break;
                case "1":
                    toastText = "You already rated this chef.";
                    break;
                default:
                {
                    toastText = "Chef rated! Refreshing the profile...";
                
                    using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                    var url = "resources/getUser?id=" + _user.email;
                
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var userJson = webClient.DownloadString(url);
                
                    var intent = new Intent(this, typeof(PrivProfileActivity));
                    intent.PutExtra("User", userJson);
                    intent.PutExtra("LoggedId", _loggedId);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                    Finish();
                    break;
                }
            }
            toast = Toast.MakeText(this, toastText, ToastLength.Long);
            toast.Show();
        }
    }
}