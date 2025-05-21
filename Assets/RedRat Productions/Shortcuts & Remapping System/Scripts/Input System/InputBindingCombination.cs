using System;
using UnityEngine.InputSystem;

namespace RedRats.ShortcutSystem.Remapping
{
    /// <summary>
    /// Contains a binding for an action made up of a button and 2 optional modifiers.
    /// </summary>
    public class InputBindingCombination : IEquatable<InputBindingCombination>
    {
        private InputBinding modifier1;
        private InputBinding modifier2;
        private InputBinding button;
        private string displayString;

        private InputBindingCombination() {}

        public bool HasSameInputs(InputBindingCombination other) => modifier1.effectivePath == other.modifier1.effectivePath &&
                                                                    modifier2.effectivePath == other.modifier2.effectivePath &&
                                                                    button.effectivePath == other.button.effectivePath;
            
        #region Equals
        public override bool Equals(object obj)
        {
            if (obj is not InputBindingCombination combo) return false;
            if (modifier1 != combo.modifier1) return false;
            if (modifier2 != combo.modifier2) return false;
            return button == combo.button;
        }

        public bool Equals(InputBindingCombination other) => Equals((object)other);
        public override int GetHashCode() => HashCode.Combine(modifier1, modifier2, button);
        public static bool operator ==(InputBindingCombination left, InputBindingCombination right) => left is null && right is null || left is not null && right is not null && left.Equals(right);

        public static bool operator !=(InputBindingCombination left, InputBindingCombination right) => !(left == right);
        #endregion
        
        public override string ToString() => DisplayString;

        public InputBinding Modifier1 { get => modifier1; }
        public InputBinding Modifier2 { get => modifier2; }
        public InputBinding Button { get => button; }
        public string DisplayString { get => displayString; }

        public class Builder
        {
            private readonly InputBindingCombination combo = new();

            public Builder From(InputBindingCombination source)
            {
                combo.modifier1 = source.modifier1;
                combo.modifier2 = source.modifier2;
                combo.button = source.button;
                return this;
            }

            public Builder WithLinkedBindings(InputBinding buttonBinding, InputBinding modifier1Binding, InputBinding modifier2Binding)
            {
                combo.modifier1 = modifier1Binding;
                combo.modifier2 = modifier2Binding;
                combo.button = buttonBinding;
                return this;
            }

            public Builder WithLinkedBindings(InputAction action, int buttonIndex, int modifier1Index = -1, int modifier2Index = -1)
            {
                combo.modifier1 = (modifier1Index == -1) ? new InputBinding("") : action.bindings[modifier1Index];
                combo.modifier2 = (modifier2Index == -1) ? new InputBinding("") : action.bindings[modifier2Index];
                combo.button = action.bindings[buttonIndex];
                return this;
            }
            
            public Builder WithModifier1(string bindingPath)
            {
                combo.modifier1.overridePath = bindingPath;
                return this;
            }
            
            public Builder WithModifier2(string bindingPath)
            {
                combo.modifier2.overridePath = bindingPath;
                return this;
            }
            
            public Builder WithButton(string bindingPath)
            {
                combo.button.overridePath = bindingPath;
                return this;
            }

            public Builder WithEmptyModifier1() => WithModifier1("");
            public Builder WithEmptyModifier2() => WithModifier2("");
            public Builder WithEmptyButton() => WithButton("");

            public Builder ClearPaths() => WithEmptyModifier1().WithEmptyModifier2().WithEmptyButton();
            
            public InputBindingCombination Build()
            {
                combo.displayString = BuildDisplayString();
                return combo;
            }

            public InputBindingCombination AsEmpty() => WithEmptyModifier1().WithEmptyModifier2().WithEmptyButton().Build();
            
            private string BuildDisplayString()
            {
                string plus1 = (combo.modifier1.effectivePath != "") ? "+" : "";
                string plus2 = (combo.modifier2.effectivePath != "") ? "+" : "";
                return $"{combo.modifier1.ToDisplayString()}{plus1}{combo.modifier2.ToDisplayString()}{plus2}{combo.button.ToDisplayString()}";
            }
        }

    }
}