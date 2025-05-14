using UnityEngine;
using UnityEngine.UI;
using System;
using RedRats.ActionHistory;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// Overseers everything happening in a toggle interactable property.
    /// </summary>
    public class IPToggle : IPWithValueBase<bool>
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private UIInfo ui;
        
        private Action<bool> whenChangeValue;

        private void Awake() => toggle.onValueChanged.AddListener(WhenValueChanged);

        public override void SetDisabled(bool isDisabled) => toggle.interactable = !isDisabled;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="titleText">Property Title.</param>
        /// <param name="value">State of the toggle checkbox.</param>
        /// <param name="whenChangeValue">The method that will run when the toggle changes it's value.</param>
        public void Construct(string titleText, bool value, Action<bool> whenChangeValue)
        {
            ConstructTitle(titleText);
            
            toggle.SetIsOnWithoutNotify(value);
            this.whenChangeValue = whenChangeValue;
        }
        
        public void UpdateValueWithoutNotify(bool value)
        {
            toggle.SetIsOnWithoutNotify(value);
            whenChangeValue?.Invoke(value);
        }

        private void WhenValueChanged(bool value) => ActionHistorySystem.AddAndExecute(new UpdateToggleAction(this, value, whenChangeValue));

        public override bool PropertyValue { get => toggle.isOn; }

        [Serializable]
        public struct UIInfo
        {
            public Image backgroundImage;
            public Image checkmarkImage;
        }
    }
}