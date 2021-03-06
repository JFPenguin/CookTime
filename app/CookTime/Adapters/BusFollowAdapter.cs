﻿using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace CookTime.Adapters {
    /// <summary>
    /// This class is a custom adapter for the ListView containing a user's followers/following.
    /// This adapter manages all of the iteration through its items in the backend.
    /// All of the methods are called and used by the adapter in the backend.
    /// It inherits from BaseAdapter.
    /// </summary>
    public class BusFollowAdapter : BaseAdapter<string> {
        private IList<string> _followItems;
        private Context _context;

        /// <summary>
        /// Constructor for the adapter
        /// </summary>
        /// <param name="context"> The context in which the adapter will inflate </param>
        /// <param name="items"> The list of items to be displayed in the ListView </param>
        public BusFollowAdapter(Context context, IList<string> items) {
            _context = context;
            _followItems = items;
        }
        
        /// <summary>
        /// This method returns the current position iterated by the adapter
        /// </summary>
        /// <param name="position"> The current position </param>
        /// <returns> The current position</returns>
        public override long GetItemId(int position) {
            return position;
        }

        /// <summary>
        /// Gets the length of the list of items to be displayed
        /// </summary>
        public override int Count => _followItems.Count;

        /// <summary>
        /// This property returns the item in the list positioned at the entered index
        /// </summary>
        /// <param name="position"> The desired index </param>
        public override string this[int position] => _followItems[position];
        
        /// <summary>
        /// This method is in charge of inflating the rows of the ListView 
        /// </summary>
        /// <param name="position"> The current position </param>
        /// <param name="convertView"> The old view to reuse, if possible. </param>
        /// <param name="parent"> The parent that this view will eventually be attached to </param>
        /// <returns></returns>
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