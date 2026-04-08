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

    /// <summary>
    /// Where a variable will be read from/written to
    /// </summary>
    public enum MemoryBank
    {
        /// <summary>
        /// Saved to the games save file
        /// </summary>
        Global = 0,
        /// <summary>
        /// Not saved to the games save file, but will survive for the game session, including module transitions
        /// </summary>
        Session = 1,
        /// <summary>
        /// Something that doesn't need to be saved nor persist for any length of time, should be used and discarded over 1 frame
        /// </summary>
        Temp = 2
    }
}