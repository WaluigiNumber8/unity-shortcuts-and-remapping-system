using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RedRats.Core;
using RedRats.Systems.FileSystem;
using RedRats.Systems.FileSystem.Compression;
using UnityEngine;

namespace RedRats.FileSystem
{
    /// <summary>
    /// Encodes/Decodes data into a JSON format.
    /// Communicates with the <see cref="FileSystem"/> and <see cref="FileLoader"/>.
    /// </summary>
    public static class JSONSystem
    {
        private const string JSON_EXTENSION = ".json";

        /// <summary>
        /// Save a file to external storage in a JSON format.
        /// </summary>
        /// <param name="path">Destination of the save. (without extension)</param>
        /// <param name="identifier">The unique identifier, differentiating data stored in the JSON file.</param>
        /// <param name="data">The data object to save.</param>
        /// <param name="newJSONFormat">Function that will create a data version of the object.</param>
        /// <param name="useCompression">Should the JSOn file be compressed to DFL?</param>
        /// <typeparam name="T">The object to convert to JSON.</typeparam>
        /// <typeparam name="TS">The data class to convert to JSON.</typeparam>
        public static void Save<T,TS>(string path, string identifier, T data, Func<T, TS> newJSONFormat, bool useCompression = true)
        {
            path += JSON_EXTENSION;
            
            StringBuilder JSONFormat = new();
            JSONFormat.Append(identifier.Length).Append(identifier).Append(JsonUtility.ToJson(newJSONFormat(data), !useCompression));
            FileSystem.SaveFile(path, JSONFormat.ToString(), (useCompression) ? new DFLCompression() : null);
        }

        /// <summary>
        /// Loads a single JSON file under a file path.
        /// </summary>
        /// <param name="path">Destination of the data.</param>
        /// <param name="identifier">Load only JSON files with this specific identifier.</param>
        /// <param name="isCompressed">Is the wanted file compressed in the DFL format?</param>
        /// <typeparam name="T">Unity readable Asset.</typeparam>
        /// <typeparam name="TS">Serialized form of the Asset.</typeparam>
        public static T Load<T, TS>(string path, string identifier, bool isCompressed = true) where TS : IEncodedObject<T>
        {
            string extension = (isCompressed) ? DFLCompression.COMPRESSED_EXTENSION : JSON_EXTENSION;
            if (Path.HasExtension(path)) path = Path.ChangeExtension(path, extension);
            
            string data = FileSystem.LoadFile(path, extension, isCompressed ? new DFLCompression() : null);
            if (!HasSameIdentifier(data, identifier)) throw new IOException($"Data does not have the same identifier. Expected: {identifier} Got: {data.Substring(1, identifier.Length)}");
            int lengthToCut = StringUtils.GrabIntFrom(data, 0) + 1;
            string jsonText = data[lengthToCut..];
            return JsonUtility.FromJson<TS>(jsonText).Decode();
        }
        
        /// <summary>
        /// Loads all JSON files under a directory path.
        /// </summary>
        /// <param name="path">Destination of the data.</param>
        /// <param name="identifier">Load only JSON files with this specific identifier.</param>
        /// <param name="deepSearch">If enabled, will also search all subdirectories.</param>
        /// <param name="areCompressed">Are the wanted files compressed in the DFL format?</param>
        /// <typeparam name="T">Unity readable Asset.</typeparam>
        /// <typeparam name="TS">Serialized form of the Asset.</typeparam>
        public static IList<T> LoadAll<T,TS>(string path, string identifier, bool deepSearch = false, bool areCompressed = true) where TS : IEncodedObject<T>
        {
            IList<string> data = FileSystem.LoadAllFiles(path, JSON_EXTENSION, deepSearch, areCompressed ? new DFLCompression() : null);
            IList<T> objects = new List<T>();
            foreach (string line in data)
            {
                if (!HasSameIdentifier(line, identifier)) continue;
                
                int lengthToCut = StringUtils.GrabIntFrom(line, 0) + 1;
                string jsonText = line[lengthToCut..];
                TS obj = JsonUtility.FromJson<TS>(jsonText);
                objects.Add(obj.Decode());
            }

            return objects;
        }

        /// <summary>
        /// Rename a .dfl file.
        /// </summary>
        /// <param name="oldPath">Path of the original file.</param>
        /// <param name="newPath">Path of the new file.</param>
        /// <param name="isCompressed">Is the file compressed in the DFL format?</param>
        public static void RenameFile(string oldPath, string newPath, bool isCompressed = true)
        {
            oldPath += (isCompressed) ? DFLCompression.COMPRESSED_EXTENSION : JSON_EXTENSION;
            newPath += (isCompressed) ? DFLCompression.COMPRESSED_EXTENSION : JSON_EXTENSION;
            FileSystem.RenameFile(oldPath, newPath);
        }

        /// <summary>
        /// Removes a file under a specific path.
        /// </summary>
        /// <param name="path">The path to the file. (No extension)</param>
        /// <param name="isCompressed">Is the file compressed in the DFL format.</param>
        public static void Delete(string path, bool isCompressed = true)
        {
            path += (isCompressed) ? DFLCompression.COMPRESSED_EXTENSION : JSON_EXTENSION;
            FileSystem.DeleteFile(path);
        }

        /// <summary>
        /// Checks if a data string contains the same identifier.
        /// </summary>
        /// <param name="allData">The data to check.</param>
        /// <param name="identifier">The identifier to check for.</param>
        /// <returns>TRUE if it contains the same identifier.</returns>
        private static bool HasSameIdentifier(string allData, string identifier)
        {
            if (string.IsNullOrEmpty(allData)) return false;
            if (!StringUtils.TryGrabIntFrom(allData, out int idLength, 0)) return false;
            if (identifier.Length != idLength) return false;
            if (identifier != allData.Substring(1, idLength)) return false;
            return true;
        }
    }
}