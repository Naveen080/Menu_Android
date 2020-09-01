﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Menu.Resources.DataClass
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get; set;
        }
        public string name
        {
            get; set;
        }
        public string employeeid
        {
            get; set;
        }
        public string Profile
        {
            get; set;
        }
    }
}