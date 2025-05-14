using UnityEngine;
using TMPro;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// Overseers everything happening in a plain text interactable property.
    /// </summary>
    public class IPPlainText : IPWithValueBase<string>
    {
        [SerializeField] private TextMeshProUGUI plainText;
        
        public override void SetDisabled(bool isDisabled) {}
        
        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="titleText">Property Title.</param>
        /// <param name="text">Text in the property.</param>
        public void Construct(string titleText, string text)
        {
            ConstructTitle(titleText);
            plainText.text = text;
        }

        public override string PropertyValue { get => plainText.text; }
    }
}