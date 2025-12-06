using System.Threading.Tasks;

namespace RPGFramework.Core.SharedTypes
{
    public interface IModuleArgs
    {

    }

    public interface IModule
    {
        Task OnEnterAsync(IModuleArgs args);
        Task OnExitAsync();
    }
}