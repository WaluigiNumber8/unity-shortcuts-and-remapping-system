using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace RedRats.Input
{
    [CreateAssetMenu(fileName = "asset_LinkedActionMaps", menuName = "Linked Action Maps", order = 10)]
    public class LinkedActionMapsAsset : ScriptableObject
    {
        [SerializeField] private LinkedActionMapData[] maps;
        
        private IDictionary<string, ISet<string>> mapsDictionary;
        
        public void RefreshDictionary()
        {
            mapsDictionary = new Dictionary<string, ISet<string>>();
            foreach (LinkedActionMapData map in maps)
            {
                mapsDictionary.Add(map.map, map.linkedMaps.ToHashSet());
            }
        }
        
        public ReadOnlyDictionary<string, ISet<string>> GetLinkedMaps { get => new(mapsDictionary); }
        
        [Serializable]
        private struct LinkedActionMapData
        {
            public string map;
            public string[] linkedMaps;
        }
    }
}