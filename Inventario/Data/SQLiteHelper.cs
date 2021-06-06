using System;
using System.Collections.Generic;
using System.Text;
// Importar librerias
using SQLite;
using Inventario.Models;
using System.Threading.Tasks;

namespace Inventario.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<ProductModel>().Wait();
        }

        // Metodo para Insertar nuevo producto
        public Task<int> SaveProductAsync(ProductModel producto)
        {
            if (producto.prodId != 0)
            {
                return db.UpdateAsync(producto);
            }
            else
            {
                return db.InsertAsync(producto);
            }
        }

        // Metodo para Eliminar un producto
        public Task<int> DeleteProductAsync(string ProductKey)
        {
            return db.Table<ProductModel>().Where(a => a.prodClave == ProductKey).DeleteAsync();
        }

        // Metodo para ver productos
        public Task<List<ProductModel>> GetProductAsync()
        {
            return db.Table<ProductModel>().ToListAsync();
        }

        // Metodo para buscar producto por ID
        public Task<ProductModel> GetProductByID(int ProductID)
        {
            return db.Table<ProductModel>().Where(a => a.prodId == ProductID).FirstOrDefaultAsync();
        }

        // Metodo para buscar producto por Clave
        public Task<ProductModel> GetProductByKey(string ProductKey)
        {
            return db.Table<ProductModel>().Where(a => a.prodClave == ProductKey).FirstOrDefaultAsync();
        }
    }
}