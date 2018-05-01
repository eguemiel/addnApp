using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System;
using Android.Provider;
using Android.App;
using AddnApp.Base;

namespace AddnApp.Cadastro
{
    public class DamageStepImageFragment : BaseWizardStepFragment
    {
        private EditText txtTagNumber;
        private EditText txtAWBNumber;
        private GridView imageGrid;
        private FloatingActionButton btnCam;
        private FloatingActionButton btnPictureGalery;
        private int selectedItemMn;
        private Android.Net.Uri File;

        public static List<Bitmap> bitmapList;


        public static DamageStepImageFragment getInstance()
        {
            return new DamageStepImageFragment();
        }

        public DamageStepImageFragment()
        {
            
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
        }


        private void BtnPictureGalery_Click(object sender, EventArgs e)
        {
            Intent pickPhoto = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
            StartActivityForResult(pickPhoto, 1);
        }

        private void ImageGrid_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this.Activity, typeof(ImageDetailDamageActivity));

            intent.PutExtra("imagePosition", e.Position);
            StartActivity(intent);
        }

        private void ImageGrid_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            selectedItemMn = e.Position;
            Activity.OpenContextMenu(imageGrid);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, (int)resultCode, data);

            switch (requestCode)
            {
                case 0:
                    if (resultCode == (int)Result.Ok)
                    {
                    }

                    break;
                case 1:
                    if (resultCode == (int)Result.Ok)
                    {
                    }
                    break;
            }

<<<<<<< .merge_file_a15424
            imageGrid.Adapter = new ImageAdapter(Context, Item.ImageList);
=======
            //imageGrid.Adapter = new ImageAdapter(Context, Item.ImageList);
>>>>>>> .merge_file_a04240
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
<<<<<<< .merge_file_a15424
            menu.Add(Menu.None, 1001, 1, Resources.GetString(Resource.String.VolumeDamageStepImageFragmentRemoveImage));
=======
            menu.Add(Menu.None, 1001, 1, "Cadastro de RR");
>>>>>>> .merge_file_a04240

            base.OnCreateContextMenu(menu, v, menuInfo);
        }

        public override void OnDestroyView()
        {
            base.OnDestroy();
        }

        public override bool OnContextItemSelected(IMenuItem mi)
        {
            switch (mi.ItemId)
            {
                case 1001:
<<<<<<< .merge_file_a15424
                    Item.ImageList.RemoveAt(selectedItemMn);
                    imageGrid.Adapter = new ImageAdapter(Context, Item.ImageList);
=======
               //     Item.ImageList.RemoveAt(selectedItemMn);
              //      imageGrid.Adapter = new ImageAdapter(Context, Item.ImageList);
>>>>>>> .merge_file_a04240
                    break;
            }

            return base.OnContextItemSelected(mi);
        }
    }
}