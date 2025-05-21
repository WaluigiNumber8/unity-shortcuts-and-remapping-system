namespace RedRats.ShortcutSystem.Saving
{
    /// <summary>
    /// A base for any data object, resembling an asset with and ID & title.
    /// </summary>
    public interface IDataAsset
    {
        string ID { get; }
        public string Title { get; }
    }
}