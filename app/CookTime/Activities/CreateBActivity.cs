using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace CookTime.Activities {
    /// <summary>
    /// This class represents the Business creation form view.
    /// It inherits from the base class for Android activities
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class CreateBActivity : AppCompatActivity {
        private string _loggedId;
        private string _userLocation = "none";
        private string _picture64;
        private EditText bsnsName;
        private EditText bsnsContact;
        private EditText bsnsTo;
        private EditText bsnsFrom;
        private TextView _location;
        private CheckBox checkbox1;
        private CheckBox checkbox2;
        private CheckBox checkbox3;
        private CheckBox checkbox4;
        private CheckBox checkbox5;
        private CheckBox checkbox6;
        private CheckBox checkbox7;
        private Button addLogo;
        private Button sendBsns;
        private bool daysChecked;
        private bool _locationObtained;
        private bool setLogo;
        private string toastText;
        private Toast _toast;
        
        /// <summary>
        /// This method is implemented to prompt the user with location permissions request.
        /// </summary>
        /// <param name="requestCode">the return code from the request</param>
        /// <param name="permissions">the permissions requested to the user</param>
        /// <param name="grantResults">communication to system with the permission requests</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
        /// <summary>
        /// This method is called when the activity is starting.
        /// The list of followers/following is displayed here.
        /// </summary>
        /// <param name="savedInstanceState"> a Bundle that contains the data the activity most recently
        /// supplied if the activity is being re-initialized after previously being shut down. </param>
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState); // essentials uses runtime permissions, so it needs to be initialized.
            SetContentView(Resource.Layout.CreateBusiness);
            GetLocation();
            _loggedId = Intent.GetStringExtra("LoggedId");
            bsnsName = FindViewById<EditText>(Resource.Id.editText);
            bsnsContact = FindViewById<EditText>(Resource.Id.editText2);
            bsnsFrom = FindViewById<EditText>(Resource.Id.editText3);
            bsnsTo = FindViewById<EditText>(Resource.Id.editText4);
            _location = FindViewById<TextView>(Resource.Id.locationText);
            checkbox1 = FindViewById<CheckBox>(Resource.Id.checkBox);
            checkbox2 = FindViewById<CheckBox>(Resource.Id.checkBox2);
            checkbox3 = FindViewById<CheckBox>(Resource.Id.checkBox3);
            checkbox4 = FindViewById<CheckBox>(Resource.Id.checkBox4);
            checkbox5 = FindViewById<CheckBox>(Resource.Id.checkBox5);
            checkbox6 = FindViewById<CheckBox>(Resource.Id.checkBox6);
            checkbox7 = FindViewById<CheckBox>(Resource.Id.checkBox7);
            addLogo = FindViewById<Button>(Resource.Id.btnImage);
            sendBsns = FindViewById<Button>(Resource.Id.btnPost);
            
             sendBsns.Click += (sender, args) =>
             {
                 if (checkbox1.Checked || checkbox2.Checked || checkbox3.Checked || checkbox4.Checked || checkbox5.Checked
                     || checkbox6.Checked || checkbox7.Checked) 
                 {
                     daysChecked = true;
                 }
                 else {
                     daysChecked = false;
                 }
            
                 if (bsnsName.Text.Equals("") || bsnsContact.Text.Equals("") || bsnsFrom.Text.Equals("") || 
                     bsnsTo.Text.Equals("") || !daysChecked || !isHours(bsnsFrom.Text, bsnsTo.Text) || !_locationObtained || !setLogo)  
                 {
                     toastText = "Please fill in correctly all of the required information";
                     if (!setLogo)
                     {
                         toastText = "You must select a logo to continue";
                     }
                 }
                 else 
                 {
                     toastText = "Business created";
                     var name = bsnsName.Text;
                     var contact = bsnsContact.Text;
                     var from = bsnsFrom.Text;
                     var to = bsnsTo.Text;
                     var hours = from + " to " + to;
                     var days = "";
                     var firstAdded = false;
                     
                     if (checkbox1.Checked) {
                         firstAdded = true;
                         days += checkbox1.Text;
                     }
                     if (checkbox2.Checked) {
                         if (!firstAdded) {
                             firstAdded = true;
                             days += checkbox2.Text;
                         }
                         else {
                             days += "-" + checkbox2.Text;
                         }
                     }
                     if (checkbox3.Checked) {
                         if (!firstAdded) {
                             firstAdded = true;
                             days += checkbox3.Text;
                         }
                         else {
                             days += "-" + checkbox3.Text;
                         }
                     }
                     if (checkbox4.Checked) {
                         if (!firstAdded) {
                             firstAdded = true;
                             days += checkbox4.Text;
                         }
                         else {
                             days += "-" + checkbox4.Text;
                         }
                     }
                     if (checkbox5.Checked) {
                         if (!firstAdded) {
                             firstAdded = true;
                             days += checkbox5.Text;
                         }
                         else {
                             days += "-" + checkbox5.Text;
                         }
                     }
                     if (checkbox6.Checked) {
                         if (!firstAdded) {
                             firstAdded = true;
                             days += checkbox6.Text;
                         }
                         else {
                             days += "-" + checkbox6.Text;
                         }
                     }
                     if (checkbox7.Checked) {
                         if (!firstAdded) {
                             days += checkbox7.Text;
                         }
                         else {
                             days += "-" + checkbox7.Text;
                         }
                     }
                     var employeeList = new List<string>();
                     employeeList.Add(_loggedId);
                     
                     var bsnsHoursStr = days + " " + hours;
                     var bsns = new Business(name, contact, _picture64, bsnsHoursStr, _userLocation, employeeList);
                     var bsnsJson = JsonConvert.SerializeObject(bsns);

                     using var webClient = new WebClient {BaseAddress = "http://" + MainActivity.Ipv4 + ":8080/CookTime_war/cookAPI/"};
                     var url = "resources/createBusiness";
                     webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                     webClient.UploadString(url, bsnsJson);  
                     
                     url = "resources/getUser?id=" + _loggedId;
                     webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                     var userJson = webClient.DownloadString(url);  
                     
                     var intent = new Intent(this, typeof(MyProfileActivity));
                     intent.PutExtra("User", userJson);
                     intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                     StartActivity(intent);
                     OverridePendingTransition(Android.Resource.Animation.SlideInLeft,Android.Resource.Animation.SlideOutRight);
                 }
                 _toast = Toast.MakeText(this, toastText, ToastLength.Short);
                 _toast.Show();
            };
             
             addLogo.Click += (sender, args) =>
             {
                 Intent gallery = new Intent();
                 gallery.SetType("image/*");
                 gallery.SetAction(Intent.ActionGetContent);
                 this.StartActivityForResult(Intent.CreateChooser(gallery, "select a photo"), 0);
             };
        }

        /// <summary>
        /// this method validates the format of the business hours that any user inputs when creating a business
        /// </summary>
        /// <param name="from">the opening hours from the user input</param>
        /// <param name="to">the closing hours from the user input</param>
        /// <returns></returns>
        private bool isHours(string from, string to) {
            var isHours = false;
            try {
                var openTime = from.Split(":");
                var closeTime = to.Split(":");
                if (openTime.Length == 2 && openTime[0].Length == 2 && openTime[1].Length == 2
                    && closeTime.Length == 2 && closeTime[0].Length == 2 && closeTime[1].Length ==2) 
                {
                    var hh1 = int.Parse(openTime[0]);
                    var mm1 = int.Parse(openTime[1]);
                    var hh2 = int.Parse(closeTime[0]);
                    var mm2 = int.Parse(closeTime[1]);
                    if (hh1 < hh2) {
                        if (hh1 < 24 && mm1 < 60 && hh2 < 24 && mm2 < 60) {
                            isHours = true;
                        }
                    }
                    else if (hh1 == hh2) {
                        if (hh1 < 24 && mm1 < 60 && mm2 > mm1 && mm2 < 60) {
                            isHours = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return isHours;
        }
        /// <summary>
        /// this method is executed in another thread while the user creates a business.
        /// </summary>
        /// <returns>the location value from the user</returns>
        private async Task GetLocation() {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                location = await Geolocation.GetLocationAsync(request);
                if (location != null)
                {
                    _userLocation = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}";
                    _locationObtained = true;
                    _location.Text = _userLocation;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                toastText = "you must enable location features to access the location values.";
                _toast = Toast.MakeText(this, toastText, ToastLength.Long);
                Finish();
            }
            catch (FeatureNotEnabledException fneEx)
            {
                toastText = "you must enable location features to access the location values.";
                _toast = Toast.MakeText(this, toastText, ToastLength.Long);
                Finish();
            }
            catch (PermissionException pEx)
            {
                toastText = "You must allow the service to access location in order to create a business.";
                _toast = Toast.MakeText(this, toastText, ToastLength.Long);
                _toast.Show();
                Finish();
            }
            catch (Exception ex)
            {
                toastText = "Could not get location. Try creating a business later.";
                _toast = Toast.MakeText(this, toastText, ToastLength.Long);
                _toast.Show();
                Finish();
            }
        }
        /// <summary>
        /// needed method that handles the result from the gallery activity
        /// </summary>
        /// <param name="requestCode">system parameter for the requested to launch activity</param>
        /// <param name="resultCode">the result from the launched activity</param>
        /// <param name="data">the obtained data from the activity, in this case, an image</param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok) {
                Stream picStream = ContentResolver.OpenInputStream(data.Data);
                Bitmap bitmap = BitmapFactory.DecodeStream(picStream);

                MemoryStream memStream = new MemoryStream();
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, memStream);
                byte[] picData = memStream.ToArray();
                _picture64 = Convert.ToBase64String(picData);
                setLogo = true;

                toastText = "logo selected";
                _toast = Toast.MakeText(this, toastText, ToastLength.Short);
                _toast.Show();
            }
        }
    }
}