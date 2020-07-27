using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using CookTime.Adapters;
using CookTime.DialogFragments;
using Newtonsoft.Json;

namespace CookTime.Activities
{
    /// <summary>
    /// This class represents the Private Business view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class PrivateBusiness : AppCompatActivity
    {
        private string bsnsJson;
        private string loggedId;
        private string logoUrl;
        private Business bsns;
        private TextView bsnsNameTV;
        private TextView _bsnsHoursTV;
        private TextView _bsnsContactTV;
        private TextView scoreView;
        private TextView _locationText;
        private ImageView _logo;
        private Button _btnFollowers;
        private Button _btnFollow;
        private Button _btnRate;
        private Button _btnDate;
        private Button _btnScore;
        private Button _btnDiff;
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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PrivBusiness);

            loggedId = Intent.GetStringExtra("LoggedId");
            bsnsJson = Intent.GetStringExtra("Bsns");
            bsns = JsonConvert.DeserializeObject<Business>(bsnsJson);
            
            bsnsNameTV = FindViewById<TextView>(Resource.Id.bsnsName);
            _bsnsHoursTV = FindViewById<TextView>(Resource.Id.businessHoursPView);
            _bsnsContactTV = FindViewById<TextView>(Resource.Id.contactPView);
            scoreView = FindViewById<TextView>(Resource.Id.scorePView);
            _locationText = FindViewById<TextView>(Resource.Id.locationView);
            _btnFollowers = FindViewById<Button>(Resource.Id.btnPBFollowers);
            _btnFollow = FindViewById<Button>(Resource.Id.btnPBFollow);
            
            _logo = FindViewById<ImageView>(Resource.Id.profilePic);
            
            _btnRate = FindViewById<Button>(Resource.Id.btnPBRate);
            _btnDate = FindViewById<Button>(Resource.Id.btnBDate);
            _btnScore = FindViewById<Button>(Resource.Id.btnBScore);
            _btnDiff = FindViewById<Button>(Resource.Id.btnBDiff);
            _menuListView = FindViewById<ListView>(Resource.Id.menuListView);

            bsnsNameTV.Text = bsns.name;
            _bsnsHoursTV.Text = "Business hours: " + bsns.businessHours;
            _bsnsContactTV.Text = "Contact: " + bsns.contact;
            _btnFollowers.Text = "Followers: " + bsns.followers.Count;
            scoreView.Text = "Score: " + bsns.rating;
            _locationText.Text = "Location: " + bsns.location;

            // sets the business logo to the imageView
            if (!string.IsNullOrEmpty(bsns.photo))
            {
                logoUrl = $"http://{MainActivity.Ipv4}:8080/CookTime_war/cookAPI/resources/getPicture?id={bsns.photo}";
                Bitmap bitmap = GetImageBitmapFromUrl(logoUrl);
                _logo.SetImageBitmap(bitmap);
            }
            
            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

             var url = "resources/isFollowingBusiness?email=" + loggedId + "&id=" + bsns.id;
             webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
             var response = webClient.DownloadString(url);
            
            _btnFollow.Text = response == "0" ? "FOLLOW" : "UNFOLLOW";
            
            url = "resources/businessPublic?id=" + bsns.id + "&filter=date";
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var send = webClient.DownloadString(url);
            
            _menuList = JsonConvert.DeserializeObject<IList<string>>(send);

            _adapter = new RecipeAdapter(this, _menuList);
            _menuListView.Adapter = _adapter;
            _menuListView.ItemClick += ListClick;

             _btnFollowers.Click += (sender, args) => 
             {
                 var intent = new Intent(this, typeof(FollowActivity));
                 intent.PutExtra("Title", "Followers");
                 intent.PutExtra("LoggedId", loggedId);
                 intent.PutStringArrayListExtra("FollowList", bsns.followers);
                 StartActivity(intent);
                 OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
             };
            
             _btnFollow.Click += (sender, args) =>
             {
                 using var webClient2 = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                 var followUrl = "resources/followBusiness?email=" + loggedId + "&id=" + bsns.id;
                 webClient2.Headers[HttpRequestHeader.ContentType] = "application/json";
                 var answer = webClient2.DownloadString(followUrl);
            
                 followUrl = "resources/getUser?id=" + loggedId;
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
            
            _btnRate.Click += (sender, args) =>
            {
                //Brings dialog fragment forward
                var transaction = SupportFragmentManager.BeginTransaction();
                var dialogRate = new DialogRate();
                
                dialogRate.Show(transaction, "rate");
                dialogRate.LoggedId = loggedId;
                dialogRate.BsnsId = bsns.id;
                dialogRate.Type = 2;
                    
                dialogRate.EventHandlerRate += RateResult;
            };
            
        }

        /// <summary>
        /// This method manages clicking on a recipe item
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
            recipeIntent.PutExtra("LoggedId", loggedId);
            StartActivity(recipeIntent);
            OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
        }
        
        /// <summary>
        /// This method sorts the MyMenu of the business according to the sortStr attribute
        /// </summary>
        private void SortMenu() {
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var url = "resources/businessPublic?id=" + bsns.id + "&filter=" + sortStr;
            var request = webClient.DownloadString(url);
            
            _menuList = JsonConvert.DeserializeObject<IList<string>>(request);
            _adapter.RecipeItems = _menuList;
            _menuListView.Adapter = _adapter;
        }
        
        /// <summary>
        /// This method is in charge of retrieving the result of the Rate Business dialog fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void RateResult(object sender, SendRateEvent e) {
            string toastText;
        
            switch (e.Message)
            {
                case "-1":
                    toastText = "Please choose a rating";
                    break;
                case "1":
                    toastText = "You already rated this business.";
                    break;
                default:
                {
                    toastText = "Business rated! Refreshing the profile...";
                    
                    using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                    var url = "resources/getBusiness?id=" + bsns.id;

                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var bsns1Json = webClient.DownloadString(url);
                    var intent = new Intent(this, typeof(PrivateBusiness));
                    intent.PutExtra("Bsns", bsns1Json);
                    intent.PutExtra("LoggedId", loggedId);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                    Finish();
                    break;
                }
            }
            var toast = Toast.MakeText(this, toastText, ToastLength.Long);
            toast.Show();
        }
        
        /// <summary>
        /// this method is used to obtain an image bitmap from a url.
        /// </summary>
        /// <param name="url">the string url that displays the image.</param>
        /// <returns>a Bitmap type object representing the image to cache it into memory</returns>
        private Bitmap GetImageBitmapFromUrl(string url) {
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
    }
}