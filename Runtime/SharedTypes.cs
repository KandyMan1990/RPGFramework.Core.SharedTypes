using System.Threading.Tasks;

namespace RPGFramework.Core.SharedTypes
{
    public interface IModule
    {
        Task OnEnterAsync();
        Task OnExitAsync();
    }

    /// <summary>
    /// Where a variable will be read from/written to
    /// </summary>
    public enum MemoryBank
    {
        /// <summary>
        /// Saved to the games save file
        /// </summary>
        Persistent = 0,
        /// <summary>
        /// Not saved to the games save file, but will survive for the game session, including module transitions
        /// </summary>
        Session = 1,
        /// <summary>
        /// Scratch space for a script's own working values. Not saved, not part of the memory service, and not
        /// shared: each running script has its own <c>TempMemory</c>, zeroed when it starts.<br /><br />
        /// Use it for intermediates — a random roll about to be compared, a loop counter, a value being
        /// built up. The point is that it costs nothing permanent:
        /// <see cref="Persistent" /> bytes are in every save file forever, and <see cref="Session" /> bytes
        /// accumulate for the whole session, so neither is somewhere to put a value that matters for
        /// three instructions.
        /// </summary>
        Temp = 2
    }
}