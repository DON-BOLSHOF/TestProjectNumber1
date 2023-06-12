using System;
using UnityEngine;

namespace Handlers.SystemHandler
{
    public sealed class StorageHandler
    {
        public void SaveTexture(Texture2D texture, string name)
        {
            var byteCode = texture.EncodeToJPG();

            var base64Texture = Convert.ToBase64String(byteCode);

            PlayerPrefs.SetString(name, base64Texture);
            PlayerPrefs.Save();
        }

        public Texture2D ReadTextureFromPlayerPrefs (string name)
        {
            var base64Tex = PlayerPrefs.GetString (name, null);

            if (string.IsNullOrEmpty(base64Tex)) return null;
            var texByte = Convert.FromBase64String (base64Tex);
            var tex = new Texture2D (2, 2);

            if (tex.LoadImage (texByte)) {
                return tex;
            }

            return null;
        }
    }
}