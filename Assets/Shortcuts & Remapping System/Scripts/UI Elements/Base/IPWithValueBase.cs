namespace RedRats.UI.Properties
{
    /// <summary>
    /// A base for all interactable properties.
    /// </summary>
    public abstract class IPWithValueBase<T> : IPBase
    {
        public abstract T PropertyValue { get; }
    }
}