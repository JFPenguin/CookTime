using System;
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
            authorText.Text = authorName;
            dishTimeText.Text = _recipe.dishTime;
            portionsText.Text = _recipe.portions.ToString();
            durationText.Text = _recipe.duration.ToString();
            difficultyText.Text = _recipe.difficulty.ToString();
            dishTagsText.Text = "not yet";
            ingredientsText.Text = "not yet";
            instructionText.Text = "not yet";
            priceText.Text = _recipe.price.ToString();
            scoreText.Text = _recipe.score.ToString();
            ratedByText.Text = "none yet";
            scoreTimes.Text = _recipe.scoreTimes.ToString();
        }
    }
}