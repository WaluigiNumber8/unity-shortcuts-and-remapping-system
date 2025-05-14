using System;
using RedRats.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static TMPro.TMP_InputField;

namespace RedRats.Core
{
    /// <summary>
    /// Allows a Slider to have an Input Field as an alternative method.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class SliderWithInput : MonoBehaviour
    {
        public event Action<float> OnValueChanged;

        [SerializeField] private Slider slider;
        [SerializeField] private TMP_InputField inputField;

        private bool changingValue;
        private int decimalMultiplier = 1;
        private string decimalsInText;

        private void Awake()
        {
            if (inputField.characterValidation != CharacterValidation.Integer &&
                inputField.characterValidation != CharacterValidation.Decimal &&
                inputField.characterValidation != CharacterValidation.Digit)
                throw new InvalidOperationException("Cannot work with non-numeric input fields! Change validation to numbers only.");

            slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(SetToInputField);
            inputField.onValueChanged.AddListener(SetToSlider);
            inputField.onEndEdit.AddListener(ClampValue);
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(SetToInputField);
            inputField.onValueChanged.RemoveListener(SetToSlider);
            inputField.onEndEdit.AddListener(ClampValue);
        }

        /// <summary>
        /// Sets a new value.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void SetValue(float value)
        {
            if (changingValue) return;

            changingValue = true;
            slider.SetValueWithoutNotify(value * decimalMultiplier);
            inputField.text = value.ToString();
            OnValueChanged?.Invoke(value);
            changingValue = false;
        }

        public void OverrideDecimalMultiplier(int multiplier)
        {
            decimalMultiplier = multiplier;
            int decimals = multiplier.ToString().Length - 1;
            decimalsInText = $"0.{new string('#', decimals)}";
        }
        public void ResetDecimalMultiplier()
        {
            decimalMultiplier = 1;
            decimalsInText = "0";
        }

        /// <summary>
        /// Set a value into the attached input field only.
        /// </summary>
        /// <param name="value">The value to set.</param>
        private void SetToInputField(float value)
        {
            if (changingValue) return;

            changingValue = true;
            value /= decimalMultiplier;
            inputField.text = (value).ToString(decimalsInText);
            
            OnValueChanged?.Invoke(value);
            changingValue = false;
        }

        /// <summary>
        /// Set a value to the slider only.
        /// </summary>
        /// <param name="stringValue">The value to set.</param>
        private void SetToSlider(string stringValue)
        {
            if (changingValue) return;
            if (EqualsToSpecialSymbol(stringValue)) return;

            changingValue = true;
            float value =  float.Parse(stringValue).RoundM(decimalMultiplier);
            value *= decimalMultiplier;
            slider.value = value;
            
            OnValueChanged?.Invoke(value);
            changingValue = false;
        }

        /// <summary>
        /// Clamps a value between the slider's min and max values.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        private void ClampValue(string value)
        {
            if (EqualsToSpecialSymbol(value)) value = 0.ToString();
            SetValue(Mathf.Clamp(float.Parse(value), slider.minValue / decimalMultiplier, slider.maxValue / decimalMultiplier));
        }

        /// <summary>
        /// Checks if a value is equal to special symbols or not.
        /// </summary>
        /// <param name="value">The value of the Input Field.</param>
        /// <returns>Is true if is equal to any special value.</returns>
        private bool EqualsToSpecialSymbol(string value)
        {
            if (value == "-") return true;
            if (string.IsNullOrEmpty(value)) return true;
            return false;
        }
        
        public TMP_InputField InputField { get => inputField; }
    }
}