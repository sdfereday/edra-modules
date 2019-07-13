using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using RedPanda.Global;

namespace RedPanda.Storage
{
    public class GlobalContext : MonoBehaviour
    {
        public List<SceneContextModel> LoadedScenes;

        private void Awake()
        {
            LoadedScenes = new List<SceneContextModel>();
            List<SceneContextModel> loadedData = SaveDataManager.LoadData<List<SceneContextModel>>(DataConsts.SCENE_DATA_FILE);

            if (loadedData != null ? loadedData.GroupBy(x => x.sceneName)
                  .Where(g => g.Count() > 1)
                  .Select(y => y.Key)
                  .Count() > 0 : false)
            {
                throw new System.Exception(ErrorConsts.DUPLICATE_SCENE_MODEL);
            }

            LoadedScenes = loadedData ?? new List<SceneContextModel>();
        }

        public SceneContextModel LoadSceneData(string sceneName)
        {
            return LoadedScenes
                .Where(x => x.sceneName == sceneName)
                .FirstOrDefault();
        }

        public void UpdateSceneData(SceneContextModel sceneModel)
        {
            var existingSceneData = LoadedScenes.Where(x => x.sceneName == sceneModel.sceneName)
                .FirstOrDefault();

            if (existingSceneData != null)
            {
                existingSceneData.boolStates = sceneModel.boolStates;
            }
            else
            {
                LoadedScenes.Add(sceneModel);
            }

            SaveDataManager.SaveData(LoadedScenes, DataConsts.SCENE_DATA_FILE);
        }
    }
}