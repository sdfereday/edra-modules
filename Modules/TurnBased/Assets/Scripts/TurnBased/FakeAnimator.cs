using UnityEngine;
using System;
using System.Collections;

/* This should be a mock service
 * To avoid coroutines, try this (and a make use of queue again):
 * https://answers.unity.com/questions/351420/simple-timer-1.html
 * And the FSM.
 * 
 * None of these will work unless invoked. But they're not staying
 * anyway, this is the next part to fix.
 */
public class FakeAnimator : MonoBehaviour
{
    private Color SpriteColour;

    private void Start()
    {
        SpriteColour = GetComponent<SpriteRenderer>().color;
    }

    public IEnumerator PlayHeal(float waitFor = 1f, Action onComplete = null)
    {
        GetComponent<SpriteRenderer>().color = Color.cyan;

        yield return new WaitForSeconds(waitFor);
        
        GetComponent<SpriteRenderer>().color = SpriteColour;
    }

    public IEnumerator PlayHit(float waitFor = 1f, Action onComplete = null)
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;

        yield return new WaitForSeconds(waitFor);

        GetComponent<SpriteRenderer>().color = SpriteColour;
    }

    public IEnumerator PlayTakeHeal(float waitFor = 1f, Action onComplete = null)
    {
        Debug.Log(gameObject.name + " took heals.");
        GetComponent<SpriteRenderer>().color = Color.blue;

        yield return new WaitForSeconds(waitFor);

        GetComponent<SpriteRenderer>().color = SpriteColour;
        Debug.Log(gameObject.name + " done taking heals.");
    }

    public IEnumerator PlayTakeDamage(float waitFor = 1f, Action onComplete = null)
    {
        Debug.Log(gameObject.name + " took dmaage.");
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(waitFor);

        GetComponent<SpriteRenderer>().color = SpriteColour;
        Debug.Log(gameObject.name + " done taking damage.");
    }

    public IEnumerator PlayThinking(float waitFor = 1f, Action onComplete = null)
    {
        Debug.Log(gameObject.name + " is thinking.");
        GetComponent<SpriteRenderer>().color = Color.green;

        yield return new WaitForSeconds(waitFor);

        GetComponent<SpriteRenderer>().color = SpriteColour;
        Debug.Log(gameObject.name + " has decided.");

        onComplete?.Invoke();
    }
}
