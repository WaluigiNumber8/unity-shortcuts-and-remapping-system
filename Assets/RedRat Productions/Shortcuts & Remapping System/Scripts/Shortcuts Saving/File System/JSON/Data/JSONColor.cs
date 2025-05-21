using UnityEngine;

namespace RedRats.Systems.FileSystem.JSON.Serialization
{
    /// <summary>
    /// The <see cref="Color"/> object converted to JSON-understandable format.
    /// </summary>
    [System.Serializable]
    public class JSONColor : IEncodedObject<Color>
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public JSONColor(Color color) : this(color.r, color.g, color.b, color.a) { }
        public JSONColor(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        /// <summary>
        /// Turns a serialized color into a unity color format.
        /// </summary>
        /// <returns>A color in a readable form.</returns>
        public Color Decode()
        {
            Color color = new Color(r, g, b, a);
            return color;
        }

    }
}