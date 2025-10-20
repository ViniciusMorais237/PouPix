using API.Entities;
using API.Entities.Dto;

namespace API.Interfaces.Services
{
    public interface IInfoBankService
    {
        Task<InfoBank> ObterInfoBank();
        Task<bool> EditarInfoBank(EdicaoIndicadoresDto edicaoInfoBank);
        Task<IEnumerable<MonetarioDiario>> ObterInfoMoney();
    }
}