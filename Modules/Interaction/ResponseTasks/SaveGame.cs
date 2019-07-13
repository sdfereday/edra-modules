using UnityEngine;

namespace RedPanda.Interaction
{
    public class SaveGame : MonoBehaviour, IResponseTask
    {
        public delegate void SaveAction();
        public static event SaveAction OnSaveSignal;

        public bool IsActive { get; private set; }
        public RESPONSE_TYPE ResponseType
        {
            get
            {
                return RESPONSE_TYPE.DEFAULT;
            }
        }

        public void Run(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            Debug.Log("DataIO was instructed to save the game.");
            OnSaveSignal?.Invoke();
        }

        public void Complete()
        {
            // ...
        }

        public void Next()
        {
            // ...
        }
    }
}