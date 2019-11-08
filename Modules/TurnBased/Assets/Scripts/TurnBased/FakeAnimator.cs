using UnityEngine;
using System;

/* This should be a mock service
 * To avoid coroutines, try this (and a make use of queue again):
 * https://answers.unity.com/questions/351420/simple-timer-1.html
 * And the FSM.
 * 
 * None of these will work unless invoked. But they're not staying
 * anyway, this is the next part to fix.
 * 
 * Some callback hell starting here... not a good idea.
 */
public class FakeAnimator : MonoBehaviour
{
    private FSM animFsm;
    private Color SpriteColour;

    private void Start()
    {
        SpriteColour = GetComponent<SpriteRenderer>().color;
        animFsm = new FSM();
    }

    private void Update()
    {
        animFsm.Update();
    }

    public void PlayHeal(Action onComplete = null)
    {
        GetComponent<SpriteRenderer>().color = Color.cyan;

        animFsm.Push(new TestAnimation(() =>
        {
            Debug.Log("Exit");
            GetComponent<SpriteRenderer>().color = SpriteColour;

            onComplete?.Invoke();
        }, "HealIt"));
    }

    public void PlayHit(Action onComplete = null)
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;

        animFsm.Push(new TestAnimation(() =>
        {
            Debug.Log("Exit");
            GetComponent<SpriteRenderer>().color = SpriteColour;

            onComplete?.Invoke();
        }, "HitIt"));
    }

    public void PlayTakeHeal(Action onComplete = null)
    {
        Debug.Log(gameObject.name + " took heals.");
        GetComponent<SpriteRenderer>().color = Color.blue;

        animFsm.Push(new TestAnimation(() =>
        {
            GetComponent<SpriteRenderer>().color = SpriteColour;
            Debug.Log(gameObject.name + " done taking heals.");

            onComplete?.Invoke();
        }));
      
    }

    public void PlayTakeDamage(Action onComplete = null)
    {
        Debug.Log(gameObject.name + " took damage.");
        GetComponent<SpriteRenderer>().color = Color.red;

        animFsm.Push(new TestAnimation(() =>
        {
            GetComponent<SpriteRenderer>().color = SpriteColour;
            Debug.Log(gameObject.name + " done taking damage.");

            onComplete?.Invoke();
        }));
    }

    public void PlayThinking(Action onComplete = null)
    {
        Debug.Log(gameObject.name + " is thinking.");
        GetComponent<SpriteRenderer>().color = Color.green;

        animFsm.Push(new TestAnimation(() =>
        {
            GetComponent<SpriteRenderer>().color = SpriteColour;
            Debug.Log(gameObject.name + " has decided.");

            onComplete?.Invoke();
        }));
    }
}
;