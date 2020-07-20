﻿using System;
using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CookTime.Adapters;
using Newtonsoft.Json;

namespace CookTime.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]

    public class SearchActivity : AppCompatActivity
    {
        private User _loggedUser;
        private List<string> _recommendations;
        private string request; //later modified to be the resulting recommendation list prior to deserialization.
        private string _filter;
        private RecomAdapter _recomAdapter;
        
        // axml objects
        private SearchView _searchBar;
        private ListView _resultView;
        //filter buttons
        //tags
        private Button _vegan;
        private Button _vegetarian;
        private Button _kosher;
        private Button _celiac;
        private Button _keto;
        private Button _carnivore;
        //time
        private Button _breakfast;
        private Button _brunch;
        private Button _lunch;
        private Button _snack;
        private Button _dinner;
        //type
        private Button _appetizer;
        private Button _entree;
        private Button _maindish;
        private Button _alcohol;
        private Button _cold;
        private Button _hot;
        private Button _dessert;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SearchView);

            var usJson = Intent.GetStringExtra("User");
            _loggedUser = JsonConvert.DeserializeObject<User>(usJson);
            
            _resultView = FindViewById<ListView>(Resource.Id.recomList);
            _searchBar = FindViewById<SearchView>(Resource.Id.searchBar);
            
            //finding buttons
            //tags
            _vegan = FindViewById<Button>(Resource.Id.veganBtn);
            _vegan.Click += FilterClick;
            _vegetarian = FindViewById<Button>(Resource.Id.vegetarianBtn);
            _vegetarian.Click += FilterClick;
            _kosher = FindViewById<Button>(Resource.Id.kosherBtn);
            _kosher.Click += FilterClick;
            _celiac = FindViewById<Button>(Resource.Id.celiacBtn);
            _celiac.Click += FilterClick;
            _keto = FindViewById<Button>(Resource.Id.ketoBtn);
            _keto.Click += FilterClick;
            _carnivore = FindViewById<Button>(Resource.Id.carnivoreBtn);
            _carnivore.Click += FilterClick;
            //time
            _breakfast = FindViewById<Button>(Resource.Id.breakfastBtn);
            _breakfast.Click += FilterClick;
            _brunch = FindViewById<Button>(Resource.Id.brunchBtn);
            _brunch.Click += FilterClick;
            _lunch = FindViewById<Button>(Resource.Id.lunchBtn);
            _lunch.Click += FilterClick;
            _snack = FindViewById<Button>(Resource.Id.snackBtn);
            _snack.Click += FilterClick;
            _dinner = FindViewById<Button>(Resource.Id.dinnerBtn);
            _dinner.Click += FilterClick;
            //type
            _appetizer = FindViewById<Button>(Resource.Id.appetizerBtn);
            _appetizer.Click += FilterClick;
            _entree = FindViewById<Button>(Resource.Id.entreeBtn);
            _entree.Click += FilterClick;
            _maindish = FindViewById<Button>(Resource.Id.maindishBtn);
            _maindish.Click += FilterClick;
            _alcohol = FindViewById<Button>(Resource.Id.alcoholBtn);
            _alcohol.Click += FilterClick;
            _cold = FindViewById<Button>(Resource.Id.coldBtn);
            _cold.Click += FilterClick;
            _hot  = FindViewById<Button>(Resource.Id.hotBtn);
            _hot.Click += FilterClick;
            
            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            
             var url = "resources/recommend?email=" + _loggedUser.email;
             webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
             request = webClient.DownloadString(url);
             _recommendations = JsonConvert.DeserializeObject<List<string>>(request);
            
            _recomAdapter = new RecomAdapter(this, _recommendations);
            _resultView.Adapter = _recomAdapter;
            _resultView.ItemClick += RecomClick;
        }

        private void RecomClick(object sender, AdapterView.ItemClickEventArgs e) {
            var profileId = _recommendations[e.Position].Split(';')[0];
            var profileName = _recommendations[e.Position].Split(';')[1];
            var profileType = _recommendations[e.Position].Split(';')[2];
            
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            
            if (profileType.Equals("user")) {
                // logic for when the recommended result is a user object
                var userUrl = "resources/getUser?id=" + profileId;
                var userRequest = webClient.DownloadString(userUrl);
                
                if (_loggedUser.email.Equals(profileId)) {
                    // loads the same user's profile, since they clicked on themselves
                    Intent userIntent = new Intent(this, typeof(MyProfileActivity));
                    
                    userIntent.PutExtra("User", userRequest);
                    StartActivity(userIntent);

                }
                else {
                    // loads another user's profile, for the user is not the same logged instance.
                    //TODO check if another user profile is or not a chef.
                    Intent userIntent = new Intent(this, typeof(PrivProfileActivity));
                    
                    userIntent.PutExtra("User", userRequest);
                    userIntent.PutExtra("LoggedId", _loggedUser.email);
                    StartActivity(userIntent);
                }
            }
            else if (profileType.Equals("recipe")) {
                // logic for when the recommended result is a recipe instance
                var recipUrl = "resources/getRecipe?id=" + profileId;
                var recipRequest = webClient.DownloadString(recipUrl);
                Intent recipIntent = new Intent(this, typeof(RecipeActivity));
                recipIntent.PutExtra("Recipe", recipRequest);
                recipIntent.PutExtra("LoggedId", _loggedUser.email);
                StartActivity(recipIntent);
            }
            else {
                //gets business information
                //TODO make Business classes to load information from server.
            }
        }

        private void FilterClick(object sender, EventArgs e)
        {
            var queryList = request;
            Button button = (Button) sender;
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var url = "resources/filterRecommend?array=" + queryList + "?data=" + button.Text;
            var response = webClient.DownloadString(url);
            
            _recommendations = JsonConvert.DeserializeObject<List<string>>(response);
            _recomAdapter.ProfileItems = _recommendations;
            _resultView.Adapter = _recomAdapter;
        }
    }
}