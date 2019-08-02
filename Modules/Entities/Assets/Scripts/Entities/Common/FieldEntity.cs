using UnityEngine;
using RedPanda.Storage;

namespace RedPanda.Entities
{
    public abstract class FieldEntity : MonoBehaviour
    {
        [SerializeField]
        private BoolSaveState _BooleanObject;
        //private void OnEnable() => SceneContext.OnSceneDataLoaded += HandleSceneDataLoaded;
        //private void OnDisable() => SceneContext.OnSceneDataLoaded -= HandleSceneDataLoaded;

        protected IRemotePrefab RemotePrefabInstance;
        protected bool IsTruthy => _BooleanObject != null ? _BooleanObject.State : false;

        // TODO: Why does the event only work sometimes?
        private void Start()
        {
            Debug.Log(this.name + " was asserted.");
            RemotePrefabInstance = GetComponent<IRemotePrefab>();
            OnAssert(IsTruthy);
        }

        //protected void UpdateBoolState(bool state) => UseState.UpdateBoolState(state);
        protected void UpdateBoolState(bool _state)
        {
            if (_BooleanObject != null)
                _BooleanObject.State = _state;
        }

        public virtual void OnAssert(bool truthy) { }
    }
}