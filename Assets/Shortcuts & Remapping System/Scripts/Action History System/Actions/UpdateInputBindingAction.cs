using System;
using RedRats.ShortcutSystem.Remapping;

namespace RedRats.ActionHistory
{
    public class UpdateInputBindingAction : ActionBase<InputBindingCombination>
    {
        private readonly InputBindingReader reader;
        private readonly InputBindingCombination value;
        private readonly InputBindingCombination lastValue;
        
        public UpdateInputBindingAction(InputBindingReader reader, InputBindingCombination value, InputBindingCombination lastValue,  Action<InputBindingCombination> fallback) : base(fallback)
        {
            this.reader = reader;
            this.value = value;
            this.lastValue = lastValue;
        }
        
        protected override void ExecuteSelf() => reader.Rebind(value);

        protected override void UndoSelf() => reader.Rebind(lastValue);
        
        public override bool NothingChanged() => value == lastValue;
        public override string ToString() => $"{reader.name}: {lastValue} -> {value}";
        public override object AffectedConstruct { get => reader; }
        public override InputBindingCombination Value { get => value; }
        public override InputBindingCombination LastValue { get => lastValue; }
    }
}