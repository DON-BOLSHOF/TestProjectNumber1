using System;
using UnityEngine;
using Utils;

namespace Handlers.SystemHandler
{
    public sealed class StorageHandler
    {
        public Action<Texture2D, int> OnReadTexture;

        public void SaveTexture(Texture2D texture, int index)
        {
            var byteCode = texture.EncodeToJPG();
            var base64Texture = Convert.ToBase64String(byteCode);

            Serializator.SerializeData(base64Texture, index.ToString());
        }

        public Texture2D ReadTextureFromPlayerPrefs (int index)
        {
            var base64Tex = Serializator.DeserializeData(index.ToString());
            
            if (string.IsNullOrEmpty(base64Tex)) return null;
                
            var texturesBytes = Convert.FromBase64String (base64Tex);
            var texture2D = new Texture2D (2, 2);

            if (!texture2D.LoadImage(texturesBytes)) return null;
            
            OnReadTexture?.Invoke(texture2D, index);
            return texture2D;
        }
    }
}