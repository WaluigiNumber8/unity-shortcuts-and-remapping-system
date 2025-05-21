using System;

namespace RedRats.ShortcutSystem.Linking
{
    /// <summary>
    /// All different types of shortcut action maps.
    /// </summary>
    [Flags]
    public enum ShortcutActionMapType
    {
        General = 1 << 0,
    }
}