using System.Net;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;

namespace CookTime {
    /// <summary>
    /// This class represents the first view seen when the app is opened.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "CookTime", MainLauncher = true)]
    public class MainActivity : AppCompatActivity {
        private Button _signUpButton;
        private Button _signInButton;
        private Toast _toast;
        
        /// <summary>
        /// This method is called when the activity is starting.
        /// It contains the logic for the buttons shown in the first view.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState) {

            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _signUpButton = FindViewById<Button>(Resource.Id.btnSignUp);
            _signUpButton.Click += (sender, args) =>
                {
                    //Brings dialog fragment forward
                    var transaction = SupportFragmentManager.BeginTransaction();
                    var dialogSignUp = new DialogSignUp();
                    dialogSignUp.Show(transaction, "sign up");
                    
                    dialogSignUp.EventHandlerSignUp += SignUpResult;
                };
            
            _signInButton = FindViewById<Button>(Resource.Id.btnSignIn);
            _signInButton.Click += (sender, args) =>
                {
                    //Brings dialog fragment forward
                    var transaction = SupportFragmentManager.BeginTransaction();
                    var dialogSignIn = new DialogSignIn();
                    dialogSignIn.Show(transaction, "sign in");
                };
        }
        
        /// <summary>
        /// This method is in charge of retrieving the data entered by the user in the Sign Up dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignUpResult(object sender, SignUpEvent e) {
            string toastText;
            if (e.Message == "2") {
                toastText = "Please fill in all of the information";
            }
            else if (e.Message == "1")
            {
                toastText = "The email entered is already taken";
            }
            else {
                var toast1 = Toast.MakeText(this, "else", ToastLength.Short);
                toast1.Show();
                
                toastText = "You have succesfully signed up to the platform";
                var newUserName = e.UserName;
                var newUserLastName = e.UserLastName;
                var newUserAge = e.UserAge;
                var newUserEmail = e.UserEmail;
                var newUserPassword = e.UserPassword;
                
                var user = new User(int.Parse(newUserAge), newUserEmail, newUserName, newUserLastName, 
                    newUserPassword);

                var jsonResult = JsonConvert.SerializeObject(user);

                using var webClient = new WebClient {BaseAddress = "http://192.168.1.9:8080/CookTime_war/cookAPI/"};

                const string url = "resources/createUser";
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.UploadString(url, jsonResult);
            }
            _toast = Toast.MakeText(this, toastText, ToastLength.Short);
            _toast.Show();
        }
    }
}
