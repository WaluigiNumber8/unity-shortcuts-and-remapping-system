namespace RedRats.Input
{
    /// <summary>
    /// Fires events for the general shortcuts Action Map.
    /// </summary>
    public class InputProfileShortcuts : InputProfileBase
    {
        private ShortcutTestInputActions.ShortcutsActions generalMap;

        private readonly InputButton changeBackground;
        private readonly InputButton changeColor;
        private readonly InputButton burstParticle;

        public InputProfileShortcuts(ShortcutTestInputActions input) : base(input)
        {
            generalMap = input.Shortcuts;
            
            changeBackground = new InputButton(generalMap.ChangeBackground);
            changeColor = new InputButton(generalMap.ChangeColor);
            burstParticle = new InputButton(generalMap.BurstParticle);
        }

        protected override void WhenEnabled()
        {
            generalMap.Enable();
            
            changeBackground.Enable();
            changeColor.Enable();
            burstParticle.Enable();
        }

        protected override void WhenDisabled()
        {
            changeBackground.Disable();
            changeColor.Disable();
            burstParticle.Disable();
            
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
        public InputButton BurstParticle { get => burstParticle; }
    }
}