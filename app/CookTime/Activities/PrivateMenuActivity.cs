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

namespace CookTime.Activities {
    /// <summary>
    /// This class represents the Following/Followers view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class PrivateMenuActivity : AppCompatActivity {
        private string _loggedId;
        private int _bsnsId;
        private ListView _menuListView;
        private IList<string> menuList;
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
        /// <summary>
        /// This method is called when the activity is starting.
        /// The list of followers/following is displayed here.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.PrivMenu);

            _menuListView = FindViewById<ListView>(Resource.Id.privMenuList);
            
            _loggedId = Intent.GetStringExtra("LoggedId");
            _bsnsId = Intent.GetIntExtra("BsnsId", 0);
            
            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

            var url = "resources/businessPrivate?id=" + _bsnsId + "&filter=date";
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var send = webClient.DownloadString(url);

            menuList = JsonConvert.DeserializeObject<IList<string>>(send);
            var adapter = new RecipeAdapter(this, menuList);
            _menuListView.Adapter = adapter;
            
            _menuListView.ItemClick += MenuClick;
        }

        /// <summary>
        /// This method manages clicking on a recipe item
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="eventArgs"> Contains the event data </param>
        private void MenuClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            var recipeId = menuList[eventArgs.Position].Split(';')[0];

            //Brings dialog fragment forward
            var transaction = SupportFragmentManager.BeginTransaction();
            var dialogChoice = new DialogBChoice();
            dialogChoice.RecipeId = recipeId;
            dialogChoice.BtnText = "MAKE PUBLIC";
            dialogChoice.Show(transaction, "choice");
            dialogChoice.EventHandlerChoice += ChoiceAction;
        }
        
        /// <summary>
        /// This method is in charge of retrieving the result of the Choice dialog fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void ChoiceAction(object sender, ChoiceBEvent e)
        {
            var toastText = "";

            using var webClient = new WebClient
                {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            var url = "resources/getRecipe?id=" + e.RecipeId;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var request = webClient.DownloadString(url);

            var response = e.Message;
            switch (response)
            {
                case 0:
                {
                    var recipeIntent = new Intent(this, typeof(RecipeActivity));
                    recipeIntent.PutExtra("Recipe", request);
                    recipeIntent.PutExtra("LoggedId", _loggedId);
                    StartActivity(recipeIntent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
                    break;
                }
                case 1:
                    toastText = "Recipe deleted.";
                    url = "resources/deleteRecipe?email=" + _loggedId + "&id=" + e.RecipeId + "&fromMyMenu=0";
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.DownloadString(url);
                    break;
                default:
                    toastText = "Recipe made public.";
                    url = "resources/moveRecipe?recipeId=" + e.RecipeId + "&businessId=" + _bsnsId;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.DownloadString(url);
                    break;
            }

            if (response == 0) return;
            url = "resources/getBusiness?id=" + _bsnsId;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var json = webClient.DownloadString(url);

            var intent = new Intent(this, typeof(MyBusiness));
            intent.PutExtra("Bsns", json);
            Intent.PutExtra("LoggedId", _loggedId);
            StartActivity(intent);
            OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
            Finish();

            var toast = Toast.MakeText(this, toastText + " Refreshing MyBusiness...", ToastLength.Short);
            toast.Show();
        }
    }
}