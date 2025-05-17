using System.Collections.Generic;
using RedRats.Core;
using RedRats.FileSystem;
using RedRats.ShortcutSystem.Input;
using RedRats.ShortcutSystem.Saving;
using UnityEngine;

namespace RedRats.Example.Core
{
    public class InputBindingEditor : MonoSingleton<InputBindingEditor>
    {
        private ExternalStorageOverseer ex;
        private InputSystem input;
        
        [SerializeField] private RectTransform propertiesContent;
        
        private ShortcutBindingsAsset asset;
        private PropertyColumnBuilder propertyColumnBuilder;

        protected override void Awake()
        {
            base.Awake();
            ex = ExternalStorageOverseer.Instance;
            input = InputSystem.Instance;
        }
        
        private void Start()
        {
            propertyColumnBuilder = new PropertyColumnBuilder(propertiesContent);
            propertyColumnBuilder.Build();     
            
            LoadFromFile();
        }

        public void SaveChanges()
        {
            asset = new ShortcutBindingsAsset.Builder().AsCopy(ShortcutToAssetConverter.Get()).Build();
            SaveToFile();
            input.ReplaceAllBindings();
        }

        public void StartEditing()
        {
            input.ClearAllInput();
            LoadFromFile();
            propertyColumnBuilder.Build();
        }
        
        private void SaveToFile() => ex.ShortcutBindings.Save(asset);
        
        private void LoadFromFile()
        {
            IList<ShortcutBindingsAsset> shortcutBindingsData = ex.ShortcutBindings.LoadAll();
            asset = (shortcutBindingsData == null || shortcutBindingsData.Count <= 0) ? new ShortcutBindingsAsset.Builder().Build() : shortcutBindingsData[0];
            ShortcutToAssetConverter.Load(asset);
        }
    }
}