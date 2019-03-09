using System.Threading.Tasks;
using PALMASoft.Sessions.Dto;

namespace PALMASoft.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
