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
    public class QuizStop_Fragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.QuizStop_layout,container,false);
            var exit_button = view.FindViewById<Button>(Resource.Id.button1);
            var score_text = view.FindViewById<TextView>(Resource.Id.score);
            FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
            HomePage_fragment home = new HomePage_fragment();
            exit_button.Click += delegate
            {
                transaction.Replace(Resource.Id.framelayout, home).AddToBackStack(null).Commit();
            };
            return view;
        }
    }
}
