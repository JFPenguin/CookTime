﻿using System.Net;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using CookTime.Adapters;
using CookTime.DialogFragments;
using Newtonsoft.Json;

namespace CookTime.Activities {
    /// <summary>
    /// This class represents a Recipe Profile
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class RecipeActivity : AppCompatActivity {
        private Recipe _recipe;
        private string _loggedId;
        private string picUrl;
        private Toast _toast;
        
        private Button authorButton;
        private Button rateButton;
        private Button commentButton;
        private Button shareButton;

        private ImageView _recipePic;
        private TextView recipeNameText;
        private TextView authorText;
        private TextView dateText;
        private TextView dishTypeText;
        private TextView dishTimeText;
        private TextView portionsText;
        private TextView durationText;
        private TextView difficultyText;
        private TextView dishTagsText;
        private TextView priceText;
        private TextView scoreText;
        private TextView scoreTimes;
        private TextView commentsText;
        private ListView ingredientListView;
        private ListView instructionsListView;
        private ListView dishTagsListView;
        private ListView commentsListView;

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
        /// All of the recipe info is shown here.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Recipe);
            

            var recipe = Intent.GetStringExtra("Recipe");
            _recipe = JsonConvert.DeserializeObject<Recipe>(recipe);
            _loggedId = Intent.GetStringExtra("LoggedId");

            // setting all the fields to axml file identities
            authorButton = FindViewById<Button>(Resource.Id.authorButton);
            rateButton = FindViewById<Button>(Resource.Id.rateButton);
            commentButton = FindViewById<Button>(Resource.Id.commentButton);
            shareButton = FindViewById<Button>(Resource.Id.shareButton);

            _recipePic = FindViewById<ImageView>(Resource.Id.recipePicture);
            recipeNameText = FindViewById<TextView>(Resource.Id.recipeNameText);
            authorText = FindViewById<TextView>(Resource.Id.authorText);
            dateText = FindViewById<TextView>(Resource.Id.dateText);
            dishTypeText = FindViewById<TextView>(Resource.Id.dishTypeText);
            dishTimeText = FindViewById<TextView>(Resource.Id.dishTimeText);
            portionsText = FindViewById<TextView>(Resource.Id.portionsText);
            durationText = FindViewById<TextView>(Resource.Id.durationText);
            difficultyText = FindViewById<TextView>(Resource.Id.difficultyText);
            dishTagsText = FindViewById<TextView>(Resource.Id.dishTagsText);
            priceText = FindViewById<TextView>(Resource.Id.priceText);
            scoreText = FindViewById<TextView>(Resource.Id.scoreText);
            scoreTimes = FindViewById<TextView>(Resource.Id.sTimesText);
            commentsText = FindViewById<TextView>(Resource.Id.commentsText);
            ingredientListView = FindViewById<ListView>(Resource.Id.ingredientListView);
            instructionsListView = FindViewById<ListView>(Resource.Id.instructionsListView);
            dishTagsListView = FindViewById<ListView>(Resource.Id.dishTagsListView);
            commentsListView = FindViewById<ListView>(Resource.Id.commentsListView);

            // Setting all the text values to the recipe attribute
            recipeNameText.Text = _recipe.name;
            authorText.Text = "Author: " + _recipe.authorName;
            dateText.Text = "Date posted: " + _recipe.postTimeString;
            dishTypeText.Text = "Dish type: " + _recipe.dishType;
            dishTimeText.Text = "Dish Time: " + _recipe.dishTime;
            portionsText.Text = "Portions: " + _recipe.portions;
            durationText.Text = "Duration: " + _recipe.duration + " minutes";
            difficultyText.Text = "Difficulty: " + _recipe.difficulty;
            dishTagsText.Text = _recipe.dishTags.Count == 0 ? "Dish tags: none" : "Dish tags:";
            priceText.Text = "Price: $" + _recipe.price;
            scoreText.Text = "Score: " + _recipe.score;
            scoreTimes.Text = "Number of Ratings: " + _recipe.scoreTimes;
            commentsText.Text = _recipe.comments.Count == 0 ? "Comments: none" : "Comments:";

            if (!string.IsNullOrEmpty(_recipe.photo))
            {
                picUrl = $"http://{MainActivity.Ipv4}:8080/CookTime_war/cookAPI/resources/getPicture?id={_recipe.photo}";
                Bitmap bitmap = GetImageBitmapFromUrl(picUrl);
                _recipePic.SetImageBitmap(bitmap);   
            }

            var adapter1 = new IngredientAdapter(this, _recipe.ingredientsList);
            ingredientListView.Adapter = adapter1;
            
            var adapter2 = new CompAdapter(this, _recipe.instructions);
            instructionsListView.Adapter = adapter2;
            
            var adapter3 = new CompAdapter(this, _recipe.dishTags);
            dishTagsListView.Adapter = adapter3;
            
            var adapter4 = new CommentAdapter(this, _recipe.comments);
            commentsListView.Adapter = adapter4;

            _recipePic.Click += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(_recipe.photo)) {
                    var transaction = SupportFragmentManager.BeginTransaction();
                    var dialogPShow = new DialogPShow();
                    dialogPShow.Url = picUrl;
                    dialogPShow.TypeText = "Recipe photo";
                    dialogPShow.Show(transaction, "recipe");
                }
            };

            shareButton.Click += (sender, args) =>
            {
                using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

                var url = "resources/shareRecipe?id=" + _recipe.id + "&email=" + _loggedId;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var send = webClient.DownloadString(url);

                string toastText;
                if (send == "0") {
                    toastText = "You've already shared this recipe.";
                }

                else {
                    toastText = "Recipe shared! Redirecting to MyProfile...";
                    url = "resources/getUser?id=" + _loggedId;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var userJson = webClient.DownloadString(url);
                    
                    var intent = new Intent(this, typeof(MyProfileActivity));
                    intent.PutExtra("User", userJson);
                    intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);  
                }
                var toast = Toast.MakeText(this, toastText, ToastLength.Short);
                toast.Show();
            };
            
           commentButton.Click += (sender, args) =>
            {
                //Brings dialog fragment forward
                 var transaction = SupportFragmentManager.BeginTransaction();
                 var dialogComm = new DialogComment();
                
                 dialogComm.Show(transaction, "rate");
                 dialogComm.LoggedId = _loggedId;
                 dialogComm.RecipeId = _recipe.id;
                    
                dialogComm.EventHandlerComm += CommResult;
            };
            
            rateButton.Click += (sender, args) =>
            {
                //Brings dialog fragment forward
                var transaction = SupportFragmentManager.BeginTransaction();
                var dialogRate = new DialogRate();
                
                dialogRate.Show(transaction, "rate");
                dialogRate.LoggedId = _loggedId;
                dialogRate.RecipeId = _recipe.id;
                dialogRate.Type = 0;
                    
                dialogRate.EventHandlerRate += RateResult;
            };
            
            authorButton.Click += (sender, args) =>
            {
                Intent intent;
                using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                string url;
                string send;
                
                if (_recipe.businessId == 0) 
                {
                    url = "resources/getUser?id=" + _recipe.authorEmail;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    send = webClient.DownloadString(url);
                    
                    if (_recipe.authorEmail == _loggedId) {
                        intent = new Intent(this, typeof(MyProfileActivity));
                        intent.PutExtra("User", send); 
                        StartActivity(intent);
                        OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                    }
                    else {
                        intent = new Intent(this, typeof(PrivProfileActivity));
                        intent.PutExtra("User", send);
                        intent.PutExtra("LoggedId", _loggedId);
                        StartActivity(intent);
                        OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight); 
                    } 
                }
                else
                {
                    url = "resources/getBusiness?id=" + _recipe.businessId;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    send = webClient.DownloadString(url);
                
                    url = "resources/isEmployee?email=" + _loggedId + "&id=" + _recipe.businessId;
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
                         intent = new Intent(this, typeof(PrivateBusiness));
                         intent.PutExtra("Bsns", send);
                         intent.PutExtra("LoggedId", _loggedId);
                         StartActivity(intent);
                         OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);   
                    }
                }
            };
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
        /// This method is in charge of retrieving the result of the Comment Recipe dialog fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void CommResult(object sender, SendCommEvent e) {
            string toastText;

            if (e.Message == "0") {
                toastText = "Please enter a comment";
            }
            
            else {
                toastText = "Recipe commented! Redirecting to the newsfeed...";
                
                using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                var url = "resources/getUser?id=" + _loggedId;
                
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var userJson = webClient.DownloadString(url);
                
                var intent = new Intent(this, typeof(NewsfeedActivity));
                intent.PutExtra("User", userJson);
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
            }
            _toast = Toast.MakeText(this, toastText, ToastLength.Long);
            _toast.Show();
        }
        
        /// <summary>
        /// This method is in charge of retrieving the result of the Rate Recipe dialog fragment.
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
                    toastText = "You cannot rate this recipe. You either are its owner or you already rated it.";
                    break;
                default:
                {
                    toastText = "Recipe rated! Redirecting to the newsfeed...";
                
                    using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                    var url = "resources/getUser?id=" + _loggedId;
                
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var userJson = webClient.DownloadString(url);
                
                    var intent = new Intent(this, typeof(NewsfeedActivity));
                    intent.PutExtra("User", userJson);
                    intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                    break;
                }
            }
            _toast = Toast.MakeText(this, toastText, ToastLength.Long);
            _toast.Show();
        }
    }
}