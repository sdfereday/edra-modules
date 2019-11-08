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