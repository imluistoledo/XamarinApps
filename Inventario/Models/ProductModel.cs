using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Inventario.Models
{
    public class ProductModel
    {
        // Atributos del modelo

        // Id del producto
        [PrimaryKey,AutoIncrement] 
        public int prodId { get; set; }

        // Clave del producto Ej.: 001, 002...
        [MaxLength(3)]
        public string prodClave { get; set; }

        // Nombre del producto
        [MaxLength(30)]
        public string prodName { get; set; }

        // Precio del producto
        [MaxLength(10)]
        public int prodPrec { get; set; }

        // Cantidad del producto
        [MaxLength(4)]
        public int prodCant { get; set; }

        // Nombre del proveedor
        [MaxLength(30)]
        public string provName { get; set; }

        // Telefono del proveedor
        [MaxLength(12)]
        public string provTel { get; set; }
    }
}
