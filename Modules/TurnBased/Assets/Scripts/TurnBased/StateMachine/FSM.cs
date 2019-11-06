using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FSM
{
    public List<AState> StateStack;
    public bool WaitingToFinish = false;

    public AState Top(List<AState> Items) => Items != null && Items.Count > 0 ? Items[Items.Count - 1] : null;

    public FSM()
    {
        StateStack = new List<AState>();
    }

    public void Update()
    {
        AState currentState = Top(StateStack);
        if (currentState != null)
        {
            currentState.Update();

            if (currentState.IsComplete)
            {
                currentState.Exit();
                StateStack = StateStack.Where(x => !x.IsComplete).ToList();

                currentState = Top(StateStack);

                if (currentState != null)
                {
                    currentState.Enter();
                }
                else
                {
                    WaitingToFinish = false;
                }
            }
        }
    }

    public void Push(AState stateInstance)
    {
        if (!StateStack.Any(x => x.id == stateInstance.id))
        {
            StateStack.Add(stateInstance);
            WaitingToFinish = true;

            AState currentState = Top(StateStack);
            if (currentState != null)
            {
                currentState.Enter();
            }
        }
    }
}
