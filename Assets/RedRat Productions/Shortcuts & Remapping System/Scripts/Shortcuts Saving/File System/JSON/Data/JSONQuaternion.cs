using UnityEngine;

namespace RedRats.Systems.FileSystem.JSON.Serialization
{
    [System.Serializable]
    public class JSONQuaternion : IEncodedObject<Quaternion>
    {
        public float x, y, z, w;

        public JSONQuaternion(Quaternion quaternion) : this(quaternion.x, quaternion.y, quaternion.z, quaternion.w) {}
        public JSONQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Returns the quaternion in a Unity readable format.
        /// </summary>
        /// <returns></returns>
        public Quaternion Decode()
        {
            return new Quaternion(x, y, z, w);
        }
        
    }
}