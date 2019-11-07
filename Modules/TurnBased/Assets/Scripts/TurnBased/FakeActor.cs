/* How it's going to work:
    * 1) Overarching FSM for the battle controller will handle things like waiting, checking if everything's done,
    * what happens next in the queue, and queue order.
    * 2) Each 'actor' will handle its own internal state, but, we will be able to read this via interfaces.
    * 3) When a turn appears, we determine who the actor belongs to, and wait for further input or decision.
    * 4) When an opponent is struck or own party member is healed, that process goes off to each affected and comes back
    * as complete when it's done. To make sure this works, we simply need to check if said actor is busy, we don't care why,
    * we just need to know when it's done.
    * 5) So long as all actors are done, we can go to the next queue item.
    * 
    * It's worth noting that for a nicer turn-based system, you have a concept of weight. Not WAIT. WEIGHT. Depending on
    * how much weight said actor has will determine their place in the queue. Don't worry about this right away.
    * If say charging a spell, an atack might land, if that's the case the animator will temporarily
    * blend that damage over the top of it. This will count as us being busy. Charging a spell doesn't
    * count as being busy however. But I won't worry about this either right now. 
    * We could also use that there PlayMaker...
*/
using UnityEngine;

public class FakeActor : MonoBehaviour
{
    public string actorName;
    public FACTION_TYPE factionType;
    public FACTION_TYPE opponentFactionType { get; private set; }

    private FSM fsm;

    // Tried interfacing but it hate's it. Needs more looking in to.
    public bool IsBusy { get { return fsm.WaitingToFinish; } }

    private void Start()
    {
        actorName = transform.name;
        fsm = new FSM();

        /* Assumes that opposites always apply (just avoids assignment confusion later on) */
        opponentFactionType = factionType == FACTION_TYPE.ALLY ? FACTION_TYPE.ENEMY : FACTION_TYPE.ALLY;
    }

    private void Update()
    {
        fsm.Update();
    }

    // Your turn
    public void TakeTurn() =>
        fsm.Push(new DoMakeChoice(this, (BATTLE_ACTION_TYPE actionType, FakeActor target) => {
            switch(actionType)
            {
                case BATTLE_ACTION_TYPE.ATTACK:
                    fsm.Push(new DoHit(this, target));
                    break;
                case BATTLE_ACTION_TYPE.HEAL:
                    fsm.Push(new DoHeal(this, target));
                    break;
            }
        }));

    // If hit, etc
    public void FakeHitReaction() =>
        fsm.Push(new DoTakeDamage(this));

    public void FakeHealReaction() =>
        fsm.Push(new DoTakeHeal(this));

}