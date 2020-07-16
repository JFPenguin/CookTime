using System;
using System.Net;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using CookTime.Activities;
using Newtonsoft.Json;

namespace CookTime.DialogFragments {
    /// <summary>
    /// This class represents the dialog fragment that shows when the user wants to sign up
    /// </summary>
    public class DialogSignUp : DialogFragment {
        private EditText _userName;
        private EditText _userLastName;
        private EditText _userAge;
        private EditText _userEmail;
        private EditText _userPassword;
        private Button _btnSendSingUp;
        public event EventHandler<SignUpEvent> EventHandlerSignUp;

        /// <summary>
        /// Creates the fragment, instantiates its user interface view and returns the view.
        /// </summary>
        /// <param name="inflater"> The LayoutInflater object that can be used to inflate any views in the fragment </param>
        /// <param name="container"> This is the parent view that the fragment's UI is attached to. </param>
        /// <param name="savedInstanceState"> Used to reconstruct the fragment from a previous state </param>
        /// <returns> The view of this fragment </returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.DialogSignUp, container, false);

            _userName = view.FindViewById<EditText>(Resource.Id.userName);
            _userLastName = view.FindViewById<EditText>(Resource.Id.userLastName);
            _userAge = view.FindViewById<EditText>(Resource.Id.userAge);
            _userEmail = view.FindViewById<EditText>(Resource.Id.userEmail);
            _userPassword = view.FindViewById<EditText>(Resource.Id.userPassword);
            _btnSendSingUp = view.FindViewById<Button>(Resource.Id.btnSendSignUp);

            _btnSendSingUp.Click += SendSignUp;

            return view;
        }

        /// <summary>
        /// This method is called when the user presses the button to sign up.
        /// It invokes the SignUpEvent event, instantiating the class and passing all the data.
        /// Finally it closes the fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event </param>
        /// <param name="e"> Contains the event data </param>
        private void SendSignUp(object sender, EventArgs e) {
            var userNameInput = _userName.Text;
            var userLastNameInput = _userLastName.Text;
            var userAgeInput = _userAge.Text;
            var userEmailInput = _userEmail.Text;
            var userPasswordInput = _userPassword.Text;
            string value;

            if (userNameInput.Equals("") || userLastNameInput.Equals("") || userAgeInput.Equals("") ||
                userEmailInput.Equals("") || userPasswordInput.Equals(""))
            {
                value = "2";
            }
            
            else {
                using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};

                var url = "resources/isEmailNew?email=" + userEmailInput;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var send = webClient.DownloadString(url);

                var response = JsonConvert.DeserializeObject<string>(send);
                    
                value = response;

                if (value == "0") {
                    Dismiss();
                }
            }            
            
            if (EventHandlerSignUp != null)
                EventHandlerSignUp.Invoke(this, new SignUpEvent(userNameInput, userLastNameInput, 
                    userAgeInput, userEmailInput, userPasswordInput, value));
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
    public class SignUpEvent : EventArgs {
        /// <summary>
        /// Constructor for the SignUpEvent class
        /// </summary>
        /// <param name="userName"> The user's name </param>
        /// <param name="userLastName"> The user's lastname </param>
        /// <param name="userAge"> The user's age </param>
        /// <param name="userEmail"> The user's email </param>
        /// <param name="userPassword"> The user's password </param>
        /// <param name="message"> String that will indicate the text in a message for the user  </param>
        public SignUpEvent(string userName, string userLastName, string userAge, string userEmail, string userPassword, 
            string message) {
            UserName = userName;
            UserLastName = userLastName;
            UserAge = userAge;
            UserEmail = userEmail;
            UserPassword = userPassword;
            Message = message;
        }

        /// <summary>
        /// Property for the userName attribute
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Property for the userLastName attribute
        /// </summary>
        public string UserLastName { get; }

        /// <summary>
        /// Property for the userAge attribute
        /// </summary>
        public string UserAge { get; }

        /// <summary>
        /// Property for the userEmail attribute
        /// </summary>
        public string UserEmail { get; }

        /// <summary>
        /// Property for the userPassword attribute
        /// </summary>
        public string UserPassword { get; }
        
        /// <summary>
        /// Property for the message attribute
        /// </summary>
        public string Message { get; }
    }
} 