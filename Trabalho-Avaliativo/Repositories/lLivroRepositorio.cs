using Livraria.Models;
using Trabalho_Avaliativo.Models;

namespace Livraria.Repositories
{
    public interface ILivroRepositorio
    {
        void Inserir(LivroModel livro);
        List<LivroModel> Listar();
        bool IsbnExiste(string isbn);
    }
}