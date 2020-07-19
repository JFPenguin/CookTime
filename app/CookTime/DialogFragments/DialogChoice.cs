using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace CookTime.DialogFragments
{
    /// <summary>
    /// This class represents the dialog fragment that shows when the user wants to comment a recipe
    /// </summary>
    public class DialogChoice : DialogFragment {
        private Button _btnView;
        private Button _btnDelete;
        private string _recipeId;
        public event EventHandler<ChoiceEvent> EventHandlerChoice;

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
            var view = inflater.Inflate(Resource.Layout.DialogChoice, container, false);

            _btnView = view.FindViewById<Button>(Resource.Id.btnView);
            _btnDelete = view.FindViewById<Button>(Resource.Id.btnDelete);

            _btnView.Click += (sender, args) =>
            {
                if (EventHandlerChoice != null)
                    EventHandlerChoice.Invoke(this, new ChoiceEvent(0, _recipeId));
                Dismiss();
            };

            _btnDelete.Click += (sender, args) =>
            {
                if (EventHandlerChoice != null)
                    EventHandlerChoice.Invoke(this, new ChoiceEvent(1, _recipeId));
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
        public string RecipeId {
            set => _recipeId = value;
        }
    }


    /// <summary>
    /// This class represents an event. It contains the choice request result.
    /// The properties inside this class will let the main view access the result.
    /// </summary>
    public class ChoiceEvent : EventArgs
    {
        /// <summary>
        /// Constructor for the SendCommEvent class
        /// </summary>
        /// <param name="message"> String that will indicate the action taken by the user  </param>
        /// <param name="recipeId"> The id for the recipe targeted in this event  </param>
        public ChoiceEvent(int message, string recipeId)
        {
            Message = message;
            RecipeId = recipeId;
        }

        /// <summary>
        /// Property for the message attribute
        /// </summary>
        public int Message { get; }
        
        /// <summary>
        /// Property for the recipeId attribute
        /// </summary>
        public string RecipeId { get; }
    }
}
