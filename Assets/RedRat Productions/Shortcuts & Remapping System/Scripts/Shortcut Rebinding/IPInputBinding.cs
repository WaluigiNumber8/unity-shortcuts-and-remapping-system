using RedRats.Core;
using RedRats.ShortcutSystem.Remapping;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// Represents a property that allows rebinding an single-button input for both keyboard and gamepad.
    /// </summary>
    public class IPInputBinding : MonoBehaviour
    {
        #region IPBase class
        [SerializeField] protected TextMeshProUGUI title;
        [SerializeField] protected GameObject middleSpace;

        /// <summary>
        /// Changes, if the user can edit the property.
        /// </summary>
        /// <param name="isDisabled">When on, the property is disabled and cannot be edited.</param>
        public void SetDisabled(bool isDisabled)
        {
            inputReader.SetActive(!isDisabled);
            inputReaderAlt.SetActive(!isDisabled);
        }

        protected void ConstructTitle(string titleText)
        {
            if (title == null) return;
            title.text = titleText;
            title.gameObject.SetActive((titleText != ""));
            if (middleSpace != null) middleSpace.SetActive((titleText != ""));
        }
        
        public string Title { get => title.text; }
        public InputAction PropertyValue { get => inputReader.Action; }
        
        #endregion
        
        [SerializeField] private InputBindingReader inputReader;
        [SerializeField] private InputBindingReader inputReaderAlt;

        public void Construct(string title, InputAction action, int bindingIndex, int modifier1Index = -1, int modifier2Index = -1, int bindingIndexAlt = -1, int modifier1IndexAlt = -1, int modifier2IndexAlt = -1)
        {
            ConstructTitle(title.WithSpacesBeforeCapitals());
            
            inputReader.Construct(action, bindingIndex, modifier1Index, modifier2Index);
            inputReaderAlt.gameObject.SetActive(bindingIndexAlt != -1);
            if (bindingIndexAlt != -1) inputReaderAlt.Construct(action, bindingIndexAlt, modifier1IndexAlt, modifier2IndexAlt);
        }

        public string InputString { get => inputReader.InputString; }
        public string InputStringAlt { get => inputReaderAlt.InputString; }
        public InputBindingReader InputReader { get => inputReader; }
        public InputBindingReader InputReaderAlt { get => inputReaderAlt; }
    }
}