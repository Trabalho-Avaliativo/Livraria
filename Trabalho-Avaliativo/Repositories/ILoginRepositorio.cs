using Trabalho_Avaliativo.Models;

namespace Trabalho_Avaliativo.Repositories
{
    public interface ILoginRepositorio
    {
        Task<bool> Cadastrar(LoginModel usuario, string senhaPura);

        Task<LoginModel?> BuscarEmail(string email);
    }
}