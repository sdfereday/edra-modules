using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DoHit : AState
{
    private Color SpriteColour;
    private string opponentTag;

    public DoHit(string _id, MonoBehaviour _monoBehaviour, string opponentTag) : 
        base(_id, _monoBehaviour)
    {
        SpriteColour = MonoBehaviourRef.GetComponent<SpriteRenderer>().color;
        this.opponentTag = opponentTag;
    }

    public override void Enter()
    {
        // Simulate attack animation and return
        IsComplete = false;
        MonoBehaviourRef.StartCoroutine(Wait(Random.Range(1f, 5f)));

        /* Naively find a target (again not performant, but we're only testing) */
        List<GameObject> PossibleTargets = GameObject
            .FindGameObjectsWithTag(opponentTag)
            .ToList();
        
        // Test the hit
        PossibleTargets[Random.Range(0, PossibleTargets.Count)]
            .GetComponent<FakeActor>()
            .FakeHitReaction();
    }

    public override IEnumerator Wait(float waitFor = 0f)
    {
        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = Color.yellow;

        yield return new WaitForSeconds(waitFor);
        IsComplete = true;

        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = SpriteColour;
    }
}
