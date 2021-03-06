﻿using System;
using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;
using CookTime.Adapters;

namespace CookTime.Activities {
    /// <summary>
    /// This class represents the Newsfeed view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class NewsfeedActivity : AppCompatActivity {
        private User _loggedUser;
        private Button _profileButton;
        private Button _searchButton;
        private ListView _newsfeedList;
        private List<string> _recipes;

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
        /// It contains the logic for the buttons shown in the first view.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Newsfeed);
            
            var json = Intent.GetStringExtra("User");
            _loggedUser = JsonConvert.DeserializeObject<User>(json);
            
            _profileButton = FindViewById<Button>(Resource.Id.profileButton);
            _profileButton.Click += ProfileClick;

            _searchButton = FindViewById<Button>(Resource.Id.searchButton);
            _searchButton.Click += SwapClick;
            
            _newsfeedList = FindViewById<ListView>(Resource.Id.recipeList);

            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            var url = "resources/newsfeed?email=" + _loggedUser.email;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var request = webClient.DownloadString(url);
            var response = JsonConvert.DeserializeObject<List<string>>(request);
            _recipes = response;

            var recipeAdapter = new RecipeAdapter(this, _recipes);
            _newsfeedList.Adapter = recipeAdapter;
            _newsfeedList.ItemClick += ListClick;
        }

        private void ProfileClick(object sender, EventArgs e)
        {
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

            var url = "resources/getUser?id=" + _loggedUser.email;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var request = webClient.DownloadString(url);
            

            var profileIntent = new Intent(this, typeof(MyProfileActivity));
            // passing the serialized User object as an intent extra with json string format
            profileIntent.PutExtra("User", request);

            StartActivity(profileIntent);
            OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
            Finish();
        }

        /// <summary>
        /// This method handles the clicking on a list view item event
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="eventArgs"> Contains the event data </param>
        private void ListClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            var recipeId = _recipes[eventArgs.Position].Split(';')[0];
            
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

            var url = "resources/getRecipe?id=" + recipeId;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var request = webClient.DownloadString(url);
            
            var recipeIntent = new Intent(this, typeof(RecipeActivity));
            recipeIntent.PutExtra("Recipe", request);
            recipeIntent.PutExtra("LoggedId", _loggedUser.email);
            StartActivity(recipeIntent);
            OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
        }

        private void SwapClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SearchActivity));

            var usJson = JsonConvert.SerializeObject(_loggedUser);
            intent.PutExtra("User", usJson);
            StartActivity(intent);
            OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
        }
    }
}