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
using Jack.Resources.DataClass;


namespace Jack.Resources
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView EmpName { get; set; }
        public TextView EmpID { get; set; }
        


    }
     public class ListViewAdapter: BaseAdapter
    {
        private Activity activity;
        private List<Employee> emp;
        public ListViewAdapter(Activity _activity,List<Employee> _emp)
        {
            activity = _activity;emp = _emp;
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
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.ListViewLayout, parent, false);
            var EmpName = view.FindViewById<TextView>(Resource.Id.Textname);
            var EmpID = view.FindViewById<TextView>(Resource.Id.Textid);
            //FindViewById<TextView>(Resource.Id.textView1);
            EmpName.Text = emp[position].name;
            EmpID.Text = emp[position].employeeid;
            return view;
        }
    }
}