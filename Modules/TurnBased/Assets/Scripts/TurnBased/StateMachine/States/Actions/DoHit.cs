using UnityEngine;
using System.Collections;

public class DoHit : AState
{
    private Color SpriteColour;
    private FakeActor opponent;

    public DoHit(MonoBehaviour _monoBehaviour, FakeActor opponent) : 
        base(_monoBehaviour)
    {
        Id = "attack";
        SpriteColour = MonoBehaviourRef.GetComponent<SpriteRenderer>().color;
        this.opponent = opponent;
    }

    public override void Enter()
    {
        // Simulate attack animation and return
        IsComplete = false;
        MonoBehaviourRef.StartCoroutine(Wait(Random.Range(1f, 5f)));

        // Test the hit on opponent (separate to own animation chain)
        opponent.FakeHitReaction();
    }

    public override IEnumerator Wait(float waitFor = 0f)
    {
        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = Color.yellow;

        yield return new WaitForSeconds(waitFor);
        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = SpriteColour;
        IsComplete = true;
    }
}
