namespace RedRats.Systems.FileSystem.Compression
{
    /// <summary>
    /// Makes a class a compression system for files.
    /// </summary>
    public interface ICompressionSystem
    {
        /// <summary>
        /// Compress a file.
        /// </summary>
        /// <param name="filePath">The path of the file to compress.</param>
        public void Compress(string filePath);
        /// <summary>
        /// Decompress a file.
        /// </summary>
        /// <param name="filePath">The path of the file to decompress.</param>
        /// <param name="originalExtension">The original extension of the file before it was compressed.</param>
        public void Decompress(string filePath, string originalExtension);
        /// <summary>
        /// Extension used for files compressed by this method.
        /// </summary>
        public string Extension { get; }
    }
}