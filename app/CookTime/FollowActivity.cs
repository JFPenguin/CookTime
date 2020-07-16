using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace CookTime {
    /// <summary>
    /// This class represents the Following/Followers view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class FollowActivity : AppCompatActivity {
        private TextView _titleText;
        private ListView _followListView;
        private IList<string> followList;
        
        /// <summary>
        /// This method is called when the activity is starting.
        /// It contains the logic for the buttons shown in the first view.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Follow);
            
            _titleText = FindViewById<TextView>(Resource.Id.titleView);
            _followListView = FindViewById<ListView>(Resource.Id.followList);

            var title = Intent.GetStringExtra("Title");
            _titleText.Text = title;

            followList = Intent.GetStringArrayListExtra("FollowList");
            
            FollowAdapter adapter = new FollowAdapter(this, followList);
            
            _followListView.Adapter = adapter;
        }
    }
}