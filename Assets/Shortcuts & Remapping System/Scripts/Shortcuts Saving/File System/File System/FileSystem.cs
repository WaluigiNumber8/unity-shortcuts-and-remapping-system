using System.Collections.Generic;
using System.IO;
using System.Linq;
using RedRats.Core;
using RedRats.Systems.FileSystem.Compression;

namespace RedRats.FileSystem
{
    /// <summary>
    /// Saves, reads and removes files and directories.
    /// </summary>
    public static class FileSystem
    {
        private static readonly FileLoader loader = new();

        /// <summary>
        /// If it doesn't exist, creates a directory at specific path.
        /// </summary>
        /// <param name="path">The location to create the directory in.</param>
        /// <param name="name">The name of the directory.</param>
        public static void CreateDirectory(string path, string name)
        {
            CreateDirectory(Path.Combine(path, name));
        }

        /// <summary>
        /// If it doesn't exist, creates a directory at specific path.
        /// </summary>
        /// <param name="path">The location to create the directory in. (including directory title)</param>
        public static void CreateDirectory(string path)
        {
            PreconditionsIO.PathNotContainsInvalidCharacters(path);
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Save a file to external storage.
        /// </summary>
        /// <param name="path">Destination of the save. (without extension)</param>
        /// <param name="data">The string of data to save.</param>
        /// <param name="compression">Method to use for compressing the file.</param>
        /// <param name="overrideByCompression">If TRUE, keep onl the compressed file.</param>
        public static void SaveFile(string path, string data, ICompressionSystem compression = null, bool overrideByCompression = true)
        {
            PreconditionsIO.PathNotContainsInvalidCharacters(path);
            try
            {
                File.WriteAllText(path, data);
                
                if (compression == null) return;
                
                compression.Compress(path);
                if (overrideByCompression) DeleteFile(path);
            }
            catch (IOException)
            {
                PreconditionsIO.ThrowMessage($"File '{Path.GetFileName(path)} could not be saved.'");
            }
        }

        /// <summary>
        /// Load a file under a specific path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="expectedExtension">The extension of the file.</param>
        /// <param name="compression">The method used to compress the file.</param>
        /// <returns>A deserialized form of the object.</returns>
        public static string LoadFile(string path, string expectedExtension, ICompressionSystem compression = null)
        {
            return loader.LoadFile(path, expectedExtension, compression);
        }

        /// <summary>
        /// Loads all objects under a path.
        /// </summary>
        /// <param name="path">Destination of the data.</param>
        /// <param name="extension">The extension of files to take into consideration.</param>
        /// <param name="deepSearch">If enabled, will also search all subdirectories.</param>
        /// <param name="compression">Method used for compressing wanted files.</param>
        public static IList<string> LoadAllFiles(string path, string extension, bool deepSearch = false, ICompressionSystem compression = null)
        {
            return loader.LoadAllFiles(path, extension, deepSearch, compression);
        }
        
        /// <summary>
        /// Returns a list of paths of all files with a specific extension from a specific folder.
        /// </summary>
        /// <param name="path">The path of the folder to search.</param>
        /// <param name="extension">The extension of files to read.</param>
        public static IList<string> LoadFilePaths(string path, string extension)
        {
            return Directory.GetFiles(path).Where(f => f.EndsWith(extension)).ToList();
        }

        /// <summary>
        /// Removes a file under a specific path. (No extension)
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public static void DeleteFile(string path)
        {
            PreconditionsIO.PathNotContainsInvalidCharacters(path);
            PreconditionsIO.FileExists(path);
            File.Delete(path);
        }

        /// <summary>
        /// Removes a directory and all it's contents under a specific path.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        public static void DeleteDirectory(string path)
        {
            PreconditionsIO.PathNotContainsInvalidCharacters(path);
            PreconditionsIO.DirectoryExists(path);
            Directory.Delete(path, true);
        }

        /// <summary>
        /// Renames a specific file.
        /// </summary>
        /// <param name="oldPath">The full path of the old file. (no extension)</param>
        /// <param name="newPath">The full path of the new file. (no extension)</param>
        public static void RenameFile(string oldPath, string newPath)
        {
            PreconditionsIO.PathNotContainsInvalidCharacters(oldPath);
            PreconditionsIO.PathNotContainsInvalidCharacters(newPath);
            PreconditionsIO.FileExists(oldPath);
            File.Move(oldPath, newPath);
        }

        /// <summary>
        /// Renames a specific directory.
        /// </summary>
        /// <param name="oldPath">The full path of the old directory.</param>
        /// <param name="newPath">The full path of the new directory.</param>
        public static void RenameDirectory(string oldPath, string newPath)
        {
            PreconditionsIO.PathNotContainsInvalidCharacters(oldPath);
            PreconditionsIO.PathNotContainsInvalidCharacters(newPath);
            PreconditionsIO.DirectoryExists(oldPath);
            Directory.Move(oldPath, newPath);
        }
    }
}
