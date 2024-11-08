using Dapper;
using LojaApi.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaApi.Repository
{
    public class PedidosRepository
    {
        private readonly string _connectionString;

        public PedidosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly PedidosRepository _pedidosRepository;


        public PedidosRepository(string connectionString, PedidosRepository pedidosRepository)
        {
            _connectionString = connectionString;
            _pedidosRepository = pedidosRepository;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<int> CadastrarPedidoDB(Pedidos pedidos)
        {
            using (var conn = Connection)
            {
                var sql = "INSERT INTO Pedidos (UsuarioId, DataPedido, StatusPedido, ValorTotal) " +
                          "VALUES (@UsuarioId, @DataPedido, @StatusPedido, @ValorTotal);" +
                          "SELECT LAST_INSERT_ID();";
                return await conn.ExecuteScalarAsync<int>(sql, pedidos);
            }
        }

        public async Task<IEnumerable<Pedidos>> ListarPedidosDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Pedidos";
                return await conn.QueryAsync<Pedidos>(sql);
            }
        }


    }
}
