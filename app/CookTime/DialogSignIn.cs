using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace CookTime {
    public class DialogSignIn : DialogFragment {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.DialogSignIn, container, false);
            return view;
        }
        
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.fragment_anim;
        }
    }
}