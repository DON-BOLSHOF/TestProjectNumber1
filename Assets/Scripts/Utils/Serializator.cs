using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Utils
{
    public static class Serializator
    {
        public static void SerializeData(string data, string name)
        {
            var binaryFormatter = new BinaryFormatter(); 
            var file = File.Create(Application.persistentDataPath 
                                   + name);

            binaryFormatter.Serialize(file, data);
            file.Close();
        }

        public static string DeserializeData(string name)
        {
            if (!File.Exists(Application.persistentDataPath + name)) return null;
            
            var binaryFormatter = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath 
                                 + name, FileMode.Open);
            
            return (string) binaryFormatter.Deserialize(file);
        }
    }
}