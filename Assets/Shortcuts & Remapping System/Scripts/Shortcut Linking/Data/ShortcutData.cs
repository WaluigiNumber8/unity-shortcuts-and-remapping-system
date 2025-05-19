using System;
using RedRats.Core;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace RedRats.ShortcutSystem.Linking
{
    [Serializable]
    public class ShortcutData
    {
        private string GroupTitle() => (trigger == null) ? "None" : $"{trigger.action.actionMap.name}/{trigger.action.name}";

        public InputActionReference trigger;
        public UnityEvent action;

        private InputAction inputAction;

        public ShortcutData(InputAction trigger, UnityEvent action)
        {
            this.trigger = InputActionReference.Create(trigger);
            this.action = action;
            RefreshInput();
        }

        public void RefreshInput()
        {
            Preconditions.IsNotNull(trigger, "Trigger Action");
            inputAction = RedRats.Input.InputSystem.Instance.GetAction(trigger);
        }

        public void Link()
        {
            if (inputAction == null) return;
            inputAction.performed += Activate;
        }

        public void Unlink()
        {
            if (inputAction == null) return;
            inputAction.performed -= Activate;
        }

        private void Activate(InputAction.CallbackContext ctx) => action.Invoke();

        public override string ToString() => $"{inputAction.name} -> {action.GetPersistentMethodName(0)}()";
    }
}