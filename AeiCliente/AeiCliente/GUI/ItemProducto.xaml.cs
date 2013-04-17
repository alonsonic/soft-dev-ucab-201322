﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using AeiCliente.ServiceReference2;

 
// La plantilla de elemento Control de usuario está documentada en http://go.microsoft.com/fwlink/?LinkId=234236

namespace AeiCliente
{
    public sealed partial class ItemProducto : UserControl
    {
        Producto producto = null;
        Page padre = null;

        public ItemProducto()
        {
            this.setImagenProducto("ms-appx:/App_Data/item.png.png");
            this.InitializeComponent();
        }

        public ItemProducto(int indexProducto, Page padre)
        {
            this.InitializeComponent();
            this.padre = padre;

            producto = ListaProducto.ListaProductos.ElementAt(indexProducto);
            textoNombreProducto.Text = producto.Nombre;
            //TODO: SETEAR LA IMAGEN
            //this.setImagenProducto("ms-appx:/App_Data/item.png.png");
        }

        public void setImagenProducto(String imageSource)
        {
            BitmapImage newImage = new BitmapImage();
            newImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            newImage.UriSource = new Uri(imageSource);
            this.imagenProducto.Source = newImage;
        }

        private void botonDetalle_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            padre.Frame.Navigate(typeof(DetalleProductoPage), producto);
        }

    }
}