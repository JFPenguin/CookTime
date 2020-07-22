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
    public class NotifActivity : AppCompatActivity {
        private string _loggedId;
        private Button btnClear;
        private ListView _notifListView;
        private IList<string> notifList;
        
        /// <summary>
        /// This method is called when the activity is starting.
        /// The list of followers/following is displayed here.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Notif);
            
            _notifListView = FindViewById<ListView>(Resource.Id.notifList);
            btnClear = FindViewById<Button>(Resource.Id.btnClear);

            _loggedId = Intent.GetStringExtra("LoggedId");
            notifList = Intent.GetStringArrayListExtra("NotifList");
            
            var adapter = new CompAdapter(this, notifList);
            
            _notifListView.Adapter = adapter;
            
            btnClear.Click += (sender, args) =>
            {
                using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                var url = "resources/deleteAllNotifications?id=" + _loggedId;
                webClient.DownloadString(url);
                
                url = "resources/getUser?id=" + _loggedId;
                var userJson = webClient.DownloadString(url);
                
                var intent = new Intent(this, typeof(MyProfileActivity));
                intent.PutExtra("User", userJson);
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);

                var toast = Toast.MakeText(this, "Notifications cleared", ToastLength.Short);
                toast.Show();
            };
        }
    }
}