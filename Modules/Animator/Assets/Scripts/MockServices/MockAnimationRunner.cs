using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RedPanda.Animator;

namespace RedPanda.MockServices
{
    public class MockAnimationRunner : MonoBehaviour
    {
        [System.Serializable]
        public class FloatPair
        {
            public string id;
            public float value;
        }

        [System.Serializable]
        public class BoolPair
        {
            public string id;
            public bool value;
        }

        public string entityId;
        public List<FloatPair> floatConditionTriggers;
        public List<BoolPair> boolConditionTriggers;

        private AnimatorLogicManager animLogic;

        private void Start()
        {
            floatConditionTriggers = floatConditionTriggers != null ? floatConditionTriggers : new List<FloatPair>();
            boolConditionTriggers = boolConditionTriggers != null ? boolConditionTriggers : new List<BoolPair>();

            animLogic = gameObject.GetComponent<AnimatorLogicManager>();
            animLogic.Init(MockAnimationService.GateData
                .FirstOrDefault(x => x.targetId == entityId)
                .gates);
        }

        private void Update()
        {
            floatConditionTriggers.ForEach(trig => animLogic.SetFloat(trig.id, trig.value));
            boolConditionTriggers.ForEach(trig => animLogic.SetBool(trig.id, trig.value));
        }
    }
}