using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Menu.Resources.DataClass;
using SQLitePCL;

namespace Menu.Resources.DataBase
{
    class Database
    {
        static string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        static string dbPath = System.IO.Path.Combine(folder, "mydbn.db3");
        Employee emp; bool delflag;
        
        public bool CreateDB()
        {
            using (var connection = new SQLite.SQLiteConnection(dbPath))
            {
                connection.CreateTable<Employee>();
                Console.WriteLine("database path" + dbPath);
                return true;
                //Console.WriteLine("Database is created");
            }
        }

        public bool InsertData(Employee employee)
        {
            using (var connection = new SQLite.SQLiteConnection(dbPath))
            {
                connection.Insert(employee);
                return true;
            }
        }
        public List<Employee> SelectTable()
        {
            using (var connection = new SQLite.SQLiteConnection(dbPath))
            {
                return connection.Table<Employee>().ToList();
            }
        }

        public bool UpdateTable(Employee employee)
        {
            var emp = employee;
            using (var connection = new SQLite.SQLiteConnection(dbPath))
            {
                var querry = connection.Table<Employee>();
                foreach (var s in querry)
                {
                    if (s.name == emp.name)
                    {
                        emp.id = s.id;
                    }
                }
                connection.Update(emp);
                return true;
            }
        }
        public bool DeleteCell(Employee employee)
        {
            using (var connection = new SQLite.SQLiteConnection(dbPath))
            {
                var querry = connection.Table<Employee>();
                foreach (var s in querry)
                {
                    if (s.name == employee.name)
                    {
                        connection.Delete<Employee>(s.id);
                        delflag = true;
                    }
                    else
                    {
                        delflag = false;
                    }

                }
            }
            return delflag;
        }

        public Employee SelectCell(string _name)
        {

            using (var connection = new SQLite.SQLiteConnection(dbPath))
            {
                var querry = connection.Table<Employee>();
                foreach (var s in querry)
                {
                    if (s.name == _name)
                    {
                        //connection.Delete<Employee>(s.id);
                        //return s;
                        emp = s;
                    }

                }
            }
            return emp;
        }
    }
}