using MySql.Data.MySqlClient;
using Livraria.Models;

namespace Livraria.Repositories
{
    public class LivroRepositorio : ILivroRepositorio
    {
        private readonly string _connectionString;

        public LivroRepositorio(IConfiguration configuration)
        {
            // Lê a string de conexão do appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public void Inserir(LivroModel livro)
        {
            using var conexao = new MySqlConnection(_connectionString);
            conexao.Open();

            var sql = "INSERT INTO livro (isbn, nome, descricao, valor) " +
                      "VALUES (@isbn, @nome, @descricao, @valor)";

            using var cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@isbn", livro.Isbn);
            cmd.Parameters.AddWithValue("@nome", livro.Nome);
            cmd.Parameters.AddWithValue("@descricao", (object?)livro.Descricao ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@valor", livro.Valor);
            cmd.ExecuteNonQuery();
        }

        public List<LivroModel> Listar()
        {
            var lista = new List<LivroModel>();

            using var conexao = new MySqlConnection(_connectionString);
            conexao.Open();

            var sql = "SELECT id, isbn, nome, descricao, valor FROM livro ORDER BY nome";
            using var cmd = new MySqlCommand(sql, conexao);
            using var leitor = cmd.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new LivroModel
                {
                    Id = leitor.GetInt32("id"),
                    Isbn = leitor.GetString("isbn"),
                    Nome = leitor.GetString("nome"),
                    Descricao = leitor.IsDBNull(leitor.GetOrdinal("descricao"))
                                ? null : leitor.GetString("descricao"),
                    Valor = leitor.GetDecimal("valor")
                });
            }

            return lista;
        }

        public bool IsbnExiste(string isbn)
        {
            using var conexao = new MySqlConnection(_connectionString);
            conexao.Open();

            var sql = "SELECT COUNT(*) FROM livro WHERE isbn = @isbn";
            using var cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@isbn", isbn);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
    }
}