using System.Threading;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace CookTime {
    [Activity(Label = "CookTime", MainLauncher = true)]
    public class MainActivity : AppCompatActivity {
        private Button signUpButton;
        private Button signInButton;
        
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
                    
                    dialogSignUp.eventSignUp += signUpResult;
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
