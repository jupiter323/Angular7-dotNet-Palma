using System.Threading.Tasks;

namespace PALMASoft.Identity
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}