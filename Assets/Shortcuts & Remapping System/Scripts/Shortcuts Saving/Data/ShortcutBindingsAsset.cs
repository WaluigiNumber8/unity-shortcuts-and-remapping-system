namespace RedRats.ShortcutSystem.Saving
{
    public class ShortcutBindingsAsset : IDataAsset
    {
        private ShortcutBindingData keyboard;
        private ShortcutBindingData keyboardAlt;
        private ShortcutBindingData gamepad;
        private ShortcutBindingData gamepadAlt;
        
        private ShortcutBindingsAsset() { }
        
        public override string ToString() => $"{Title}";
        
        public string ID { get => "ZZ"; }
        public string Title { get => "Shortcuts"; }
        public ShortcutBindingData Keyboard { get => keyboard; }
        public ShortcutBindingData KeyboardAlt { get => keyboardAlt; }
        public ShortcutBindingData Gamepad { get => gamepad; }
        public ShortcutBindingData GamepadAlt { get => gamepadAlt; }
        
        public class Builder
        {
            private readonly ShortcutBindingsAsset Asset = new();
            
            public Builder()
            {
                Asset.keyboard = new ShortcutBindingData();
                Asset.keyboardAlt = new ShortcutBindingData();
                Asset.gamepad = new ShortcutBindingData();
                Asset.gamepadAlt = new ShortcutBindingData();
            }
        
            public Builder WithKeyboard(ShortcutBindingData data)
            {
                Asset.keyboard = data;
                return this;
            }
            
            public Builder WithKeyboardAlt(ShortcutBindingData data)
            {
                Asset.keyboardAlt = data;
                return this;
            }
            
            public Builder WithGamepad(ShortcutBindingData data)
            {
                Asset.gamepad = data;
                return this;
            }
            
            public Builder WithGamepadAlt(ShortcutBindingData data)
            {
                Asset.gamepadAlt = data;
                return this;
            }
            
            public ShortcutBindingsAsset Build() => Asset;

            public Builder AsCopy(ShortcutBindingsAsset newShortcutBindings)
            {
                return WithKeyboard(newShortcutBindings.Keyboard)
                      .WithKeyboardAlt(newShortcutBindings.KeyboardAlt)
                      .WithGamepad(newShortcutBindings.Gamepad)
                      .WithGamepadAlt(newShortcutBindings.GamepadAlt);
            }
        }
    }
}