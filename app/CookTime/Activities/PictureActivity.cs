using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Square.Picasso;

namespace CookTime.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]

    public class PictureActivity : AppCompatActivity
    {
        private TextView _picType;
        private ImageView _image;
        private string _loggedId;
        private int _bsnsId;
        private string url;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Picture);
            _picType.Text = Intent.GetStringExtra("type");
            url = Intent.GetStringExtra("photo");
            _picType = FindViewById<TextView>(Resource.Id.titleText);
            _image = FindViewById<ImageView>(Resource.Id.picView);
            if (_picType.Equals("user")) {
                Picasso.Get().Load(url).Into(_image);
            }
        }
    }
}