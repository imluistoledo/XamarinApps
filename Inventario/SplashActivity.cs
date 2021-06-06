﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Importar esta libreria
using Android.Content.PM;

namespace Inventario.Droid
{
    // Este elemento se modifico para el splash screen
    [Activity(Label = "Inventario", Theme = "@style/SplashTheme",
        MainLauncher = true, NoHistory = true, Icon = "@mipmap/logo",
        ConfigurationChanges = ConfigChanges.ScreenSize)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Iniciar splash screen
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            // Create your application here
        }
    }
}