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
using Windows.UI.Xaml.Navigation;
using AeiCliente.ServicioUsuario;
using AeiCliente.ServicioProducto;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace AeiCliente
{

    public sealed partial class DetalleProductoPage : Page
    {
		bool comentariosVisible = false;
        public static AeiCliente.ServicioProducto.Producto producto = null;
        ServicioProductoClient servicioProducto = new ServicioProductoClient();
        public DetalleProductoPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            producto = e.Parameter as AeiCliente.ServicioProducto.Producto;
            textNombre.Text = producto.Nombre;
            textDescripcion.Text = producto.Descripcion;
            textPrecio.Text = "Precio " + producto.Precio.ToString() + " Bs";
            if (producto.Cantidad == 1) textCantidad.Text = producto.Cantidad.ToString() + " unidad disponible";
            else textCantidad.Text = producto.Cantidad.ToString()+" unidades disponibles";
            producto.Calificaciones = await servicioProducto.buscarCalificacionProductoAsync(producto.Id);
            cargarComentarios();
        }
        private void cargarComentarios()
        {
            List<Calificacion> listaCalificacion = producto.Calificaciones;
            if (listaCalificacion != null)
            {
                for (int indexCalificacion = 0; indexCalificacion < listaCalificacion.Count; indexCalificacion++)
                {
                    this.textComentarios.Text = this.textComentarios.Text + "\n \n" + listaCalificacion[indexCalificacion].Usuario.Nombre + " " + listaCalificacion[indexCalificacion].Usuario.Apellido + "\n" + listaCalificacion[indexCalificacion].Comentario;
                }
            }
            else this.textComentarios.Text = "\n \n Aun no tenemos calificaciones para este producto.";
        }

        private void botonMostrarComentario_Click(object sender, RoutedEventArgs e)
        {
			if(comentariosVisible)
			{
				OcultarComentarios.Begin();
				comentariosVisible = false;
			}
			else
			{
				mostrarComentarios.Begin();
        		comentariosVisible = true;
			}
		}

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        	this.Frame.Navigate(typeof(ListaCompraPage));
        }

        private void botonComprar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        	// TODO: Agregar implementación de controlador de eventos aquí.
        }
	}
}
