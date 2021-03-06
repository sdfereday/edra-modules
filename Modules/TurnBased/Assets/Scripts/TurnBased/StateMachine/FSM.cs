﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/* This FSM type is an auto-exit type. It might be worthwhile making a different
 * type if more control is required. This sort of FSM is good for automation or
 * animating state types. */
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

                /* We don't enter in the same loop as an exit, or the states may conflict. */
                currentState = Top(StateStack);

                WaitingToFinish = currentState == null;
            }
        }
    }

    public void Push(AState stateInstance)
    {
        if (stateInstance.Stackable || !StateStack.Any(x => x.Id == stateInstance.Id))
        {
            StateStack.Add(stateInstance);
            WaitingToFinish = true;

            AState currentState = Top(StateStack);
            if (currentState != null)
            {
                currentState.Enter();
            }
        } else
        {
            Debug.Log("Duplicate state tried to be pushed:");
            Debug.Log(stateInstance.Id);
        }
    }
}
