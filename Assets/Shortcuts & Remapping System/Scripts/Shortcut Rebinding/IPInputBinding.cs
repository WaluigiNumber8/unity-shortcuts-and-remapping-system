using RedRats.Core;
using RedRats.ShortcutSystem.Remapping;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// Represents a property that allows rebinding an single-button input for both keyboard and gamepad.
    /// </summary>
    public class IPInputBinding : IPWithValueBase<InputAction>
    {
        [SerializeField] private InputBindingReader inputReader;
        [SerializeField] private InputBindingReader inputReaderAlt;

        public void Construct(string title, InputAction action, int bindingIndex, int modifier1Index = -1, int modifier2Index = -1, int bindingIndexAlt = -1, int modifier1IndexAlt = -1, int modifier2IndexAlt = -1)
        {
            ConstructTitle(title.WithSpacesBeforeCapitals());
            
            inputReader.Construct(action, bindingIndex, modifier1Index, modifier2Index);
            inputReaderAlt.gameObject.SetActive(bindingIndexAlt != -1);
            if (bindingIndexAlt != -1) inputReaderAlt.Construct(action, bindingIndexAlt, modifier1IndexAlt, modifier2IndexAlt);
        }

        public override void SetDisabled(bool isDisabled)
        {
            inputReader.SetActive(!isDisabled);
            inputReaderAlt.SetActive(!isDisabled);
        }

        public override InputAction PropertyValue { get => inputReader.Action; }

        public string InputString { get => inputReader.InputString; }
        public string InputStringAlt { get => inputReaderAlt.InputString; }
        public InputBindingReader InputReader { get => inputReader; }
        public InputBindingReader InputReaderAlt { get => inputReaderAlt; }
    }
}