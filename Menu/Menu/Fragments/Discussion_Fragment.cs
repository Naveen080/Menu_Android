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
using Android.Support.V7.Widget;
using System.Net.Http;
using Java.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Menu.Fragments
{
    public class Discussion_Fragment : Fragment
    {
        static View view;
        EditText disc_text;
        
        Button disc_button;
        Button delete_button;
        Button edit_button;
        RecyclerView recyclerView;
        private RecyclerView.LayoutManager recyclerview_layoutmanger;
        private RecyclerView.Adapter recyclerview_adapter;
        private RecycleViewClass<RecycleObjectClass> RecycleViewItem = new RecycleViewClass<RecycleObjectClass>();
       // List<string> RecycleViewList = new List<string>();
        RecycleObjectClass ROCobject = new RecycleObjectClass();
        List<RecycleObjectClass> dvslst = new List<RecycleObjectClass>();
        public static List<RecycleObjectClass> RecycleViewItem1 = new List<RecycleObjectClass>();
        static int index_id;
       // RecycleObjectClass a = new RecycleObjectClass();

        string data;
        int counter = 50;
        

        Notification.Builder builder;
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
            
            view = inflater.Inflate(Resource.Layout.Discussion_layout, container, false);
            builder = new Notification.Builder(this.Activity);
            builder.SetContentTitle("Submitted Ticket");
            
            disc_text = view.FindViewById<EditText>(Resource.Id.discussion_text);
            disc_button = view.FindViewById<Button>(Resource.Id.dsubmit);
            delete_button = view.FindViewById<Button>(Resource.Id.dsdelete);
            edit_button = view.FindViewById<Button>(Resource.Id.dsput);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView1);
            getdata();
            disc_button.Click += async delegate
            {

                postdata();
                notifier();
                getdata();

            };
            delete_button.Click += delegate
            {
                delete(index_id);
            };

            edit_button.Click += delegate
            {
                putdata(index_id);
            };
            return view;
        }
        
        public void notifier()
        {
            builder.SetContentText(disc_text.Text);
            builder.SetSmallIcon(Resource.Mipmap.ic_launcher);
            notify = builder.Build();
            notificationmanager = this.Activity.GetSystemService(Context.NotificationService) as NotificationManager;
            notificationmanager.Notify(notification_id, notify);
            disc_text.Text = "";
        }

        public void getdata()
        {
            HttpClient client = new HttpClient();
            RecycleViewItem1.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = client.GetAsync("https://5f5efdb5df620f00163e5196.mockapi.io/api/test/gettest").Result;
                if (response.IsSuccessStatusCode)
                {
                    dvslst = JsonConvert.DeserializeObject<RecycleObjectClass[]>(response.Content.ReadAsStringAsync().Result).ToList();
                    //RecycleViewItem = new RecycleViewClass<RecycleObjectClass>();
                    foreach (var s in dvslst)
                    {
                        if (s.id > 50)
                        {
                            //RecycleViewItem.Add(new RecycleObjectClass { id = s.id, TicketID = s.TicketID, TicketDetails = s.TicketDetails });
                            RecycleViewItem1.Add(s);
                            Console.WriteLine("id"+s.id+"data"+s.TicketDetails);
                        }
                    }
                    Console.WriteLine(RecycleViewItem1.Count);
                    recyclerview_layoutmanger = new LinearLayoutManager(this.Activity, LinearLayoutManager.Vertical, false);
                    recyclerView.SetLayoutManager(recyclerview_layoutmanger);
                    recyclerview_adapter = new RecyclerAdapter(RecycleViewItem1);
                    recyclerView.SetAdapter(recyclerview_adapter);
                }

            }
            catch
            {

            }
        }
        
        public async void postdata()
        {
            HttpClient client = new HttpClient();
            ROCobject.TicketDetails = disc_text.Text;
            ROCobject.TicketID = counter.ToString();
            ROCobject.id = counter;
            counter++;
            client.BaseAddress = new Uri("https://5f5efdb5df620f00163e5196.mockapi.io/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string serializedObject = JsonConvert.SerializeObject(ROCobject);
            //Console.WriteLine(ROCobject.GetType());
            HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://5f5efdb5df620f00163e5196.mockapi.io/api/test/gettest", contentPost);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                Console.WriteLine("posted successfully");
                disc_text.Text = "";
                //UserAccount product = JsonConvert.DeserializeObject<UserAccount>(data);
                //return product;
            }
        }
        public void delete(int id)
        {
            HttpClient client = new HttpClient();
            var url = "https://5f5efdb5df620f00163e5196.mockapi.io/api/test/gettest";
            var item_id = RecycleViewItem1[id].id;
            var object_url = string.Concat(url + "/", item_id.ToString());
            Console.WriteLine(object_url);
            var response = client.DeleteAsync(object_url);
            disc_text.Text = "";
            getdata();
          
        }

        public void putdata(int id)
        {
            HttpClient client = new HttpClient();
            var url = "https://5f5efdb5df620f00163e5196.mockapi.io/api/test/gettest";
            var item_id = RecycleViewItem1[id].id;
            var object_url = string.Concat(url + "/", item_id.ToString());
            ROCobject.id = id;ROCobject.TicketDetails = disc_text.Text;ROCobject.TicketID = id.ToString();
            client.BaseAddress = new Uri("https://5f5efdb5df620f00163e5196.mockapi.io/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string serializedObject = JsonConvert.SerializeObject(ROCobject);
            HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
            client.PutAsync(object_url,contentPost);
            disc_text.Text = "";
            getdata();
        }

        public void itemselect(int index, string data)
        {
            //make view and text view as static for doing item selection and displaying it in text view.
            Console.WriteLine("the selected row is "+ index);
            Console.WriteLine("the selected row id is " + data);
            disc_text = view.FindViewById<EditText>(Resource.Id.discussion_text);
            //a.id = index;
            index_id = index;
            disc_text.Text = RecycleViewItem1[index].TicketDetails;
            foreach (var s in RecycleViewItem1)
            {
                if(s.TicketID == data)
                {
                    Console.WriteLine("the selected row id is in if " + s.TicketID);
                   // disc_text.Text = s.TicketDetails;
                }
               // Console.WriteLine("id" + s.id + "data" + s.TicketDetails);
            }
          
            
            
        }
        
    }
}
