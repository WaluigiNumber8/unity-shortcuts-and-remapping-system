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
            b.BuildDropdown("Border", new[] { "Basic", "Fancy", "None" }, 0, parent, GeneralActions.Instance.ChangeBorder);
            b.BuildSlider("Color", 0, 365, 128, parent, v => GeneralActions.Instance.ChangeGridColor((int)v));
            b.BuildToggle("Visibility", true, parent, GeneralActions.Instance.ToggleGridVisibility);
        }
    }
}
