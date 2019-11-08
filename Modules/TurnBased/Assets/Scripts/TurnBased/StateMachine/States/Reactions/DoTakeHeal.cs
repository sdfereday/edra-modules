public class DoTakeHeal : AState
{
    private FakeAnimator anim;

    public DoTakeHeal(FakeAnimator anim)
    {
        Id = "takeHeal";
        this.anim = anim;
    }

    public override void Enter()
    {
        IsComplete = false;
        anim.PlayTakeHeal(() => {
            IsComplete = true;
        });
    }
}
