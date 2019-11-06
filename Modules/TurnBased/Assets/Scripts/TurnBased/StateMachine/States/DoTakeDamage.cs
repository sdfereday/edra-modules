using UnityEngine;
using System.Collections;

public class DoTakeDamage : AState
{
    private Color SpriteColour;

    public DoTakeDamage(string _id, MonoBehaviour _monoBehaviour) :
        base(_id, _monoBehaviour)
    {
        SpriteColour = MonoBehaviourRef.GetComponent<SpriteRenderer>().color;
    }

    public override void Enter()
    {
        // Simulate attack animation and return
        IsComplete = false;
        MonoBehaviourRef.StartCoroutine(Wait(UnityEngine.Random.Range(1f, 5f)));

        // .name? Nooooo!
        Debug.Log(MonoBehaviourRef.name + " took damage!");
    }

    public override IEnumerator Wait(float waitFor = 0f)
    {
        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(waitFor);
        IsComplete = true;

        Debug.Log(MonoBehaviourRef.name + " done taking damage.");
        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = SpriteColour;
    }
}
