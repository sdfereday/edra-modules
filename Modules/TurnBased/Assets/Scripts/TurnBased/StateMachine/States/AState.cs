using UnityEngine;
using System.Collections;

public abstract class AState
{
    public string Id { get; protected set; }
    public bool IsComplete { get; protected set; }

    // Should really not do this
    public MonoBehaviour MonoBehaviourRef { get; set; }

    public AState(MonoBehaviour _monoBehaviour)
    {
        MonoBehaviourRef = _monoBehaviour;
    }

    public virtual void Enter()
    {
        IsComplete = false;
        MonoBehaviourRef.StartCoroutine(Wait(Random.Range(1f, 5f)));

        Debug.Log("Entered State - " + Id);
    }

    public void Update()
    {
        // ...
    }

    public void Exit()
    {
        Debug.Log("Exited State - " + Id);
    }

    public virtual IEnumerator Wait(float waitFor = 0f)
    {
        yield return new WaitForSeconds(waitFor);
        IsComplete = true;
    }
}