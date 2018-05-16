using System;
using Android.Content;
using Android.Graphics;
using Java.Text;
using Java.Util;
using System.IO;

namespace AddnApp.Helpers
{
    public static class Helpers
    {
        public static T Cast<T>(this Java.Lang.Object obj) where T : class
        {
            var propertyInfo = obj.GetType().GetProperty("Instance");
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
        }

        public static string ConvertToBase64String(Bitmap image, int quality = 100)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                image.Compress(Bitmap.CompressFormat.Jpeg, quality, ms);
                byte[] byteImage = ms.ToArray();
                return Convert.ToBase64String(byteImage, Base64FormattingOptions.InsertLineBreaks);
            };
        }

        public static Bitmap GetImageBitmapFromUrl(Android.Net.Uri uri, Context ctx)
        {
            Bitmap imageBitmap = null;
            Bitmap compressedBitmap = null;

            System.IO.Stream inputStream = ctx.ContentResolver.OpenInputStream(uri);            
            imageBitmap = BitmapFactory.DecodeStream(inputStream);

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                imageBitmap.Compress(Bitmap.CompressFormat.Jpeg, 60, ms);
                byte[] byteImage = ms.ToArray();
                compressedBitmap = BitmapFactory.DecodeByteArray(byteImage, 0, byteImage.Length);
            };
            

            if (inputStream != null)
                inputStream.Close();

            imageBitmap.Dispose();

            return compressedBitmap;
        }

        public static Java.IO.File GetOutputMediaFile()
        {
            Java.IO.File mediaStorageDir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraDemo");

            if (!mediaStorageDir.Exists())
            {
                if (!mediaStorageDir.Mkdirs())
                {
                    return null;
                }
            }

            String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").Format(new Date());
            return new Java.IO.File(mediaStorageDir.Path + Java.IO.File.Separator +
                        "IMG_" + timeStamp + ".jpg");
        }

        public static byte[] GetImageArray(Bitmap image)
        {
            byte[] byteImage;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                byteImage = ms.ToArray();
            }
            return byteImage;
        }
    }
}