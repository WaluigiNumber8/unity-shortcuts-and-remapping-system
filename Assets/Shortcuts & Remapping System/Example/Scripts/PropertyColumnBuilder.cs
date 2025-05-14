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
            parent.ReleaseAllProperties();
            b.BuildInputBinding(InputSystem.Instance.Shortcuts.ChangeBackground.Action, InputDeviceType.Keyboard, parent);
            b.BuildInputBinding(InputSystem.Instance.Shortcuts.ChangeColor.Action, InputDeviceType.Keyboard, parent);
        }
    }
}
