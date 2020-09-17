using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Menu
{
    class RecycleViewClass <T>
    {
        readonly List<T> mItems;
        RecyclerView.Adapter mAdapter;
        public RecycleViewClass()
        {
            mItems= new List<T>();
        }
        public RecyclerView.Adapter Adapter
        {
            get
            {
                return mAdapter;
            }
            set
            {
                mAdapter = value;
            }
        }

        public void Add(T item)
        {
            mItems.Add(item);
            if (Adapter != null)
            {
                Adapter.NotifyItemInserted(0);
            }
        }

        public void Remove(int position)
        {
            mItems.RemoveAt(position);
            if (Adapter != null)
            {
                Adapter.NotifyItemRemoved(0);
            }
        }
        public T fetch(int index)
        {
            Console.WriteLine("inside adapter with index: "+index);
            return mItems.ElementAt(index);
            //return mItems[index];
        }
        public T this[int index]
        {
            get
            {
                Console.WriteLine(mItems[index]);
                return mItems[index];
                
            }
            set
            {
                mItems[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return mItems.Count;
            }
        }
    }
}
