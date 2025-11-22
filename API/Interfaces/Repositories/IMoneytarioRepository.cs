using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IMoneytarioRepository
    {
        Task<InfoMoneyMes> ObterInfoMes(DateTime data);
    }

}
