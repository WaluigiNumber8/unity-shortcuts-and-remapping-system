namespace RedRats.ShortcutSystem.Input
{
    /// <summary>
    /// Fires events for the general shortcuts Action Map.
    /// </summary>
    public class InputProfileShortcuts : InputProfileBase
    {
        private TestInputActions.ShortcutsActions generalMap;

        private readonly InputButton changeBackground, changeColor;
        
        public InputProfileShortcuts(TestInputActions input) : base(input)
        {
            generalMap = input.Shortcuts;
            
            changeBackground = new InputButton(generalMap.ChangeBackground);
            changeColor = new InputButton(generalMap.ChangeColor);
        }

        protected override void WhenEnabled()
        {
            generalMap.Enable();
            
            changeBackground.Enable();
            changeColor.Enable();
        }

        protected override void WhenDisabled()
        {
            changeBackground.Disable();
            changeColor.Disable();
            
            generalMap.Disable();
            generalMap.Disable();
        }

        public override bool IsMapEnabled { get => generalMap.enabled; }

        public void ActivateGeneralMap(bool value)
        {
            if (value) generalMap.Enable();
            else generalMap.Disable();
        }
        
        public InputButton ChangeBackground { get => changeBackground; }
        public InputButton ChangeColor { get => changeColor; }
    }
}