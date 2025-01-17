﻿using recuperacion.Modelo;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media.Abstractions;

namespace recuperacion
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            TakePhoto.Clicked += TakePhoto_Clicked;
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            var photo =
                await Plugin.Media.CrossMedia.Current
                    .TakePhotoAsync(new StoreCameraMediaOptions());
            if (photo != null)
            {
                Photo.Source = ImageSource.FromStream(() =>
                {
                    return photo.GetStream();
                });
            }
        }

        private async void tbiNuevaUbicacion_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtLatitud.Text) && string.IsNullOrEmpty(TxtLongitud.Text) && string.IsNullOrEmpty(TxtDescripcionCorta.Text) && string.IsNullOrEmpty(TxtDescripcionUbicacion.Text))
            {
                Limpiar();
            }
            else
            {
                bool confirmacion = await DisplayAlert("¿Desea cancelar la ubicación actual?", "Se borraran los datos actuales", "Aceptar", "Cancelar");
                if (confirmacion)
                    Limpiar();  
            }
        }

        private async void BtnVerLista_Clicked(object sender, EventArgs e)
        {
            //  ValidarSalida();
            await Navigation.PushAsync(new ListaUbicaciones());
        }

        private void BtnLocationActual_Clicked(object sender, EventArgs e)
        {
     
        }

        private async void BtnCapturar_Clicked(object sender, EventArgs e)
        {
            ObtenerPosition();
        }

        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(TxtLatitud.Text)
                && !string.IsNullOrEmpty(TxtLongitud.Text)
                && !string.IsNullOrEmpty(TxtDescripcionCorta.Text)
                && !string.IsNullOrEmpty(TxtDescripcionUbicacion.Text))
            {
         App.GetInstanceDB.SaveUbicationAsync(new Ubicacion
                {
                    DescripcionCorta = TxtDescripcionCorta.Text,
                    DescripcionLarga = TxtDescripcionUbicacion.Text,
                    Latitud = double.Parse(TxtLatitud.Text),
                    Longitud = double.Parse(TxtLongitud.Text),
                    Image = Photo.Source,
                    
                });
                Limpiar();
            }
            else
            {
                DisplayAlert("Campos incompletos", "Todos los campos deben estar llenos", "Aceptar");
            }
        }

    
        private void TbiLista_Clicked(object sender, EventArgs e)
        {
            // ValidarSalida();
        Navigation.PushAsync(new ListaUbicaciones());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ObtenerPosition();
        }

        
        private void Limpiar()
        {
            TxtDescripcionCorta.Text = "";
            TxtDescripcionUbicacion.Text = "";
            TxtLatitud.Text = "";
            TxtLongitud.Text = "";
            Photo.Source = "";
        }

        
        private async void ValidarSalida()
        {
            if (string.IsNullOrEmpty(TxtLatitud.Text) && string.IsNullOrEmpty(TxtLongitud.Text) && string.IsNullOrEmpty(TxtDescripcionCorta.Text) && string.IsNullOrEmpty(TxtDescripcionUbicacion.Text))
            {
                await Navigation.PushAsync(new ListaUbicaciones());
            }
            else
            {
                bool confirmacion = await DisplayAlert("¿Desea guardar los datos?", "Si sale se borraran los datos actuales", "Aceptar", "Cancelar");
                if (confirmacion)
                    await Navigation.PushAsync(new ListaUbicaciones());
            }
        }
       private async void ObtenerPosition()
        {
            var ubicacionActual = CrossGeolocator.Current;
            
            if (ubicacionActual.IsGeolocationAvailable)
            {

                if (!ubicacionActual.IsGeolocationEnabled)
                {
                    DisplayAlert("Ubicación", "Debe encender la  ubicacion o GPS de su dispositivo", "Aceptar");

                }
                else
                {
                    var position = await ubicacionActual.GetPositionAsync();
                    TxtLatitud.Text = position.Latitude.ToString();
                    TxtLongitud.Text = position.Longitude.ToString();

                }
            }
            else
            {
                DisplayAlert("Permiso denegado", "Para poder acceder a la localización debe pertir acceder a la ubicacion", "Aceptar");
            }
        }
    }
}
