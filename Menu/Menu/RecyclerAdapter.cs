using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Menu.Fragments;
using System.Collections.Generic;

namespace Menu
{
    internal class RecyclerAdapter : RecyclerView.Adapter
    {
        private RecycleViewClass<RecycleObjectClass> recycleViewList;
        private List<RecycleObjectClass> recycleViewList1;
        Context context;
        int row_id;
        string row_text_id;
        public Discussion_Fragment disc_obj = new Discussion_Fragment();
        public RecyclerAdapter(List<RecycleObjectClass> recycleViewList)
        {
            this.recycleViewList1 = recycleViewList;
            System.Console.WriteLine("inside RecycleAdapter:");
            System.Console.WriteLine("Number of elements in List are: "+ this.recycleViewList1.Count);
           
            NotifyDataSetChanged();
        }

        public class MyView : RecyclerView.ViewHolder
        {
            public View mainview
            {
                get;
                set;
            }
            public TextView ticketid
            {
                get;
                set;
            }
            public TextView ticket
            {
                get;set;
            }
            public MyView(View view) : base(view)
            {
                mainview = view;
            }
        }
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View listitem = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListViewLayout, parent, false);
                TextView ticketid = listitem.FindViewById<TextView>(Resource.Id.Textname);
                TextView ticketinfo = listitem.FindViewById<TextView>(Resource.Id.Textname2);
            MyView view = new MyView(listitem)
                {
                    ticketid = ticketid,ticket=ticketinfo

                };
                return view;
            }


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyView myholder = holder as MyView;
            myholder.ticketid.Text = "TicketID:"+ recycleViewList1[position].id;
            myholder.ticket.Text = "SUBJECT" + recycleViewList1[position].TicketDetails;

            myholder.ItemView.Click += (sender, e) =>
            {
                row_text_id = myholder.ticketid.Text;
                row_id = myholder.Position;
                
                disc_obj.itemselect(row_id,row_text_id);
            };

        }

        
        public override int ItemCount
        {
            get
            {
                return recycleViewList1.Count;
            }
        }

        
    }
}
