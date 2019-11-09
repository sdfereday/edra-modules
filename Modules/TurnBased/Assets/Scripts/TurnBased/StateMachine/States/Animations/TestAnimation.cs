using UnityEngine;
using System;

public class TestAnimation : AState
{
    public override bool Stackable => true;
    public float targetTime = 1f;
    private string Tag;
    private Action onComplete;

    public TestAnimation(Action onComplete = null, string tag = "")
    {
        Id = "testAnimation";
        this.Tag = tag;
        this.onComplete = onComplete;
    }

    public override void Enter()
    {
        IsComplete = false;
    }

    public override void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            IsComplete = true;
        }
    }

    public override void Exit()
    {
        if (Tag.Length > 0)
        {
            Debug.Log("Anim exited:");
            Debug.Log(Tag);
        }

        onComplete?.Invoke();
    }
}
