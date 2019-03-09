using System.Threading.Tasks;
using PALMASoft.Views;
using Xamarin.Forms;

namespace PALMASoft.Services.Modal
{
    public interface IModalService
    {
        Task ShowModalAsync(Page page);

        Task ShowModalAsync<TView>(object navigationParameter) where TView : IXamarinView;

        Task<Page> CloseModalAsync();
    }
}
