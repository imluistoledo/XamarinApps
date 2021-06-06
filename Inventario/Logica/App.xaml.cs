using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventario.Data;
using System.IO;

namespace Inventario
{
    public partial class App : Application
    {
        static SQLiteHelper db;
        public static SQLiteHelper SQLiteDB
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BDDProductos.db3"));
                }
                return db;
            }
        }
        public App() 
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
