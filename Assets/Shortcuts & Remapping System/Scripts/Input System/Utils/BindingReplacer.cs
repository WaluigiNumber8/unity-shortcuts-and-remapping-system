using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace RedRats.ShortcutSystem.Input
{
    /// <summary>
    /// Replaces TwoOptionalModifierComposites with built-in modifiers.
    /// </summary>
    public static class BindingReplacer
    {
        private static readonly IList<InputBinding> compositeBindings = new List<InputBinding>();
        private static bool isInsideComposite;

        /// <summary>
        /// Replace TwoOptionalModifierComposites with built-in modifiers.
        /// </summary>
        /// <param name="actions">The input action asset.</param>
        public static void ReplaceBindings(InputActionAsset actions)
        {
            foreach (InputActionMap map in actions.actionMaps)
            {
                foreach (InputAction action in map.actions)
                {
                    ProcessAction(action);
                }
            }
        }

        private static void ProcessAction(InputAction action)
        {
            isInsideComposite = false;
            compositeBindings.Clear();

            for (int i = action.bindings.Count - 1; i >= 0; i--)
            {
                InputBinding binding = action.bindings[i];
                
                if (binding.isComposite)
                {
                    isInsideComposite = false;
                    if (!binding.IsTwoOptionalModifiersComposite()) return;
                    ProcessCompositeHead(action, i);
                    continue;
                }
                if (binding.isPartOfComposite)
                {
                    ProcessCompositePart(binding);
                    continue;
                }
            }
        }

        private static void ProcessCompositeHead(InputAction action, int bindingIndex)
        {
            switch (compositeBindings.Count)
            {
                case 1:
                    action.AddBinding(compositeBindings[0].effectivePath);
                    break;
                case 2:
                    action.AddCompositeBinding("OneModifier").With("Modifier", compositeBindings[1].effectivePath).With("Binding", compositeBindings[0].effectivePath);
                    break;
                case 3:
                    action.AddCompositeBinding("TwoModifiers").With("Modifier1", compositeBindings[2].effectivePath).With("Modifier2", compositeBindings[1].effectivePath).With("Binding", compositeBindings[0].effectivePath);
                    break;
            }
            action.ChangeBinding(bindingIndex).Erase();
        }
        
        private static void ProcessCompositePart(InputBinding binding)
        {
            if (isInsideComposite)
            {
                if (!string.IsNullOrEmpty(binding.effectivePath)) compositeBindings.Add(binding);
                return;
            }
                            
            isInsideComposite = true;
            compositeBindings.Clear();
            if (!string.IsNullOrEmpty(binding.effectivePath)) compositeBindings.Add(binding);
        }
    }
}