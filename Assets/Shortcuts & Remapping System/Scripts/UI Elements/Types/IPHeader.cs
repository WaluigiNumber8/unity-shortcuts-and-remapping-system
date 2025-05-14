using UnityEngine;
using UnityEngine.UI;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// Prepares the Header property for correct use.
    /// </summary>
    public class IPHeader : IPWithValueBase<bool>
    {
        [SerializeField] private Image line;
        
        public override void SetDisabled(bool isDisabled) {}
        
        /// <summary>
        /// Sets the property title and state.
        /// </summary>
        /// <param name="headerText"></param>
        public void Construct(string headerText)
        {
            title.text = headerText;
        }

        public override bool PropertyValue { get => false; }
    }
}