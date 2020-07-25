using System;
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
using Square.Picasso;
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
        private string picassoUrl;
        private TextView _nameView;
        private TextView _ageView;
        private TextView _chefView;
        private TextView _scoreText;
        private ImageButton _pfp;
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

            _pfp = FindViewById<ImageButton>(Resource.Id.profilePic);
            
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

            if (!string.IsNullOrEmpty(_loggedUser.userPhotos))
            {
                picassoUrl = $"http://{MainActivity.Ipv4}:8080/CookTime_war/cookAPI/resources/getPicture?id={_loggedUser.userPhotos}";
                Picasso.Get().Load(picassoUrl).Into(_pfp);
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
                dialogPicture.Photo = _loggedUser.userPhotos;
                dialogPicture.Show(transaction, "choice");
                dialogPicture.EventHandlerChoice += PictureAction;
            };
        }

        private void PictureAction(object sender, PicEvent e)
        {
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            // var url = "resources/getImage?user=" + _loggedUser.email;
            // webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            // var request = webClient.DownloadString(url);
            var response = e.Message;
            if (response == 0) {
                // do this code when the user chose to see the image
                if (!string.IsNullOrEmpty(_loggedUser.userPhotos)) {
                    Intent seePicIntent = new Intent(this, typeof(PictureActivity));
                    seePicIntent.PutExtra("type", "user");
                    seePicIntent.PutExtra("photo", picassoUrl);
                    StartActivity(seePicIntent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
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
                //TODO put the code that shows how to access gallery and assign the new byte array.
                Intent gallery = new Intent();
                gallery.SetType("image/*");
                gallery.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(gallery, "select a photo"),0);
                //string byte64 = Convert.ToBase64String(_loggedUser.photo, 0, _loggedUser.photo.Length);
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
                //_pfp.SetImageBitmap(DecodeBitmapFromStream(data.Data, 200, 200));
                // TODO send the image to the server with POST API method.
                
                
                
                // using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                // webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                // var url = $"resource/addUserPicture?id={_loggedUser.email}";
                // webClient.UploadString(url, photo);
            }
        }

        private Bitmap DecodeBitmapFromStream(Android.Net.Uri data, int requestedWidth, int requestedHeight)
        {
            //Decode with inJustDecodeBounds = true to check dimensions
            Stream stream = ContentResolver.OpenInputStream(data);
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InJustDecodeBounds = true;
            BitmapFactory.DecodeStream(stream, null, options);
            
            //Calculate inSampleSize
            options.InSampleSize = CalculateInSampleSize(options, requestedWidth, requestedHeight);
            Console.WriteLine("inSampleSize: " + CalculateInSampleSize(options, requestedWidth, requestedHeight));
            Console.WriteLine("outW: " + options.OutHeight);
            Console.WriteLine("outH" + options.OutWidth);
            Bitmap bitmap = BitmapFactory.DecodeStream(stream, null, options);
            return bitmap;
        }

        private int CalculateInSampleSize(BitmapFactory.Options options, int requestedWidth, int requestedHeight)
        {
            //raw height and width of image
            int height = options.OutHeight;
            int width = options.OutWidth;
            int inSampleSize = 1;

            if (height > requestedHeight || width > requestedWidth)
            {
                //here the image is bigger than we need it to be
                int halfHeight = height / 2;
                int halfWidth = width / 2;
                while ((halfHeight / inSampleSize) > requestedHeight && (halfWidth / inSampleSize) > requestedWidth)
                {
                    inSampleSize *= 2;
                }
            }
            return inSampleSize;
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