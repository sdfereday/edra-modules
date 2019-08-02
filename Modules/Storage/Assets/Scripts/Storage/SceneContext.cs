using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

namespace RedPanda.Storage
{
    public class SceneContext : MonoBehaviour
    {
        public delegate void SceneDataLoadedAction();
        public static event SceneDataLoadedAction OnSceneDataLoaded;
        public string SceneName;
        public List<BoolSaveState> MapEntityBoolStates;
        public List<QuantitySaveState> MapEntityQtyStates;

        private GlobalDataContext GlobalDataContext;

        private void Awake()
        {
            GlobalDataContext = FindObjectOfType<GlobalDataContext>();
            EntityWithState[] existingWithState = FindObjectsOfType<EntityWithState>();

            // Check for objects in scene that have persistent state
            MapEntityBoolStates = existingWithState.Length > 0 ?
                existingWithState
                    .Select(x => x.BoolStateObject)
                    .Distinct()
                    .ToList() : new List<BoolSaveState>();

            MapEntityBoolStates.ForEach(x => x.State = false);

            MapEntityQtyStates = existingWithState.Length > 0 ?
                existingWithState
                    .Select(x => x.QtyStateObject)
                    .Distinct()
                    .ToList() : new List<QuantitySaveState>();

            MapEntityQtyStates.ForEach(x => x.State = 0);

            // Load in and bootstrap
            SceneName = SceneManager.GetActiveScene().name;
            var loadedSceneState = GlobalDataContext.LoadSceneData(SceneName);

            if (loadedSceneState != null)
            {
                loadedSceneState.boolStates.ForEach(x =>
                {
                    var boolState = MapEntityBoolStates.FirstOrDefault(ent => ent.name == x.name);

                    if (boolState != null)
                    {
                        boolState.State = x.state;
                    }
                });

                loadedSceneState.quantityStates.ForEach(x =>
                {
                    var qtyState = MapEntityQtyStates.FirstOrDefault(ent => ent.name == x.name);

                    if (qtyState != null)
                    {
                        qtyState.State = x.state;
                    }
                });

                OnSceneDataLoaded?.Invoke();
            }
        }

        public void SaveToDisk()
        {
            var payload = new SceneContextModel()
            {
                sceneName = SceneManager.GetActiveScene().name,
                boolStates = MapEntityBoolStates.Select(x =>
                {
                    return new BoolStateModel()
                    {
                        name = x.name,
                        state = x.State
                    };
                })
                .Distinct()
                .ToList(),
                quantityStates = MapEntityQtyStates.Select(x =>
                {
                    return new QuantityStateModel()
                    {
                        name = x.name,
                        state = x.State
                    };
                })
                .Distinct()
                .ToList()
            };

            GlobalDataContext.UpdateSceneData(payload);
        }
    }
}