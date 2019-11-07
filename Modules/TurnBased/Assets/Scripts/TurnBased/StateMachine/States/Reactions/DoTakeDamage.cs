﻿using UnityEngine;
using System.Collections;

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
        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = SpriteColour;
        IsComplete = true;

        Debug.Log(MonoBehaviourRef.name + " done taking damage.");
    }
}