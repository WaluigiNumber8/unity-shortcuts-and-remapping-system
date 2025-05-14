using System.IO;
using System.IO.Compression;
using RedRats.Core;

namespace RedRats.Systems.FileSystem.Compression
{
    /// <summary>
    /// Compresses/Decompresses files into .dfl files.
    /// Uses <see cref="DeflateStream"/> to accomplish this.
    /// </summary>
    public class DFLCompression : ICompressionSystem
    {
        public const string COMPRESSED_EXTENSION = ".dfl";

        public void Compress(string filePath)
        {
            PreconditionsIO.PathNotContainsInvalidCharacters(filePath);
            
            string compressedPath = Path.ChangeExtension(filePath, COMPRESSED_EXTENSION);
            using FileStream originalFileStream = File.Open(filePath, FileMode.Open);
            using FileStream compressedFileStream = File.Create(compressedPath);
            using DeflateStream compressor = new(compressedFileStream, CompressionMode.Compress);
            originalFileStream.CopyTo(compressor);
        }

        public void Decompress(string filePath, string originalExtension)
        {
            PreconditionsIO.PathNotContainsInvalidCharacters(filePath);
            
            string decompressedPath = Path.ChangeExtension(filePath, originalExtension);
            using FileStream compressedFileStream = File.Open(filePath, FileMode.Open);
            using FileStream outputFileStream = File.Create(decompressedPath);
            using DeflateStream decompressor = new(compressedFileStream, CompressionMode.Decompress);
            decompressor.CopyTo(outputFileStream);
        }

        public string Extension { get => COMPRESSED_EXTENSION; }
    }
}