using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Builds <see cref="Sprite"/>s
    /// </summary>
    public class SpriteBuilder
    {
        private Texture2D tex = null;
        private int pixelsPerUnit = 16;
        private Vector2 pivot = new Vector2(0.5f, 0.5f);
        
        public SpriteBuilder WithTexture(Texture2D tex)
        {
            this.tex = tex;
            this.tex.Apply();
            return this;
        }
        
        /// <summary>
        /// With Pixels per Unit.
        /// </summary>
        public SpriteBuilder WithPPU(int pixelsPerUnit)
        {
            this.pixelsPerUnit = pixelsPerUnit;
            return this;
        }
        
        public SpriteBuilder WithPivot(Vector2 pivot)
        {
            this.pivot = pivot;
            return this;
        }

        public SpriteBuilder WithSingleColorTexture(Color color, int width, int height)
        {
            WithTexture(new TextureBuilder()
                .WithSize(width, height)
                .WithColor(color)
                .Build());
            return this;
        }
        
        public SpriteBuilder WithEmptyTexture(int width, int height) => WithSingleColorTexture(new Color(0, 0, 0, 1), width, height);

        public Sprite Build()
        {
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), pivot, pixelsPerUnit);
        }
    }
}