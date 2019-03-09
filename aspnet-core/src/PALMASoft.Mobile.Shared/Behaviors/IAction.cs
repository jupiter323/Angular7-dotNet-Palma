using Xamarin.Forms.Internals;

namespace PALMASoft.Behaviors
{
    [Preserve(AllMembers = true)]
    public interface IAction
    {
        bool Execute(object sender, object parameter);
    }
}