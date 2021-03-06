﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;

namespace CookTime.Activities {
    /// <summary>
    /// This class represents the form that businesses use to create a recipe.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class CreateRBActivity : AppCompatActivity {
        private string _loggedId;
        private string _picture64;
        private int _bsnsId;
        private EditText recipeNameEditText;
        private EditText recipePortionsEditText;
        private EditText recipeDurationEditText;
        private EditText recipeIngredientEditText;
        private EditText recipeQuantityEditText;
        private EditText recipeUnitEditText;
        private EditText recipePriceEditText;
        private EditText recipeInstructionEditText;
        private RadioGroup radioGroupDiff;
        private RadioGroup radioGroupTime;
        private RadioGroup radioGroupType;
        private RadioGroup radioGroupVis;
        private CheckBox checkBox;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private CheckBox checkBox5;
        private CheckBox checkBox6;
        private Button btnIngredient;
        private Button btnInstruction;
        private Button btnImage;
        private Button btnPost;
        private bool tagsChecked;
        private bool _imageSelected;
        private Toast _toast;
        private string toastText;
        private List<string> ingredients = new List<string>();
        private List<string> instructions = new List<string>();
        private List<string> tags = new List<string>();
        
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
        /// The list of followers/following is displayed here.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateBRecipe);
            
            _loggedId = Intent.GetStringExtra("LoggedId");
            _bsnsId = Intent.GetIntExtra("BsnsId", 0);
            
            recipeNameEditText = FindViewById<EditText>(Resource.Id.editText);
            recipePortionsEditText = FindViewById<EditText>(Resource.Id.editText2);
            recipeDurationEditText = FindViewById<EditText>(Resource.Id.editText3);
            recipeIngredientEditText = FindViewById<EditText>(Resource.Id.editText4);
            recipeQuantityEditText = FindViewById<EditText>(Resource.Id.editText5);
            recipeUnitEditText = FindViewById<EditText>(Resource.Id.editText6);
            recipeInstructionEditText = FindViewById<EditText>(Resource.Id.editText7);
            recipePriceEditText = FindViewById<EditText>(Resource.Id.editText8);
            radioGroupDiff = FindViewById<RadioGroup>(Resource.Id.radioGroupDiff);
            radioGroupTime = FindViewById<RadioGroup>(Resource.Id.radioGroupTime);
            radioGroupType = FindViewById<RadioGroup>(Resource.Id.radioGroupType);
            radioGroupVis = FindViewById<RadioGroup>(Resource.Id.radioGroupVis);
            checkBox = FindViewById<CheckBox>(Resource.Id.checkBox);
            checkBox2 = FindViewById<CheckBox>(Resource.Id.checkBox2);
            checkBox3 = FindViewById<CheckBox>(Resource.Id.checkBox3);
            checkBox4 = FindViewById<CheckBox>(Resource.Id.checkBox4);
            checkBox5 = FindViewById<CheckBox>(Resource.Id.checkBox5);
            checkBox6 = FindViewById<CheckBox>(Resource.Id.checkBox6);
            btnIngredient = FindViewById<Button>(Resource.Id.btnIngredient);
            btnInstruction = FindViewById<Button>(Resource.Id.btnInstruction);
            btnImage = FindViewById<Button>(Resource.Id.btnImage);
            btnPost = FindViewById<Button>(Resource.Id.btnPost);
            
            btnIngredient.Click += (sender, args) =>
            {
                if (recipeIngredientEditText.Text.Equals("") || recipeQuantityEditText.Text.Equals("") ||
                    recipeUnitEditText.Text.Equals(""))
                {
                    toastText = "Please fill in all of the ingredient information";
                }
                else
                {
                    toastText = "Ingredient added!";
                    ingredients.Add(recipeIngredientEditText.Text + ";" + recipeQuantityEditText.Text + ";" + 
                                     recipeUnitEditText.Text);
                    recipeIngredientEditText.Text = "";
                    recipeQuantityEditText.Text = "";
                    recipeUnitEditText.Text = "";
                }
                _toast = Toast.MakeText(this, toastText, ToastLength.Short);
                _toast.Show();
            };
            
