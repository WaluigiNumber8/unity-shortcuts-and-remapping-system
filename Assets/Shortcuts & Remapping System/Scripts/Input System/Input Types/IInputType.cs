using UnityEngine.InputSystem;

namespace RedRats.ShortcutSystem.Input
{
    public interface IInputType
    {
        /// <summary>
        /// Enable input reading.
        /// </summary>
        public void Enable();
        
        /// <summary>
        /// Disable input reading.
        /// </summary>
        public void Disable();
        
        /// <summary>
        /// Get the action associated with this input.
        /// </summary>
        public InputAction Action { get; }
    }
}