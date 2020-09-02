using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;

using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Menu.Fragments
{
    public class Discussion_Fragment : Fragment
    {
        View view;
        Notification notify;
        NotificationManager notificationmanager;
        const int notification_id = 0;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view = inflater.Inflate(Resource.Layout.Discussion_layout, container, false);
            Notification.Builder builder = new Notification.Builder(this.Activity);
            
            builder.SetContentTitle("Submitted response");
            
            var disc_text = view.FindViewById<TextView>(Resource.Id.discussion_text);
            var disc_button = view.FindViewById<Button>(Resource.Id.dsubmit);

            disc_button.Click += delegate
            {
                builder.SetContentText(disc_text.Text);
                builder.SetSmallIcon(Resource.Mipmap.ic_launcher);
                notify = builder.Build();
                notificationmanager = this.Activity.GetSystemService(Context.NotificationService) as NotificationManager;
                notificationmanager.Notify(notification_id, notify);
                disc_text.Text = "";

            };
            return view;
        }
    }
}
