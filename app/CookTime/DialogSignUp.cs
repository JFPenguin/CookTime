using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace CookTime{
    class DialogSignUp : DialogFragment {
        private EditText userName;
        private EditText userLastName;
        private EditText userAge;
        private EditText userEmail;
        private EditText userPassword;
        private Button btnSendSingUp;
        public event EventHandler<SignUpEvent> eventSignUp;
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
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

        void sendSignUp(object sender, EventArgs e) {
            if (eventSignUp != null)
                eventSignUp.Invoke(this, new SignUpEvent(userName.Text, userLastName.Text, userAge.Text, userEmail.Text, userPassword.Text));
            Dismiss();
        }
        
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.fragment_anim;
        }
    }
    
    class SignUpEvent : EventArgs {
        private string userName;
        private string userLastName;
        private string userAge;
        private string userEmail;
        private string userPassword;
        
        public string UserName {
            get => userName;
            set => userName = value;
        }
    
        public string UserLastName {
            get => userLastName;
            set => userLastName = value;
        }
    
        public string UserAge {
            get => userAge;
            set => userAge = value;
        }
    
        public string UserEmail {
            get => userEmail;
            set => userEmail = value;
        }
    
        public string UserPassword {
            get => userPassword;
            set => userPassword = value;
        }
    
        public SignUpEvent(string userName, string userLastName, string userAge, string userEmail, string userPassword) {
            this.userName = userName;
            this.userLastName = userLastName;
            this.userAge = userAge;
            this.userEmail = userEmail;
            this.userPassword = userPassword;
        }
    }
} 