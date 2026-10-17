namespace RPGFramework.Core.SharedTypes
{
    /// <summary>How a module change goes.</summary>
    public enum ModuleChangeKind : byte
    {
        /// <summary>Opens the module over the one on top, which is suspended and stays loaded.</summary>
        Over,

        /// <summary>Closes the module on top and resumes the one under it.</summary>
        Close,

        /// <summary>Exits the module on top and enters another in its place.</summary>
        Replace,

        /// <summary>Exits every module, top first, and enters another alone.</summary>
        Clear
    }

    /// <summary>
    /// How a module is entered, for <see cref="IModule.OnEnterAsync" />. A module may define more of its own.
    /// </summary>
    public static class ModuleEntries
    {
        /// <summary>Entered afresh, keeping nothing from an earlier visit.</summary>
        public const byte NEW = 0;

        /// <summary>Entered again after a module that replaced it, carrying on from what it kept when it left.</summary>
        public const byte RETURN = 1;
    }

    /// <summary>
    /// A change a router decides on, which Core carries out under a cover: the transitions that cover and reveal the
    /// screen, by the hash of their names in the game's transition database, <c>0</c> for its defaults.
    /// </summary>
    public readonly struct ModuleChange
    {
        public ModuleChangeKind Kind     { get; }
        public byte             ModuleId { get; }
        public byte             Entry    { get; }
        public ulong            Cover    { get; }
        public ulong            Reveal   { get; }

        private ModuleChange(ModuleChangeKind kind, byte moduleId, byte entry, ulong cover, ulong reveal)
        {
            Kind     = kind;
            ModuleId = moduleId;
            Entry    = entry;
            Cover    = cover;
            Reveal   = reveal;
        }

        public static ModuleChange Over(byte moduleId, byte entry = ModuleEntries.NEW)
        {
            return new ModuleChange(ModuleChangeKind.Over, moduleId, entry, 0, 0);
        }

        public static ModuleChange Close()
        {
            return new ModuleChange(ModuleChangeKind.Close, 0, ModuleEntries.NEW, 0, 0);
        }

        public static ModuleChange Replace(byte moduleId, byte entry = ModuleEntries.NEW)
        {
            return new ModuleChange(ModuleChangeKind.Replace, moduleId, entry, 0, 0);
        }

        public static ModuleChange Clear(byte moduleId, byte entry = ModuleEntries.NEW)
        {
            return new ModuleChange(ModuleChangeKind.Clear, moduleId, entry, 0, 0);
        }

        /// <summary>The same change, covered and revealed by these transitions, <c>0</c> for the defaults.</summary>
        public ModuleChange WithTransitions(ulong cover, ulong reveal)
        {
            ModuleChange change = new ModuleChange(Kind, ModuleId, Entry, cover, reveal);

            return change;
        }

        public override string ToString()
        {
            string text = Kind == ModuleChangeKind.Close ? Kind.ToString() : $"{Kind} [{ModuleId}] entry [{Entry}]";

            return text;
        }
    }

    /// <summary>
    /// Decides each change between modules. A module finishing tells Core what happened, as an outcome its own shared
    /// types define; Core asks the router what follows, and carries it out. A game binds one in its global installer.
    /// </summary>
    public interface IModuleRouter
    {
        /// <summary>
        /// What follows <paramref name="outcome" /> from the module on top, <paramref name="moduleId" />. Throws for an
        /// outcome it has no route for.
        /// </summary>
        ModuleChange Route(byte moduleId, byte outcome);
    }
}