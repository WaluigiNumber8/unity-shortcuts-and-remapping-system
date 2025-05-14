using System;
using System.Collections.Generic;
using RedRats.ActionHistory;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// Overseers everything happening in a dropdown interactable property.
    /// </summary>
    public class IPDropdown : IPWithValueBase<int>
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private UIInfo ui;
        
        private Action<int> whenValueChange;
        private int lastValue;

        private void Awake() => dropdown.onValueChanged.AddListener(WhenValueChanged);

        public override void SetDisabled(bool isDisabled) => dropdown.interactable = !isDisabled;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="titleText">Property Title.</param>
        /// /// <param name="options">The list of options the dropdown will be filled with.</param>
        /// <param name="startingValue">Index of the dropdownOption.</param>
        /// <param name="whenValueChange">Method that will run when the dropdown value changes.</param>
        public void Construct(string titleText, IEnumerable<string> options, int startingValue, Action<int> whenValueChange)
        {
            FillDropdown(options);
            ConstructTitle(titleText);
            
            lastValue = startingValue;
            dropdown.SetValueWithoutNotify(startingValue);
            dropdown.RefreshShownValue();
            
            this.whenValueChange = whenValueChange;
        }

        /// <summary>
        /// Updates the dropdown value without invoking the value change event. Assigned <see cref="whenValueChange"/> action still runs.
        /// </summary>
        /// <param name="value">The new value for the dropdown.</param>
        public void UpdateValueWithoutNotify(int value)
        {
            lastValue = dropdown.value;
            dropdown.SetValueWithoutNotify(value);
            dropdown.RefreshShownValue();
            whenValueChange?.Invoke(value);
        }

        private void WhenValueChanged(int value)
        {
            ActionHistorySystem.AddAndExecute(new UpdateDropdownAction(this, value, lastValue, whenValueChange));
            lastValue = value;
        }
        
        /// <summary>
        /// Fills the dropdown with strings.
        /// </summary>
        /// <param name="options">List of strings, that will become values.</param>
        private void FillDropdown(IEnumerable<string> options)
        {
            dropdown.options.Clear();
            foreach (string option in options)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData {text = option});
            }
        }

        public override int PropertyValue { get => dropdown.value; }

        [Serializable]
        public struct UIInfo
        {
            public Image headerBackgroundImage;
            public TextMeshProUGUI labelText;
            public Image dropdownArrowImage;
            public Image dropdownImage;
            public Toggle itemToggle;
            public TextMeshProUGUI toggleLabelText;
            public Image toggleBackgroundImage;
            public Image toggleCheckmarkImage;
        }
    }
}