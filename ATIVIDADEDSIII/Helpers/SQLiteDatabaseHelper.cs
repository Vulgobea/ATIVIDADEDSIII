using ATIVIDADEDSIII.Models;
using Microsoft.VisualBasic;
using SQLite;
using System.Data.Common;


namespace ATIVIDADEDSIII.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path) 
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }
        public Task<int> Insert(Produto p) 
        {
            return _conn.InsertAsync(p);
        }
        
        public Task<List<Produto>> Update(Produto p) 
        { 
            string sql = "UPDATE Produto SET Descricao = ?, Quantidade = ?, Preco = ?, Categoria = ? WHERE Id = ?";

            return _conn.QueryAsync<Produto>(
               sql, p.Descricao, p.Quantidade, p.Preco, p.Categoria, p.Id
             );
        }

        public Task<int> Delete(int id) 
        { 
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAll() 
        { 
           return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q) 
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%' ";
            return _conn.QueryAsync<Produto>(sql);
        }
        public Task<List<Produto>> GetByCategoryAsync(string categoria)
        {
            return _conn.Table<Produto>().Where(p => p.Categoria == categoria).ToListAsync();
        }

    }
}
