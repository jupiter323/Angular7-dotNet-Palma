using System.Collections.Generic;
using MvvmHelpers;
using PALMASoft.Models.NavigationMenu;

namespace PALMASoft.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}