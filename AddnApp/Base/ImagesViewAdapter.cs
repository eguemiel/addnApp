using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Java.Lang;
using TiagoSantos.EnchantedViewPager;

namespace AddnApp.Base
{
    public class ImagesViewAdapter : EnchantedViewPagerAdapter<ImagesViewAdapter>
    {
        Context Context;

        LayoutInflater inflater;

        public ImagesViewAdapter(Context context, List<ImagesViewAdapter> list) : base(list)
        {
            Context = context;
            inflater = LayoutInflater.From(Context);
            mAlbumlist = albumList;
        }

        public override Object InstantiateItem(ViewGroup container, int position)
        {
            if (mAlbumlist.size() == 0) return null;

            int itemPosition = position % mAlbumlist.size();

            ImageView mCurrentView = (ImageView)inflater.inflate(R.layout.item_view, container, false);

            BitmapFactory.Options opts = new BitmapFactory.Options();
            opts.inScaled = false;

            AlbumArt album = this.mAlbumlist.get(itemPosition);
            Bitmap bitmap = BitmapFactory.decodeResource(Context.getResources(), album.getImageResource(), opts);

            mCurrentView.setImageBitmap(bitmap);
            mCurrentView.setTag(EnchantedViewPager.ENCHANTED_VIEWPAGER_POSITION + position);
            container.addView(mCurrentView);

            return mCurrentView;
        }
    }
}