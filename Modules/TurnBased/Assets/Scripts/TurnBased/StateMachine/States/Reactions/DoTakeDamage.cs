using UnityEngine;

public class DoTakeDamage : AState
{
    private Color SpriteColour;

    public DoTakeDamage(MonoBehaviour _monoBehaviour) :
        base(_monoBehaviour)
    {
        Id = "takeDamage";
        SpriteColour = MonoBehaviourRef.GetComponent<SpriteRenderer>().color;
    }

    public override void Enter()
    {
        IsComplete = false;

        MonoBehaviourRef.GetComponent<FakeAnimator>().PlayTakeDamage();
    }
}
