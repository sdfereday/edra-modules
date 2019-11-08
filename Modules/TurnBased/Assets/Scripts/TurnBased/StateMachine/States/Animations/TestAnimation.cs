using UnityEngine;
using System;

public class TestAnimation : AState
{
    public float targetTime = 3f;
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

        if (Tag.Length > 0)
        {
            Debug.Log(Tag);
        }
    }

    public override void Update()
    {
        if (Tag.Length > 0)
        {
            Debug.Log(Tag);
        }

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
            Debug.Log(Tag);
        }

        onComplete?.Invoke();
    }
}
