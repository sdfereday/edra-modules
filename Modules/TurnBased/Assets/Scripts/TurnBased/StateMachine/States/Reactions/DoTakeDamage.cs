public class DoTakeDamage : AState
{
    private FakeAnimator anim;

    public DoTakeDamage(FakeAnimator anim)
    {
        Id = "takeDamage";
        this.anim = anim;
    }

    public override void Enter()
    {
        IsComplete = false;
        anim.PlayTakeDamage(() => {
            IsComplete = true;
        });
    }
}
