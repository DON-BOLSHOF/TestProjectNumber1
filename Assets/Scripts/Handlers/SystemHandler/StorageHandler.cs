using System;
using UnityEngine;

namespace Handlers.SystemHandler
{
    public sealed class StorageHandler
    {
        public Action<Texture2D, int> OnReadTexture;

        public void SaveTexture(Texture2D texture, int index)
        {
            var byteCode = texture.EncodeToJPG();

            var base64Texture = Convert.ToBase64String(byteCode);

            PlayerPrefs.SetString(index.ToString(), base64Texture);
            PlayerPrefs.Save();
        }

        public Texture2D ReadTextureFromPlayerPrefs (int index)
        {
            var base64Tex = PlayerPrefs.GetString (index.ToString(), null);

            if (string.IsNullOrEmpty(base64Tex)) return null;
            var texByte = Convert.FromBase64String (base64Tex);
            var tex = new Texture2D (2, 2);

            if (tex.LoadImage (texByte)) {
                OnReadTexture?.Invoke(tex, index);
                return tex;
            }

            return null;
        }
    }
}