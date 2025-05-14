using System;
using RedRats.ActionHistory;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// Overseers everything happening in an input field interactable property.
    /// </summary>
    public class IPInputField : IPWithValueBase<string>
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private UIInfo ui;

        private Action<string> whenFinishEditing;
        private float minLimit, maxLimit;
        private string lastValue;

        private void Awake() => inputField.onEndEdit.AddListener(WhenValueChanged);

        public override void SetDisabled(bool isDisabled) => inputField.interactable = !isDisabled;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="titleText">Property Title.</param>
        /// <param name="inputtedText">Text in the input field.</param>
        /// <param name="whenFinishEditing">Method that runs when value in the field changes.</param>
        /// <param name="characterValidation">The validation to use for inputted symbols.</param>
        /// <param name="minLimit">The minimum allowed value (when InputField deals with numbers).</param>
        /// <param name="maxLimit">The maximum allowed value (when InputField deals with numbers).</param>
        public void Construct(string titleText, string inputtedText, Action<string> whenFinishEditing, TMP_InputField.CharacterValidation characterValidation, float minLimit, float maxLimit)
        {
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
            
            ConstructTitle(titleText);
            
            lastValue = inputtedText;
            inputField.characterValidation = characterValidation;
            inputField.SetTextWithoutNotify(inputtedText);
            inputField.ForceLabelUpdate();
            lastValue = inputtedText;
            
            this.whenFinishEditing = whenFinishEditing;
        }

        public void UpdateValue(string value) => WhenValueChanged(value);
        
        public void UpdateValueWithoutNotify(string value)
        {
            // Clamp the value if it's a number.
            if (inputField.characterValidation is TMP_InputField.CharacterValidation.Integer or TMP_InputField.CharacterValidation.Decimal or TMP_InputField.CharacterValidation.Digit)
            {
                value = Mathf.Clamp(float.Parse(value), minLimit, maxLimit).ToString();
            }
            
            lastValue = value;
            inputField.SetTextWithoutNotify(value);
            inputField.ForceLabelUpdate();
            whenFinishEditing?.Invoke(value);
        }
        
        /// <summary>
        /// Updates the content type of the held InputField.
        /// </summary>
        /// <param name="contentType">The new content type to use.</param>
        public void UpdateContentType(TMP_InputField.ContentType contentType) => inputField.contentType = contentType;
        
        private void WhenValueChanged(string value)
        {
            ActionHistorySystem.AddAndExecute(new UpdateInputFieldAction(this, value, lastValue, whenFinishEditing), true);
            lastValue = value;
        }
        
        public override string PropertyValue { get => inputField.text; }

        [Serializable]
        public struct UIInfo
        {
            public Image inputFieldImage;
            public TextMeshProUGUI inputtedText;
        }
    }
}
