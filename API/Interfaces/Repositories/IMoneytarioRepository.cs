using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IMoneytarioRepository
    {
        Task<InfoMoneyMes> ObterInfoMes(DateTime data);
        Task<IEnumerable<InfoMoneyDiario>> ObterInfoDiariaMensal(DateTime data);
        Task<IEnumerable<Historico?>> ObterHistorico(DateTime data);
        Task<Banco?> ObterInfoBanco(int idBanco);
        Task<bool> InserirInfoDiariaPadrao(List<InfoMoneyDiario> dias);
    }

}
