using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using RedPanda.Interaction;

namespace RedPanda.Storage
{
    public class SceneContext : MonoBehaviour
    {
        public delegate void SceneDataLoadedAction();
        public static event SceneDataLoadedAction OnSceneDataLoaded;

        public string SceneName;
        public List<BoolSaveState> MapEntityStates;

        private GlobalContext GlobalContextData;

        private void Awake()
        {
            GlobalContextData = GameObject.FindGameObjectWithTag(DataConsts.GLOBAL_CONTEXT_TAG)
                .GetComponent<GlobalContext>();

            SceneName = SceneManager.GetActiveScene().name;
            MapEntityStates.ForEach(x => x.State = false);

            var loadedSceneState = GlobalContextData.LoadSceneData(SceneName);

            if (loadedSceneState != null)
            {
                loadedSceneState.boolStates.ForEach(x =>
                {
                    var boolState = MapEntityStates.FirstOrDefault(ent => ent.name == x.name);

                    if (boolState != null)
                    {
                        boolState.State = x.state;
                    }
                });

                OnSceneDataLoaded?.Invoke();
            }
        }

        private void OnEnable() => SaveGame.OnSaveSignal += SaveToDisk;
        private void OnDisable() => SaveGame.OnSaveSignal -= SaveToDisk;

        private void SaveToDisk()
        {
            var payload = new SceneContextModel()
            {
                sceneName = SceneManager.GetActiveScene().name,
                boolStates = MapEntityStates.Select(x =>
                {
                    return new BoolStateModel()
                    {
                        name = x.name,
                        state = x.State
                    };
                }).ToList()
            };

            GlobalContextData.UpdateSceneData(payload);
        }
    }
}