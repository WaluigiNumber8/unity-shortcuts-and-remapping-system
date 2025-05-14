using System;
using System.Collections.Generic;
using RedRats.Core;
using TMPro;
using UnityEngine;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// Builds Properties.
    /// </summary>
    public class UIPropertyBuilder : MonoSingleton<UIPropertyBuilder>
    {
        [Header("Property prefabs")] 
        [SerializeField] private IPHeader headerProperty;
        [SerializeField] private IPInputField inputFieldProperty;
        [SerializeField] private IPDropdown dropdownProperty;
        [SerializeField] private IPToggle toggleProperty;
        [SerializeField] private IPSlider sliderProperty;
        
        [Header("Other")]
        [SerializeField] private Transform poolParent;

        private UIPropertyPool<IPHeader> headerPool;
        private UIPropertyPool<IPInputField> inputFieldPool;
        private UIPropertyPool<IPDropdown> dropdownPool;
        private UIPropertyPool<IPToggle> togglePool;
        private UIPropertyPool<IPSlider> sliderPool;
        
        private UIPropertyPool<IPContentBlock> contentBlockHorizontalPool, contentBlockVerticalPool, contentBlockColumn2Pool;

        protected override void Awake()
        {
            base.Awake();
            headerPool = new UIPropertyPool<IPHeader>(headerProperty, poolParent, 25, 100);
            inputFieldPool = new UIPropertyPool<IPInputField>(inputFieldProperty, poolParent, 50, 150);
            dropdownPool = new UIPropertyPool<IPDropdown>(dropdownProperty, poolParent, 50, 150);
            togglePool = new UIPropertyPool<IPToggle>(toggleProperty, poolParent, 50, 150);
            sliderPool = new UIPropertyPool<IPSlider>(sliderProperty, poolParent, 50, 150);
        }

        #region Properties

        /// <summary>
        /// Builds the Header Property.
        /// </summary>
        /// <param name="headerText">The text of the header.</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <returns>The property itself.</returns>
        public void BuildHeader(string headerText, Transform parent)
        {
            IPHeader header = headerPool.Get(parent);
            header.name = $"{headerText} Header";
            header.Construct(headerText);
        }

        /// <summary>
        /// Builds the Input Field Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenFinishEditing">Is called when the user finishes editing the InputField.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <param name="characterValidation">The validation to use for inputted symbols.</param>
        /// <param name="minLimit">The minimum allowed value (when InputField deals with numbers).</param>
        /// <param name="maxLimit">The maximum allowed value (when InputField deals with numbers).</param>
        /// <returns>The property itself.</returns>
        public void BuildInputField(string title, string value, Transform parent, Action<string> whenFinishEditing, bool isDisabled = false, TMP_InputField.CharacterValidation characterValidation = TMP_InputField.CharacterValidation.Regex, float minLimit = float.MinValue, float maxLimit = float.MaxValue)
        {
            IPInputField inputField = inputFieldPool.Get(parent);
            inputField.name = $"{title} InputField";
            inputField.Construct(title, value, whenFinishEditing, characterValidation, minLimit, maxLimit);
            inputField.SetDisabled(isDisabled);
        }

        /// <summary>
        /// Builds the Toggle Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenValueChange">What happens when the property changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <returns>The property itself.</returns>
        public void BuildToggle(string title, bool value, Transform parent, Action<bool> whenValueChange, bool isDisabled = false)
        {
            IPToggle toggle = togglePool.Get(parent);
            toggle.name = $"{title} Toggle";
            toggle.Construct(title, value, whenValueChange);
            toggle.SetDisabled(isDisabled);
        }

        /// <summary>
        /// Builds the Dropdown Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="options">The list of options the dropdown will be filled with.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenValueChange">What happens when the property changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <returns>The property itself.</returns>
        public void BuildDropdown(string title, IEnumerable<string> options, int value, Transform parent, Action<int> whenValueChange, bool isDisabled = false)
        {
            IPDropdown dropdown = dropdownPool.Get(parent);
            dropdown.name = $"{title} Dropdown";
            dropdown.Construct(title, options, value, whenValueChange);
            dropdown.SetDisabled(isDisabled);
        }

        /// <summary>
        /// Builds the Slider Property.
        /// </summary>
        /// <param name="title">The text of the property title.</param>
        /// <param name="minValue">Minimum allowed value on the slider.</param>
        /// <param name="maxValue">Maximum allowed value on the slider.</param>
        /// <param name="startingValue">Starting value of the slider.</param>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="whenValueChange">Method that will run when the slider changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <returns>The property itself.</returns>
        public void BuildSlider(string title, float minValue, float maxValue, float startingValue, Transform parent, Action<float> whenValueChange, bool isDisabled = false)
        {
            IPSlider slider = sliderPool.Get(parent);
            slider.name = $"{title} Slider";
            slider.Construct(title, minValue, maxValue, startingValue, whenValueChange);
            slider.SetDisabled(isDisabled);
        }

        /// <summary>
        /// Builds the Slider Property.
        /// </summary>
        /// <param name="title">The text of the property title.</param>
        /// <param name="minValue">Minimum allowed value on the slider.</param>
        /// <param name="maxValue">Maximum allowed value on the slider.</param>
        /// <param name="startingValue">Starting value of the slider.</param>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="whenValueChange">Method that will run when the slider changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <returns>The property itself.</returns>
        public void BuildSlider(string title, int minValue, int maxValue, int startingValue, Transform parent, Action<float> whenValueChange, bool isDisabled = false)
        {
            IPSlider slider = sliderPool.Get(parent);
            slider.name = $"{title} Slider";
            slider.Construct(title, minValue, maxValue, startingValue, whenValueChange);
            slider.SetDisabled(isDisabled);
        }

        #endregion

        #region Content Blocks

        /// <summary>
        /// Build the Horizontal Content Block.
        /// </summary>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="isDisabled">Show/Hide the content block at init..</param>
        /// <returns></returns>
        public IPContentBlock CreateContentBlockHorizontal(Transform parent, bool isDisabled = false)
        {
            IPContentBlock contentBlock = contentBlockHorizontalPool.Get(parent);
            contentBlock.SetDisabled(isDisabled);
            return contentBlock;
        }
        
        /// <summary>
        /// Build the Vertical Content Block.
        /// </summary>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="isDisabled">Show/Hide the content block at init..</param>
        /// <returns></returns>
        public IPContentBlock CreateContentBlockVertical(Transform parent, bool isDisabled = false)
        {
            IPContentBlock contentBlock = contentBlockVerticalPool.Get(parent);
            contentBlock.SetDisabled(isDisabled);
            return contentBlock;
        }
        
        /// <summary>
        /// Build the 2 Column Content Block.
        /// </summary>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="isDisabled">Show/Hide the content block at init..</param>
        /// <returns></returns>
        public IPContentBlock CreateContentBlockColumn2(Transform parent, bool isDisabled = false)
        {
            IPContentBlock contentBlock = contentBlockColumn2Pool.Get(parent);
            contentBlock.SetDisabled(isDisabled);
            return contentBlock;
        }

        #endregion
        
        [Serializable]
        public struct ContentBlockInfo
        {
            public IPContentBlock vertical;
            public IPContentBlock horizontal;
            public IPContentBlock column2;
        }

        [Serializable]
        public struct VerticalVariantsInfo
        {
            public IPInputField inputFieldProperty;
            public IPDropdown dropdownProperty;
            public IPPlainText plainTextProperty;
        }
    }
}