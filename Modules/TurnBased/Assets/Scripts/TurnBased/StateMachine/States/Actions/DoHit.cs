using UnityEngine;

public class DoHit : AState
{
    private FakeActor target;

    public DoHit(MonoBehaviour _monoBehaviour, FakeActor target) : 
        base(_monoBehaviour)
    {
        Id = "attack";
        this.target = target;
    }

    public override void Enter()
    {
        IsComplete = false;

        MonoBehaviourRef.GetComponent<FakeAnimator>().PlayHit();

        target.FakeHitReaction();
    }
}
