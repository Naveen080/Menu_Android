using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Menu.Fragments
{
    public class QuizStart_Fragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Test_Fragment test_fragment = new Test_Fragment();
            FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
            View view = inflater.Inflate(Resource.Layout.QuizStart_layout,container,false);
            var start_button = view.FindViewById<Button>(Resource.Id.button1);
            start_button.Click += delegate
            {
                //Test_Fragment test = new Test_Fragment();
                transaction.Replace(Resource.Id.framelayout, test_fragment).AddToBackStack(null).Commit();
                
            };

            return view;

            
        }
    }
}
