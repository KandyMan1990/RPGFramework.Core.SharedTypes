using System.Threading.Tasks;

namespace RPGFramework.Core.SharedTypes
{
    public interface IModule
    {
        /// <summary>
        /// Entered, its scene loaded and its container built. <paramref name="entry" /> is how the router chose to enter it,
        /// one of <see cref="ModuleEntries" />'s or the module's own.
        /// </summary>
        Task OnEnterAsync(byte entry);

        /// <summary>Left, for good: its scene unloads and its container is disposed after this. It may be suspended.</summary>
        Task OnExitAsync();

        /// <summary>
        /// Another module opens over this one, which stays loaded, and should be still until <see cref="OnResumeAsync" />:
        /// taking no input, ticking nothing, and showing nothing it would not want seen under the other.
        /// </summary>
        Task OnSuspendAsync();

        /// <summary>The module opened over this one has closed, and this one carries on from where it was.</summary>
        Task OnResumeAsync();
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