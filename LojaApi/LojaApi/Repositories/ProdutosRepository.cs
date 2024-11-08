using Dapper;
using LojaApi.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProdutosApi.Repositories
{
    public class ProdutosRepository
    {
        private readonly string _connectionString;

        public ProdutosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Produtos>> ListarProdutosDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Produtos";
                return await conn.QueryAsync<Produtos>(sql);
            }
        }

        public async Task<int> RegistrarProdutoDB(Produtos produtos)
        {
            using (var conn = Connection)
            {
                var sql = "INSERT INTO Produtos (Nome, Descricao, Preco, QuantidadeEstoque) " +
                          "VALUES (@Nome, @Descricao, @Preco, @QuantidadeEstoque);" +
                          "SELECT LAST_INSERT_ID();";
                return await conn.ExecuteScalarAsync<int>(sql, produtos);
            }
        }

        public async Task<int> AtualizarProdutoDB(Produtos produtos)
        {
            using (var conn = Connection)
            {
                var sql = "UPDATE Produtos SET Nome = @Nome, Descricao = @Descricao, Preco = @Preco, " +
                          "QuantidadeEstoque = @QuantidadeEstoque, WHERE Id = @Id";
                return await conn.ExecuteAsync(sql, produtos);
            }
        }

        public async Task<int> ExcluirProdutoDB(int id)
        {


            using (var conn = Connection)
            {
                var sqlExcluirProduto = "DELETE FROM Produtos WHERE Id = @Id";
                return await conn.ExecuteAsync(sqlExcluirProduto, new { Id = id });
            }
        }
    }
}