using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using recuperacion.Modelo;
using Xamarin.Forms.Maps;

namespace recuperacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        
        private Ubicacion ubicacionSeleccionada;

        public MapPage()
        {
            InitializeComponent();
        }
        
        public MapPage(Ubicacion ubicacion)
        {
            InitializeComponent();
            ubicacionSeleccionada = ubicacion;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if(ubicacionSeleccionada!=null)
            {
                
                var posicion = new Position(ubicacionSeleccionada.Latitud, ubicacionSeleccionada.Longitud);

                
                Pin ubicacionPin = new Pin {
                    Address = ubicacionSeleccionada.DescripcionLarga,
                    Label = ubicacionSeleccionada.DescripcionCorta,
                    Type = PinType.Place,
                    
                    Position = posicion
                };
                mapa.Pins.Add(ubicacionPin);

                
                mapa.MoveToRegion(MapSpan.FromCenterAndRadius(posicion,Distance.FromKilometers(0.5)));
            }
        }

        private async void tbiNuevaUbicacion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
          await    Navigation.PushAsync(new MainPage());
        }
    }
}