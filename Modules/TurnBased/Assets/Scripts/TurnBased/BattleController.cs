using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace RedPanda.TurnBased
{
    public class BattleController : MonoBehaviour
    {
        private void OnSceneLoadComplete() => Init();
        private void OnSceneLoadStarted() => Init();
        private List<FakeActor> Actors;
        private FSM fsm;
        private Queue<FakeActor> actorsQueue = new Queue<FakeActor>();

        private bool started = false;

        private void Start()
        {
            fsm = new FSM();

            // Careful with this, it's quite performance-heavy
            Actors = FindObjectsOfType<FakeActor>().ToList();
            Actors.ForEach(x => actorsQueue.Enqueue(x));
        }

        private void Init()
        {
            started = true;
        }

        private void Update()
        {
            if (!started) return;

            fsm.Update();

            // Can this 'not' be done in the update? Might be more performent that way.
            /* If nobody is busy... */
            if (Actors.All(x => !x.IsBusy) && actorsQueue.Count > 0)
            {
                Debug.Log("DeQueue:");
                /* -> Get the next actor in the queue and tell it to decide (any states from here are out of the scope of this bit). */
                actorsQueue.Dequeue().TakeTurn();

                /* --> Requeue them again */
                if (actorsQueue.Count == 0)
                {
                    Actors.ForEach(x => actorsQueue.Enqueue(x));
                }
            }
        }

        public void StartBattle() => Init();
    }
}