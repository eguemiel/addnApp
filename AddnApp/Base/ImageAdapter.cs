using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Collections.Generic;
using System.Linq;

namespace AddnApp.Cadastro
{
    internal class ImageAdapter : BaseAdapter
    {
        private Context context;
        private List<Bitmap> imageList;

        public ImageAdapter(Context context, List<Bitmap> imageList)
        {
            this.context = context;
            this.imageList = imageList;
        }

        public override int Count
        {
            get
            {
                if (this.imageList != null && this.imageList.Any())
                    return this.imageList.Count();
                else
                    return 0;
            }
        }

        public override Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView imageView;
            if (convertView == null)
            {
                imageView = new ImageView(this.context);
                imageView.LayoutParameters = new AbsListView.LayoutParams(250, 250);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imageView.SetPadding(20, 20, 20, 20);
            }
            else
            {
                imageView = (ImageView)convertView;
            }

            var bitmap = imageList.ElementAt(position);
            Bitmap bitmapResized;

            double widthMult = (double)250 / bitmap.Width;
            double heigthMult = (double)250 / bitmap.Height;
            bitmapResized = Bitmap.CreateScaledBitmap(bitmap, (int)(bitmap.Width * widthMult), (int)(bitmap.Height * heigthMult), true);
            
            imageView.SetImageBitmap(bitmapResized);

            return imageView;
        }
    }
}