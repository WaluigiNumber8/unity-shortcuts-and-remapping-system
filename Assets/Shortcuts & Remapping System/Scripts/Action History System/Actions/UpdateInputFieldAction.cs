using System;
using RedRats.UI.Properties;
using UnityEngine;

namespace RedRats.ActionHistory
{
    /// <summary>
    /// An action that updates an input field.
    /// </summary>
    public class UpdateInputFieldAction : ActionBase<string>
    {
        private readonly IPInputField inputField;
        private readonly string value;
        private readonly string lastValue;

        public UpdateInputFieldAction(IPInputField inputField, string value, string lastValue, Action<string> fallback) : base(fallback)
        {
            this.inputField = inputField;
            this.value = value;
            this.lastValue = lastValue;
        }

        protected override void ExecuteSelf() => inputField.UpdateValueWithoutNotify(value);

        protected override void UndoSelf() => inputField.UpdateValueWithoutNotify(lastValue);

        public override bool NothingChanged() => value == lastValue;
        
        public override object AffectedConstruct
        {
            get
            {
                try { return inputField?.gameObject; }
                catch (MissingReferenceException) { return null; }
            }
        }

        public override string Value { get => value; }
        public override string LastValue { get => lastValue; }

        public override string ToString() => $"{inputField.name}: {lastValue} -> {value}";
    }
}