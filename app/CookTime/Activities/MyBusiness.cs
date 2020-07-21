using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using CookTime.Adapters;
using CookTime.DialogFragments;
using Newtonsoft.Json;

namespace CookTime.Activities
{
    /// <summary>
    /// This class represents the My Profile view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MyBusiness : AppCompatActivity
    {
        private string bsnsJson;
        private string loggedId;
        private Business bsns;
        private bool _seeingPublic = true;
        // axml objects
        private TextView bsnsNameTV;
        private TextView _bsnsHoursTV;
        private TextView _bsnsContactTV;
        private TextView scoreView;
        private Button _btnMyProfile;
        private Button _btnFollowers;
        private Button _btnSettings;
        private Button _btnPrivMenu;
        private Button _btPost;
        private Button _btnDate;
        private Button _btnScore;
        private Button _btnDiff;
        private string sortStr;
        private ListView _menuListView;
        private IList<string> _menuList;
        private Toast _toast;
        private RecipeAdapter _adapter;

        /// <summary>
        /// This method is called when the activity is starting.
        /// It contains the logic for the buttons shown in this activity.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Business);

            loggedId = Intent.GetStringExtra("LoggedId");
            bsnsJson = Intent.GetStringExtra("Bsns");
            bsns = JsonConvert.DeserializeObject<Business>(bsnsJson);

            bsnsNameTV = FindViewById<TextView>(Resource.Id.bsnsName);
            _bsnsHoursTV = FindViewById<TextView>(Resource.Id.businessHoursView);
            _bsnsContactTV = FindViewById<TextView>(Resource.Id.contactView);
            scoreView = FindViewById<TextView>(Resource.Id.scoreView);
            _btnMyProfile = FindViewById<Button>(Resource.Id.btnMyProfile);
            _btnFollowers = FindViewById<Button>(Resource.Id.btnBFollowers);
            _btnSettings = FindViewById<Button>(Resource.Id.btnBSettings);
            _btnPrivMenu = FindViewById<Button>(Resource.Id.btnPrivMenu);
            _btPost = FindViewById<Button>(Resource.Id.btnBRecipe);
            _btnDate = FindViewById<Button>(Resource.Id.btnBDate);
            _btnScore = FindViewById<Button>(Resource.Id.btnBScore);
            _btnDiff = FindViewById<Button>(Resource.Id.btnBDiff);
            _menuListView = FindViewById<ListView>(Resource.Id.myMenuListView);

            bsnsNameTV.Text = bsns.name;
            _bsnsHoursTV.Text = "Business hours: " + bsns.businessHours;
            _bsnsContactTV.Text = "Contact: " + bsns.contact;
            _btnFollowers.Text = "Followers: " + bsns.followers.Count;
            scoreView.Text = "Score: " + bsns.rating;
            
            // using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            //
            // var url = "resources/businessPublic" + bsns.id + "&filter=date";
            // webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            // var send = webClient.DownloadString(url);
            //
            // _menuList = JsonConvert.DeserializeObject<IList<string>>(send);
            //
            // _adapter = new RecipeAdapter(this, _menuList);
            // _menuListView.Adapter = _adapter;
            // _menuListView.ItemClick += ListClick;
            //
            // _btnMyProfile.Click += (sender, args) =>
            // {
            //     using var webClient2 = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            //
            //     var url2 = "resources/getUser?id=" + loggedId;
            //     webClient2.Headers[HttpRequestHeader.ContentType] = "application/json";
            //     var request = webClient2.DownloadString(url2);
            //     
            //     Intent intent = new Intent(this, typeof(NewsfeedActivity));
            //     intent.PutExtra("User", request);
            //     
            //     StartActivity(intent);
            //     OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            //     Finish();
            // };
            //
            //
            _btnSettings.Click += (sender, args) =>
            {
                //Brings dialog fragment forward
                var transaction = SupportFragmentManager.BeginTransaction();
                var dialogAdd = new DialogAdd();
                dialogAdd.Show(transaction, "settings");
                dialogAdd.BsnsId = bsns.id;
                dialogAdd.EventHandlerAdd += AddResult;
                
            };
            //
            // _btnFollowers.Click += (sender, args) => 
            // {
            //     // TODO use BusFollowActivity to implement different logic without modifying the existing algorithms for regular users.
            //     Intent intent = new Intent(this, typeof(BusFollowActivity));
            //     intent.PutExtra("Title", "Followers");
            //     intent.PutExtra("LoggedId", loggedId);
            //     intent.PutStringArrayListExtra("FollowList", bsns.followers);
            //     StartActivity(intent);
            //     OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            // };
            //
            // _btnDate.Click += (sender, args) =>
            // {
            //     sortStr = "date";
            //     SortMenu();
            // };
            //
            // _btnScore.Click += (sender, args) =>
            // {
            //     sortStr = "score";
            //     SortMenu();
            // };
            //
            // _btnDiff.Click += (sender, args) =>
            // {
            //     sortStr = "difficulty";
            //     SortMenu();
            // };
            //
            // _btPost.Click += (sender, args) => {
            //     //TODO change normal recipe creation to business specific creation
            //     Intent intent = new Intent(this, typeof(CreateRActivity));
            //     intent.PutExtra("LoggedId", loggedId);
            //     StartActivity(intent);
            //     OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            // };
            //
            // _btnPrivMenu.Click += (sender, args) =>
            // {
            //     var toastText = "";
            //     using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            //     webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            //     var type = "";
            //     
            //     _adapter.RecipeItems = _menuList;
            //     
            //     if (_seeingPublic) {
            //         _btnPrivMenu.Text = "Private Menu";
            //         toastText = "now showing private menu";
            //         type = "businessPrivate";
            //         _seeingPublic = false;
            //     }
            //     else {
            //         _btnPrivMenu.Text = "Public Menu";
            //         toastText = "now showing public menu";
            //         type = "businessPublic";
            //         _seeingPublic = true;
            //         
            //     }
            //     var url = "resources/" + type + "?id=" + bsns.id + "&filter=date";
            //     var send = webClient.DownloadString(url);
            //     
            //     _menuList = JsonConvert.DeserializeObject<IList<string>>(send);
            //     _adapter.RecipeItems = _menuList;
            //     _menuListView.Adapter = _adapter;
            //     
            //     _toast = Toast.MakeText(this, toastText, ToastLength.Short);
            //     _toast.Show();
            // };
        }

