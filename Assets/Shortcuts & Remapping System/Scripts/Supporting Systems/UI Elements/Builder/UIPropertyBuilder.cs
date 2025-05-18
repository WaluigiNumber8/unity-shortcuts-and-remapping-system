using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.ShortcutSystem.Input;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// Builds Properties.
    /// </summary>
    public class UIPropertyBuilder : MonoSingleton<UIPropertyBuilder>
    {
        [Header("Property prefabs")] 
        [SerializeField] private IPInputBinding inputBindingProperty;
        
        [Header("Other")] 
        [SerializeField] private Transform poolParent;

        private UIPropertyPool<IPInputBinding> inputBindingPool;
        
        protected override void Awake()
        {
            base.Awake();
            inputBindingPool = new UIPropertyPool<IPInputBinding>(inputBindingProperty, poolParent, 50, 150);
        }

        #region Properties

        /// <summary>
        /// Builds the Input Binding property.
        /// </summary>
        /// <param name="action">The input action the binding is assigned to.</param>
        /// <param name="device">To which device is the property limited to.</param>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="useAlt">The action can have an alternative input.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable</param>
        public void BuildInputBinding(InputAction action, InputDeviceType device, Transform parent, bool useAlt = true, bool isDisabled = false)
        {
            int bindingIndex = InputSystemUtils.GetBindingIndexByDevice(action, device);
            int bindingIndexAlt = useAlt ? InputSystemUtils.GetBindingIndexByDevice(action, device, true) : -1;
            
            //If action is composite, spawn for each binding
            if (action.bindings[bindingIndex].isPartOfComposite)
            {
                //If is a modifier composite, spawn only one
                if (action.bindings[bindingIndex - 1].IsTwoOptionalModifiersComposite())
                {
                    ConstructInputBinding(action.name, true);
                    return;
                }
                //Any other type spawn for each binding
                while (bindingIndex < action.bindings.Count && action.bindings[bindingIndex].isPartOfComposite)
                {
                    string title = $"{action.name}{action.bindings[bindingIndex].name.Capitalize()}";
                    ConstructInputBinding(title);
                    bindingIndex++;
                    bindingIndexAlt++;
                }
                return;
            }
            ConstructInputBinding(action.name);
            
            void ConstructInputBinding(string title, bool useModifiers = false)
            {
                IPInputBinding inputBinding = inputBindingPool.Get(parent);
                inputBinding.name = $"{title} InputBinding";
                inputBinding.Construct(title, action, 
                                       (useModifiers) ? bindingIndex + 2 : bindingIndex,
                                       (useModifiers) ? bindingIndex : -1,
                                       (useModifiers) ? bindingIndex + 1 : -1,
                                       (useModifiers) ? bindingIndexAlt + 2 : bindingIndexAlt, 
                                       (useModifiers) ? bindingIndexAlt : -1, 
                                       (useModifiers) ? bindingIndexAlt + 1 : -1);
                inputBinding.SetDisabled(isDisabled);
            }
        }

        #endregion
    }
}