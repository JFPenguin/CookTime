﻿using System;
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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]

    public class SearchActivity : AppCompatActivity {
        private User _loggedUser;
        private List<string> _recommendations;
        private string request; //later modified to be the resulting recommendation list prior to deserialization.
        private string _filter;
        private RecomAdapter _recomAdapter;
        private bool _searched;
        private Toast _refToast;
        
        // axml objects
        private EditText _searchTxt;
        private TextView _resultType;
        private ListView _resultView;
        private ImageButton _srchBtn;
        private Button _refresh;
        //filter buttons
        private Button _rating;
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
        /// All of the recipe info is shown here.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SearchView);

            var usJson = Intent.GetStringExtra("User");
            _loggedUser = JsonConvert.DeserializeObject<User>(usJson);
            _resultView = FindViewById<ListView>(Resource.Id.recomList);
            _srchBtn = FindViewById<ImageButton>(Resource.Id.srchButton);
            _srchBtn.Click += SearchClick;
            _searchTxt = FindViewById<EditText>(Resource.Id.searchInput);
            _refresh = FindViewById<Button>(Resource.Id.refreshBtn);
            _refresh.Click += RefreshClick;
            _resultType = FindViewById<TextView>(Resource.Id.resultType);
            //finding buttons
            _rating = FindViewById<Button>(Resource.Id.ratingBtn);
            _rating.Click += RatingClick;
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
            _dessert = FindViewById<Button>(Resource.Id.dessertBtn);
            _dessert.Click += FilterClick;
            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            var url = "resources/recommend?email=" + _loggedUser.email;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            request = webClient.DownloadString(url);

            _recommendations = JsonConvert.DeserializeObject<List<string>>(request);

            _recomAdapter = new RecomAdapter(this, _recommendations);
            _resultView.Adapter = _recomAdapter;
            _resultView.ItemClick += RecomClick;
        }

        /// <summary>
        /// This method is invoked when the user clicks on a result inside the recommended profiles.
        /// It handles the type of activity that has to be opened, based on the provided information.
        /// </summary>
        /// <param name="sender">the view in which the event happened.</param>
        /// <param name="e">the item in the list that was clicked by the user.</param>
        private void RecomClick(object sender, AdapterView.ItemClickEventArgs e) {
            var profileId = _recommendations[e.Position].Split(';')[0];
            var profileType = _recommendations[e.Position].Split(';')[2];
            
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            
            if (profileType.Equals("user") || profileType.Equals("chef")) {
                // logic for when the recommended result is a user object
                var userUrl = "resources/getUser?id=" + profileId;
                var userRequest = webClient.DownloadString(userUrl);
                
                if (_loggedUser.email.Equals(profileId)) {
                    // loads the same user's profile, since they clicked on themselves
                    var userIntent = new Intent(this, typeof(MyProfileActivity));
                    
                    userIntent.PutExtra("User", userRequest);
                    StartActivity(userIntent);
                }
                else {
                    // loads another user's profile, for the user is not the same logged instance.
                    var userIntent = new Intent(this, typeof(PrivProfileActivity));
                    userIntent.PutExtra("User", userRequest);
                    userIntent.PutExtra("LoggedId", _loggedUser.email);
                    StartActivity(userIntent);
                }
            }
            else if (profileType.Equals("recipe")) {
                // logic for when the recommended result is a recipe instance
                var recipUrl = "resources/getRecipe?id=" + profileId;
                var recipRequest = webClient.DownloadString(recipUrl);
                var recipIntent = new Intent(this, typeof(RecipeActivity));
                recipIntent.PutExtra("Recipe", recipRequest);
                recipIntent.PutExtra("LoggedId", _loggedUser.email);
                StartActivity(recipIntent);
            }
            else {
                //gets business information
                var url = "resources/getBusiness?id=" + profileId;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var send = webClient.DownloadString(url);
                
                url = "resources/isEmployee?email=" + _loggedUser.email + "&id=" + profileId;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var result = webClient.DownloadString(url);

                if (result == "1") 
                {
                    var intent = new Intent(this, typeof(MyBusiness));
                    intent.PutExtra("Bsns", send);
                    intent.PutExtra("LoggedId", _loggedUser.email);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                }
                else
                {
                    var intent = new Intent(this, typeof(PrivateBusiness));
                    intent.PutExtra("Bsns", send);
                    intent.PutExtra("LoggedId", _loggedUser.email);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);   
                }
            }
        }
        
        /// <summary>
        /// This method is invoked when the user clicks a filter button.
        /// It applies the filter on the existing results
        /// </summary>
        /// <param name="sender">the button in which the event occurred.</param>
        /// <param name="e">arguments from the event</param>
        private void FilterClick(object sender, EventArgs e) {
            var queryList = request;
            var newQuery = queryList.Replace("[","");
            newQuery = newQuery.Replace("]", "");
            var button = (Button) sender;
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var url = "resources/filterRecommend?array=" + newQuery + "&data=" + button.Text;
            var response = webClient.DownloadString(url);
            _recommendations = JsonConvert.DeserializeObject<List<string>>(response);
            request = response;
            _recomAdapter.ProfileItems = _recommendations;
            _resultView.Adapter = _recomAdapter;
            string toastText;
            if (_recommendations.Count == 0) {
                toastText = "Results did not match with filter selection. Please try another search or refresh";
            }
            else {
                toastText = "Results filtered according to selection";
            }
            _refToast = Toast.MakeText(this, toastText, ToastLength.Short);
            _refToast.Show();
        }
        
        /// <summary>
        /// This method handles the logic implemented when the user clicks on the "search" button.
        /// </summary>
        /// <param name="sender">the object in which the event occurred, the search button.</param>
        /// <param name="e"> event arguments</param>
        private void SearchClick(object sender, EventArgs e) {
            if (_searchTxt.Text == "") {
                _refToast = Toast.MakeText(this, "please write a search query", ToastLength.Short);
                _refToast.Show();
                return;
            }
            if (!_searched) {
                // sets the prompt to notify the user the window is now showing search results instead of recommendations
                _resultType.Text = "Search Results";
                _searched = true;
            }
            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var url = "resources/searchByName?search=" + _searchTxt.Text;
            var response = webClient.DownloadString(url);
            
            request = response;
            _recommendations = JsonConvert.DeserializeObject<List<string>>(response);
            _recomAdapter.ProfileItems = _recommendations;
            _resultView.Adapter = _recomAdapter;
            string toastText;
            if (_recommendations.Count == 0) {
                toastText = "No items in server matched the search query";
            }
            else {
                toastText = "Showing search results";
            }
            _refToast = Toast.MakeText(this, toastText, ToastLength.Short);
            _refToast.Show();
        }
        
        /// <summary>
        /// This method is used to refresh the "Search/recommendations" view.
        /// It reloads the window and obtains a new set of recommendations from the server.
        /// </summary>
        /// <param name="sender">the object in which the event occurred.</param>
        /// <param name="e"> arguments coming from the event</param>
        private void RefreshClick(object sender, EventArgs e)
        {
            var refresh = new Intent(this, typeof(SearchActivity));
            _refToast = Toast.MakeText(this, "recommendations refreshed", ToastLength.Short);
            _refToast.Show();
            refresh.PutExtra("User", JsonConvert.SerializeObject(_loggedUser));
            StartActivity(refresh);
            OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            Finish();
        }

        /// <summary>
        /// This method is used to show results by rating when the user pushes the Rating button.
        /// </summary>
        /// <param name="sender">the button that caused the event</param>
        /// <param name="e">the arguments from the event</param>
        private void RatingClick(object sender, EventArgs e) {
            _resultType.Text = "best rated results";
            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var url = "resources/ratings";
            
            var response = webClient.DownloadString(url);
            request = response;
            _recommendations = JsonConvert.DeserializeObject<List<string>>(response);
            _recomAdapter.ProfileItems = _recommendations;
            _resultView.Adapter = _recomAdapter;
            _refToast = Toast.MakeText(this, "showing by rating", ToastLength.Short);
            _refToast.Show();
        }
    }
}