using UnityEngine;
using System.Collections;

public abstract class AState
{
    public string id;
    public bool IsComplete { get; protected set; }

    // Should really not do this
    public MonoBehaviour MonoBehaviourRef { get; set; }

    public AState(string _id, MonoBehaviour _monoBehaviour)
    {
        id = _id;
        MonoBehaviourRef = _monoBehaviour;
    }

    public virtual void Enter()
    {
        IsComplete = false;
        MonoBehaviourRef.StartCoroutine(Wait(Random.Range(1f, 5f)));

        Debug.Log("Entered State - " + id);
    }

    public void Update()
    {
        // ...
    }

    public void Exit()
    {
        Debug.Log("Exited State - " + id);
    }

    public virtual IEnumerator Wait(float waitFor = 0f)
    {
        yield return new WaitForSeconds(waitFor);
        IsComplete = true;
    }
}