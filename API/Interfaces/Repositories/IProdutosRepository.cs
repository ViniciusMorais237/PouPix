using API.Entities.Dto;
using API.Entities.Produto;
using API.Repositories.Models;

namespace API.Interfaces.Repositories;

public interface IProdutosRepository
{
    Task<bool> AdicionarDesejo(DesejoDB produto);
    Task<bool> InserirQuantiaDesejo(int id, int quantia);
    Task<IEnumerable<DesejoDB>> ObterDesejos();
}