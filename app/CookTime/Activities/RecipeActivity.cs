using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;

namespace CookTime.Activities {
    /// <summary>
    /// This class represents a Recipe Profile
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class RecipeActivity : AppCompatActivity {
        private Recipe _recipe;
        private string _loggedId;
        private string authorName;

        private Button shareButton;
        private Button rateButton;
        private Button authorButton;
        
        private TextView recipeNameText;
        private TextView authorText;
        private TextView dateText;
        private TextView dishTypeText;
        private TextView dishTimeText;
        private TextView portionsText;
        private TextView durationText;
        private TextView difficultyText;
        private TextView priceText;
        private TextView scoreText;
        private TextView scoreTimes;
        private ListView ingredientListView;
        private ImageView recipeImage;

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
            authorName = Intent.GetStringExtra("AuthorName");
            _loggedId = Intent.GetStringExtra("LoggedId");
            
            // using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            // var url = "resources/getPicture?id=" + _recipe.photos[0];
            // webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            // var photo = webClient.DownloadString(url);

            // setting all the fields to axml file identities
            shareButton = FindViewById<Button>(Resource.Id.shareButton);
            rateButton = FindViewById<Button>(Resource.Id.rateButton);
            authorButton = FindViewById<Button>(Resource.Id.authorButton);

            recipeNameText = FindViewById<TextView>(Resource.Id.recipeNameText);
            authorText = FindViewById<TextView>(Resource.Id.authorText);
            dateText = FindViewById<TextView>(Resource.Id.dateText);
            dishTypeText = FindViewById<TextView>(Resource.Id.dishTypeText);
            dishTimeText = FindViewById<TextView>(Resource.Id.dishTimeText);
            portionsText = FindViewById<TextView>(Resource.Id.portionsText);
            durationText = FindViewById<TextView>(Resource.Id.durationText);
            difficultyText = FindViewById<TextView>(Resource.Id.difficultyText);
            priceText = FindViewById<TextView>(Resource.Id.priceText);
            scoreText = FindViewById<TextView>(Resource.Id.scoreText);
            scoreTimes = FindViewById<TextView>(Resource.Id.sTimesText);
            ingredientListView = FindViewById<ListView>(Resource.Id.ingredientListView);
            recipeImage = FindViewById<ImageView>(Resource.Id.recipeImage);

            // Setting all the text values to the recipe attribute
            recipeNameText.Text = _recipe.name;
            authorText.Text = "Author: " + authorName;
            dateText.Text = "Date posted: " + _recipe.postTimeString;
            dishTypeText.Text = "Dish type: " + _recipe.dishType;
            dishTimeText.Text = "Dish Time: " + _recipe.dishTime;
            portionsText.Text = "Portions: " + _recipe.portions;
            durationText.Text = "Duration: " + _recipe.duration + " minutes";
            difficultyText.Text = "Difficulty: " + _recipe.difficulty;
            priceText.Text = "Price: $" + _recipe.price;
            scoreText.Text = "Score: " + _recipe.score;
            scoreTimes.Text = "Number of Ratings: " + _recipe.scoreTimes;
            
            CompAdapter adapter = new CompAdapter(this, _recipe.ingredientsList);
            ingredientListView.Adapter = adapter;
            
            //TODO set the image in the ImageView
            
            authorButton.Click += (sender, args) => 
            {
                if (_recipe.authorEmail == _loggedId) {
                    using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

                    var url = "resources/getUser?id=" + _recipe.authorEmail;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var send = webClient.DownloadString(url);
                
                    Intent intent = new Intent(this, typeof(MyProfileActivity));
                    intent.PutExtra("User", send); 
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                }
                else {
                    using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

                    var url = "resources/getUser?id=" + _recipe.authorEmail;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var send = webClient.DownloadString(url);
                
                    Intent intent = new Intent(this, typeof(PrivProfileActivity));
                    intent.PutExtra("User", send);
                    intent.PutExtra("LoggedId", _loggedId);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight); 
                }
            };
        }
    }
}