using System.Net;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace CookTime.DialogFragments
{
    /// <summary>
    /// This class represents the dialog fragment that shows when the user wants to comment a recipe
    /// </summary>
    public class DialogPShow : DialogFragment
    {
        private TextView _type;
        private ImageView _image;
        private string _url;
        private string _typeText;

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
            var view = inflater.Inflate(Resource.Layout.Picture, container, false);
            _type = view.FindViewById<TextView>(Resource.Id.titleText);
            _image = view.FindViewById<ImageView>(Resource.Id.picView);
            _type.Text = _typeText;
            Bitmap bitmap = GetImageBitmapFromUrl(_url);
            _image.SetImageBitmap(bitmap);
            return view;
        }
        
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient()){
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0) {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return imageBitmap;
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
        /// setter property for the _typeText attribute
        /// </summary>
        public string TypeText {
            set => _typeText = value;
        }

        /// <summary>
        /// setter property for the _url attribute
        /// </summary>
        public string Url
        {
            set => _url = value;
        }
    }
}