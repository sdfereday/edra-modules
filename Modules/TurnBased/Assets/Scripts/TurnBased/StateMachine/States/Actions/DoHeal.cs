public class DoHeal : AState
{
    private FakeAnimator anim;
    private FakeActor target;

    public DoHeal(FakeAnimator anim, FakeActor target)    
    {
        Id = "heal";
        this.anim = anim;
        this.target = target;
    }

    public override void Enter()
    {
        IsComplete = false;

        anim.PlayHeal(() => {
            IsComplete = true;
        });

        target.FakeHealReaction();
    }
}
