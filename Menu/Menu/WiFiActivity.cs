using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Menu
{
    [Activity(Label = "WiFiActivity")]

    public class WiFiActivity : AppCompatActivity
    {
        WifiManager wifimanager;
        //WifiReceiver 
        ListView lstview;
        Button scanbtn;
        int REQUEST_READ_PHONE_STATE = 100;
        List<string> wifilist;
        ArrayAdapter<string> lstadapter;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.wifilayout);

            scanbtn = FindViewById<Button>(Resource.Id.button1);
            lstview = FindViewById<ListView>(Resource.Id.listView1);

            wifimanager = (WifiManager)GetSystemService(Context.WifiService);

            var permissionCheck = ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessWifiState);

            if (permissionCheck != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ChangeWifiState }, REQUEST_READ_PHONE_STATE);
            }
            else
            {
                //TODO
            }
            if (wifimanager.IsWifiEnabled == false)
            {
                Toast.MakeText(this, "WiFi is disabled.Enabling it", ToastLength.Long).Show();
                wifimanager.SetWifiEnabled(true);
            }
            scanbtn.Click += delegate
            {
                wifiscan();
            };
        }

        public void wifiscan()
        {
            wifilist.Clear();
            
            wifilist.Add(wifimanager.ScanResults.ToString());
            lstadapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, wifilist);
            lstview.Adapter = lstadapter;

        }

        internal class wifireciever : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
                IList<ScanResult> scanwifinetworks = WiFiActivity.ScanResults;
            }
        }
        


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            switch (requestCode)
            {
                case 100:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            // Permission granted, yay! Start the Bluetooth device scan.
                            Toast.MakeText(this, "You can scan for wifi now", ToastLength.Short).Show();
                        }
                        else
                        {
                            Toast.MakeText(this, "You cannot scan until you allow the location access", ToastLength.Short).Show();
                            // Alert the user that this application requires the location permission to perform the scan.
                        }
                    }
                    break;
            }
        }
    }

    /*public class SampleReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var results = 

            String value = intent.GetStringExtra("key");
        }
    }*/
}
