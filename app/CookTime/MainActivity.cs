using System.Threading;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace CookTime {
    /// <summary>
    /// This class represents the first view seen when the app is opened.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "CookTime", MainLauncher = true)]
    public class MainActivity : AppCompatActivity {
        private Button signUpButton;
        private Button signInButton;
        
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

            signUpButton = FindViewById<Button>(Resource.Id.btnSignUp);
            signUpButton.Click += (sender, args) =>
                {
                    //Brings dialog fragment forward
                    var transaction = SupportFragmentManager.BeginTransaction();
                    var dialogSignUp = new DialogSignUp();
                    dialogSignUp.Show(transaction, "sign up");
                    
                    dialogSignUp.eventHandlerSignUp += signUpResult;
                };
            
            signInButton = FindViewById<Button>(Resource.Id.btnSignIn);
            signInButton.Click += (sender, args) =>
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
        private void signUpResult(object sender, SignUpEvent e) {
            var newUserName = e.UserName;
            var newUserLastName = e.UserLastName;
            var newUserAge = e.UserAge;
            var newUserEmail = e.UserEmail;
            var newUserPassword = e.UserPassword;

            var thread = new Thread(ActsLikeRequest);
            thread.Start();
        }
        
        private void ActsLikeRequest() {
            Thread.Sleep(3000);
        }
    }
}