            btnInstruction.Click += (sender, args) =>
            {
                if (recipeInstructionEditText.Text.Equals(""))
                {
                    toastText = "Please fill in the instruction";
                }
                else
                {
                    toastText = "Instruction added!";
                    instructions.Add(recipeInstructionEditText.Text);
                    recipeInstructionEditText.Text = "";
                }
                _toast = Toast.MakeText(this, toastText, ToastLength.Short);
                _toast.Show();
            };
            btnImage.Click += (sender, args) =>
            {
                Intent gallery = new Intent();
                gallery.SetType("image/*");
                gallery.SetAction(Intent.ActionGetContent);
                this.StartActivityForResult(Intent.CreateChooser(gallery, "select a photo"), 0);
            };
            
            btnPost.Click += (sender, args) =>
            {
                if (checkBox.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked
                    || checkBox6.Checked) 
                {
                    tagsChecked = true;
                }
                else {
                    tagsChecked = false;
                }

                if (recipeNameEditText.Text.Equals("") || recipePortionsEditText.Text.Equals("") ||
                    recipeDurationEditText.Text.Equals("") || recipePriceEditText.Text.Equals("") || 
                    instructions.Count == 0 || ingredients.Count == 0 || radioGroupDiff.CheckedRadioButtonId == -1 || 
                    radioGroupTime.CheckedRadioButtonId == -1 || radioGroupType.CheckedRadioButtonId == -1 || 
                    radioGroupVis.CheckedRadioButtonId == -1 || !tagsChecked || !_imageSelected)
                {
                    toastText = "Please fill in all of the required information";
                    if (!_imageSelected)
                    {
                        toastText = "you must select an image to continue";
                    }
                }
                else
                {
                    toastText = "Recipe posted!";

                    var name = recipeNameEditText.Text;
                    var portions = int.Parse(recipePortionsEditText.Text);
                    var duration = int.Parse(recipeDurationEditText.Text);
                    var price = float.Parse(recipePriceEditText.Text);
                    
                    var checkedDiff = radioGroupDiff.CheckedRadioButtonId;
                    var checkedTime = radioGroupTime.CheckedRadioButtonId;
                    var checkedType = radioGroupType.CheckedRadioButtonId;
                    var checkedVis = radioGroupVis.CheckedRadioButtonId;
                    
                    var diff = int.Parse(FindViewById<RadioButton>(checkedDiff).Text);
                    var time = FindViewById<RadioButton>(checkedTime).Text;
                    var type = FindViewById<RadioButton>(checkedType).Text;
                    var vis = FindViewById<RadioButton>(checkedVis).Text;

                    var isPrivate = vis == "Private" ? "1" : "0";

                    if (checkBox.Checked) {
                        tags.Add(checkBox.Text);
                    }
                    if (checkBox2.Checked) {
                        tags.Add(checkBox2.Text);
                    }
                    if (checkBox3.Checked) {
                        tags.Add(checkBox3.Text);
                    }
                    if (checkBox4.Checked) {
                        tags.Add(checkBox4.Text);
                    }
                    if (checkBox5.Checked) {
                        tags.Add(checkBox5.Text);
                    }
                    if (checkBox6.Checked) {
                        tags.Add(checkBox6.Text);
                    }
                    
                    var recipe = new Recipe(_loggedId, name, _picture64, diff, tags, time, type, duration, ingredients,
                        instructions, portions, price, _bsnsId);
                    var recipeJson = JsonConvert.SerializeObject(recipe);
                    
                    using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                    var url = "resources/createRecipeB?isPrivate=" + isPrivate;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.UploadString(url, recipeJson);  
                    
                    url = "resources/getUser?id=" + _loggedId;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var userJson = webClient.DownloadString(url);  
                    
                    var intent = new Intent(this, typeof(MyProfileActivity));
                    intent.PutExtra("User", userJson);
                    intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                    StartActivity(intent);
                    OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                }
                _toast = Toast.MakeText(this, toastText, ToastLength.Short);
                _toast.Show();
            };
        }
        
        /// <summary>
        /// needed method that handles the result from the gallery activity
        /// </summary>
        /// <param name="requestCode">system parameter for the requested to launch activity</param>
        /// <param name="resultCode">the result from the launched activity</param>
        /// <param name="data">the obtained data from the activity, in this case, an image</param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok) {
                Stream picStream = ContentResolver.OpenInputStream(data.Data);
                Bitmap bitmap = BitmapFactory.DecodeStream(picStream);

                MemoryStream memStream = new MemoryStream();
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, memStream);
                byte[] picData = memStream.ToArray();
                _picture64 = Convert.ToBase64String(picData);
                _imageSelected = true;
                toastText = "recipe image selected";
                _toast = Toast.MakeText(this, toastText, ToastLength.Short);
                _toast.Show();
            }
        }
    }
}