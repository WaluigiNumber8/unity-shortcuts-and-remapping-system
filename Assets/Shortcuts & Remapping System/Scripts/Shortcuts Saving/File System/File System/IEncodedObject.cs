
namespace RedRats.Systems.FileSystem
{
    /// <summary>
    /// A base for all objects, encoded for external storing.
    /// <typeparam name="T">The Unity-readable form of the object.</typeparam>
    /// </summary>
    public interface IEncodedObject<out T>
    {
        /// <summary>
        /// Decodes the object, so that it can be read by Unity.
        /// </summary>
        /// <returns>The data in a Unity-readable form.</returns>
        public T Decode();
    }
}