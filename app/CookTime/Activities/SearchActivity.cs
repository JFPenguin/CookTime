using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;

namespace CookTime.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]

    public class SearchActivity : AppCompatActivity
    {
        private User _loggedUser;
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
            
        }
    }
}