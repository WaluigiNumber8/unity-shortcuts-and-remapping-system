using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using RedRats.ShortcutSystem.Saving;
using RedRats.Systems.FileSystem;

namespace RedRats.FileSystem
{
    /// <summary>
    /// Enables CRUD operations for assets on external storage.
    /// </summary>
    /// <typeparam name="T">The type of asset.</typeparam>
    /// <typeparam name="TS">Serialized form of the asset.</typeparam>
    public class CRUDOperations<T, TS> : ICRUDOperations<T, TS> where T : IDataAsset where TS : IEncodedObject<T>
    {
        private SaveableData data;
        private readonly Func<T,TS> newSerializedObject;
        private readonly string dataIdentifier;
        private readonly bool useCompression;

        public CRUDOperations(Func<T,TS> newSerializedObject, string dataIdentifier, bool useCompression = true)
        {
            this.newSerializedObject = newSerializedObject;
            this.dataIdentifier = dataIdentifier;
            this.useCompression = useCompression;
        }

        /// <summary>
        /// Saves a room under the currently edited pack.
        /// </summary>
        /// <param name="asset">The room to save.</param>
        public void Save(T asset)
        {
            Preconditions.IsNotNull(data, "Saveable Data");
            
            data.TryAddNewFilePath(asset.ID, asset.Title);
            JSONSystem.Save(data.GetFilePath(asset.ID), dataIdentifier, asset, r => newSerializedObject(r), useCompression);
        }

        public T Load(T asset) => throw new NotImplementedException();

        /// <summary>
        /// Loads all assets.
        /// </summary>
        /// <returns>A list of those assets.</returns>
        public IList<T> LoadAll()
        {
            IList<T> loadedData = JSONSystem.LoadAll<T, TS>(data.Path, data.Identifier, false, useCompression);
            loadedData = FindAndRemoveDuplicates(loadedData);
            foreach (T piece in loadedData)
            {
                data.TryAddNewFilePath(piece.ID, piece.Title);
            }
            return loadedData;
        }
        
        /// <summary>
        /// Updates the title of a room under the currently edited pack.
        /// </summary>
        /// <param name="asset">The room, whose title to update.</param>
        public void Update(T asset)
        {
            Preconditions.IsNotNull(data, "Saveable Data");
            
            Save(asset);
            data.GetFileTitleAndPath(asset.ID, out string title, out string oldPath);
            if (asset.Title == title) return;

            data.UpdateFileTitle(asset.ID, asset.Title);
            JSONSystem.RenameFile(oldPath, data.GetFilePath(asset.ID), useCompression);
        }
        
        /// <summary>
        /// Deletes a room from the currently edited pack.
        /// </summary>
        /// <param name="asset">The room to delete.</param>
        public void Delete(T asset)
        {
            Preconditions.IsNotNull(data, "Saveable Data");
            
            JSONSystem.Delete(data.GetFilePath(asset.ID));
            data.RemoveFilePath(asset.ID);
        }
        
        /// <summary>
        /// Updates CRUD information.
        /// </summary>
        /// <param name="saveableData">New data to read from.</param>
        public void RefreshSaveableData(SaveableData saveableData)
        {
            FileSystem.CreateDirectory(saveableData.Path);
            data = saveableData;
        }
        
        /// <summary>
        /// Searches a list for duplicate entries. If it finds any, will remove them from the list and throw an error message.
        /// </summary>
        /// <param name="list">The list to search.</param>
        /// <returns>Returns a cleaned up list.</returns>
        private static IList<T> FindAndRemoveDuplicates(IList<T> list)
        {
            if (list == null || list.Count <= 1) return list;
            
            Dictionary<string, int> duplicates = list.GroupBy(asset => asset.Title)
                                            .Where(g => g.Count() > 1)
                                            .ToDictionary(x => x.Key, y => y.Count());

            if (duplicates.Count <= 0) return list;

            foreach (KeyValuePair<string, int> dupe in duplicates)
            {
                string name = typeof(T).FullName.Split('.')[^1];
                PreconditionsIO.ThrowMessage($" The {name} called '{dupe.Key}' was not loaded as it has duplicates ({(dupe.Value-1).ToString()}). \n\n Edited changes to any '{dupe.Key}' will not be saved until all duplicates are removed.");
            }
            
            return list.GroupBy(asset => asset.ID)
                        .Select(asset => asset.First())
                        .ToList();
        }
    }
}