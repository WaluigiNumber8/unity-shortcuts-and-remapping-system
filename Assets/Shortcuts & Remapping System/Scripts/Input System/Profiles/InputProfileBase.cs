namespace RedRats.ShortcutSystem.Input
{
    /// <summary>
    /// A base for all input profiles.
    /// </summary>
    public abstract class InputProfileBase
    {
        protected readonly TestInputActions input;
        
        protected InputProfileBase(TestInputActions input) => this.input = input;
        
        /// <summary>
        /// Enables the profile.
        /// </summary>
        public void Enable() => WhenEnabled();

        /// <summary>
        /// Disables the profile.
        /// </summary>
        public void Disable() => WhenDisabled();

        public abstract bool IsMapEnabled { get; }
        
        /// <summary>
        /// Actions that happen when the profile is enabled.
        /// </summary>
        protected abstract void WhenEnabled();

        /// <summary>
        /// Actions that happen when the profile is disabled.
        /// </summary>
        protected abstract void WhenDisabled();
    }
}