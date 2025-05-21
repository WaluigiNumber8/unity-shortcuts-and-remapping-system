using System;
using UnityEngine.InputSystem;

namespace RedRats.Input
{
    /// <summary>
    /// Contains events for a button.
    /// </summary>
    public class InputButton : IInputType
    {
        public event Action OnPress;
        public event Action OnRelease;

        private readonly InputAction action;
        private bool isHeld;

        public InputButton(InputAction action) => this.action = action;

        public void Enable()
        {
            action.performed += CallPress;
            action.canceled += CallRelease;
        }
        
        public void Disable()
        {
            if (isHeld) OnRelease?.Invoke();
            action.performed -= CallPress;
            action.canceled -= CallRelease;
        }


        private void CallPress(InputAction.CallbackContext ctx)
        {
            isHeld = true;
            OnPress?.Invoke();
        }

        private void CallRelease(InputAction.CallbackContext ctx)
        {
            isHeld = false;
            OnRelease?.Invoke();
        }

        public override string ToString() => action.name;

        public bool IsHeld { get => isHeld; }
        public InputAction Action { get => action; }
    }
}