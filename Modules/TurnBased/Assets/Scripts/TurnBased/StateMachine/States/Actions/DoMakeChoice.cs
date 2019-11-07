using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DoMakeChoice : AState
{
    public FACTION_TYPE factionType;
    public FACTION_TYPE opponentFactionType;
    private Color SpriteColour;
    private Action<BATTLE_ACTION_TYPE, FakeActor> OnChoice;
    private List<BATTLE_ACTION_TYPE> PossibleActions = new List<BATTLE_ACTION_TYPE>
    {
        BATTLE_ACTION_TYPE.ATTACK,
        BATTLE_ACTION_TYPE.HEAL
    };

    public DoMakeChoice(MonoBehaviour _monoBehaviour, Action<BATTLE_ACTION_TYPE, FakeActor> OnChoice = null) :
        base(_monoBehaviour)
    {
        Id = "choice";
        FakeActor ActorInstance = MonoBehaviourRef.GetComponent<FakeActor>();
        factionType = ActorInstance.factionType;
        opponentFactionType = ActorInstance.opponentFactionType;

        this.OnChoice = OnChoice;

        SpriteColour = MonoBehaviourRef.GetComponent<SpriteRenderer>().color;
    }

    /* Simulate chosen actions and animation delays */
    public IEnumerator OnAttackChosen(float waitFor = 0f)
    {
        Debug.Log("Decided To Attack!");
        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = Color.green;

        /* Naively find a target (again not performant, but we're only testing) */
        List<FakeActor> PossibleTargets = GameObject
            .FindObjectsOfType<FakeActor>()
            .Where(x => x.factionType == opponentFactionType)
            .ToList();

        FakeActor ChosenTarget = PossibleTargets[UnityEngine.Random.Range(0, PossibleTargets.Count)];

        /* Simulate animation */
        yield return new WaitForSeconds(waitFor);
        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = SpriteColour;
        IsComplete = true;

        OnChoice?.Invoke(BATTLE_ACTION_TYPE.ATTACK, ChosenTarget);
    }

    public IEnumerator OnHealChosen(float waitFor = 0f)
    {
        Debug.Log("Decided To Heal!");
        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = Color.green;

        /* Naively find a target (again not performant, but we're only testing) */
        List<FakeActor> PossibleTargets = GameObject
            .FindObjectsOfType<FakeActor>()
            .Where(x => x.factionType == factionType)
            .ToList();

        FakeActor ChosenTarget = PossibleTargets[UnityEngine.Random.Range(0, PossibleTargets.Count)];

        /* Simulate animation */
        yield return new WaitForSeconds(waitFor);
        MonoBehaviourRef.GetComponent<SpriteRenderer>().color = SpriteColour;
        IsComplete = true;

        OnChoice?.Invoke(BATTLE_ACTION_TYPE.HEAL, ChosenTarget);        
    }

    public override void Enter()
    {
        IsComplete = false;

        Debug.Log("Making A Choice...");

        /* First pick an action to perform (again naively, this is where AI would kick in). */
        BATTLE_ACTION_TYPE chosenAction = PossibleActions[UnityEngine.Random.Range(0, PossibleActions.Count)];

        switch(chosenAction)
        {
            case BATTLE_ACTION_TYPE.ATTACK:
                MonoBehaviourRef.StartCoroutine(OnAttackChosen(UnityEngine.Random.Range(1f, 5f)));
                break;
            case BATTLE_ACTION_TYPE.HEAL:
                MonoBehaviourRef.StartCoroutine(OnHealChosen(UnityEngine.Random.Range(1f, 5f)));
                break;
        }
    }
}
