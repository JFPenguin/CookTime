using System;
using System.Collections.Generic;
using System.IO;
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
    /// This class represents the MyBusiness view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MyBusiness : AppCompatActivity
    {
        private string bsnsJson;
        private string loggedId;
        private string logoUrl;
        private Business bsns;
        private TextView bsnsNameTV;
        private TextView _bsnsHoursTV;
        private TextView _bsnsContactTV;
        private TextView scoreView;
        private TextView _location;
        private ImageView _logo;
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
        /// This method is implemented to prompt the user with location permissions request.
        /// </summary>
        /// <param name="requestCode">the return code from the request</param>
        /// <param name="permissions">the permissions requested to the user</param>
        /// <param name="grantResults">communication to system with the permission requests</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults) {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
        /// <summary>
        /// This method is called when the activity is starting.
        /// It contains the logic for the buttons shown in this activity.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Business);

            loggedId = Intent.GetStringExtra("LoggedId");
            bsnsJson = Intent.GetStringExtra("Bsns");
            bsns = JsonConvert.DeserializeObject<Business>(bsnsJson);

            bsnsNameTV = FindViewById<TextView>(Resource.Id.bsnsName);
            _bsnsHoursTV = FindViewById<TextView>(Resource.Id.businessHoursView);
            _bsnsContactTV = FindViewById<TextView>(Resource.Id.contactView);
            scoreView = FindViewById<TextView>(Resource.Id.scoreView);
            _location = FindViewById<TextView>(Resource.Id.locationView);
            _logo = FindViewById<ImageView>(Resource.Id.businessLogo);
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
            _location.Text = "Location: " + bsns.location;
            
            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

            var url = "resources/businessPublic?id=" + bsns.id + "&filter=date";
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var send = webClient.DownloadString(url);

            _menuList = JsonConvert.DeserializeObject<IList<string>>(send);

            _adapter = new RecipeAdapter(this, _menuList);
            _menuListView.Adapter = _adapter;
            _menuListView.ItemClick += ListClick;

            if (!string.IsNullOrEmpty(bsns.photo)) {
                logoUrl = $"http://{MainActivity.Ipv4}:8080/CookTime_war/cookAPI/resources/getPicture?id={bsns.photo}";
                Bitmap bitmap = GetImageBitmapFromUrl(logoUrl);
                _logo.SetImageBitmap(bitmap);
            }

            _btnMyProfile.Click += (sender, args) =>
            {
                using var webClient2 = new WebClient
                    {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

                var url2 = "resources/getUser?id=" + loggedId;
                webClient2.Headers[HttpRequestHeader.ContentType] = "application/json";
                var request = webClient2.DownloadString(url2);

                var intent = new Intent(this, typeof(MyProfileActivity));
                intent.PutExtra("User", request);
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,
                    Android.Resource.Animation.SlideOutRight);
                Finish();
            };

            _btnSettings.Click += (sender, args) =>
            {
                //Brings dialog fragment forward
                var transaction = SupportFragmentManager.BeginTransaction();
                var dialogAdd = new DialogAdd();
                dialogAdd.Show(transaction, "settings");
                dialogAdd.BsnsId = bsns.id;
                dialogAdd.EventHandlerAdd += AddResult;

            };
            
            _btnFollowers.Click += (sender, args) => 
            {
                var intent = new Intent(this, typeof(FollowActivity));
                intent.PutExtra("Title", "Followers");
                intent.PutExtra("LoggedId", loggedId);
                intent.PutStringArrayListExtra("FollowList", bsns.followers);
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
            
            _btPost.Click += (sender, args) => 
            {
                var intent = new Intent(this, typeof(CreateRBActivity));
                intent.PutExtra("LoggedId", loggedId);
                intent.PutExtra("BsnsId", bsns.id);
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            };
            
            _btnPrivMenu.Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(PrivateMenuActivity));
                intent.PutExtra("LoggedId", loggedId);
                intent.PutExtra("BsnsId", bsns.id);
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            };

            _logo.Click += (sender, args) =>
            {
                var transaction = SupportFragmentManager.BeginTransaction();
                var dialogPicture = new DialogPicture();
                dialogPicture.Photo = bsns.photo;
                dialogPicture.Show(transaction, "choice");
                dialogPicture.EventHandlerChoice += PictureAction;
            };
        }
        
        /// <summary>
        /// This method is in charge of retrieving the result of the Picture dialog fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void PictureAction(object sender, PicEvent e)
        {
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            var response = e.Message;
            if (response == 0) {
                // do this code when the user chose to see the logo
                if (!string.IsNullOrEmpty(bsns.photo)) {
                    var transaction = SupportFragmentManager.BeginTransaction();
                    var dialogPShow = new DialogPShow();
                    
                    dialogPShow.Url = logoUrl;
                    dialogPShow.TypeText = "Business logo";
                    dialogPShow.Show(transaction, "bsns");
                }
                else {
                    // happens when the user has no image so we must display default.
                    string toastText = "you have not set a logo yet. To view one, you must first set it up.";
                    _toast = Toast.MakeText(this, toastText, ToastLength.Short);
                    _toast.Show();
                }
            }
            else {
                // do this code when the user chose to change their image.
                Intent gallery = new Intent();
                gallery.SetType("image/*");
                gallery.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(gallery, "select a logo"),0);
            }
        }
        
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                Stream picStream = ContentResolver.OpenInputStream(data.Data);
                Bitmap bitmap = BitmapFactory.DecodeStream(picStream);
                _logo.SetImageBitmap(bitmap);
                
                MemoryStream memStream = new MemoryStream();
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, memStream);
                byte[] picData = memStream.ToArray();
                
                using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var url = $"resources/addBusinessPicture?id={bsns.id}";
                try
                {
                    var base64 = Convert.ToBase64String(picData);
                    webClient.UploadString(url, base64);
                    Console.WriteLine("managed to post");
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    Console.WriteLine("Could not post");
                    // post failed, reloading profile
                    url = $"resources/getUser?id={loggedId}";
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var json = webClient.DownloadString(url);
                
                    var intent = new Intent(this, typeof(MyProfileActivity));
                    intent.PutExtra("User", json);
                    intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                    Finish();
                
                    _toast = Toast.MakeText(this, "could not post picture.", ToastLength.Short);
                    _toast.Show();
                    throw;
                }

                // if POST did not fail, now we will execute a profile refresh.
                url = $"resources/getBusiness?id={bsns.id}";
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var json1 = webClient.DownloadString(url);
                
                var intent1 = new Intent(this, typeof(MyBusiness));
                intent1.PutExtra("Bsns", json1);
                intent1.PutExtra("LoggedId", loggedId);
                intent1.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent1);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                Finish();
                
                _toast = Toast.MakeText(this, "Business logo updated. Refreshing MyBusiness...", ToastLength.Short);
                _toast.Show();
            }
        }

        
        
        
        /// <summary>
        /// This method manages clicking on a recipe item
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="eventArgs"> Contains the event data </param>
        private void ListClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            var recipeId = _menuList[eventArgs.Position].Split(';')[0];

            //Brings dialog fragment forward
            var transaction = SupportFragmentManager.BeginTransaction();
            var dialogChoice = new DialogBChoice();
            dialogChoice.RecipeId = recipeId;
            dialogChoice.BtnText = "MAKE PRIVATE";
            dialogChoice.Show(transaction, "choice");
            dialogChoice.EventHandlerChoice += ChoiceAction;
        }

        /// <summary>
        /// This method is in charge of retrieving the data entered by the user in the Settings dialog fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void AddResult(object sender, SendAddEvent e)
        {
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
                    recipeIntent.PutExtra("LoggedId", loggedId);
                    StartActivity(recipeIntent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,
                        Android.Resource.Animation.SlideOutRight);
                    break;
                }
                case 1:
                    toastText = "Recipe deleted.";
                    url = "resources/deleteRecipe?email=" + loggedId + "&id=" + e.RecipeId + "&fromMyMenu=0";
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.DownloadString(url);
                    break;
                default:
                    toastText = "Recipe made private.";
                    url = "resources/moveRecipe?recipeId=" + e.RecipeId + "&businessId=" + bsns.id;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.DownloadString(url);
                    break;
            }

            if (response == 0) return;
            url = "resources/getBusiness?id=" + bsns.id;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var json = webClient.DownloadString(url);

            var intent = new Intent(this, typeof(MyBusiness));
            intent.PutExtra("Bsns", json);
            Intent.PutExtra("LoggedId", loggedId);
            StartActivity(intent);
            OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
            Finish();

            _toast = Toast.MakeText(this, toastText + " Refreshing MyBusiness...", ToastLength.Short);
            _toast.Show();
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
        /// this method is used to obtain an image bitmap from a url.
        /// </summary>
        /// <param name="url">the string url that displays the image.</param>
        /// <returns>a Bitmap type object representing the image to cache it into memory</returns>
        private Bitmap GetImageBitmapFromUrl(string url)
        {
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