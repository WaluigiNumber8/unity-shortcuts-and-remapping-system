namespace RedRats.Input
{
    public class InputProfileUI : InputProfileBase
    {
        private TestInputActions.UIActions map;

        private readonly InputVector2 navigate;
        private readonly InputVector2 pointerPosition;

        private readonly InputButton select;
        private readonly InputButton cancel;
        private readonly InputButton contextSelect;

        public InputProfileUI(TestInputActions input) : base(input)
        {
            map = input.UI;

            navigate = new InputVector2(map.Navigate);
            pointerPosition = new InputVector2(map.Point);
            select = new InputButton(map.Click);
            cancel = new InputButton(map.Cancel);
            contextSelect = new InputButton(map.RightClick);
        }
        
        protected override void WhenEnabled()
        {
            map.Enable();
            
            navigate.Enable();
            pointerPosition.Enable();
            select.Enable();
            cancel.Enable();
            contextSelect.Enable();
        }

        protected override void WhenDisabled()
        {
            navigate.Disable();
            pointerPosition.Disable();
            select.Disable();
            cancel.Disable();
            contextSelect.Disable();
            
            map.Disable();
        }

        public override bool IsMapEnabled { get => map.enabled; }

        public InputVector2 Navigate { get => navigate; }
        public InputVector2 PointerPosition { get => pointerPosition; }
        public InputButton Select { get => select; }
        public InputButton Cancel { get => cancel; }
        public InputButton ContextSelect { get => contextSelect; }
    }
}