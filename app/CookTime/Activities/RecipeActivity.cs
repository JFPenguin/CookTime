using System;
using System.Net;
using System.Net.Mime;
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
        //axml objects
        private Button shareButton;
        private Button rateButton;
        private Button authorButton;
        
        private TextView recipeNameText;
        private TextView authorText;
        private TextView dishTimeText;
        private TextView portionsText;
        private TextView durationText;
        private TextView difficultyText;
        private TextView dishTagsText;
        private TextView ingredientsText;
        private TextView instructionText;
        private TextView priceText;
        private TextView scoreText;
        private TextView ratedByText;
        private TextView scoreTimes;
        private ImageView recipeImage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Recipe);
            
            var recipe = Intent.GetStringExtra("Recipe");
            _recipe = JsonConvert.DeserializeObject<Recipe>(recipe);
            //TODO fix the line above, it crashes the application when loading this activity.
            authorName = Intent.GetStringExtra("Author");
            using var webClient = new WebClient{BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
            var url = "resources/getPicture?id=" + _recipe.photos[0];
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var photo = webClient.DownloadString(url);

            // setting all the fields to axml file identities
            shareButton = FindViewById<Button>(Resource.Id.shareButton);
            rateButton = FindViewById<Button>(Resource.Id.rateButton);
            authorButton = FindViewById<Button>(Resource.Id.authorButton);

            recipeNameText = FindViewById<TextView>(Resource.Id.recipeNameText);
            authorText = FindViewById<TextView>(Resource.Id.authorText);
            dishTimeText = FindViewById<TextView>(Resource.Id.dishTimeText);
            portionsText = FindViewById<TextView>(Resource.Id.portionsText);
            durationText = FindViewById<TextView>(Resource.Id.durationText);
            difficultyText = FindViewById<TextView>(Resource.Id.difficultyText);
            dishTagsText = FindViewById<TextView>(Resource.Id.dishTagstext);
            ingredientsText = FindViewById<TextView>(Resource.Id.ingredientsText);
            instructionText = FindViewById<TextView>(Resource.Id.instructionText);
            priceText = FindViewById<TextView>(Resource.Id.priceText);
            scoreText = FindViewById<TextView>(Resource.Id.scoreText);
            ratedByText = FindViewById<TextView>(Resource.Id.ratedByText);
            scoreTimes = FindViewById<TextView>(Resource.Id.sTimesText);
            recipeImage = FindViewById<ImageView>(Resource.Id.recipeImage);

            //setting all the text values to the recipe attribute
            recipeNameText.Text = _recipe.name;
            authorText.Text = "author: " + authorName;
            dishTimeText.Text = "time: " + _recipe.dishTime;
            portionsText.Text = "portions: " + _recipe.portions;
            durationText.Text = "duration: " + _recipe.duration + " minutes";
            difficultyText.Text = "difficulty: " + _recipe.difficulty;
            dishTagsText.Text = "tags: " + _recipe.dishTags[0];
            ingredientsText.Text = "ingredients: " + _recipe.ingredientsList[0];
            instructionText.Text = "instructions: " + _recipe.instructions[0];
            priceText.Text = "price: " + _recipe.price + " $";
            scoreText.Text = "score: " + _recipe.score;
            ratedByText.Text = "rated by: " + "none yet"; //so far, this array contains no elements and caused nullpointerexceptions to crash the app.
            scoreTimes.Text = "times rated: " + _recipe.scoreTimes;
            //TODO set the image in the ImageView
        }
    }
}