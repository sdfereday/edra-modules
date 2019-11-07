using UnityEngine;
using System.Collections;

public class DoHeal : AState
{
    private Color SpriteColour;
    private FakeActor opponent;

    public DoHeal(MonoBehaviour _monoBehaviour, FakeActor opponent) :
        base(_monoBehaviour)
    {
        Id = "heal";
        SpriteColour = MonoBehaviourRef.GetComponent<SpriteRenderer>().color;
        this.opponent = opponent;
    }

    public override void Enter()
    {
        // Simulate heal animation and return
        IsComplete = false;
        MonoBehaviourRef.StartCoroutine(Wait(Random.Range(1f, 5f)));

        /* Test the hit on opponent (separate to own animation chain), note that this
         * doesn't take in to account if you're healing yourself. So may want to consider that.
         * 
         * UPDATE: Bug identified. If we're in a 'heal ally' state, and the ally is 'us', then
         * the two states will get confused with each other. So some sort of solution will be
         * required for this. Remember that you've got an animation for spellcasting
         * already happening at this point, so whatever animation happens after that may well need
         * to just be an effect that overlays. Think how FF7 does it for example. The green writing appears
         * over the top of the existing animation triggered. Or, you could do a straight swap
         * out. Experiment a bit.
         * 
         * But evidently at present this doesn't work so well (same goes for attack I'd imagine):
         */
        opponent.FakeHealReaction();
    }

    public override IEnumerator Wait(float waitFor = 0f)
    {
        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = Color.yellow;

        yield return new WaitForSeconds(waitFor);
        IsComplete = true;

        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = SpriteColour;
    }
}
