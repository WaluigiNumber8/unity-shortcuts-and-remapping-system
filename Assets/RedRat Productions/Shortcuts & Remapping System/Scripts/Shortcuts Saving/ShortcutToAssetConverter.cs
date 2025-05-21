using RedRats.Core;
using RedRats.Input;
using static RedRats.Input.InputSystemUtils;

namespace RedRats.ShortcutSystem.Saving
{
    /// <summary>
    /// Converts a <see cref="TestInputActions"/> to an <see cref="ShortcutBindingsAsset"/>.
    /// </summary>
    public static class ShortcutToAssetConverter
    {
        private static readonly InputSystem input = InputSystem.Instance;

        /// <summary>
        /// Builds a <see cref="ShortcutBindingsAsset"/> from the current <see cref="TestInputActions"/> and returns it.
        /// </summary>
        public static ShortcutBindingsAsset Get()
        {
            ShortcutBindingData keyboard = new();
            ShortcutBindingData keyboardAlt = new();
            ShortcutBindingData gamepad = new();
            ShortcutBindingData gamepadAlt = new();
            
            keyboard.ChangeBackground = GetPath(input.Shortcuts.ChangeBackground.Action, InputDeviceType.Keyboard);
            keyboardAlt.ChangeBackground = GetPath(input.Shortcuts.ChangeBackground.Action, InputDeviceType.Keyboard, true);
            gamepad.ChangeBackground = GetPath(input.Shortcuts.ChangeBackground.Action, InputDeviceType.Gamepad);
            gamepadAlt.ChangeBackground = GetPath(input.Shortcuts.ChangeBackground.Action, InputDeviceType.Gamepad, true);
            keyboard.ChangeColor = GetPath(input.Shortcuts.ChangeColor.Action, InputDeviceType.Keyboard);
            keyboardAlt.ChangeColor = GetPath(input.Shortcuts.ChangeColor.Action, InputDeviceType.Keyboard, true);
            gamepad.ChangeColor = GetPath(input.Shortcuts.ChangeColor.Action, InputDeviceType.Gamepad);
            gamepadAlt.ChangeColor = GetPath(input.Shortcuts.ChangeColor.Action, InputDeviceType.Gamepad, true);
            keyboard.BurstParticle = GetPath(input.Shortcuts.BurstParticle.Action, InputDeviceType.Keyboard);
            keyboardAlt.BurstParticle = GetPath(input.Shortcuts.BurstParticle.Action, InputDeviceType.Keyboard, true);
            gamepad.BurstParticle = GetPath(input.Shortcuts.BurstParticle.Action, InputDeviceType.Gamepad);
            gamepadAlt.BurstParticle = GetPath(input.Shortcuts.BurstParticle.Action, InputDeviceType.Gamepad, true);
            
            return new ShortcutBindingsAsset.Builder()
                .WithKeyboard(keyboard)
                .WithKeyboardAlt(keyboardAlt)
                .WithGamepad(gamepad)
                .WithGamepadAlt(gamepadAlt)
                .Build();
        }

        /// <summary>
        /// Loads <see cref="ShortcutBindingsAsset"/> paths into the current <see cref="TestInputActions"/>.
        /// </summary>
        /// <param name="asset">The asset to load the paths from.</param>
        public static void Load(ShortcutBindingsAsset asset)
        {
            Preconditions.IsNotNull(asset, nameof(asset));
            if (asset.Keyboard.ChangeBackground == null || asset.Gamepad.ChangeBackground == null) return;

            ApplyBindingOverride(asset.Keyboard.ChangeBackground, input.Shortcuts.ChangeBackground.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ChangeBackground, input.Shortcuts.ChangeBackground.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ChangeBackground, input.Shortcuts.ChangeBackground.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ChangeBackground, input.Shortcuts.ChangeBackground.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.ChangeColor, input.Shortcuts.ChangeColor.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ChangeColor, input.Shortcuts.ChangeColor.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ChangeColor, input.Shortcuts.ChangeColor.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ChangeColor, input.Shortcuts.ChangeColor.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.BurstParticle, input.Shortcuts.BurstParticle.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.BurstParticle, input.Shortcuts.BurstParticle.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.BurstParticle, input.Shortcuts.BurstParticle.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.BurstParticle, input.Shortcuts.BurstParticle.Action, InputDeviceType.Gamepad, true);
        }
    }
}