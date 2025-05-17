using System.Collections.Generic;
using System.IO;
using RedRats.Core;
using RedRats.Systems.FileSystem.Compression;

namespace RedRats.FileSystem
{
    /// <summary>
    /// Loads a list of JSON files.
    /// </summary>
    public class FileLoader
    {
        private readonly IList<string> dataList;
        private string searchedExtension;
        private bool deepSearchEnabled;
        private ICompressionSystem compressionSystem;

        public FileLoader() => dataList = new List<string>();

        /// <summary>
        /// Load a file under a specific path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="expectedExtension">The extension of the file.</param>
        /// <returns>A deserialized form of the object.</returns>
        /// <exception cref="IOException">IS thrown when the object could not be loaded.</exception>
        public string LoadFile(string path, string expectedExtension, ICompressionSystem compression = null)
        {
            PreconditionsIO.PathNotContainsInvalidCharacters(path);
            PreconditionsIO.FileExists(path);
            try
            {
                compression?.Decompress(path, expectedExtension);
                string dataFile = (compression == null) ? path : Path.ChangeExtension(path, expectedExtension);
                
                string allData = File.ReadAllText(dataFile);
                if (compression != null) FileSystem.DeleteFile(dataFile);
                return allData;
            }
            catch (IOException)
            {
                throw new IOException($"File under the path '{path}' could not be loaded.");
            }
        }

        /// <summary>
        /// Loads all files under a specific path.
        /// </summary>
        /// <param name="path">The root path to start loading from.</param>
        /// <param name="extension">The extension of files to take into consideration.</param>
        /// <param name="deepSearch">If enabled, will also search all subdirectories.</param>
        /// <param name="compression">How (if) is the file compressed.</param>
        /// <returns>A list where each element being file data represented by a string.</returns>
        public IList<string> LoadAllFiles(string path, string extension, bool deepSearch = false, ICompressionSystem compression = null)
        {
            PreconditionsIO.PathNotContainsInvalidCharacters(path);
            PreconditionsIO.DirectoryExists(path);

            dataList.Clear();
            searchedExtension = extension;
            deepSearchEnabled = deepSearch;
            compressionSystem = compression;

            SearchDirectory(new DirectoryInfo(path));

            return dataList;
        }

        /// <summary>
        /// Process files in a specific directory.
        /// </summary>
        /// <param name="directory">The directory to search.</param>
        private void SearchDirectory(DirectoryInfo directory)
        {
            ProcessFiles(directory.GetFiles(), directory.FullName);

            if (!deepSearchEnabled) return;

            DirectoryInfo[] directories = directory.GetDirectories();
            if (directories.Length <= 0) return;

            foreach (DirectoryInfo dir in directories)
            {
                SearchDirectory(dir);
            }
        }

        /// <summary>
        /// Load a list of files.
        /// </summary>
        /// <param name="files">The files to load.</param>
        /// <param name="path">The path of the files.</param>
        private void ProcessFiles(FileInfo[] files, string path)
        {
            if (files.Length <= 0) return;
            foreach (FileInfo file in files)
            {
                if (compressionSystem != null && file.Extension != compressionSystem.Extension) continue;
                if (compressionSystem == null && file.Extension != searchedExtension) continue;

                string filePath = Path.Combine(path, file.Name);
                dataList.Add(LoadFile(filePath, searchedExtension, compressionSystem));
            }
        }
    }
}
