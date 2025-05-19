using RedRats.Input;
using UnityEngine;

namespace RedRats.ShortcutSystem.Linking
{
    /// <summary>
    /// Activates shortcut actions maps on enable.
    /// </summary>
    public class ShortcutMapActivator : MonoBehaviour
    {
        [SerializeField] private ShortcutActionMapType activatedMaps = ShortcutActionMapType.General;
        
        private InputSystem input;

        private void Awake() => input = InputSystem.Instance;

        private void OnEnable()
        {
            input.Shortcuts.ActivateGeneralMap((activatedMaps & ShortcutActionMapType.General) == ShortcutActionMapType.General);
        }
    }
}