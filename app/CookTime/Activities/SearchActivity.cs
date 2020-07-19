using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
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
        
        // axml objects
        private Button _newsfeedBtn;
        private SearchView _searchBar;
        private ListView _resultView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SearchView);

            var usJson = Intent.GetStringExtra("User");
            _loggedUser = JsonConvert.DeserializeObject<User>(usJson);
            
            _newsfeedBtn = FindViewById<Button>(Resource.Id.newsfeedBtn);
            _resultView = FindViewById<ListView>(Resource.Id.recomList);
            _searchBar = FindViewById<SearchView>(Resource.Id.searchBar);
            
            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            
            var url = "resources/recommend?email=" + _loggedUser.email;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var request = webClient.DownloadString(url);
            _recommendations = JsonConvert.DeserializeObject<List<string>>(request);
            
            RecomAdapter recomAdapter = new RecomAdapter(this, _recommendations);
            _resultView.Adapter = recomAdapter;
            _resultView.ItemClick += RecomClick;
        }

        private void RecomClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var profileId = _recommendations[e.Position].Split(';')[0];
            var profileName = _recommendations[e.Position].Split(';')[1];
            var profileType = _recommendations[e.Position].Split(';')[2];
            
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            
            if (profileType.Equals("user")) {
                // logic for when the recommended result is a user object
                var userUrl = "resources/getUser?id=" + profileId;
                var userRequest = webClient.DownloadString(userUrl);
                
                Intent userIntent;
                
                if (_loggedUser.email.Equals(profileId)) {
                    // loads the same user's profile, since they clicked on themselves
                    userIntent = new Intent(this, typeof(MyProfileActivity));
                    userIntent.PutExtra("User", JsonConvert.SerializeObject(_loggedUser));
                }
                else {
                    // loads another user's profile, for the user is not the same logged instance.
                    //TODO check if another user profile is or not a chef.
                    userIntent = new Intent(this, typeof(PrivProfileActivity));
                    userIntent.PutExtra("User", userRequest);
                }
                StartActivity(userIntent);
            }
            else if (profileType.Equals("recipe")) {
                // logic for when the recommended result is a recipe instance
                var recipUrl = "resources/getRecipe?id=" + profileId;
                var recipRequest = webClient.DownloadString(recipUrl);
                Intent recipIntent = new Intent(this, typeof(RecipeActivity));
                recipIntent.PutExtra("Recipe", recipRequest);

            }
            else {
                //gets business information
                //TODO make Business classes to load information from server.
            }
        }
    }
}