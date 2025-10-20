using Dapper;
using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IInfoBankRepository
    {
        Task<InfoBank> ObterInfoBank();
        Task<bool> EditarInfoBank(string query, DynamicParameters param);
        Task<IEnumerable<MonetarioDiario>> ObterInfoMoney();
    }
}