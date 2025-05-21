using System.Collections.Generic;
using RedRats.Core;
using RedRats.FileSystem;
using RedRats.Input;
using RedRats.ShortcutSystem.Saving;
using UnityEngine;

namespace RedRats.Example.Core
{
    public class InputBindingEditor : MonoSingleton<InputBindingEditor>
    {
        [SerializeField] private RectTransform propertiesContent;
        
        private ExternalStorageOverseer ex;
        private InputSystem input;
        private PropertyColumnBuilder propertyColumnBuilder;
        private ShortcutBindingsAsset asset;

        protected override void Awake()
        {
            base.Awake();
            ex = ExternalStorageOverseer.Instance;
            input = InputSystem.Instance;
            propertyColumnBuilder = new PropertyColumnBuilder(propertiesContent);
        }
        
        private void Start()
        {
            InitAsset();
            LoadFromFile();
            propertyColumnBuilder.Build();
        }


        public void SaveChanges()
        {
            InitAsset();
            input.ReplaceAllBindings();
            SaveToFile();
        }

        public void StartEditing()
        {
            input.ClearAllInput();
            LoadFromFile();
            ShortcutToAssetConverter.Load(asset);
            propertyColumnBuilder.Build();
        }
        
        private void InitAsset() => asset = new ShortcutBindingsAsset.Builder().AsCopy(ShortcutToAssetConverter.Get()).Build();
        
        private void SaveToFile() => ex.ShortcutBindings.Save(asset);
        
        private void LoadFromFile()
        {
            IList<ShortcutBindingsAsset> shortcutBindingsData = ex.ShortcutBindings.LoadAll();
            asset = (shortcutBindingsData == null || shortcutBindingsData.Count <= 0) ? new ShortcutBindingsAsset.Builder().Build() : shortcutBindingsData[0];
            ShortcutToAssetConverter.Load(asset);
        }
    }
}