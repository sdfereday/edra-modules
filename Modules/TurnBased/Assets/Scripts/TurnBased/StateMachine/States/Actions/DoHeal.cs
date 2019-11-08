using UnityEngine;

public class DoHeal : AState
{
    private FakeActor target;

    public DoHeal(MonoBehaviour _monoBehaviour, FakeActor target) :
        base(_monoBehaviour)
    {
        Id = "heal";
        this.target = target;
    }

    public override void Enter()
    {
        IsComplete = false;

        MonoBehaviourRef.GetComponent<FakeAnimator>().PlayHeal();

        target.FakeHealReaction();

        /* Possible solution to below: Don't use a coroutine to simulate animations.
         * Use some sort of separate service that can be pushed to, that way, stacks
         * can be achieved. */
        // MonoBehaviourRef.StartCoroutine(Wait(Random.Range(1f, 5f)));

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
         * 
         * To test a fix, we'll 'assume' that the heal function is a different animation. In reality
         * you'd just overlay an effect. You might do this with say, attacking, but that's not needed
         * right now if at all (and again could just overlay another effect). Anyway, fixing this means
         * we have fixed it in other edge cases.
         * 
         * If we are the target and we run the following, we expect the state to interrupt whatever
         * current animation we have going on here. Again, as an example case to prove the fix.
         * 
         * The same problem exists if we get attacked and our spell cast carries over more than one
         * turn. But, I don't feel this is needed at this stage. That's a feature for later, however
         * the reasoning is the same. You must stack an animation over the top, then continue after that.
         */
    }
}
