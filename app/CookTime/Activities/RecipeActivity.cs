using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;

namespace CookTime.Activities {
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class RecipeActivity : AppCompatActivity
    {
        private Recipe _recipe;
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
        private TextView dishTagsText;
        private TextView ingredientsText;
        private TextView instructionText;
        private TextView priceText;
        private TextView scoreText;
        private TextView scoreTimes;
        private ImageView recipeImage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Recipe);
            
            var recipe = Intent.GetStringExtra("Recipe");
            _recipe = JsonConvert.DeserializeObject<Recipe>(recipe);
            authorName = Intent.GetStringExtra("AuthorName");
            
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
            dishTagsText = FindViewById<TextView>(Resource.Id.dishTagstext);
            ingredientsText = FindViewById<TextView>(Resource.Id.ingredientsText);
            instructionText = FindViewById<TextView>(Resource.Id.instructionText);
            priceText = FindViewById<TextView>(Resource.Id.priceText);
            scoreText = FindViewById<TextView>(Resource.Id.scoreText);
            scoreTimes = FindViewById<TextView>(Resource.Id.sTimesText);
            recipeImage = FindViewById<ImageView>(Resource.Id.recipeImage);

            //setting all the text values to the recipe attribute
            recipeNameText.Text = _recipe.name;
            authorText.Text = "Author: " + authorName;
            dateText.Text = "Date posted: " + _recipe.postTimeString;
            dishTypeText.Text = "Dish type: " + _recipe.dishType;
            dishTimeText.Text = "Dish Time: " + _recipe.dishTime;
            portionsText.Text = "Portions: " + _recipe.portions;
            durationText.Text = "Duration: " + _recipe.duration + " minutes";
            difficultyText.Text = "Difficulty: " + _recipe.difficulty;
            dishTagsText.Text = "Tags: " + _recipe.dishTags[0];
            ingredientsText.Text = "Ingredients: " + _recipe.ingredientsList[0];
            instructionText.Text = "Instructions: " + _recipe.instructions[0];
            priceText.Text = "Price: $" + _recipe.price;
            scoreText.Text = "Score: " + _recipe.score;
            scoreTimes.Text = "Number of Ratings: " + _recipe.scoreTimes;
            //TODO set the image in the ImageView
        }
    }
}