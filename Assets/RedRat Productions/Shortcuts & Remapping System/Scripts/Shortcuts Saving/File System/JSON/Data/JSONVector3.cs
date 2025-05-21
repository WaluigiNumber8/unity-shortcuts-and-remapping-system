using UnityEngine;

namespace RedRats.Systems.FileSystem.JSON.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="Vector3"/> struct.
    /// </summary>
    [System.Serializable]
    public class JSONVector3 : IEncodedObject<Vector3>
    {
        public float x, y, z;

        public JSONVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public JSONVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        /// <summary>
        /// Returns the Vector in a Unity acceptable format.
        /// </summary>
        /// <returns>The Vector3.</returns>
        public Vector3 Decode()
        {
            return new Vector3(x, y, z);
        }
    }
}