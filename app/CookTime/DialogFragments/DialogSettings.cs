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
    /// This class represents the dialog fragment that shows when the user wants to edit its info
    /// </summary>
    public class DialogSettings : DialogFragment {
        private string _email;
        private EditText _currentPass;
        private EditText _newPass;
        private EditText _newPass2;
        private Button _btnPass;
        public event EventHandler<SendPassEvent> EventHandlerPass;

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

            var view = inflater.Inflate(Resource.Layout.DialogSettings, container, false);
            
            _currentPass = view.FindViewById<EditText>(Resource.Id.currentPass);
            _newPass = view.FindViewById<EditText>(Resource.Id.newPass);
            _newPass2 = view.FindViewById<EditText>(Resource.Id.newPass2);
            _btnPass = view.FindViewById<Button>(Resource.Id.btnPassword);
            
            _btnPass.Click += SendPassword;
            
            return view;
        }
        
        /// <summary>
        /// This method is called when the user presses the button to submit the changes.
        /// It invokes the SendPassword event, instantiating the class and passing all the data.
        /// Finally it closes the fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event</param>
        /// <param name="e"> Contains the event data </param>
        private void SendPassword(object sender, EventArgs e) {
            var currentPassInput = _currentPass.Text;
            var newPassInput = _newPass.Text;
            var newPassInput2 = _newPass2.Text;
            string value;

            if (currentPassInput.Equals("") || newPassInput.Equals("") || newPassInput2.Equals("")) {
                value = "3";
            }
            
            else if (!newPassInput.Equals(newPassInput2)) {
                value = "2";
            }
            else {
                using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                var url = "resources/editUserPassword?email=" + _email + "&newPassword=" + newPassInput + "&password=" + currentPassInput;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var send = webClient.DownloadString(url);
                
                var response = JsonConvert.DeserializeObject<string>(send);
                    
                value = response;

                if (value == "1") {
                    Dismiss();
                }
            }            
            
            if (EventHandlerPass != null)
                EventHandlerPass.Invoke(this, new SendPassEvent(value));
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

        /// <summary>
        /// Property for the _email attribute
        /// </summary>
        public string Email
        {
            set => _email = value;
        }
    }
    
    /// <summary>
    /// This class represents an event. It contains all of the data that the user entered.
    /// The properties inside this class will let the main view access the user data.
    /// </summary>
    public class SendPassEvent : EventArgs {
        /// <summary>
        /// Constructor for the SignInEvent class
        /// </summary>
        /// <param name="message"> String that will indicate the text in a message for the user  </param>
        public SendPassEvent(string message) {
            Message = message;
        }
        
        /// <summary>
        /// Property for the message attribute
        /// </summary>
        public string Message { get; }
    }
}