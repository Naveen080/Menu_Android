using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Menu.Resources.DataClass;

namespace Menu.Resources
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView EmpName { get; set; }
        public TextView EmpID { get; set; }
    }
    class ListViewAdapter : BaseAdapter
    {
        private Fragment fragment;
        //public View view1 = null;
        private List<Employee> emp;
        private Activity activity;
        public ListViewAdapter(Activity _activity, List<Employee> _emp)
        {
            //fragment = _fragment; 
            activity = _activity;
           // view1 = _view;
            emp = _emp;
        }
        public override int Count // => throw new NotImplementedException();
        {
            get
            {
                return emp.Count;
            }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
        public override long GetItemId(int position)
        {
            return emp[position].id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //throw new NotImplementedException();
            var rootView = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.ListViewLayout, parent, false);
            //NOTE: even in fragment also we need to use the activity associated with the fragment in above line
            
            var EmpName = rootView.FindViewById<TextView>(Resource.Id.Textname);
           // var EmpID = view.FindViewById<TextView>(Resource.Id.TextID);
            //FindViewById<TextView>(Resource.Id.textView1);
            EmpName.Text = emp[position].name;
            //EmpID.Text = emp[position].employeeid;
            return rootView;
        }
    }
}