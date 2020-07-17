using System.Net.Mime;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace CookTime.Activities {
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class RecipeActivity : AppCompatActivity
    {
        private int _id;
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
        private ImageView recipeImage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Recipe);
            // setting all the fields to axml file identities
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
            recipeImage = FindViewById<ImageView>(Resource.Id.recipeImage);
            

        }
    }
}