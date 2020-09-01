using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Runtime;
using Android.Widget;
using Java.Security;
using Android.Views;
using Android.Content;
using Menu.Fragments;
//[Activity(Theme = "@style/Theme.DesignDemo")]

namespace Menu
{
    //[Activity(Theme = "@style/Theme.DesignDemo")]
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.DesignDemo", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);
            var profile = FindViewById<ImageView>(Resource.Id.imageView1);
            profile.SetImageResource(Resource.Drawable.person);

            //added newly
            FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
            HomeFragment home = new HomeFragment();
            transaction.Replace(Resource.Id.framelayout, home).AddToBackStack(null).Commit();
            //HomeFragment home = new HomeFragment();
            // transaction.Add(Resource.Id.framelayout, home).Commit();
            //SupportActionBar.SetDisplayHomeAsUpEnabled = true;
        }
        void setupDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);
                /*System.Console.WriteLine(e.MenuItem);
                if(e.MenuItem.ToString() == "Employee")
                {
                    //var intent = new Intent(this, typeof(Employee_Activity));
                    //StartActivity(intent);
                    
                }*/

                //added new
                FragmentTransaction transaction1 = this.FragmentManager.BeginTransaction();
                switch (e.MenuItem.TitleFormatted.ToString())
                {
                    case "Test":
                        Test_Fragment test = new Test_Fragment();
                        transaction1.Replace(Resource.Id.framelayout, test).AddToBackStack(null).Commit();
                        break;

                    case "Employee":
                        Employee_Fragment employee = new Employee_Fragment();
                        transaction1.Replace(Resource.Id.framelayout, employee).AddToBackStack(null).Commit();
                        break;

                    case "Home":
                        HomeFragment home = new HomeFragment();
                        transaction1.Replace(Resource.Id.framelayout, home).AddToBackStack(null).Commit();
                        break;
                }

                drawerLayout.CloseDrawers();
            };
        }

        
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            navigationView.InflateMenu(Resource.Menu.nav_menu); //Navigation Drawer Layout Menu Creation 
            return true;

            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
    }
}