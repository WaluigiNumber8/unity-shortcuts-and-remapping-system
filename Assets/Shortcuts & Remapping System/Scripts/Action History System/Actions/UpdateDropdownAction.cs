using System;
using RedRats.UI.Properties;
using UnityEngine;

namespace RedRats.ActionHistory
{
    /// <summary>
    /// An action that updates a dropdown.
    /// </summary>
    public class UpdateDropdownAction : ActionBase<int>
    {
        private readonly IPDropdown dropdown;
        private readonly int value;
        private readonly int lastValue;

        public UpdateDropdownAction(IPDropdown dropdown, int value, int lastValue, Action<int> fallback) : base(fallback)
        {
            this.dropdown = dropdown;
            this.value = value;
            this.lastValue = lastValue;
        }

        protected override void ExecuteSelf() => dropdown.UpdateValueWithoutNotify(value);

        protected override void UndoSelf() => dropdown.UpdateValueWithoutNotify(lastValue);

        public override bool NothingChanged() => value == lastValue;
        
        public override object AffectedConstruct
        {
            get
            {
                try { return dropdown?.gameObject; }
                catch (MissingReferenceException) { return null; }
            }
        }

        public override int Value { get => value; }
        public override int LastValue { get => lastValue; }

        public override string ToString() => $"{dropdown.name}: {lastValue} -> {value}";
    }
}