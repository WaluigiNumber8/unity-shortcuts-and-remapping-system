using RedRats.Input;
using RedRats.UI.Properties;
using UnityEngine;

namespace RedRats.Example.Core
{
    public class PropertyColumnBuilder
    {
        private readonly UIPropertyBuilderForInputBindingOnly b = UIPropertyBuilderForInputBindingOnly.Instance;
        private readonly InputSystem input = InputSystem.Instance;
        
        private readonly Transform parent;

        public PropertyColumnBuilder(Transform parent)
        {
            this.parent = parent;
        }
        
        public void Build()
        {
            //Destroy all child objects under parent
            Clear();
            
            b.BuildInputBinding(input.Shortcuts.ChangeBackground.Action, InputDeviceType.Keyboard, parent);
            b.BuildInputBinding(input.Shortcuts.ChangeColor.Action, InputDeviceType.Keyboard, parent);
            b.BuildInputBinding(input.Shortcuts.BurstParticle.Action, InputDeviceType.Keyboard, parent);
        }

        private void Clear()
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}
