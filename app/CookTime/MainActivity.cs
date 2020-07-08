﻿using System.Net;
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
        private Toast toast;
        
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
            string toastText;
            if (e.Empty) {
                toastText = "Please fill in all of the information";
            }
            else {
                toastText = "You have succesfully signed up to the platform";
                var newUserName = e.UserName;
                var newUserLastName = e.UserLastName;
                var newUserAge = e.UserAge;
                var newUserEmail = e.UserEmail;
                var newUserPassword = e.UserPassword; 
                
                // try {  
                //     using (var webClient = new WebClient()) {  
                //         webClient.BaseAddress = "http://181.194.46.33:8080/CookTime_war/";
                //         var url = "resources/createUser";
                //         webClient.Headers[HttpRequestHeader.ContentType] = "application/json";  
                //         string data = JsonConvert.SerializeObject(city);  
                //         var response = webClient.UploadString(url, data);
                //         result = JsonConvert.DeserializeObject<ReturnMessageInfo>(response);
                //     }  
                // } catch (Exception ex) {  
                //     throw ex;  
                // }  
                // var thread = new Thread(request);
                // thread.Start();
            }
            toast = Toast.MakeText(this, toastText, ToastLength.Short);
            toast.Show();
        }
        
        // private void request() {
        //     
        // }
    }
}
