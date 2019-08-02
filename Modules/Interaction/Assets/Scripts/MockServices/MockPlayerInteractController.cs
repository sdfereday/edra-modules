using UnityEngine;
using RedPanda.Interaction;

namespace RedPanda.MockServices
{
    public class MockPlayerInteractController : MonoBehaviour
    {
        private InteractionManager InteractionManager;

        private void Start()
        {
            InteractionManager = GetComponent<InteractionManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                InteractionManager.Interact(OnInteracted);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractionManager.Cancel(OnCancelled);
            }
        }

        private void OnInteracted (INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            Debug.Log($"{transform.name} interacted with {originTransform.name}.");
        }

        private void OnCancelled(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            Debug.Log($"{transform.name} cancelled its interaction with {originTransform.name}.");
        }
    }
}
