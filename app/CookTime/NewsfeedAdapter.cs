using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace CookTime
{
    public class NewsfeedAdapter : BaseAdapter<string>
    {
        private IList<string> _recipeItems;
        private Context _context;

        public NewsfeedAdapter(Context context, List<string> items)
        {
            _recipeItems = items;
            _context = context;
        }
        public override int Count => _recipeItems.Count;

        public override string this[int position] => _recipeItems[position];

        public override long GetItemId(int position) {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(_context).Inflate(Resource.Layout.ListViewRow, null, false);
            }

            TextView recipeTxt = row.FindViewById<TextView>(Resource.Id.rowText);
            recipeTxt.Text = _recipeItems[position].Split(';')[1];
            
            return row;
        }
    }
}