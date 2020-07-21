using System.Collections.Generic;
using Android.App;
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
            // var url = "resources/myMenu?email=" + _loggedUser.email + "&filter=date";
            // webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            // var send = webClient.DownloadString(url);
            //
            // _myMenuList = JsonConvert.DeserializeObject<IList<string>>(send);
            //
            // _adapter = new RecipeAdapter(this, _myMenuList);
            // _myMenuListView.Adapter = _adapter;
            // _myMenuListView.ItemClick += ListClick;
            //
            // _btnNewsfeed.Click += (sender, args) =>
            // {
            //     using var webClient2 = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            //
            //     var url2 = "resources/getUser?id=" + _loggedUser.email;
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
            //     Intent intent = new Intent(this, typeof(FollowActivity));
            //     intent.PutExtra("Title", "Followers");
            //     intent.PutExtra("LoggedId", _loggedUser.email);
            //     intent.PutStringArrayListExtra("FollowList", _loggedUser.followerEmails);
            //     
            //     StartActivity(intent);
            //     OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            // };
            //
            // _btnFollowing.Click += (sender, args) =>
            // {
            //     Intent intent = new Intent(this, typeof(FollowActivity));
            //     intent.PutExtra("Title", "Following");
            //     intent.PutExtra("LoggedId", _loggedUser.email);
            //     intent.PutStringArrayListExtra("FollowList", _loggedUser.followingEmails);
            //     
            //     StartActivity(intent);
            //     OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            // };
            //
            // _btnNotif.Click += (sender, args) =>
            // {
            //     Intent intent = new Intent(this, typeof(NotifActivity));
            //     intent.PutStringArrayListExtra("NotifList", _loggedUser.notifications);
            //     intent.PutExtra("LoggedId", _loggedUser.email);
            //
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
            // _btnRecipe.Click += (sender, args) =>
            // {
            //     Intent intent = new Intent(this, typeof(CreateRActivity));
            //     intent.PutExtra("LoggedId", _loggedUser.email);
            //
            //     StartActivity(intent);
            //     OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            // };
            //
            // _btnChef.Click += (sender, args) =>
            // {
            //     if (_loggedUser.chef) {
            //         Toast toast = Toast.MakeText(this, "You're already a chef", ToastLength.Short);
            //         toast.Show();
            //     }
            //     else {
            //         //Brings dialog fragment forward
            //         var transaction = SupportFragmentManager.BeginTransaction();
            //         var dialogChef = new DialogChef();
            //         dialogChef.LoggedId = _loggedUser.email;
            //         dialogChef.Show(transaction, "chef");
            //         
            //         dialogChef.EventHandlerChef += ChefResult;
            //     }
            // };
            //
            // _btnBusiness.Click += (sender, args) =>
            // {
            //     if (_loggedUser.business == 0) {
            //         Intent intent = new Intent(this, typeof(CreateBActivity));
            //         intent.PutExtra("LoggedId", _loggedUser.email);
            //
            //         StartActivity(intent);
            //         OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            //     }
            //     else {
            //         // using var webClient3 = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            //         // var url3= "resources/getBusiness?id=" + _loggedUser.business;
            //         // webClient3.Headers[HttpRequestHeader.ContentType] = "application/json";
            //         // var bsnsJson = webClient3.DownloadString(url3);
            //     }
            // };
        }

        //     private void ListClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        //     {
        //         var recipeId = _myMenuList[eventArgs.Position].Split(';')[0];
        //         
        //         //Brings dialog fragment forward
        //         var transaction = SupportFragmentManager.BeginTransaction();
        //         var dialogChoice = new DialogChoice();
        //         dialogChoice.RecipeId = recipeId;
        //         dialogChoice.Show(transaction, "choice");
        //         dialogChoice.EventHandlerChoice += ChoiceAction;
        //     }
        //     
        /// <summary>
        /// This method is in charge of retrieving the data entered by the user in the Settings dialog fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void AddResult(object sender,SendAddEvent e) {
            var toastText = "";
            if (e.Message == "3")
            {
                toastText = "Email doesn't exist";
            } 
            else if (e.Message == "0")
            {
                toastText = "User already in a business";
            } 
            else if (e.Message == "1")
            {
                toastText = "User added";
            } 
        
            _toast = Toast.MakeText(this, toastText, ToastLength.Short);
            _toast.Show();
        }
        //     
        //     /// <summary>
        //     /// This method is in charge of retrieving the result of the Choice dialog fragment.
        //     /// </summary>
        //     /// <param name="sender"> Reference to the object that raised the event </param>
        //     /// <param name="e"> Contains the event data </param>
        //     private void ChoiceAction(object sender, ChoiceEvent e) {
        //         
        //         using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
        //         var url = "resources/getRecipe?id=" + e.RecipeId;
        //         webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
        //         var request = webClient.DownloadString(url);
        //         
        //         var response = e.Message;
        //         if (response == 0) {
        //             Intent recipeIntent = new Intent(this, typeof(RecipeActivity));
        //             recipeIntent.PutExtra("Recipe", request);
        //             recipeIntent.PutExtra("LoggedId", _loggedUser.email);
        //             StartActivity(recipeIntent);
        //             OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
        //         }
        //         else
        //         {
        //             url = "resources/deleteRecipe?email=" + _loggedUser.email + "&id=" + e.RecipeId;
        //             webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
        //             webClient.DownloadString(url);
        //             
        //             url = "resources/getUser?id=" + _loggedUser.email;
        //             webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
        //             var json = webClient.DownloadString(url);
        //             
        //             Intent intent = new Intent(this, typeof(MyProfileActivity));
        //             intent.PutExtra("User", json);
        //             intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
        //             StartActivity(intent);
        //             OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
        //             Finish();
        //             
        //             _toast = Toast.MakeText(this, "Recipe deleted. Refreshing MyProfile...", ToastLength.Short);
        //             _toast.Show();
        //         }
        //     }
        //
        //     /// <summary>
        //     /// This method sorts the MyMenu of the user according to the sortStr attribute
        //     /// </summary>
        //     private void SortMenu()
        //     {
        //         using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
        //         var url = "resources/myMenu?email=" + _loggedUser.email + "&filter=" + sortStr;
        //         webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
        //         var request = webClient.DownloadString(url);
        //
        //         _myMenuList = JsonConvert.DeserializeObject<IList<string>>(request);
        //         _adapter.RecipeItems = _myMenuList;
        //         _myMenuListView.Adapter = _adapter;
        //     }
        //     
        //     /// <summary>
        //     /// This method is in charge of retrieving theresult of a chef request by the user..
        //     /// </summary>
        //     /// <param name="sender"> Reference to the object that raised the event </param>
        //     /// <param name="e"> Contains the event data </param>
        //     private void ChefResult(object sender, SendChefEvent e) {
        //         var toastText = "You already have a pending request";
        //         if (e.Message == "2") {
        //             toastText = "Request sent!";
        //
        //             using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
        //             var url = "resources/getUser?id=" + _loggedUser.email;
        //             webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
        //             var message = webClient.DownloadString(url);
        //             
        //             Intent intent = new Intent(this, typeof(MyProfileActivity));
        //             intent.PutExtra("User", message);
        //             intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
        //             StartActivity(intent);
        //             OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
        //             Finish();
        //         }
        //         _toast = Toast.MakeText(this, toastText, ToastLength.Short);
        //         _toast.Show();
        //     }
        // }
    }
}