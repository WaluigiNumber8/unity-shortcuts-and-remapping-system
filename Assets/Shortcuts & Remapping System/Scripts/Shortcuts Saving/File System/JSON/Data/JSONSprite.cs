using RedRats.Core;
using UnityEngine;

namespace RedRats.Systems.FileSystem.JSON.Serialization
{
    /// <summary>
    /// The <see cref="Sprite"/> object converted to JSON-understandable format.
    /// </summary>
    [System.Serializable]
    public class JSONSprite : IEncodedObject<Sprite>
    {
        public string name;
        public float rectX;
        public float rectY;
        public float rectWidth;
        public float rectHeight;
        public float pivotX;
        public float pivotY;
        public int textureWidth;
        public int textureHeight;
        public byte[] textureBytes;

        public JSONSprite(Sprite sprite)
        {
            Sprite spriteToUse = (sprite == null) ? new SpriteBuilder().WithEmptyTexture(16, 16).WithPPU(16).Build() : sprite;
            name = spriteToUse.name;
            rectX = spriteToUse.rect.x;
            rectY = spriteToUse.rect.y;
            rectWidth = spriteToUse.rect.width;
            rectHeight = spriteToUse.rect.height;
            pivotX = spriteToUse.pivot.x;
            pivotY = spriteToUse.pivot.y;
            textureWidth = spriteToUse.texture.width;
            textureHeight = spriteToUse.texture.height;
            textureBytes = spriteToUse.texture.EncodeToPNG();
        }

        public JSONSprite(string name, float rectX, float rectY, float rectWidth, float rectHeight, float pivotX, float pivotY, int textureWidth, int textureHeight, byte[] textureBytes)
        {
            this.name = name;
            this.rectX = rectX;
            this.rectY = rectY;
            this.rectWidth = rectWidth;
            this.rectHeight = rectHeight;
            this.pivotX = pivotX;
            this.pivotY = pivotY;
            this.textureWidth = textureWidth;
            this.textureHeight = textureHeight;
            this.textureBytes = textureBytes;
        }

        /// <summary>
        /// Decodes the sprite and returns in the sprite format.
        /// </summary>
        /// <returns>A Sprite that Unity can use.</returns>
        public Sprite Decode()
        {
            Texture2D texture = new(textureWidth, textureHeight);
            texture.filterMode = FilterMode.Point;
            texture.LoadImage(textureBytes);
            Sprite sprite = Sprite.Create(texture,
                                          new Rect(rectX, rectY, rectWidth, rectHeight),
                                          new Vector2(pivotX / rectWidth, pivotY / rectHeight),
                                          16);
            sprite.name = name;
            return sprite;
        }

    }
}