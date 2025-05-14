using System;
using RedRats.UI.Properties;
using UnityEngine;

namespace RedRats.ActionHistory
{
    /// <summary>
    /// An action that updates a toggle.
    /// </summary>
    public class UpdateToggleAction : ActionBase<bool>
    {
        private readonly IPToggle toggle;
        private readonly bool value;
        
        public UpdateToggleAction(IPToggle toggle, bool value, Action<bool> fallback) : base(fallback)
        {
            this.toggle = toggle;
            this.value = value;
        }
        
        protected override void ExecuteSelf() => toggle.UpdateValueWithoutNotify(value);

        protected override void UndoSelf() => toggle.UpdateValueWithoutNotify(!value);

        public override bool NothingChanged() => false;
        
        public override object AffectedConstruct
        {
            get
            {
                try { return toggle?.gameObject; }
                catch (MissingReferenceException) { return null; }
            }
        }

        public override bool Value { get => value; }
        public override bool LastValue { get => !value; }

        public override string ToString() => $"{toggle.name}: {!value} -> {value}";
    }
}