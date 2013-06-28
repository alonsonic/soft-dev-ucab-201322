﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace AeiMobile
{
    public partial class PerfilPage : PhoneApplicationPage
    {
        public PerfilPage()
        {
            InitializeComponent();
            cargarInformacionUsuario();
        }

        private void cargarInformacionUsuario()
        {
            this.textUsuario.Text = BufferUsuario.Usuario.Nombre +" "+BufferUsuario.Usuario.Apellido;
            this.textCorreo.Text = BufferUsuario.Usuario.Email;
            this.textNacimiento.Text = BufferUsuario.Usuario.FechaNacimiento.ToString("yyyy-MM-dd");
            cargarListaDirecciones();
        }

        private void cargarListaDirecciones()
        {
            foreach (var item in BufferUsuario.Usuario.Direcciones)
            {
                this.listDireccion.Items.Add("Estado: " + item.Estado + " Ciudad: " + item.Ciudad + "/n Descripción: " +item.Descripcion );
            }
        }
    }
}