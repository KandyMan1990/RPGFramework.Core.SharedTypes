# RPGFramework.Core.SharedTypes

The smallest contracts every RPG Framework module shares, kept apart from Core so a module's own shared-types package
can depend on them without depending on Core.

Requires Unity 6000.6 or newer, and depends on nothing.

- **`IModule`**: what Core switches between. A module is entered and exited asynchronously, with `OnEnterAsync` and
  `OnExitAsync`, so it can fade, load and unload as it goes, and suspended and resumed, with `OnSuspendAsync` and
  `OnResumeAsync`, while another opens over it. `OnEnterAsync` is told how it is entered: `ModuleEntries.NEW`, or
  `RETURN` after a module that replaced it. `OnRevealed` says it is on screen, once the reveal of its entry or its
  resumption has played, for a module that takes no input, or starts nothing the player should see, until then.
- **`IModuleRouter`** and **`ModuleChange`**: what decides each change between modules, and the change it decides —
  open a module over the one on top, close the top, replace it, or clear them all.
- **`MemoryBank`**: where a variable lives.
  - **Persistent** is written to the save file.
  - **Session** survives a change of module but not a restart, and is reset whenever a save begins, whether a new game
    or a load.
  - **Temp** is a running script's own scratch space, zeroed when the script starts and shared with nothing.
  - **Settings** holds the player's settings — the language, the volumes, the message speeds and any of the game's
    own — written to the settings file rather than a save, as they belong to whoever is at the machine, not to a
    playthrough. They are read as the game starts and kept through every new game and load, and scripts cannot use
    them.
