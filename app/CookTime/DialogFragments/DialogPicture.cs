using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace CookTime.DialogFragments
{
    /// <summary>
    /// This class represents the dialog fragment that shows when the user wants to comment a recipe
    /// </summary>
    public class DialogPicture : DialogFragment {
        private Button _btnView;
        private Button _btnChange;
        private string _photo;
        public event EventHandler<PicEvent> EventHandlerChoice;

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
            var view = inflater.Inflate(Resource.Layout.DialogPicture, container, false);

            _btnView = view.FindViewById<Button>(Resource.Id.btnView);
            _btnChange = view.FindViewById<Button>(Resource.Id.btnChange);

            _btnView.Click += (sender, args) =>
            {
                if (EventHandlerChoice != null)
                    EventHandlerChoice.Invoke(this, new PicEvent(0, _photo));
                Dismiss();
            };

            _btnChange.Click += (sender, args) =>
            {
                if (EventHandlerChoice != null)
                    EventHandlerChoice.Invoke(this, new PicEvent(1, _photo));
                Dismiss();
            };

            return view;
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
        public string Photo {
            set => _photo = value;
        }
    }


    /// <summary>
    /// This class represents an event. It contains the choice request result.
    /// The properties inside this class will let the main view access the result.
    /// </summary>
    public class PicEvent : EventArgs
    {
        /// <summary>
        /// Constructor for the ChoiceEvent class
        /// </summary>
        /// <param name="message"> String that will indicate the action taken by the user  </param>
        /// <param name="photo"> The string to use as a getImage through the API</param>
        public PicEvent(int message, string photo)
        {
            Message = message;
            Photo = photo;
        }

        /// <summary>
        /// Property for the message attribute
        /// </summary>
        public int Message { get; }
        
        /// <summary>
        /// Property for the recipeId attribute
        /// </summary>
        public string Photo { get; }
    }
}
