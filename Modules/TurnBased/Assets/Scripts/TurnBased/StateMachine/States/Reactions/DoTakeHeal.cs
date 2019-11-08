using UnityEngine;

public class DoTakeHeal : AState
{
    private Color SpriteColour;

    public DoTakeHeal(MonoBehaviour _monoBehaviour) :
        base(_monoBehaviour)
    {
        Id = "takeHeal";
        SpriteColour = MonoBehaviourRef.GetComponent<SpriteRenderer>().color;
    }

    public override void Enter()
    {
        IsComplete = false;
        
        MonoBehaviourRef.GetComponent<FakeAnimator>().PlayTakeHeal();
    }
}
