using System;
using System.Net;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using CookTime.Activities;

namespace CookTime.DialogFragments
{
    /// <summary>
    /// This class represents the dialog fragment that shows when the user wants to rate a recipe
    /// </summary>
    public class DialogRate : DialogFragment {
        private int _recipeId;
        private string _chefId;
        private int type;
        private string _loggedId;
        private RadioGroup _radioGroup;
        private Button _btnSendRate;
        public event EventHandler<SendRateEvent> EventHandlerRate;

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
            var view = inflater.Inflate(Resource.Layout.DialogRate, container, false);

            _radioGroup = view.FindViewById<RadioGroup>(Resource.Id.rateRadioGroup);
            _btnSendRate = view.FindViewById<Button>(Resource.Id.btnSendRate);

            _btnSendRate.Click += SendRate;

            return view;
        }

        /// <summary>
        /// This method is called when the user presses the button to submit the rating.
        /// It invokes the SendRate event, instantiating the class and passing all the data.
        /// Finally it closes the fragment.
        /// </summary>
        /// <param name="sender"> Reference to the object that raised the event</param>
        /// <param name="e"> Contains the event data </param>
        private void SendRate(object sender, EventArgs e)
        {
             var value = "0";
            
             var _checkedItemId = _radioGroup.CheckedRadioButtonId;
            
             if (_checkedItemId == -1) {
                 value = "-1";
             }
             else {
                 var radioButton = View.FindViewById<RadioButton>(_checkedItemId);
                 using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                 string url;

                 if (type == 0) {
                     url = "resources/rateRecipe?id=" + _recipeId + "&email=" + _loggedId + "&rating=" + radioButton.Text;
                     webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                     value = webClient.DownloadString(url);
                 }
                 else if (type == 1) {
                     url = "resources/rateChef?ownEmail=" + _loggedId + "&chefEmail=" + _chefId + "&rating=" + radioButton.Text;
                     webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                     value = webClient.DownloadString(url);
                 }
                 
                 Dismiss();
             }
            
             if (EventHandlerRate != null)
                 EventHandlerRate.Invoke(this, new SendRateEvent(value));
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
        /// Property for the _recipeId attribute
        /// </summary>
        public int RecipeId
        {
            set => _recipeId = value;
        }
        
        /// <summary>
        /// Property for the _loggedId attribute
        /// </summary>
        public string LoggedId
        {
            set => _loggedId = value;
        }

        /// <summary>
        /// Property for the _type attribute
        /// </summary>
        public int Type
        {
            set => type = value;
        }

        /// <summary>
        /// Property for the _chefId attribute
        /// </summary>
        public string ChefId
        {
            set => _chefId = value;
        }
    }
        
        /// <summary>
        /// This class represents an event. It contains the rating request result.
        /// The properties inside this class will let the main view access the result.
        /// </summary>
        public class SendRateEvent : EventArgs {
            /// <summary>
            /// Constructor for the SendRateEvent class
            /// </summary>
            /// <param name="message"> String that will indicate the text in a message for the user  </param>
            public SendRateEvent(string message) {
                Message = message;
            }
            
            /// <summary>
            /// Property for the message attribute
            /// </summary>
            public string Message { get; }
        }
    
}