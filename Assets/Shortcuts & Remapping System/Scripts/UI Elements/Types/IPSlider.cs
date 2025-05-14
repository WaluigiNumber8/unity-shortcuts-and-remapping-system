using System;
using RedRats.ActionHistory;
using RedRats.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// Prepares the Slider property for correct use.
    /// </summary>
    public class IPSlider : IPWithValueBase<float>
    {
        [SerializeField] private Slider slider;
        [SerializeField] private IPInputField inputField;

        [SerializeField] private DecimalInfo decimals;
        [SerializeField] private UIInfo ui;

        private int decimalMultiplier;
        private float lastValue;
        private Action<float> whenValueChange;

        public override void SetDisabled(bool isDisabled)
        {
            slider.interactable = !isDisabled;
            if (inputField != null) inputField.SetDisabled(isDisabled);
        }

        /// <summary>
        /// Sets the property title and state.
        /// </summary>
        /// <param name="titleText">The text of the property title.</param>
        /// <param name="minValue">Minimum allowed value on the slider.</param>
        /// <param name="maxValue">Maximum allowed value on the slider.</param>
        /// <param name="startingValue">Starting value of the slider.</param>
        /// <param name="whenValueChange">Method that will run when the slider changes value.</param>
        public void Construct(string titleText, float minValue, float maxValue, float startingValue, Action<float> whenValueChange)
        {
            this.whenValueChange = null; 
            decimalMultiplier = 1;
            for (int i = 0; i < decimals.allowedDecimals; i++) decimalMultiplier *= 10;
            decimals.sliderWithInput.OverrideDecimalMultiplier(decimalMultiplier);
            
            ConstructTitle(titleText);
            
            inputField.UpdateContentType(TMP_InputField.ContentType.DecimalNumber);
            slider.maxValue = Mathf.RoundToInt(maxValue * decimalMultiplier);
            slider.minValue = Mathf.RoundToInt(minValue * decimalMultiplier);
            UpdateValueWithoutNotify(startingValue);
            lastValue = startingValue * decimalMultiplier;
            
            
            slider.onValueChanged.RemoveListener(WhenValueChanged);
            slider.onValueChanged.RemoveListener(WhenValueChangedToInt);
            this.whenValueChange = whenValueChange;
            slider.onValueChanged.AddListener(WhenValueChanged);
        }
        
        /// <summary>
        /// Sets the property title and state.
        /// </summary>
        /// <param name="titleText">The text of the property title.</param>
        /// <param name="minValue">Minimum allowed value on the slider.</param>
        /// <param name="maxValue">Maximum allowed value on the slider.</param>
        /// <param name="startingValue">Starting value of the slider.</param>
        /// <param name="whenValueChange">Method that will run when the slider changes value.</param>
        public void Construct(string titleText, int minValue, int maxValue, int startingValue, Action<float> whenValueChange)
        {
            this.whenValueChange = null; 
            decimalMultiplier = 1;
            decimals.sliderWithInput.ResetDecimalMultiplier();
            
            ConstructTitle(titleText);
            
            inputField.UpdateContentType(TMP_InputField.ContentType.IntegerNumber);
            slider.maxValue = maxValue;
            slider.minValue = minValue;
            UpdateValueWithoutNotify(startingValue);
            lastValue = startingValue;
            
            slider.onValueChanged.RemoveListener(WhenValueChanged);
            slider.onValueChanged.RemoveListener(WhenValueChangedToInt);
            this.whenValueChange = whenValueChange;
            slider.onValueChanged.AddListener(WhenValueChangedToInt);
        }

        public void UpdateValueWithoutNotify(float value)
        {
            decimals.sliderWithInput.SetValue(value);
            whenValueChange?.Invoke(value);
            lastValue = value * decimalMultiplier;
        }
        
        private void WhenValueChanged(float value)
        {
            ActionHistorySystem.AddAndExecute(new UpdateSliderAction(this, value / decimalMultiplier, lastValue / decimalMultiplier, whenValueChange));
            lastValue = value;
        }
        
        private void WhenValueChangedToInt(float value)
        {
            int v = Mathf.RoundToInt(value);
            ActionHistorySystem.AddAndExecute(new UpdateSliderAction(this, v, lastValue, whenValueChange));
            lastValue = v;
        }
        
        public override float PropertyValue { get => slider.value / decimalMultiplier; }
        public IPInputField InputField { get => inputField; }

        [Serializable]
        public struct DecimalInfo
        {
            public int allowedDecimals;
            public SliderWithInput sliderWithInput;
        }
        
        [Serializable]
        public struct UIInfo
        {
            public Image fillImage;
            public Image backgroundImage;
            public Image handleImage;
        }
    }
}