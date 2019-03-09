using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using PALMASoft.Dto;

namespace PALMASoft.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
