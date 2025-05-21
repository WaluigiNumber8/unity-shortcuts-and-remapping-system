using System;
using RedRats.Systems.FileSystem;

namespace RedRats.ShortcutSystem.Saving
{
    /// <summary>
    /// Serialized form of the <see cref="ShortcutBindingsAsset"/>.
    /// </summary>
    [Serializable]
    public class JSONShortcutBindingsAsset : IEncodedObject<ShortcutBindingsAsset>
    {
        public ShortcutBindingData Keyboard, KeyboardAlt, Gamepad, GamepadAlt;
        
        public JSONShortcutBindingsAsset(ShortcutBindingsAsset shortcutBindings)
        {
            Keyboard = shortcutBindings.Keyboard;
            KeyboardAlt = shortcutBindings.KeyboardAlt;
            Gamepad = shortcutBindings.Gamepad;
            GamepadAlt = shortcutBindings.GamepadAlt;
        }
        
        public ShortcutBindingsAsset Decode()
        {
            return new ShortcutBindingsAsset.Builder() 
                .WithKeyboard(Keyboard)
                .WithKeyboardAlt(KeyboardAlt)
                .WithGamepad(Gamepad)
                .WithGamepadAlt(GamepadAlt)
                .Build();
        }
    }
}