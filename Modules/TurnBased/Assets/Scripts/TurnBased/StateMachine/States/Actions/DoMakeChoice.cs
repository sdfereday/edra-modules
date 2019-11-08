using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DoMakeChoice : AState
{
    public FACTION_TYPE factionType;
    public FACTION_TYPE opponentFactionType;
    private FakeAnimator anim;
    private Color SpriteColour;
    private Action<BATTLE_ACTION_TYPE, FakeActor> OnChoice;
    private List<BATTLE_ACTION_TYPE> PossibleActions = new List<BATTLE_ACTION_TYPE>
    {
        BATTLE_ACTION_TYPE.ATTACK,
        BATTLE_ACTION_TYPE.HEAL
    };

    public DoMakeChoice(FakeAnimator anim, FakeActor ActorInstance, SpriteRenderer renderer, Action<BATTLE_ACTION_TYPE, FakeActor> OnChoice = null)
    {
        Id = "choice";
        factionType = ActorInstance.factionType;
        opponentFactionType = ActorInstance.opponentFactionType;

        SpriteColour = renderer.color;

        this.anim = anim;
        this.OnChoice = OnChoice;
    }

    /* Simulate chosen actions and animation delays */
    public void OnAttackChosen()
    {
        Debug.Log("Decided To Attack!");
        
        /* Naively find a target (again not performant, but we're only testing) */
        List<FakeActor> PossibleTargets = GameObject
            .FindObjectsOfType<FakeActor>()
            .Where(x => x.factionType == opponentFactionType)
            .ToList();

        FakeActor ChosenTarget = PossibleTargets[UnityEngine.Random.Range(0, PossibleTargets.Count)];

        IsComplete = true;

        OnChoice?.Invoke(BATTLE_ACTION_TYPE.ATTACK, ChosenTarget);
    }

    public void OnHealChosen(float waitFor = 0f)
    {
        Debug.Log("Decided To Heal!");
        
        /* Naively find a target (again not performant, but we're only testing) */
        List<FakeActor> PossibleTargets = GameObject
            .FindObjectsOfType<FakeActor>()
            .Where(x => x.factionType == factionType)
            .ToList();

        FakeActor ChosenTarget = PossibleTargets[UnityEngine.Random.Range(0, PossibleTargets.Count)];

        IsComplete = true;

        OnChoice?.Invoke(BATTLE_ACTION_TYPE.HEAL, ChosenTarget);        
    }

    public override void Enter()
    {
        IsComplete = false;

        Debug.Log("Making A Choice...");

        /* Fake pick just for example purposes */
        anim.PlayThinking(() => {
            /* First pick an action to perform (again naively, this is where AI would kick in). */
            BATTLE_ACTION_TYPE chosenAction = PossibleActions[UnityEngine.Random.Range(0, PossibleActions.Count)];

            switch (chosenAction)
            {
                case BATTLE_ACTION_TYPE.ATTACK:
                    OnAttackChosen();
                    break;
                case BATTLE_ACTION_TYPE.HEAL:
                    OnHealChosen();
                    break;
            }
        });        
    }
}
