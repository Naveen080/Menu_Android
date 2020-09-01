using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Java.Security;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Menu.Resources.DataBase;
using Menu.Resources.DataClass;
using Android.Support.V7.RecyclerView.Extensions;
using Menu.Resources;

namespace Menu
{
	public class Employee_Fragment : Fragment
	{
		ListView lstdata; //= new ListView(null);
		List<Employee> lstsource = new List<Employee>();
		Database db = new Database();
		List<Employee> disp = new List<Employee>();
		bool insertflag, deleteflag, editflag;
		View view;
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			
			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			view = inflater.Inflate(Resource.Layout.Employee_layout, container, false);
			lstdata = view.FindViewById<ListView>(Resource.Id.listView1);
			db.CreateDB();
			var editname = view.FindViewById<EditText>(Resource.Id.EmployeeName);
			var editid = view.FindViewById<EditText>(Resource.Id.EmployeeID);
			var editprofile = view.FindViewById<EditText>(Resource.Id.Profile);

			var btnadd = view.FindViewById<Button>(Resource.Id.AddButton);
			var btndelete = view.FindViewById<Button>(Resource.Id.DeleteButton);
			var btnedit = view.FindViewById<Button>(Resource.Id.EditButton);
			Console.WriteLine("Data in db are");
			disp = db.SelectTable();
			foreach (var s in disp)
			{
				Console.WriteLine(s.id + s.employeeid + s.name);
			}
            Console.WriteLine(this.ToString());
			LoadData();

			btnadd.Click += delegate
			{
				Employee employee = new Employee() { name = editname.Text, employeeid = editid.Text, Profile = editprofile.Text };
				insertflag = db.InsertData(employee);
				Console.WriteLine("inserted the data" + insertflag);
				editname.Text = ""; editid.Text = ""; editprofile.Text = "";
				LoadData();
			};

			btndelete.Click += delegate
			{
				Employee employee = new Employee() { name = editname.Text, employeeid = editid.Text, Profile = editprofile.Text };
				deleteflag = db.DeleteCell(employee);
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
				var textname = e.View.FindViewById<TextView>(Resource.Id.Textname);
				var emp = db.SelectCell(textname.Text);
				editname.Text = emp.name;
				editid.Text = emp.employeeid;
				editprofile.Text = emp.Profile;
				
			};

			return view;

		}
		public void LoadData()
		{
			lstsource = db.SelectTable();

			ListViewAdapter adapter = new ListViewAdapter(this.Activity, lstsource);
			
			lstdata.Adapter = adapter;
		}
	}
}
