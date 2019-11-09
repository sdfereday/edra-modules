public class DoHit : AState
{
    private FakeAnimator anim;
    private FakeActor target;

    public DoHit(FakeAnimator anim, FakeActor target) 
    {
        Id = "attack";
        this.anim = anim;
        this.target = target;
    }

    public override void Enter()
    {
        IsComplete = false;

        anim.PlayHit(() => {
            IsComplete = true;
        });
        target.FakeHitReaction();
    }
}
