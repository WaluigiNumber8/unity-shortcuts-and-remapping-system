using System;

namespace RedRats.ActionHistory
{
    /// <summary>
    /// Represents a single action/command that is recorded in the action history.
    /// </summary>
    public abstract class ActionBase<T> : IAction
    {
        private readonly Action<T> fallback;

        protected ActionBase(Action<T> fallback) => this.fallback = fallback;

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            if (AffectedConstruct == null)
            {
                fallback?.Invoke(Value);
                return;
            }
            ExecuteSelf();
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            if (AffectedConstruct == null)
            {
                fallback?.Invoke(LastValue);
                return;
            }
            UndoSelf();
        }

        /// <summary>
        /// Returns TRUE if the action did not change anything.
        /// </summary>
        public abstract bool NothingChanged();

        /// <summary>
        /// Executes unique action.
        /// </summary>
        protected abstract void ExecuteSelf();
        
        /// <summary>
        /// Undo unique action.
        /// </summary>
        protected abstract void UndoSelf();
        

        /// <summary>
        /// Which construct is affected by this action.
        /// </summary>
        public abstract object AffectedConstruct { get; }

        object IAction.Value => Value;
        object IAction.LastValue => LastValue;

        public abstract T Value { get; }
        public abstract T LastValue { get; }
    }
}