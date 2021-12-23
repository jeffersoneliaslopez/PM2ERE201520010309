using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using recuperacion.DataBase;

namespace recuperacion
{
    public partial class App : Application
    {

        
        private static ManagerDataBase InstanceDataBase;

        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new ListaUbicaciones());
            MainPage = new NavigationPage(new Login());

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
        public static ManagerDataBase GetInstanceDB
        {
            get
            {
                
                if (InstanceDataBase == null)
                {
                    
                    string dbName = "dbexamen.sqlite";
                    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    string finalPathDb = Path.Combine( folderPath,dbName);
                    
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    
                    InstanceDataBase = new ManagerDataBase(finalPathDb);
                }
                return InstanceDataBase;
            }

        }


    }
}
