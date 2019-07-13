using UnityEngine;
using System.Linq;
using RedPanda.Animator;
using RedPanda.MockServices;

public class MockAnimationRunner : MonoBehaviour
{
    public string entityId;

    private void Start()
    {
        gameObject.GetComponent<AnimatorLogicManager>()
            .Init(MockAnimationService.GateData
                .FirstOrDefault(x => x.targetId == entityId)
                .gates);
    }
}
