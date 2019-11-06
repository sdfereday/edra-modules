using System;
using UnityEngine;
using System.Collections;

public class DoMakeChoice : AState
{
    private Color SpriteColour;
    private Action<string> OnChoice;

    public DoMakeChoice(string _id, MonoBehaviour _monoBehaviour, Action<string> OnChoice = null) :
        base(_id, _monoBehaviour)
    {
        this.OnChoice = OnChoice;
        SpriteColour = MonoBehaviourRef.GetComponent<SpriteRenderer>().color;
    }

    public override void Enter()
    {
        IsComplete = false;
        MonoBehaviourRef.StartCoroutine(Wait(UnityEngine.Random.Range(1f, 5f)));

        Debug.Log("Making A Choice...");
    }

    public override IEnumerator Wait(float waitFor = 0f)
    {
        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = Color.green;

        yield return new WaitForSeconds(waitFor);
        IsComplete = true;

        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = SpriteColour;

        /* Assumes we pressed attack for example */
        if (OnChoice != null)
        {
            Debug.Log("Decided To Attack!");
            OnChoice("attack");
        }
    }
}
