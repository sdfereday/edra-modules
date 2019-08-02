using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using RedPanda.UserInput;

namespace RedPanda.Interaction
{
    [RequireComponent(typeof(ActionManager))]
    public class InteractionTrigger : MonoBehaviour
    {
        [SerializeField]
        public Transform root;
        public Transform interactPosition;
        public float interactRadius;
        public LayerMask selectObjectsToHit;

        private ActionManager ActionManager { get; set; }
        private Collider2D RootCollider { get; set; }
        private Collider2D CurrentCollider { get; set; }

        private void Start()
        {
            ActionManager = GetComponent<ActionManager>();
            RootCollider = root != null ? root.GetComponent<Collider2D>() : GetComponent<Collider2D>();
            CurrentCollider = GetComponent<Collider2D>();
        }

        private List<Collider2D> GetInteractees()
        {
            return Physics2D.OverlapCircleAll(interactPosition.position, interactRadius, selectObjectsToHit)
                .Where(x => x != CurrentCollider && x != RootCollider)
                .OrderBy(x => Vector2.Distance(x.transform.position, transform.position))
                .Where(x => x != null)
                .ToList();
        }

        private Collider2D GetClosestInteractee()
        {
            var interactees = GetInteractees();
            return interactees.Count() > 0 ? interactees[0] : null;
        }

        public void Interact(INPUT_TYPE originInputType)
        {
            var closestInteractee = GetClosestInteractee();
            var interactible = closestInteractee?.GetComponent<IInteractible>();

            if (interactible != null)
            {
                // Perform related actions on player side (trigger carrying, gaining items, global events, etc)
                ActionManager.Act(interactible.GetInteractibleType(), interactible.Transform);

                // Perform related actions on interactible's side (trigger anims, individual data changes, etc)
                if (!ActionManager.ResponseMustFinish)
                    interactible.Use(RootCollider, originInputType);
            }
        }
    }
}