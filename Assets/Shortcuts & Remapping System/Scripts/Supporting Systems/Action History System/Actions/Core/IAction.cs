namespace RedRats.ActionHistory
{
    /// <summary>
    /// Represents a single action/command that is recorded in the action history.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute();
        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo();
        /// <summary>
        /// Returns TRUE if the action did not change anything.
        /// </summary>
        public bool NothingChanged();
        /// <summary>
        /// Which construct is affected by this action.
        /// </summary>
        public object AffectedConstruct { get; }
        public object Value { get; }
        public object LastValue { get; }
    }
}