﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using AeiMobile.ServicioAEI;
using System.Windows.Media.Imaging;

namespace AeiMobile
{
    public partial class ArticuloPivotPage : PhoneApplicationPage
    {
        private bool menuAbierto = false;
        public static Producto producto;

        public ArticuloPivotPage()
        {
            InitializeComponent();
            Menu.Children.Add(new MenuControl());

            ServicioAEIClient servicio = new ServicioAEIClient();
            cargarInformacionProducto();
        }

        
        private void cargarInformacionProducto()
        {
            this.textPrecio.Text = producto.Precio.ToString() + " Bs";
            this.textCantidad.Text = "Cantidad: " + producto.Cantidad.ToString();
            this.textDescripcion.Text = producto.Descripcion;
            this.pivotProducto.Title = producto.Nombre;
            cargarListaCalifiacion();
            //setImagenProducto();
        }

        private void cargarListaCalifiacion()
        {
            if(producto.Calificaciones != null && producto.Calificaciones.Count > 0)
            {
                for (int i = 0; i < producto.Calificaciones.Count; i++)
                {
                    this.listCalificacion.Items.Add(producto.Calificaciones[i].Usuario.Nombre + " " + producto.Calificaciones[i].Usuario.Apellido + ". \n Fecha: "+producto.Calificaciones[i].Fecha.ToString("dd-MM-yyyy")+
                            ".  Puntaje: " +producto.Calificaciones[i].Puntaje+ " estrella(s). \n Comentario: " + producto.Calificaciones[i].Comentario);
                }
            }
            else
            {
                this.listCalificacion.Items.Add("Aun no tenemos calificaciones para este producto.");
            }
            
        }

        public void setImagenProducto()
        {
            BitmapImage newImage = new BitmapImage();
            newImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            newImage.UriSource = new Uri(producto.ImagenDetalle);
            this.imagenProducto.Source = newImage;
            imagenProducto.UpdateLayout();
        }

        private void buttonComprar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ServicioAEIClient servicioAei = new ServicioAEIClient();

            if (producto.Cantidad == 0)
            {
                MessageBox.Show("Este articulo esta agotado, buscaremos en otra tienda");
                return;
            }

            servicioAei.checkearProductoCarritoAsync(BufferUsuario.Usuario, producto);
            servicioAei.checkearProductoCarritoCompleted += (s, a) =>
            {
                bool chequear = a.Result;
                if (chequear)
                {
                    MessageBox.Show("Ya posee este producto en el carrito");
                    return;
                }

                ConfirmacionPage.producto = producto;
                NavigationService.Navigate(new Uri("/ConfirmacionPage.xaml", UriKind.Relative));
            };
        }

        private void botonMenu_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (menuAbierto)
            {
                StoryMenuCerrar.Begin();
                menuAbierto = false;
            }
            else
            {
                StoryMenuAbrir.Begin();
                menuAbierto = true;
            }
        }
    }
    
}