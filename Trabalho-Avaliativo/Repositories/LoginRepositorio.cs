
using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;
using System.Data;
using Trabalho_Avaliativo.Models;

namespace Trabalho_Avaliativo.Repositories
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly string _connectionString;
        private readonly PasswordHasher<LoginModel> _passwordHasher;

        public LoginRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            _passwordHasher = new PasswordHasher<LoginModel>();
        }

        public async Task<bool> Cadastrar(LoginModel usuario, string senhaPura)
        {
            usuario.SenhaHash = _passwordHasher.HashPassword(usuario, senhaPura);

            using var conexao = new MySqlConnection(_connectionString);
            await conexao.OpenAsync();

            var query = "INSERT INTO login (nome, email, senhahash) VALUES (@Nome, @Email, @SenhaHash)";
            using var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@Nome", usuario.Nome);
            comando.Parameters.AddWithValue("@Email", usuario.Email);
            comando.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);

            var linhasAfetadas = await comando.ExecuteNonQueryAsync();

            return linhasAfetadas > 0;
        }

        public async Task<LoginModel?> BuscarEmail(string email)
        {
            using var conexao = new MySqlConnection(_connectionString);
            await conexao.OpenAsync();

            var query = "SELECT Id, Nome, Email, SenhaHash FROM login WHERE Email = @Email";
            using var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@Email", email);

            using var reader = await comando.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new LoginModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nome = reader.GetString("Nome"),
                    Email = reader.GetString("Email"),
                    SenhaHash = reader.GetString("SenhaHash")
                };
            }

            return null;
        }
    }
}
