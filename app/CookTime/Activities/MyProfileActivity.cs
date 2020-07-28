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
using Stream = System.IO.Stream;

namespace CookTime.Activities {
    /// <summary>
    /// This class represents the My Profile view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MyProfileActivity : AppCompatActivity {
        private User _loggedUser;
        private string userJson;
        private string pictureUrl;
        private TextView _nameView;
        private TextView _ageView;
        private TextView _chefView;
        private TextView _scoreText;
        private ImageView _pfp;
        private Button _btnFollowers;
        private Button _btnFollowing;
        private Button _btnSettings;
        private Button _btnNewsfeed;
        private Button _btnNotif;
        private Button _btnDate;
        private Button _btnScore;
        private Button _btnDiff;
        private Button _btnRecipe;
        private Button _btnChef;
        private Button _btnBusiness;
        private string sortStr;
        private ListView _myMenuListView;
        private IList<string> _myMenuList;
        private Toast _toast;
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
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.MyProfile);

            userJson = Intent.GetStringExtra("User");
            _loggedUser = JsonConvert.DeserializeObject<User>(userJson);
            
            _nameView = FindViewById<TextView>(Resource.Id.myNameView);
            _ageView = FindViewById<TextView>(Resource.Id.myAgeView);
            _chefView = FindViewById<TextView>(Resource.Id.chefText);
            _scoreText = FindViewById<TextView>(Resource.Id.scoreText);

            _pfp = FindViewById<ImageView>(Resource.Id.profilePic);
            
            _btnFollowers = FindViewById<Button>(Resource.Id.btnMyFollowers);
            _btnFollowing = FindViewById<Button>(Resource.Id.btnMyFollowing);
            _btnSettings = FindViewById<Button>(Resource.Id.btnSettings);
            _btnNewsfeed = FindViewById<Button>(Resource.Id.btnNewsfeed);
            _btnNotif = FindViewById<Button>(Resource.Id.btnNotif);
            _btnDate = FindViewById<Button>(Resource.Id.btnDate);
            _btnScore = FindViewById<Button>(Resource.Id.btnScore);
            _btnDiff = FindViewById<Button>(Resource.Id.btnDiff);
            _btnRecipe = FindViewById<Button>(Resource.Id.btnRecipe);
            _btnChef = FindViewById<Button>(Resource.Id.btnChef);
            _btnBusiness = FindViewById<Button>(Resource.Id.btnCreateBsns);
            
            _myMenuListView = FindViewById<ListView>(Resource.Id.myMenuListView);

            _nameView.Text = "Name: " + _loggedUser.firstName + " " + _loggedUser.lastName;
            _ageView.Text = "Age: " + _loggedUser.age;

            if (!string.IsNullOrEmpty(_loggedUser.photo)) {
                Console.WriteLine(_loggedUser.photo);
                pictureUrl = $"http://{MainActivity.Ipv4}:8080/CookTime_war/cookAPI/resources/getPicture?id={_loggedUser.photo}";
                Bitmap bitmap = GetImageBitmapFromUrl(pictureUrl);
                _pfp.SetImageBitmap(bitmap);
            }

            if (_loggedUser.chef) {
                _chefView.Text = "Chef: yes";
                _scoreText.Text = "Score : " + _loggedUser.chefScore;
            }
            else {
                _chefView.Text = "Chef: no";
                _scoreText.Text = "";
            }
            
            _btnFollowers.Text = "FOLLOWERS: " + _loggedUser.followerEmails.Count;
            _btnFollowing.Text = "FOLLOWING: " + _loggedUser.followingEmails.Count;
            
            using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            
            var url = "resources/myMenu?email=" + _loggedUser.email + "&filter=date";
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var send = webClient.DownloadString(url);
            
            _myMenuList = JsonConvert.DeserializeObject<IList<string>>(send);
            
            _adapter = new RecipeAdapter(this, _myMenuList);
            _myMenuListView.Adapter = _adapter;
            _myMenuListView.ItemClick += ListClick;
            
            _btnNewsfeed.Click += (sender, args) =>
            {
                using var webClient2 = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

                var url2 = "resources/getUser?id=" + _loggedUser.email;
                webClient2.Headers[HttpRequestHeader.ContentType] = "application/json";
                var request = webClient2.DownloadString(url2);
                
                var intent = new Intent(this, typeof(NewsfeedActivity));
                intent.PutExtra("User", request);
                
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                Finish();
            };
            
            _btnSettings.Click += (sender, args) =>
            {
                //Brings dialog fragment forward
                var transaction = SupportFragmentManager.BeginTransaction();
                var dialogSettings = new DialogSettings();
                dialogSettings.Show(transaction, "settings");
            
                dialogSettings.Email = _loggedUser.email;
                dialogSettings.EventHandlerPass += PassResult;
            };
            
            _btnFollowers.Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(FollowActivity));
                intent.PutExtra("Title", "Followers");
                intent.PutExtra("LoggedId", _loggedUser.email);
                intent.PutStringArrayListExtra("FollowList", _loggedUser.followerEmails);
                
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            };
            
            _btnFollowing.Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(FollowActivity));
                intent.PutExtra("Title", "Following");
                intent.PutExtra("LoggedId", _loggedUser.email);
                intent.PutStringArrayListExtra("FollowList", _loggedUser.followingEmails);
                
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            };
            
            _btnNotif.Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(NotifActivity));
                intent.PutStringArrayListExtra("NotifList", _loggedUser.notifications);
                intent.PutExtra("LoggedId", _loggedUser.email);

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
            
            _btnRecipe.Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(CreateRActivity));
                intent.PutExtra("LoggedId", _loggedUser.email);

                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            };
            
            _btnChef.Click += (sender, args) =>
            {
                if (_loggedUser.chef) {
                    var toast = Toast.MakeText(this, "You're already a chef", ToastLength.Short);
                    toast.Show();
                }
                else {
                    //Brings dialog fragment forward
                    var transaction = SupportFragmentManager.BeginTransaction();
                    var dialogChef = new DialogChef();
                    dialogChef.LoggedId = _loggedUser.email;
                    dialogChef.Show(transaction, "chef");
                    
                    dialogChef.EventHandlerChef += ChefResult;
                }
            };
            
            _btnBusiness.Click += (sender, args) =>
            {
                if (_loggedUser.business == 0) {
                    var intent = new Intent(this, typeof(CreateBActivity));
                    intent.PutExtra("LoggedId", _loggedUser.email);

                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                }
                else {
                    using var webClient3 = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                    var url3= "resources/getBusiness?id=" + _loggedUser.business;
                    webClient3.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var bsnsJson = webClient3.DownloadString(url3);
                    
                    var intent = new Intent(this, typeof(MyBusiness));
                    intent.PutExtra("Bsns", bsnsJson);
                    intent.PutExtra("LoggedId", _loggedUser.email);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                }
            };
            _pfp.Click += (sender, args) =>
            {
                var transaction = SupportFragmentManager.BeginTransaction();
                var dialogPicture = new DialogPicture();
                dialogPicture.Photo = _loggedUser.photo;
                dialogPicture.Show(transaction, "choice");
                dialogPicture.EventHandlerChoice += PictureAction;
            };
        }

        private void PictureAction(object sender, PicEvent e)
        {
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            var response = e.Message;
            if (response == 0) {
                // do this code when the user chose to see the image
                if (!string.IsNullOrEmpty(_loggedUser.photo)) {
                    var transaction = SupportFragmentManager.BeginTransaction();
                    var dialogPShow = new DialogPShow();
                    
                    dialogPShow.Url = pictureUrl;
                    dialogPShow.TypeText = "Profile Picture";
                    dialogPShow.Show(transaction, "pfp");
                }
                else {
                    // happens when the user has no image so we must display default.
                    string toastText = "you have not set a picture yet. To view one, you must first set it up.";
                    _toast = Toast.MakeText(this, toastText, ToastLength.Short);
                    _toast.Show();
                }
            }
            else {
                // do this code when the user chose to change their image.
                Intent gallery = new Intent();
                gallery.SetType("image/*");
                gallery.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(gallery, "select a photo"),0);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                Stream picStream = ContentResolver.OpenInputStream(data.Data);
                Bitmap bitmap = BitmapFactory.DecodeStream(picStream);
                _pfp.SetImageBitmap(bitmap);
                
                MemoryStream memStream = new MemoryStream();
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, memStream);
                byte[] picData = memStream.ToArray();
                
                using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var url = $"resources/addUserPicture?id={_loggedUser.email}";
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
                    url = $"resources/getUser?id={_loggedUser.email}";
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
                url = $"resources/getUser?id={_loggedUser.email}";
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var json1 = webClient.DownloadString(url);
                
                var intent1 = new Intent(this, typeof(MyProfileActivity));
                intent1.PutExtra("User", json1);
                intent1.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent1);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                Finish();
                
                _toast = Toast.MakeText(this, "Profile picture updated. Refreshing MyProfile...", ToastLength.Short);
                _toast.Show();
            }
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


        /// <summary>
        /// This method is in charge of retrieving of showing recipes when clicking on them
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="eventArgs"> Contains the event data </param>
        private void ListClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            var recipeId = _myMenuList[eventArgs.Position].Split(';')[0];
            
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
        private void PassResult(object sender, SendPassEvent e) {
            var toastText = e.Message switch
            {
                "3" => "Please fill in all of the information",
                "2" => "The new passwords do not match",
                "0" => "Wrong current password",
                _ => "Password changed successfully"
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
                var recipeIntent = new Intent(this, typeof(RecipeActivity));
                recipeIntent.PutExtra("Recipe", request);
                recipeIntent.PutExtra("LoggedId", _loggedUser.email);
                StartActivity(recipeIntent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            }
            else
            {
                url = "resources/deleteRecipe?email=" + _loggedUser.email + "&id=" + e.RecipeId + "&fromMyMenu=1";
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.DownloadString(url);
                
                url = "resources/getUser?id=" + _loggedUser.email;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var json = webClient.DownloadString(url);
                
                var intent = new Intent(this, typeof(MyProfileActivity));
                intent.PutExtra("User", json);
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                Finish();
                
                _toast = Toast.MakeText(this, "Recipe deleted. Refreshing MyProfile...", ToastLength.Short);
                _toast.Show();
            }
        }

        /// <summary>
        /// This method sorts the MyMenu of the user according to the sortStr attribute
        /// </summary>
        private void SortMenu()
        {
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            var url = "resources/myMenu?email=" + _loggedUser.email + "&filter=" + sortStr;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var request = webClient.DownloadString(url);

            _myMenuList = JsonConvert.DeserializeObject<IList<string>>(request);
            _adapter.RecipeItems = _myMenuList;
            _myMenuListView.Adapter = _adapter;
        }
        
        /// <summary>
        /// This method is in charge of retrieving theresult of a chef request by the user..
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void ChefResult(object sender, SendChefEvent e) {
            var toastText = "You already have a pending request";
            if (e.Message == "2") {
                toastText = "Request sent!";

                using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                var url = "resources/getUser?id=" + _loggedUser.email;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var message = webClient.DownloadString(url);
                
                var intent = new Intent(this, typeof(MyProfileActivity));
                intent.PutExtra("User", message);
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                Finish();
            }
            _toast = Toast.MakeText(this, toastText, ToastLength.Short);
            _toast.Show();
        }
    }
}