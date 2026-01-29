using System.Runtime.InteropServices;
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
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RuntimeResumeData
    {
        public byte ModuleId;
        public int  Arg0;
        public int  Arg1;
        public int  Arg2;
        public int  Arg3;
    }
}