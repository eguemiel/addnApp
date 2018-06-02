﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using System;
using AddnApp.Cadastro;
using AddnApp.Entities;

namespace AddnApp
{
    [Activity(Label = "AddnApp",  Theme = "@style/AppTheme")]
    public class MainActivity : ADDNAbstractActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        private Android.Support.V7.Widget.Toolbar toolbar;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Program.Main = this;
            SetContentView(Resource.Layout.activity_main);
            Loading = FindViewById<ViewGroup>(Resource.Id.loading);
            Loading.BringToFront();
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            Navigate<HomeFragment>();
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            drawerLayout.CloseDrawer(Android.Support.V4.View.GravityCompat.Start);

            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.nav_cadastrorr:
                    var wizard = this.Navigate<CadastroRRFragment>();                     
                    wizard.Data = new RegistroDeReforma();
                    break;
                case Resource.Id.nav_sair:
                    StartActivity(typeof(LoginActivity));
                    Finish();
                    break;
                case Resource.Id.nav_fechar:
                    Finish();
                    break;
                default:
                    break;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnMenuOpened(int featureId, IMenu menu)
        {
            return base.OnMenuOpened(featureId, menu);
        }

    }
}

