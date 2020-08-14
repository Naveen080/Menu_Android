using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Jack.Resources;
using Jack.Resources.DataBase;
using Jack.Resources.DataClass;
using Java.Security;
using System;
using System.Collections.Generic;

namespace Jack
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ListView lstdata;
        List<Employee> lstsource = new List<Employee>();
        Database1 db = new Database1();
        List<Employee> disp = new List<Employee>();
        bool insertflag, deleteflag,editflag;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.layout1);
            lstdata = FindViewById<ListView>(Resource.Id.listView1);
            
            db.CreateDB();
            var editname = FindViewById<EditText>(Resource.Id.EmployeeName);
            var editid = FindViewById<EditText>(Resource.Id.EmployeeID);
            var editprofile = FindViewById<EditText>(Resource.Id.Profile);

            var btnadd = FindViewById<Button>(Resource.Id.AddButton);
            var btndelete = FindViewById<Button>(Resource.Id.DeleteButton);
            var btnedit = FindViewById<Button>(Resource.Id.EditButton);
            Console.WriteLine("Data in db are");
            disp = db.SelectTable();
            foreach(var s in disp)
            {
                Console.WriteLine(s.id+s.employeeid+s.name);
            }

            LoadData();

            btnadd.Click += delegate
            {
                Employee employee = new Employee() { name = editname.Text, employeeid = editid.Text, Profile = editprofile.Text };
                insertflag=db.InsertData(employee);
                Console.WriteLine("inserted the data"+insertflag);
                editname.Text = "";editid.Text = "";editprofile.Text = "";
                LoadData();
            };

            btndelete.Click += delegate
            {
                Employee employee = new Employee() { name = editname.Text, employeeid = editid.Text, Profile = editprofile.Text };
                deleteflag=db.DeleteCell(employee);
                Console.WriteLine("deleted the data" + deleteflag);
                LoadData();
            };

            btnedit.Click += delegate
            {
                Employee employee = new Employee() { name = editname.Text, employeeid = editid.Text, Profile = editprofile.Text };
                editflag = db.UpdateTable(employee);
                LoadData();
            };

            lstdata.ItemClick += (s, e) =>
            {
                //for (int i = 0; i < lstdata.Count; i++)
                 //{
                     //if (e.Position == i)
                        // lstdata.GetChildAt(e.Position).SetBackgroundColor(Android.Graphics.Color.DarkGray);
                    // else
                         //lstdata.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                 //}

                 var textname = e.View.FindViewById<TextView>(Resource.Id.Textname);
                 var emp = db.SelectCell(textname.Text);
                 editname.Text = emp.name;
                 editid.Text = emp.employeeid;
                 editprofile.Text = emp.Profile;
                 Console.WriteLine(textname.Text);
                Console.WriteLine(emp.id+emp.name+emp.employeeid);


            };



        void LoadData()
        {
            lstsource = db.SelectTable();
            var adapter = new ListViewAdapter(this, lstsource);
            lstdata.Adapter = adapter;
        }
    }

        

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
