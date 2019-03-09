using System.Collections.Generic;
using PALMASoft.Authorization.Users.Dto;
using PALMASoft.Dto;

namespace PALMASoft.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}