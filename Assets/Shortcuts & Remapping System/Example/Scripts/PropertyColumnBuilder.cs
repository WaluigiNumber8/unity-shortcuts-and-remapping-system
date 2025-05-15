using RedRats.ShortcutSystem.Input;
using RedRats.UI.Properties;
using UnityEngine;

namespace RedRats.Example.Core
{
    public class PropertyColumnBuilder
    {
        private readonly UIPropertyBuilder b = UIPropertyBuilder.Instance;
        private readonly Transform parent;
        
        public PropertyColumnBuilder(Transform parent)
        {
            this.parent = parent;
        }
        
        public void Build()
        {
            InputSystem input = InputSystem.Instance;
            
            parent.ReleaseAllProperties();
            b.BuildInputBinding(input.Shortcuts.ChangeBackground.Action, InputDeviceType.Keyboard, parent);
            b.BuildInputBinding(input.Shortcuts.ChangeColor.Action, InputDeviceType.Keyboard, parent);
            b.BuildInputBinding(input.Shortcuts.BurstParticle.Action, InputDeviceType.Keyboard, parent);
        }
    }
}
