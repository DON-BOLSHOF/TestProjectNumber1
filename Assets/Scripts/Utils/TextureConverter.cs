using UnityEngine;

namespace Utils
{
    public static class TextureConverter
    {
        public static Sprite ConvertTexture(Texture2D texture2D)
        {
            return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(0.5f, 0.5f), 100f);
        }
    }
}