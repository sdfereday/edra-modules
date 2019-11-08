using UnityEngine;

public abstract class AState
{
    public string Id { get; protected set; }
    public bool IsComplete { get; protected set; }

    public virtual void Enter()
    {
        IsComplete = false;
        Debug.Log("Entered State - " + Id);
    }

    public virtual void Update()
    {
        // ...
    }

    public virtual void Exit()
    {
        Debug.Log("Exited State - " + Id);
    }
}