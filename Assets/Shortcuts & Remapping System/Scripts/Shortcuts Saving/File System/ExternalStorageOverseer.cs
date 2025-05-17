using RedRats.Core;
using RedRats.ShortcutSystem.Saving;

namespace RedRats.FileSystem
{
    public sealed class ExternalStorageOverseer : Singleton<ExternalStorageOverseer>
    {
        private readonly CRUDOperations<ShortcutBindingsAsset, JSONShortcutBindingsAsset> shortcutBindingsCRUD;

        private ExternalStorageOverseer()
        {
            shortcutBindingsCRUD = new CRUDOperations<ShortcutBindingsAsset, JSONShortcutBindingsAsset>(s => new JSONShortcutBindingsAsset(s), "01", false);
            shortcutBindingsCRUD.RefreshSaveableData(new SaveableData("", "01"));
        }
        
        public ICRUDOperations<ShortcutBindingsAsset, JSONShortcutBindingsAsset> ShortcutBindings { get => shortcutBindingsCRUD; }
    }
}