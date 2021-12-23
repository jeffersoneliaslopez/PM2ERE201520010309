using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace recuperacion.Modelo
{
    public class Ubicacion
    {
        [PrimaryKey,AutoIncrement]
        public int IdUbicacion { get; set; }
        public double Latitud { set; get; }
        public double Longitud { set; get; }
        [MaxLength(30)]

        public String DescripcionCorta { set; get; }
        [MaxLength(60)]
        public String DescripcionLarga { set; get; }
        public ImageSource Image { get; internal set; }
    }
}