        private void ListClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            var recipeId = _menuList[eventArgs.Position].Split(';')[0];
            
            //Brings dialog fragment forward
            var transaction = SupportFragmentManager.BeginTransaction();
            var dialogChoice = new DialogChoice();
            dialogChoice.RecipeId = recipeId;
            dialogChoice.Show(transaction, "choice");
            dialogChoice.EventHandlerChoice += ChoiceAction;
        }
    
        /// <summary>
        /// This method is in charge of retrieving the data entered by the user in the Settings dialog fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void AddResult(object sender,SendAddEvent e) {
            var toastText = e.Message switch
            {
                "3" => "Email doesn't exist",
                "0" => "User already in a business",
                "1" => "User added",
                "4" => "Please fill in the info",
                _ => ""
            };

            _toast = Toast.MakeText(this, toastText, ToastLength.Short);
            _toast.Show();
        }
        
        /// <summary>
        /// This method is in charge of retrieving the result of the Choice dialog fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void ChoiceAction(object sender, ChoiceEvent e) {
            
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            var url = "resources/getRecipe?id=" + e.RecipeId;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var request = webClient.DownloadString(url);
            
            var response = e.Message;
            if (response == 0) {
                Intent recipeIntent = new Intent(this, typeof(RecipeActivity));
                recipeIntent.PutExtra("Recipe", request);
                recipeIntent.PutExtra("LoggedId", loggedId);
                StartActivity(recipeIntent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            }
            else
            {
                //TODO check if the email to be sent when deleting the recipe can be the contact email, or if it has to be a user's email.
                url = "resources/deleteRecipe?email=" + loggedId + "&id=" + e.RecipeId; //here
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.DownloadString(url);
                
                url = "resources/getBusiness?id=" + bsns.id;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var json = webClient.DownloadString(url);
                
                Intent intent = new Intent(this, typeof(Business));
                intent.PutExtra("Business", json);
                Intent.PutExtra("LoggedId", loggedId);
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                Finish();
                
                _toast = Toast.MakeText(this, "Recipe deleted. Refreshing MyBusiness...", ToastLength.Short);
                _toast.Show();
            }
        }
        
            /// <summary>
            /// This method sorts the MyMenu of the user according to the sortStr attribute
            /// </summary>
            private void SortMenu() {
                using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                
                var type = "businessPublic";
                //checking if the menu displayed is private or public to see which to filter
                if (!_seeingPublic) {
                    type = "businessPrivate";
                }
                
                var url = "resources/" + type +  "?id=" + bsns.id + "&filter=" + sortStr;
                var request = webClient.DownloadString(url);
                _menuList = JsonConvert.DeserializeObject<IList<string>>(request);
                _adapter.RecipeItems = _menuList;
                _menuListView.Adapter = _adapter;
            }
            
    }
}