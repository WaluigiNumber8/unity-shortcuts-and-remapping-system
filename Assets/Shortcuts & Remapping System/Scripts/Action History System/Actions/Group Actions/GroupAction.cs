namespace RedRats.ActionHistory
{
    /// <summary>
    /// Groups multiple actions, happening on the same construct, into one.
    /// </summary>
    public class GroupAction : GroupActionBase
    {
        public override bool NothingChanged() => actions[0].LastValue.Equals(actions[^1].Value);

        public override string ToString() => $"{actions[0].AffectedConstruct} x {actions.Count}";
    }
}