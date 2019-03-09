using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace PALMASoft.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
