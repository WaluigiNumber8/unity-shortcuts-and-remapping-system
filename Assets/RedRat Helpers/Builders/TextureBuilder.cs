using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Builds <see cref="Texture2D"/>s.
    /// </summary>
    public class TextureBuilder
    {
        private int width = 0;
        private int height = 0;
        private Color color = Color.clear;
        private FilterMode filterMode = FilterMode.Point;
        
        public TextureBuilder WithSize(Vector2Int size) => WithSize(size.x, size.y);
        public TextureBuilder WithSize(int size) => WithSize(size, size);
        public TextureBuilder WithSize(int width, int height)
        {
            this.width = width;
            this.height = height;
            return this;
        }
        
        public TextureBuilder WithColor(Color color)
        {
            this.color = color;
            return this;
        }
        
        public TextureBuilder WithFilterMode(FilterMode filterMode)
        {
            this.filterMode = filterMode;
            return this;
        }
        
        public Texture2D Build()
        {
            Texture2D tex = new(width, height);
            tex.filterMode = filterMode;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tex.SetPixel(x, y, color);
                }
            }
            tex.Apply();
            return tex;
        }
    }
}