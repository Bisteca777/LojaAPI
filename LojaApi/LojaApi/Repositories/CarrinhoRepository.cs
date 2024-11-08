using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaApi.Repositories
{
    public class CarrinhoRepository
    {
    
            private readonly string _connectionString;

            public CarrinhoRepository(string connectionString)
            {
                _connectionString = connectionString;
            }

            private IDbConnection Connection => new MySqlConnection(_connectionString);

            public async Task<IEnumerable<CarrinhoRepository>> ListarProdutosCarrimho()
            {
                using (var conn = Connection)
                {
                    var sql = "SELECT * FROM Carrinho";
                    return await conn.QueryAsync<CarrinhoRepository>(sql);
                }
            }

            public async Task<int> AdicionarCarrinho(CarrinhoRepository produtos)
            {
                using (var conn = Connection)
                {
                    var sql = "INSERT INTO Carrinho (UsuarioId , ProdutoId , Quantidade ) " +
                              "VALUES (@UsuarioId, @ProdutoId, @Quantidade);" +
                              "SELECT LAST_INSERT_ID();";
                    return await conn.ExecuteScalarAsync<int>(sql, produtos);
                }
            }

            

           
       
    }
}
