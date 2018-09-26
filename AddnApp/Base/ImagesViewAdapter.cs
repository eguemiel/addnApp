using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.Lang;
using TiagoSantos.EnchantedViewPager;

namespace AddnApp.Base
{
    public class ImagesViewAdapter : EnchantedViewPagerAdapter<Bitmap>
    {
        Context Context;
        LayoutInflater inflater;
        List<Bitmap> bitmapList; 


        public ImagesViewAdapter(Context context, List<Bitmap> list) : base(list)
        {
            Context = context;
            inflater = LayoutInflater.From(Context);
            bitmapList = list;
        }

        public override Object InstantiateItem(ViewGroup container, int position)
        {
            if (bitmapList.Count == 0) return null;

            int itemPosition = position % bitmapList.Count;

            ImageView mCurrentView = (ImageView)inflater.Inflate(Resource.Layout.item_view, container, false);

            BitmapFactory.Options opts = new BitmapFactory.Options();
            opts.InScaled = false;

            Bitmap bitmap = this.bitmapList[itemPosition];

            mCurrentView.SetImageBitmap(bitmap);
            mCurrentView.Tag = EnchantedViewPager.EnchantedViewpagerPosition + position;
            container.AddView(mCurrentView);

            return mCurrentView;
        }

        
        override public bool IsViewFromObject(View view, Object @object)
        {
            return view == @object;
        }

        public override void DestroyItem(ViewGroup container, int position, Object @object)
        {
            container.RemoveView((View)@object);
        }
    }
}