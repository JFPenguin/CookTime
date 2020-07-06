using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace CookTime {
    class OnSignUpEvent : EventArgs
    {
        private string userName;
        private string userLastName;
        private string userAge;
        private string userEmail;
        private string userPassword;
    }
    
    class DialogSignUp : DialogFragment {
        private EditText userName;
        private EditText userLastName;
        private EditText userAge;
        private EditText userEmail;
        private EditText userPassword;
        private Button btnSendSingUp;
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.DialogSignUp, container, false);

            userName = view.FindViewById<EditText>(Resource.Id.userName);
            userLastName = view.FindViewById<EditText>(Resource.Id.userLastName);
            userAge = view.FindViewById<EditText>(Resource.Id.userAge);
            userEmail = view.FindViewById<EditText>(Resource.Id.userEmail);
            userPassword = view.FindViewById<EditText>(Resource.Id.userPassword);
            btnSendSingUp = view.FindViewById<Button>(Resource.Id.btnSendSignUp);

            btnSendSingUp.Click += sendSignUp;

            return view;
        }

        void sendSignUp(object sender, EventArgs e)
        {
            
        }
        
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.fragment_anim;
        }
    }
} 