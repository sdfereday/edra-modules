using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace RedPanda.Storage
{
    public static class SaveDataManager
    {
        public static string UserDirectory = Path.Combine(Application.persistentDataPath, DATA_SLOT.SLOT_0.ToString());

        private static void CreateIfNone(string _dir, string _fileLocation)
        {
            Directory.CreateDirectory(_dir);

            using (StreamWriter streamWriter = File.CreateText(_fileLocation))
            {
                streamWriter.Write("[]");
            }
        }

        private static T DoLoad<T>(string fullPath)
        {
            using (StreamReader streamReader = File.OpenText(fullPath))
            {
                string jsonString = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
        }

        public static void SaveData<T>(T data, string fileName)
        {
            string fileWithDir = string.Format("{0}/{1}", UserDirectory, fileName);

            if (!File.Exists(fileWithDir))
                CreateIfNone(UserDirectory, fileWithDir);

            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
            using (StreamWriter streamWriter = File.CreateText(fileWithDir))
            {
                streamWriter.Write(jsonString);
            }
        }

        public static T LoadData<T>(string fileName)
        {
            string fileWithDir = string.Format("{0}/{1}", UserDirectory, fileName);

            if (!File.Exists(fileWithDir))
                CreateIfNone(UserDirectory, fileWithDir);

            return DoLoad<T>(fileWithDir);
        }

        public static T LoadAssetData<T>(string path)
        {
            // TODO: Make sure not to rely on resource folder, it should be avoided in production.
            Debug.LogWarning("Loading from resources isn't a good idea in production!");
            Debug.LogWarning(path);
            string jsonString = Resources.Load<TextAsset>(path).text;
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}