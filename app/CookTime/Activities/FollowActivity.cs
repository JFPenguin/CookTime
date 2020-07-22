using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using CookTime.Adapters;

namespace CookTime.Activities {
    /// <summary>
    /// This class represents the Following/Followers view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class FollowActivity : AppCompatActivity {
        private string _loggedId;
        private TextView _titleText;
        private ListView _followListView;
        private IList<string> followList;
        
        /// <summary>
        /// This method is called when the activity is starting.
        /// The list of followers/following is displayed here.
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

            _loggedId = Intent.GetStringExtra("LoggedId");
            followList = Intent.GetStringArrayListExtra("FollowList");
            
            FollowAdapter adapter = new FollowAdapter(this, followList);
            
            _followListView.Adapter = adapter;
            
            _followListView.ItemClick += FollowClick;
        }

        /// <summary>
        /// This method manages a click over a follower/following name. It opens the profile of that user.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void FollowClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var id = followList[e.Position].Split(";")[0];
            var type = followList[e.Position].Split(";")[2];
            
            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            Intent intent;
            string send;
            
            if (type == "user") {
                var url = "resources/getUser?id=" + id;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                send = webClient.DownloadString(url);
                
                if (id ==_loggedId)
                {
                    intent = new Intent(this, typeof(MyProfileActivity));
                    intent.PutExtra("User", send);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight); 
                }
                else
                {
                    intent = new Intent(this, typeof(PrivProfileActivity));
                    intent.PutExtra("User", send);
                    intent.PutExtra("LoggedId", _loggedId);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight); 
                }
            }
            else
            {
                var url = "resources/getBusiness?id=" + id;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                send = webClient.DownloadString(url);
                
                url = "resources/isEmployee?email=" + _loggedId + "&id=" + id;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var result = webClient.DownloadString(url);

                if (result == "1") 
                {
                    intent = new Intent(this, typeof(MyBusiness));
                    intent.PutExtra("Bsns", send);
                    intent.PutExtra("LoggedId", _loggedId);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                }
                else
                {
                    //TODO add PrivBusiness activity
                    // intent = new Intent(this, typeof(PrivBusiness));
                    // intent.PutExtra("Bsns", send);
                    // intent.PutExtra("LoggedId", _loggedId);
                    // StartActivity(intent);
                    // OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);   
                }
            }
        }
    }
}