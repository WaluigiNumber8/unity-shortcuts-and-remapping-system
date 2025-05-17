using RedRats.ShortcutSystem.Input;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = RedRats.ShortcutSystem.Input.InputSystem;

namespace RedRats.Example.Core
{
    /// <summary>
    /// Represents a tooltip for a specific hotkey.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ShortcutTooltip : MonoBehaviour
    {
        [SerializeField] private InputActionReference inputAction;
        [SerializeField] private InputDeviceType device;
        [SerializeField] private bool forAlt;
        
        private TextMeshProUGUI text;

        private void Awake() => text = GetComponent<TextMeshProUGUI>();
        private async void OnEnable()
        {
            await Awaitable.NextFrameAsync();
            Refresh();
        }

        private void Refresh()
        {
            InputAction action = InputSystem.Instance.GetAction(inputAction.action);
            text.text = InputSystemUtils.GetPath(action, device, forAlt).ToUpper();
        }
    }
}