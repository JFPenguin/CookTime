using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace CookTime {
    public class FollowAdapter : BaseAdapter<string> {
        private IList<string> _followItems;
        private Context _context;

        public FollowAdapter(Context context, IList<string> items)
        {
            _context = context;
            _followItems = items;
        }
        
        public override long GetItemId(int position) {
            return position;
        }

        public override int Count => _followItems.Count;

        public override string this[int position] => _followItems[position];
        
        public override View GetView(int position, View convertView, ViewGroup parent) {
            View row = convertView;

            if (row == null) {
                row = LayoutInflater.From(_context).Inflate(Resource.Layout.ListViewRow, null, false);
            }

            TextView followTxt = row.FindViewById<TextView>(Resource.Id.rowText);
            
            followTxt.Text = _followItems[position].Split(";")[1];

            return row;
        }
    }
}