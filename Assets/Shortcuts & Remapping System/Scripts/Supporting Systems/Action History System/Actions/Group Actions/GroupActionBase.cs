using System.Collections.Generic;

namespace RedRats.ActionHistory
{
    /// <summary>
    /// A base for all group actions.
    /// </summary>
    public abstract class GroupActionBase : IAction
    {
        protected readonly IList<IAction> actions = new List<IAction>();

        public void Execute()
        {
            foreach (IAction action in actions)
            {
                action.Execute();
            }
        }

        public void Undo()
        {
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                actions[i].Undo();
            }
        }

        public void AddAction(IAction action) => actions.Add(action);
        public int ActionsCount => actions.Count;
        public abstract bool NothingChanged();
        public object AffectedConstruct => actions[0]?.AffectedConstruct;
        public object Value { get => -1; }
        public object LastValue { get => -1; }
    }
}