using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace RedRats.Input
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
                    action.AddBinding(compositeBindings[0].effectivePath, groups: compositeBindings[0].groups, interactions: compositeBindings[0].interactions, processors: compositeBindings[0].processors);
                    break;
                case 2:
                    action.AddCompositeBinding("OneModifier", interactions: compositeBindings[1].interactions, processors: compositeBindings[1].processors)
                          .With("Modifier", compositeBindings[1].effectivePath, processors: compositeBindings[1].processors, groups: compositeBindings[1].groups)
                          .With("Binding", compositeBindings[0].effectivePath, groups: compositeBindings[0].groups, processors: compositeBindings[0].processors);
                    break;
                case 3:
                    action.AddCompositeBinding("TwoModifiers", interactions: compositeBindings[2].interactions, processors: compositeBindings[2].processors)
                          .With("Modifier1", compositeBindings[2].effectivePath, processors: compositeBindings[2].processors, groups: compositeBindings[2].groups)
                          .With("Modifier2", compositeBindings[1].effectivePath, processors: compositeBindings[1].processors, groups: compositeBindings[1].groups)
                          .With("Binding", compositeBindings[0].effectivePath, groups: compositeBindings[0].groups, processors: compositeBindings[0].processors);
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