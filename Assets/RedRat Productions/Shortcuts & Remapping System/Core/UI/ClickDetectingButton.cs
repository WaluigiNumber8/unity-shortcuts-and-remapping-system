using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RedRats.UI.Core.Interactables.Buttons
{
    /// <summary>
    /// A <see cref="Button"/>, enhanced with different click events.
    /// </summary>
    public class ClickDetectingButton : Button
    {
        public event Action OnClickRight;
        public event Action OnClickMiddle;

        public override void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    break;
                case PointerEventData.InputButton.Right:
                    OnClickRight?.Invoke();
                    break;
                case PointerEventData.InputButton.Middle:
                    OnClickMiddle?.Invoke();
                    break;
                default: throw new ArgumentOutOfRangeException($"{eventData.button} is not a supported button type.");
            }
            base.OnPointerClick(eventData);
        }
    }
}