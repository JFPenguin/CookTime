﻿using System;
using System.Net;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using CookTime.Activities;
using Newtonsoft.Json;

namespace CookTime.DialogFragments {
    /// <summary>
    /// This class represents the dialog fragment that shows when the user wants to sign in
    /// </summary>
    public class DialogSignIn : DialogFragment {
        private EditText _userEmail;
        private EditText _userPassword;
        private Button _btnSendSignIn;
        public event EventHandler<SignInEvent> EventHandlerSignIn;
        
        /// <summary>
        /// Creates the fragment, instantiates its user interface view and returns the view
        /// </summary>
        /// <param name="inflater"> The LayoutInflater object that can be used to inflate any views in the fragment </param>
        /// <param name="container">  This is the parent view that the fragment's UI is attached to. </param>
        /// <param name="savedInstanceState"> Used to reconstruct the fragment from a previous state  </param>
        /// <returns> The view of this fragment </returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.DialogSignIn, container, false);
            
            _userEmail = view.FindViewById<EditText>(Resource.Id.signInEmail);
            _userPassword = view.FindViewById<EditText>(Resource.Id.signInPassword);
            _btnSendSignIn = view.FindViewById<Button>(Resource.Id.btnSendSignIn);
            
            _btnSendSignIn.Click += SendSignIn;
            
            return view;
        }
        
        /// <summary>
        /// This method is called when the user presses the button to sign up.
        /// It invokes the SignUpEvent event, instantiating the class and passing all the data.
        /// Finally it closes the fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event</param>
        /// <param name="e"> Contains the event data </param>
        private void SendSignIn(object sender, EventArgs e) {
            var userEmailInput = _userEmail.Text;
            var userPasswordInput = _userPassword.Text;
            string value;

            if (userEmailInput.Equals("") || userPasswordInput.Equals("")) {
                value = "3";
            }
            
            else {
                using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                var url = "resources/auth?email=" + userEmailInput + "&password=" + userPasswordInput;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var send = webClient.DownloadString(url);
                
                var response = JsonConvert.DeserializeObject<string>(send);
                    
                value = response;
                
                if (value == "1") {
                    Dismiss();
                }
            }            
            
            if (EventHandlerSignIn != null)
                EventHandlerSignIn.Invoke(this, new SignInEvent(userEmailInput, value));
        }
        
        /// <summary>
        /// This method is run when the fragment finished its creation. The animations are set in here.
        /// </summary>
        /// <param name="savedInstanceState"> Used to reconstruct the fragment from a previous state </param>
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.fragment_anim;
        }
    }
    
    /// <summary>
    /// This class represents an event. It contains all of the data that the user entered.
    /// The properties inside this class will let the main view access the user data.
    /// </summary>
    public class SignInEvent : EventArgs {
        /// <summary>
        /// Constructor for the SignInEvent class
        /// </summary>
        /// <param name="userEmail"> The email input from the user  </param>
        /// <param name="message"> String that will indicate the text in a message for the user  </param>
        public SignInEvent(string userEmail, string message) {
            UserEmail = userEmail;
            Message = message;
        }
        
        /// <summary>
        /// Property for the message attribute
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Property for the userEmail attribute
        /// </summary>
        public string UserEmail { get;}
    }
    
}