﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeiWebServices.Permanencia
{
    interface DAOCategoria
    {
        List<Categoria> categorias();
        Categoria buscarCategoriaPorProducto(int idProducto);
    }
}
