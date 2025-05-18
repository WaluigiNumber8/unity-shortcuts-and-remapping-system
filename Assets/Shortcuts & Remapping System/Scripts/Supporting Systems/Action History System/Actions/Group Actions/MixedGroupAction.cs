using System.Linq;

namespace RedRats.ActionHistory
{
    /// <summary>
    /// Groups multiple actions, happening on different constructs, into one.
    /// </summary>
    public class MixedGroupAction : GroupActionBase
    {
        public override bool NothingChanged() => actions.All(action => action.NothingChanged());

        public override string ToString() => string.Join(", ", actions.Select(action => action.ToString()));
    }
}